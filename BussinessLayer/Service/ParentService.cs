using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.Execution;
using BussinessLayer.IService;
using BussinessLayer.Utils.Configurations;
using DataAccessLayer.DTO;
using DataAccessLayer.DTO.Parents;
using DataAccessLayer.Entity;
using DataAccessLayer.IRepository;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace BussinessLayer.Service
{
    public class ParentService(IParentRepository parentRepository, IUserRepository userRepository,
        IMapper mapper,
        IOptionsMonitor<AppSetting> option, IHttpContextAccessor httpContextAccessor) : IParentService
    {
        private readonly AppSetting _appSettings = option.CurrentValue;

        #region HashingPassword
        public static void CreatePasswordHash(string password, out byte[] hash, out byte[] salt)
        {
            using var hmac = new HMACSHA512();
            salt = hmac.Key;
            hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        }

        public static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            using var hmac = new HMACSHA512(storedSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            return computedHash.SequenceEqual(storedHash);
        }
        #endregion
        public async Task AddParentAsync(ParentRegister parent)
        {
            try
            {
                List<Parent> Parent = await parentRepository.GetAllAsync();
                if (Parent.Any(p => p.Email == parent.Email))
                {
                    throw new InvalidOperationException("A parent with this email already exists.");
                }

                CreatePasswordHash(parent.Password, out byte[] hash, out byte[] salt);

                var newParent = mapper.Map<Parent>(parent);
                newParent.CreatedDate = DateTime.Now;
                UserDTo userdto = new()
                {
                    isStaff = false,
                    Email = parent.Phone.ToString(),
                    Hash = hash,
                    Salt = salt
                };
                var user = mapper.Map<User>(userdto);
                try
                {
                    await userRepository.AddAsync(user);
                    userRepository.Save();
                    newParent.Userid = user.UserId;
                    await parentRepository.AddAsync(newParent);
                    parentRepository.Save();
                }
                catch
                {
                    userRepository.Delete(user.UserId);
                    userRepository.Save();
                    parentRepository.Delete(newParent.Userid);
                    parentRepository.Save();

                }
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                throw new Exception("An error occurred while adding the parent.", ex);
            }
        }

        public void DeleteParent(int id)
        {
            if(parentRepository.GetByIdAsync(id) != null)
            {
                parentRepository.Delete(id);
                parentRepository.Save();   
            }

        }
        public void UpdateParent(ParentUpdate parentdto)
        {
            Parent parent = (Parent) parentRepository.GetByIdAsync(parentdto.Parentid).Result;
            if (parent != null)
            {
                // Only assign if value is not null/empty/0
                if (!string.IsNullOrWhiteSpace(parentdto.Address))
                    parent.Address = parentdto.Address;
                if (!string.IsNullOrWhiteSpace(parentdto.Email))
                    parent.Email = parentdto.Email;
                if (!string.IsNullOrWhiteSpace(parentdto.Fullname))
                    parent.Fullname = parentdto.Fullname;
                if (parentdto.Phone != 0)
                    parent.Phone = parentdto.Phone;
                parentRepository.Update(parent);
                parentRepository.Save();
            }
        }



        public async Task<string> GenerateToken(LoginDTO login)
        {

            try
            {
                var parentlist = await parentRepository.GetAllAsync();
                var userlist = await userRepository.GetAllAsync();
                User user = userlist.FirstOrDefault(x => x.Email == login.Email);

                if (user != null &&
                    VerifyPasswordHash(login.Password, user.Hash, user.Salt))
                {
                    Parent parent = parentlist.FirstOrDefault(x => x.Userid == user.UserId);
                    var jwtTokenHandler = new JwtSecurityTokenHandler();

                    var secretKeyBytes = Encoding.UTF8.GetBytes(_appSettings.SecretKey);
                    string status = parent.IsDeleted ? "Tạm ngừng" : "Hoạt động";
                    var tokenDescription = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(new[] {
               new Claim("Id", parent.Parentid.ToString()),
                new Claim("Fullname", parent.Fullname),
                new Claim("Email", parent.Email ?? string.Empty),
                new Claim("Phone", parent.Phone.ToString()),
                new Claim("Address", parent.Address),
                new Claim("Status", status),
                new Claim("Role", "Parent"),
                new Claim("DateCreated", parent.CreatedDate.ToString())
           }),

                        Expires = DateTime.UtcNow.AddMinutes(180),
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKeyBytes),
                        SecurityAlgorithms.HmacSha512Signature)
                    };

                    var principal = new ClaimsPrincipal(tokenDescription.Subject);
                    httpContextAccessor.HttpContext.User = principal;
                    Console.WriteLine(httpContextAccessor.HttpContext.User.Identity.Name);
                    var tokenParent = jwtTokenHandler.CreateToken(tokenDescription);
                    return jwtTokenHandler.WriteToken(tokenParent);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return null;
        }

        public async Task<List<ParentDTO>> GetAllParentsAsync()
        {
            List<ParentDTO> list = mapper.Map<List<ParentDTO>>(await parentRepository.GetAllAsync()); 
            return list;
        }

        public async Task<ParentDTO> GetParentByIdAsync(int id)
        {
            ParentDTO parent = mapper.Map<ParentDTO>(await parentRepository.GetByIdAsync(id));
            return parent;
        }


        public async Task<string> ValidateGoogleToken(string token)
        {
            var payload = await GoogleJsonWebSignature.ValidateAsync(token, new GoogleJsonWebSignature.ValidationSettings
            {
                Audience = new[] { _appSettings.GoogleClientId }
            });
            string email = payload.Email;
            var parent = (await parentRepository.GetAllAsync())
                .FirstOrDefault(p => p.Email == email);
            if (parent == null) return null;
            LoginDTO parentLogin = mapper.Map<LoginDTO>(parent);
            return await GenerateToken(parentLogin);
        }
    }
}

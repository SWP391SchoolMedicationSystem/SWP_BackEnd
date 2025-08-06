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
using DataAccessLayer.DTO.Students;
using DataAccessLayer.Entity;
using DataAccessLayer.IRepository;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace BussinessLayer.Service
{
    public class ParentService(
        IStudentRepo studentRepo,
        IStudentService studentService,
        IParentRepository parentRepository, IUserRepository userRepository,
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
                //Check existed Email
                List<Parent> Parent = await parentRepository.GetAllAsync();
                if (Parent.Any(p => p.Email == parent.Email))
                {
                    throw new InvalidOperationException("Email này đã tồn tại.");
                }

                //Create new User
                CreatePasswordHash(parent.Password, out byte[] hash, out byte[] salt);
                User user = mapper.Map<User>(new UserDTO
                {
                    isStaff = false,
                    Email = parent.Email,
                    Hash = hash,
                    Salt = salt
                });

                //Create new Parent
                var newParent = mapper.Map<Parent>(parent);
                newParent.CreatedDate = DateTime.Now;

                try
                {
                    await userRepository.AddAsync(user);
                    await userRepository.SaveChangesAsync();
                    newParent.Userid = user.UserId;
                    await parentRepository.AddAsync(newParent);
                    await parentRepository.SaveChangesAsync();
                    foreach(var student in parent.Students)
                    {
                        student.Parentid = newParent.Parentid;
                        await studentService.AddStudentAsync(student);
                    }
                    

                }

                catch (Exception ex)
                {
                    
                    studentRepo.Delete(newParent.Userid);
                    studentRepo.Save();
                    parentRepository.Delete(newParent.Parentid);
                    parentRepository.Save();
                    userRepository.Delete(user.UserId);
                    userRepository.Save();
                    throw ex;
                }

            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                throw new Exception("An error occurred while adding the parent.", ex);
            }

        }

        public async Task DeleteParent(int id)
        {
            var parent = await parentRepository.GetByIdAsync(id);
            if ( parent != null)
            {
                var user = (await userRepository.GetAllAsync())
                    .FirstOrDefault(u => u.UserId == parent.Userid);
                user.IsDeleted = true;
                userRepository.Update(user);
                parent.IsDeleted = true;
                parentRepository.Save();   
            }

        }
        public async Task UpdateParent(ParentUpdate parentdto)
        {
            Parent parent = await parentRepository.GetByIdAsync(parentdto.Parentid);
            if (parent != null)
            {
                // Only assign if value is not null/empty/0
                    parent.Address = parentdto.Address;
                    parent.Email = parentdto.Email;
                    parent.Fullname = parentdto.Fullname;
                    parent.Phone = parentdto.Phone;
                parentRepository.Update(parent);
                parentRepository.Save();
            }
            else
            {
                throw new KeyNotFoundException($"Parent with ID {parentdto.Parentid} not found.");
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
            List<Parent> list = await parentRepository.GetAllAsync();
            var parentlist = mapper.Map<List<ParentDTO>>(list);
            foreach(ParentDTO parent in parentlist)
            {
                var studentlist = studentRepo.GetAll().Where(s => s.Parentid == parent.Parentid);
                var studentdto = mapper.Map<List<StudentParentDTO>>(studentlist);
                parent.Students = studentdto;
            }
            
            return parentlist;
        }

        public async Task<ParentDTO> GetParentByIdAsync(int id)
        {
            ParentDTO parent = mapper.Map<ParentDTO>(await parentRepository.GetByIdAsync(id));
            return parent;
        }

        public async Task<ParentDTO> GetParentByEmailAsync(string email)
        {
            var parents = await parentRepository.GetAllAsync();
            var parent = parents.FirstOrDefault(p => p.Email == email);
            if (parent == null) return null;
            ParentDTO parentdto = mapper.Map<ParentDTO>(parent);

            var studentlist = studentRepo.GetAll().Where(s => s.Parentid == parent.Parentid);
            var studentdto = mapper.Map<List<StudentParentDTO>>(studentlist);
            parentdto.Students = studentdto;
            return parentdto;
        }

        public async Task<ParentVaccineEvent> GetParentByEmailForEvent(string email)
        {
            var parent = await parentRepository.GetParentForEvent(email);
            if (parent == null) return null;
            ParentVaccineEvent parentEvent = mapper.Map<ParentVaccineEvent>(parent);

            return parentEvent;
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

        public async Task<string> GenerateGoogleToken(LoginGoogleDTO login)
        {
            try
            {
                var parentlist = await parentRepository.GetAllAsync();
                var userlist = await userRepository.GetAllAsync();
                User user = userlist.FirstOrDefault(x => x.Email == login.Email);

                if (user != null)
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
    }
}

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
using DataAccessLayer.Entity;
using DataAccessLayer.IRepository;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace BussinessLayer.Service
{
    public class ParentService : IParentService
    {
        private readonly IParentRepository _parentRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly AppSetting _appSettings;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ParentService(IParentRepository parentRepository, IUserRepository userRepository,
            IMapper mapper, 
            IOptionsMonitor<AppSetting> option, IHttpContextAccessor httpContextAccessor)
        {
            _parentRepository = parentRepository;
            _userRepository = userRepository;
            _mapper = mapper;
            _appSettings = option.CurrentValue;
            _httpContextAccessor = httpContextAccessor;
        }

        #region HashingPassword
        public void CreatePasswordHash(string password, out byte[] hash, out byte[] salt)
        {
            using var hmac = new HMACSHA512();
            salt = hmac.Key;
            hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        }

        public bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
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
                List<Parent> Parent = await _parentRepository.GetAllAsync();
                if (Parent.Any(p => p.Email == parent.Email))
                {
                    throw new InvalidOperationException("A parent with this email already exists.");
                }

                CreatePasswordHash(parent.Password, out byte[] hash, out byte[] salt);

                var newParent = _mapper.Map<Parent>(parent);
                newParent.CreatedBy = 2; // Tạm thời đặt giá trị CreatedBy, sẽ thay đổi sau.
                newParent.CreatedDate = DateTime.Now;

                // Initialize the 'user' variable before using it
                UserDTo userdto = new UserDTo
                {
                    isStaff = false,
                    Email = parent.Phone.ToString(),
                    Hash = hash,
                    Salt = salt
                };
                var user = _mapper.Map<User>(userdto);
                try
                {
                    await _userRepository.AddAsync(user);
                    _userRepository.Save();
                    newParent.Userid = user.UserId;
                    await _parentRepository.AddAsync(newParent);
                    _parentRepository.Save();
                }
                catch
                {
                    _userRepository.Delete(user.UserId);
                    _userRepository.Save();
                    _parentRepository.Delete(newParent.Userid);
                    _parentRepository.Save();

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
            if(_parentRepository.GetByIdAsync(id) != null)
            {
                _parentRepository.Delete(id);
                _parentRepository.Save();   
            }

        }
        public void UpdateParent(ParentUpdate parentdto)
        {
            Parent parent = (Parent) _parentRepository.GetByIdAsync(parentdto.Parentid).Result;
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
                _parentRepository.Update(parent);
                _parentRepository.Save();
            }
        }



        public async Task<string> GenerateToken(LoginDTO login)
        {

            try
            {
                var parentlist = await _parentRepository.GetAllAsync();
                var userlist = await _userRepository.GetAllAsync();
                User user = userlist.FirstOrDefault(x => x.Email == login.EmailorPhone);

                if (user != null &&
                    VerifyPasswordHash(login.Password, user.Hash, user.Salt))
                {
                    Parent parent = parentlist.FirstOrDefault(x => x.Userid == user.UserId);
                    var jwtTokenHandler = new JwtSecurityTokenHandler();

                    var secretKeyBytes = Encoding.UTF8.GetBytes(_appSettings.SecretKey);

                    var tokenDescription = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(new[] {
               new Claim("Id", parent.Parentid.ToString()),
                new Claim("Fullname", parent.Fullname),
                new Claim("Email", parent.Email ?? string.Empty),
                new Claim("Phone", parent.Phone.ToString()),
                new Claim("Address", parent.Address),
                new Claim("DateCreated", parent.CreatedDate.ToString())
           }),

                        Expires = DateTime.UtcNow.AddMinutes(180),
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKeyBytes),
                        SecurityAlgorithms.HmacSha512Signature)
                    };

                    var principal = new ClaimsPrincipal(tokenDescription.Subject);
                    _httpContextAccessor.HttpContext.User = principal;
                    Console.WriteLine(_httpContextAccessor.HttpContext.User.Identity.Name);
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
            List<ParentDTO> list = _mapper.Map<List<ParentDTO>>(await _parentRepository.GetAllAsync()); 
            return list;
        }

        public async Task<ParentDTO> GetParentByIdAsync(int id)
        {
            ParentDTO parent = _mapper.Map<ParentDTO>(await _parentRepository.GetByIdAsync(id));
            return parent;
        }

        public Task<List<ParentDTO>> GetParentsByClassIdAsync(int classId)
        {
            throw new NotImplementedException();
        }

        public Task<List<ParentDTO>> GetParentsByStaffIdAsync(int staffId)
        {
            throw new NotImplementedException();
        }

        public Task<List<ParentDTO>> GetParentsByStudentIdAsync(int studentId)
        {
            throw new NotImplementedException();
        }

        public async Task<string> ValidateGoogleToken(string token)
        {
            var payload = await GoogleJsonWebSignature.ValidateAsync(token, new GoogleJsonWebSignature.ValidationSettings
            {
                Audience = new[] { _appSettings.GoogleClientId }
            });
            string email = payload.Email;
            var parent = (await _parentRepository.GetAllAsync())
                .FirstOrDefault(p => p.Email == email);
            if (parent == null) return null;
            LoginDTO parentLogin = _mapper.Map<LoginDTO>(parent);
            return await GenerateToken(parentLogin);
        }


    }
}

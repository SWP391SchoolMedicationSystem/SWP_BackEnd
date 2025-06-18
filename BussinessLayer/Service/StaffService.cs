using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using AutoMapper;
using BussinessLayer.IService;
using BussinessLayer.Utils.Configurations;
using DataAccessLayer.DTO;
using DataAccessLayer.DTO.Staffs;
using DataAccessLayer.Entity;
using DataAccessLayer.IRepository;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace BussinessLayer.Service
{
    public class StaffService(IMapper mapper, 
        IStaffRepository staffRepository, IUserRepository userRepository, 
       IRoleRepository roleRepository 
       ,IHttpContextAccessor httpContextAccessor, IOptionsMonitor<AppSetting> option) :
        IStaffService
    {
        private readonly AppSetting _appSettings = option.CurrentValue;

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
        public async Task AddStaffAsync(StaffRegister register)
        {
            using var transaction = staffRepository.BeginTransaction();
            try
            {
                List<Staff> existingStaff = await staffRepository.GetAllAsync();
                if (existingStaff.Any(s => s.Email == register.Email))
                {
                    throw new InvalidOperationException("A staff member with this email already exists.");
                }
                CreatePasswordHash(register.Password, out byte[] hash, out byte[] salt);
                Staff staff = mapper.Map<Staff>(register);
                staff.CreatedAt = DateTime.Now;
                User user = new ()
                {
                    Email = register.Email,
                    IsStaff = true,
                    Hash = hash,
                    Salt = salt,
                };
                user.Staff.Add(staff);
                var listrole = roleRepository.GetAllAsync();
                Role role = listrole.Result.FirstOrDefault(r => r.Roleid == register.RoleID);
                role.Staff.Add(staff);
                await userRepository.AddAsync(user);
                staff.Userid = user.UserId;
                await staffRepository.AddAsync(staff);
                roleRepository.Save();
                userRepository.Save();
                staffRepository.Save();


                transaction.Commit();


            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw new Exception("An error occurred while adding staff: " + ex.Message);
            }
        }

        public void DeleteStaff(int id)
        {
            if (GetStaffByIdAsync(id) == null)
            {
                throw new KeyNotFoundException($"Staff with ID {id} not found.");
            }
            else
            { 
            staffRepository.Delete(id);
                staffRepository.Save();

            }

        }

        public async Task<string> GenerateToken(LoginDTO login)
        {
            try
            {
                var stafflist = await staffRepository.GetAllAsync();
                var userlist = await userRepository.GetAllAsync();
                User user = userlist.FirstOrDefault(x => x.Email == login.Email);

                if (user != null &&
                    VerifyPasswordHash(login.Password, user.Hash, user.Salt))
                {
                    Staff staff = stafflist.FirstOrDefault(x => x.Userid == user.UserId);
                    var jwtTokenHandler = new JwtSecurityTokenHandler();
                    string role = null;
                    if (staff.Roleid == 1)
                    {
                        role = "Admin";
                    }
                    else if (staff.Roleid == 2)
                    {
                        role = "Manager";
                    }
                    else if (staff.Roleid == 3)
                    {
                        role = "Nurse";
                    }
                    else
                    {
                        role = "Other";
                    }
                    var secretKeyBytes = Encoding.UTF8.GetBytes(_appSettings.SecretKey);
                    string status = staff.IsDeleted ? "Tạm ngừng" : "Hoạt động";
                    var tokenDescription = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(new[] {
               new Claim("Id", staff.Staffid.ToString()),
                new Claim("Fullname", staff.Fullname),
                new Claim("Email", staff.Email ?? string.Empty),
                new Claim("Phone", staff.Phone.ToString()),
                new Claim("Status", status),
                new Claim("Role", role),
                new Claim("DateCreated", staff.CreatedAt.ToString())
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

        public async Task<List<StaffDTO>> GetAllStaffAsync()
        {
            List<Staff> staffList = await staffRepository.GetAllAsync();
            List<StaffDTO> staffDTOs = staffList.Select(staff => mapper.Map<StaffDTO>(staff)).ToList();
            return staffDTOs;
        }

        public async Task<StaffDTO> GetStaffByIdAsync(int id)
        {
            StaffDTO staffDTO = mapper.Map<StaffDTO>(await staffRepository.GetByIdAsync(id));
            return staffDTO ?? throw new KeyNotFoundException($"Staff with ID {id} not found.");
        }

        public void UpdateStaff(StaffUpdate staff)
        {
            Staff staffupdated = mapper.Map<Staff>(staff);
            staffupdated.UpdatedAt = DateTime.Now;
            staffRepository.Update(staffupdated);
            staffRepository.Save();
        }

        public async Task<String> ValidateGoogleToken(string token)
        {
            try
            {var payload = await GoogleJsonWebSignature.ValidateAsync(token, new GoogleJsonWebSignature.ValidationSettings
            {
                Audience = new[] { _appSettings.GoogleClientId }
            });
            string email = payload.Email;
            var staff = (await staffRepository.GetAllAsync())
                .FirstOrDefault(p => p.Email == email);
            if (staff == null) return null;
            LoginDTO stafflogin = mapper.Map<LoginDTO>(staff);
                return await GenerateToken(stafflogin); }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
    }

}

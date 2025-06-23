using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BussinessLayer.IService;
using BussinessLayer.Utils.Configurations;
using DataAccessLayer.DTO;
using DataAccessLayer.Entity;
using DataAccessLayer.IRepository;
using Google.Apis.Auth;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.VisualBasic.FileIO;
using static System.Net.WebRequestMethods;

namespace BussinessLayer.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IEmailRepo _emailRepo;
        private readonly IOtpRepo _otpRepo;
        private readonly IMapper _mapper;
        private readonly IStaffService _staffService;
        private readonly IParentService _parentService;
        private readonly IEmailService _emailService;
        private readonly AppSetting _appSettings;
        private readonly SchoolMedicalSystemContext _context;
        public UserService(IUserRepository userRepository, IMapper mapper,
            IParentService parentService, IStaffService staffService, IOptionsMonitor<AppSetting> option, IEmailRepo emailRepo, IOtpRepo otpRepo, IEmailService emailService, SchoolMedicalSystemContext context)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _parentService = parentService;
            _staffService = staffService;
            _appSettings = option.CurrentValue;
            _emailRepo = emailRepo;
            _otpRepo = otpRepo;
            _emailService = emailService;
            _context = context;
        }
        public async Task AddAsync(User entity)
        {
            await _userRepository.AddAsync(entity);
        }

        public void Delete(int ID)
        {
            if (GetByIdAsync(ID) != null)
            {
                _userRepository.Delete(ID);
            }
            else
            {
                throw new InvalidOperationException("User not found.");
            }
        }


        public Task<List<User>> GetAllAsync()
        {
            return _userRepository.GetAllAsync();
        }

        public Task<User> GetByIdAsync(int id)
        {
            return _userRepository.GetByIdAsync(id);
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            var users = await _userRepository.GetAllAsync();
            var filteredUsers = users.FirstOrDefault(u => u.Email == email);

            return filteredUsers;
        }

        public async Task<string> Login(LoginDTO dto)
        {
            try
            {
                List<User> users = await _userRepository.GetAllAsync();
                User user = users.FirstOrDefault(u => u.Email == dto.Email);
                if (user.IsStaff)
                {
                    return await _staffService.GenerateToken(dto);
                }
                else return await _parentService.GenerateToken(dto);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw new InvalidOperationException("Login failed. Please check your credentials.");
            }
        }

        public async Task<bool> ResetPassword(string email, string newPassword)
        {
            var user = await GetByEmailAsync(email);
            if (user == null)
                return false;
            // Hash the new password
            using var hmac = new HMACSHA512();
            user.Salt = hmac.Key;
            user.Hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(newPassword));
            
            _userRepository.Update(user);
            await _userRepository.SaveChangesAsync();
            return true;
        }

        public void Save()
        {
            _userRepository.Save();
        }

        public void Update(User entity)
        {
            _userRepository.Update(entity);
        }

        public async Task<string> ValidateGoogleToken(string token)
        {

                try
                {
                    var payload = await GoogleJsonWebSignature.ValidateAsync(token, new GoogleJsonWebSignature.ValidationSettings
                    {
                        Audience = new[] { _appSettings.GoogleClientId }
                    });
                    string email = payload.Email;
                    var user = _userRepository.GetAll().FirstOrDefault(u => u.Email == email);
                if (user == null) return null;
                    if (user.IsStaff == true)
                    {
                    LoginGoogleDTO stafflogin = _mapper.Map<LoginGoogleDTO>(user);
                        return await _staffService.GenerateGoogleToken(stafflogin);
                    }
                    else
                    {
                    LoginGoogleDTO parentLogin = _mapper.Map<LoginGoogleDTO>(user);
                        return await _parentService.GenerateGoogleToken(parentLogin);
                    }
                }
                catch (Exception e) {
                throw new InvalidOperationException(e.Message);
            }
        }

        public async Task<bool> SendOTPEmailAsync(string email)
        {
            var userExist = await GetByEmailAsync(email);
            if (userExist == null)
                return false;

            var existingOtps = await _otpRepo.GetAllAsync();
            var filterExistingOtps = existingOtps.Where(o => o.Email == email && !o.IsUsed && o.ExpiresAt > DateTime.UtcNow);

            foreach (var oldOtp in filterExistingOtps)
            {
                oldOtp.IsUsed = true; // Invalidate old OTP
            }

            string otp;
            bool isOtpUnique;

            //Check unique of OTP, if not then generate another one until it unique
            do
            {
                otp = GenerateOtp(6);
                isOtpUnique = !await _context.Otps.AnyAsync(o => o.OtpCode == otp && !o.IsUsed && o.ExpiresAt > DateTime.UtcNow);
            }
            while (!isOtpUnique);

            var otpEntry = new Otp
            {
                Email = email,
                OtpCode = otp,
                ExpiresAt = DateTime.UtcNow.AddMinutes(5),
                IsUsed = false
            };

            var emailTemplateDTO = new EmailDTO
            {
                To = email,
                Subject = "Yêu cầu đặt lại mật khẩu",
                Body = $"Mã OTP đễ đặt lại mật khẩu của bạn là :\n <h1>{otp}</h1>",
            };

            _otpRepo.Add(otpEntry);
            await _otpRepo.SaveChangesAsync();

            await _emailService.SendEmailAsync(emailTemplateDTO);
            return true;
        }

        public static string GenerateOtp(int length = 6)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var result = new StringBuilder(length);

            // Create a byte array to hold the random data
            var data = new byte[sizeof(uint)];

            using (var rng = RandomNumberGenerator.Create())
            {
                for (int i = 0; i < length; i++)
                {
                    // The range of valid values for the random number.
                    // We want to avoid modulo bias, so we find the largest multiple of
                    // chars.Length that is less than or equal to byte.MaxValue (255) or uint.MaxValue.
                    // In this optimized version, we use a uint (4 bytes).
                    uint range = uint.MaxValue / (uint)chars.Length * (uint)chars.Length;

                    // This do-while loop is the core of the rejection sampling.
                    // It will keep generating a new random number until it gets one
                    // that is within the allowed 'range'.
                    uint randomValue;
                    do
                    {
                        rng.GetBytes(data);
                        randomValue = BitConverter.ToUInt32(data, 0);
                    }
                    while (randomValue >= range);

                    result.Append(chars[(int)(randomValue % chars.Length)]);
                }
            }

            return result.ToString();
        }

        public async Task<bool> ValidateOtpAsync(OtpDTO request)
        {
            const int MAX_ATTEMPTS = 3;
            var otpList = await _otpRepo.GetAllAsync();
            var otpEntry = otpList.FirstOrDefault(o =>
                        o.Email == request.Email &&
                        //o.OtpCode == request.OtpCode &&
                        !o.IsUsed &&
                        o.ExpiresAt > DateTime.UtcNow);

            if (otpEntry == null)
                return false;

            if (otpEntry.OtpCode != request.OtpCode)
            {
                otpEntry.FailedAttempts++;

                if (otpEntry.FailedAttempts >= MAX_ATTEMPTS)
                {
                    otpEntry.IsUsed = true; // Lock the OTP
                }

                await _otpRepo.SaveChangesAsync();
                return false;
            }

            if (otpEntry.FailedAttempts >= MAX_ATTEMPTS)
            {
                return false;
            }

            otpEntry.IsUsed = true;
            await _otpRepo.SaveChangesAsync();

            var emailTemplateDTO = new EmailDTO
            {
                To = request.Email,
                Subject = "Thông báo bảo mật",
                Body = $"Mật khẩu tài khoản đã được thay đổi",
            };
            await _emailService.SendEmailAsync(emailTemplateDTO);

            return true;
        }

    }
}


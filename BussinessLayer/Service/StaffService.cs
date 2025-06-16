using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BussinessLayer.IService;
using DataAccessLayer.DTO;
using DataAccessLayer.Entity;
using DataAccessLayer.IRepository;

namespace BussinessLayer.Service
{
    public class StaffService(IMapper mapper, IStaffRepository staffRepository, IUserRepository) :
        IStaffService
    {
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
        public async Task AddStaffAsync(StaffRegister register)
        {
            List<Staff> existingStaff = await staffRepository.GetAllAsync();
            if (existingStaff.Any(s => s.Email == register.Email))
            {
                throw new InvalidOperationException("A staff member with this email already exists.");
            }
            CreatePasswordHash(register.Password, out byte[] hash, out byte[] salt);
            Staff staff = mapper.Map<Staff>(register);
            staff.CreatedAt = DateTime.Now;

            await staffRepository.AddAsync();
        }

        public void DeleteStaff(int id)
        {
            throw new NotImplementedException();
        }

        public Task<string> GenerateToken(LoginDTO login)
        {
            throw new NotImplementedException();
        }

        public Task<List<StaffDTO>> GetAllStaffAsync()
        {
            throw new NotImplementedException();
        }

        public Task<StaffDTO> GetStaffByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public void UpdateStaff(StaffDTO staff)
        {
            throw new NotImplementedException();
        }

        public Task<StaffDTO> ValidateGoogleToken(string token)
        {
            throw new NotImplementedException();
        }
    }
}

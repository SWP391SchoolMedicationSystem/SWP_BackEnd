using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DataAccessLayer.DTO;
using DataAccessLayer.Entity;
using DataAccessLayer.IRepository;

namespace BussinessLayer.IService
{
    public interface IUserService
    {
        Task<List<User>> GetAllAsync();
        Task<User> GetByIdAsync(int id);
        Task AddAsync(User entity);
        void Update(User entity, int id);
        void Delete(int ID);
        void Save();
        Task<string> Login(LoginDTO dto);
        Task<bool> ResetPassword(string email, string newPassword);
        Task<string> ValidateGoogleToken(string token);
        Task<bool> SendOTPEmailAsync(string email);
        Task<bool> ValidateOtpAsync(OtpDTO request);
    }
}

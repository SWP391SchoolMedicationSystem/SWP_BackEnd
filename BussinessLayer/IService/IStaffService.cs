using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.DTO;

namespace BussinessLayer.IService
{
    public interface IStaffService 
    {
        Task<List<StaffDTO>> GetAllStaffAsync();
        Task<StaffDTO> GetStaffByIdAsync(int id);
        Task AddStaffAsync(StaffRegister staff);
        void UpdateStaff(StaffDTO staff);
        void DeleteStaff(int id);
        Task<String> GenerateToken(LoginDTO login);
        Task<StaffDTO> ValidateGoogleToken(string token);
    }
}

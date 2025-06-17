using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.DTO;
using DataAccessLayer.DTO.Staffs;
using DataAccessLayer.Entity;

namespace BussinessLayer.IService
{
    public interface IStaffService 
    {
        Task<List<StaffDTO>> GetAllStaffAsync();
        Task<StaffDTO> GetStaffByIdAsync(int id);
        Task AddStaffAsync(StaffRegister staff);
        void UpdateStaff(StaffUpdate staff);
        void DeleteStaff(int id);
        Task<String> GenerateToken(LoginDTO login);
        Task<string> ValidateGoogleToken(string token);
    }
}

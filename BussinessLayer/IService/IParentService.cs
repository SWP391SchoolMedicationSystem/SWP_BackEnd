using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.DTO;
using DataAccessLayer.DTO.Parents;
using DataAccessLayer.Entity;

namespace BussinessLayer.IService
{
    public interface IParentService
    {
        Task<List<ParentDTO>> GetAllParentsAsync();
        Task<ParentDTO> GetParentByIdAsync(int id);
        Task AddParentAsync(ParentRegister parent);
        void UpdateParent(ParentUpdate parent);
        void DeleteParent(int id);
        Task<String> GenerateToken(LoginDTO login);
        Task<string> ValidateGoogleToken(string token);
    }
}

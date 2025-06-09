using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.DTO;
using DataAccessLayer.Entity;

namespace BussinessLayer.IService
{
    public interface IParentService
    {
        Task<List<ParentDTO>> GetAllParentsAsync();
        Task<ParentDTO> GetParentByIdAsync(int id);
        Task AddParentAsync(ParentRegister parent);
        void UpdateParent(ParentDTO parent);
        void DeleteParent(int id);
        Task<List<ParentDTO>> GetParentsByStudentIdAsync(int studentId);
        Task<List<ParentDTO>> GetParentsByClassIdAsync(int classId);
        Task<List<ParentDTO>> GetParentsByStaffIdAsync(int staffId);
        Task<String> GenerateToken(ParentLoginDTO login);
    }
}

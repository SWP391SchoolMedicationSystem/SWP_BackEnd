using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.DTO.StudentSpecialNeeds;

namespace BussinessLayer.IService
{
    public interface IStudentSpecialNeedService
    {
        Task<List<StudentSpecialNeedDTO>> GetAllStudentSpecialNeedsAsync();
        Task<StudentSpecialNeedDTO> GetStudentSpecialNeedByIdAsync(int id);
        Task AddStudentSpecialNeed(CreateSpecialStudentNeedDTO studentSpecialNeed);
        Task UpdateStudentSpecialNeed(UpdateStudentSpecialNeedDTO studentSpecialNeed);
        Task<List<StudentSpecialNeedDTO>> GetStudentSpecialNeedsByStudentIdAsync(int studentId);
        Task<List<StudentSpecialNeedDTO>> GetStudentSpecialNeedsByCategoryIdAsync(int categoryId);
        Task DeleteStudentSpecialNeed(int id);
    }
}

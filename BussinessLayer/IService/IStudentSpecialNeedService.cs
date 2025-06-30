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
        void AddStudentSpecialNeed(CreateSpecialStudentNeedDTO studentSpecialNeed);
        void UpdateStudentSpecialNeed(UpdateStudentSpecialNeedDTO studentSpecialNeed);
        Task<List<StudentSpecialNeedDTO>> GetStudentSpecialNeedsByStudentIdAsync(int studentId);
        Task<List<StudentSpecialNeedDTO>> GetStudentSpecialNeedsByCategoryIdAsync(int categoryId);
        //        void DeleteStudentSpecialNeed(int id);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.DTO;
using DataAccessLayer.Entity;

namespace BussinessLayer.IService
{
    public interface IStudentService
    {
        Task<List<StudentDTO>> GetAllStudentsAsync();
        Task<StudentDTO> GetStudentByIdAsync(int id);
        Task<Student> AddStudentAsync(StudentDTO student);
        Task DeleteStudent(int id);
        Task UploadStudentList(List<InsertStudent> studentlist);
        Task<Student> UpdateStudentAsync(StudentDTO student, int id);
        Task<List<StudentDTO>> GetStudentByParentId(int parentId);
    }
}

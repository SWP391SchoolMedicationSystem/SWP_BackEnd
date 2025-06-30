using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.DTO;
using DataAccessLayer.DTO.Students;
using DataAccessLayer.Entity;
using Microsoft.AspNetCore.Http;

namespace BussinessLayer.IService
{
    public interface IStudentService
    {
        Task<List<StudentDTO>> GetAllStudentsAsync();
        Task<StudentDTO> GetStudentByIdAsync(int id);
        Task<Student> AddStudentAsync(UpdateStudentDTo student);
        Task DeleteStudent(int id);
        Task<string> UploadStudentList(List<InsertStudent> studentlist);
        Task<Student> UpdateStudentAsync(UpdateStudentDTo student);
        Task<List<StudentDTO>> GetStudentByParentId(int parentId);
        public (List<InsertStudent>, string) ProcessExcelFile(IFormFile file);

    }
}

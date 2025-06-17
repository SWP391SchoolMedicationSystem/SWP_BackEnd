using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.DTO;

namespace BussinessLayer.IService
{
    public interface IStudentService
    {
        Task<List<StudentDTO>> GetAllStudentsAsync();
        Task<StudentDTO> GetStudentByIdAsync(int id);
        Task AddStudentAsync(StudentDTO student);
        void DeleteStudent(int id);
        Task UploadStudentList(List<InsertStudent> studentlist); //NEW

    }
}

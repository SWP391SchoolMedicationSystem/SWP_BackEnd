using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BussinessLayer.IService;
using DataAccessLayer.DTO.StudentSpecialNeeds;
using DataAccessLayer.Entity;
using DataAccessLayer.IRepository;

namespace BussinessLayer.Service
{
    public class StudentSpecialNeedService : IStudentSpecialNeedService
    {
        public void AddStudentSpecialNeed(CreateSpecialStudentNeedDTO studentSpecialNeed)
        {
            throw new NotImplementedException();
        }

        public void DeleteStudentSpecialNeed(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<StudentSpecialNeedDTO>> GetAllStudentSpecialNeedsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<StudentSpecialNeedDTO> GetStudentSpecialNeedByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<StudentSpecialNeedDTO>> SearchStudentSpecialNeedsAsync(string searchTerm)
        {
            throw new NotImplementedException();
        }

        public void UpdateStudentSpecialNeed(UpdateStudentSpecialNeedDTO studentSpecialNeed)
        {
            throw new NotImplementedException();
        }
    }
}

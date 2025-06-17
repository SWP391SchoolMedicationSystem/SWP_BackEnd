using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BussinessLayer.IService;
using DataAccessLayer.DTO;
using DataAccessLayer.Entity;
using DataAccessLayer.IRepository;

namespace BussinessLayer.Service
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepo _studentrepo;
        private readonly IClassRoomRepository _classroomrepo;
        private readonly IParentRepository _parentrepo;
        private readonly IMapper _mapper;
        public StudentService(IStudentRepo studentrepo, IClassRoomRepository classRoomRepository,
            IParentRepository parentRepository, IMapper mapper)
        {
            _studentrepo = studentrepo;
            _classroomrepo = classRoomRepository;
            _parentrepo = parentRepository;
            _mapper = mapper;
        }

        public async Task AddStudentAsync(StudentDTO student)
        {
            Student addedstudent = _mapper.Map(Student)
            await _studentrepo.AddAsync(student)
        }

        public void DeleteStudent(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<StudentDTO>> GetAllStudentsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<StudentDTO> GetStudentByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UploadStudentList(List<InsertStudent> studentlist)
        {
            throw new NotImplementedException();
        }
    }
}

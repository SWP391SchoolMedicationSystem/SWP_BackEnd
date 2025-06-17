using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BussinessLayer.IService;
using DataAccessLayer.DTO;
using DataAccessLayer.Entity;
using DataAccessLayer.IRepository;
using DataAccessLayer.Repository;

namespace BussinessLayer.Service
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepo _studentrepo;
        private readonly IClassRoomRepository _classroomrepo;
        private readonly IClassRoomService _classservice;
        private readonly IParentRepository _parentrepo;
        private readonly IMapper _mapper;
        public StudentService(IStudentRepo studentrepo, IClassRoomRepository classRoomRepository,
            IParentRepository parentRepository, IMapper mapper, IClassRoomService classservice)
        {
            _studentrepo = studentrepo;
            _classroomrepo = classRoomRepository;
            _parentrepo = parentRepository;
            _mapper = mapper;
            _classservice = classservice;
        }

        public async Task<Student> AddStudentAsync(StudentDTO student)
        {
            Student addedStudent = _mapper.Map<Student>(student);
            await _studentrepo.AddAsync(addedStudent);
            return addedStudent;
        }
        //COMMENT

        public void DeleteStudent(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Student>> GetAllStudentsAsync()
        {
            return await _studentrepo.GetAllAsync();
        }

        public Task<StudentDTO> GetStudentByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task UploadStudentList(List<InsertStudent> studentlist)
        {
            try
            {
                var parentlist = await _parentrepo.GetAllAsync();
             foreach (var student in studentlist)
                {
                    if (student != null)
                    {
                        Classroom classroom = await _classservice.GetClassRoomByName(student.className);
                        Parent parent = parentlist.FirstOrDefault(p => p.Fullname == student.parentName && p.Phone == student.parentphone);
                        if (parent != null && classroom != null)
                        {
                            StudentDTO addstudent = new()
                            {
                                Fullname = student.fullName,
                                Age = DateTime.Now.Year - student.birthDate.Year -
                                      (DateTime.Now.DayOfYear < student.birthDate.DayOfYear ? 1 : 0),
                                BloodType = student.bloodtype,
                                Classid = classroom.Classid,
                                Parentid = parent.Parentid,
                                Dob = student.birthDate,
                                Gender = student.gender == "Nam" ? true : student.gender == "Nữ" ? false : throw new ArgumentException("Invalid gender value"),
                                StudentCode = student.studentCode,

                            };
                            Student newstudent = await AddStudentAsync(addstudent);
                            classroom.Students.Add(newstudent);
                            parent.Students.Add(newstudent);
                            _classroomrepo.Save();
                            _parentrepo.Save();
                            _studentrepo.Save();
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception($"Error uploading student list: {e.Message}", e);
            }
        }
    }
}


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
            IParentRepository parentRepository, IMapper mapper)
        {
            _studentrepo = studentrepo;
            _classroomrepo = classRoomRepository;
            _parentrepo = parentRepository;
            _mapper = mapper;
        }

        public async Task<Student> AddStudentAsync(StudentDTO student)
        {
            Student addedstudent = _mapper.Map<Student>(student);
            await _studentrepo.AddAsync(addedstudent);
            await _studentrepo.SaveChangesAsync();
            return addedstudent;
        }

        public void DeleteStudent(int id)
        {
            _studentrepo.Delete(id);
            _studentrepo.Save();
        }

        public async Task<List<Student>> GetAllStudentsAsync()
        {
            var list = await _studentrepo.GetAllAsync();
            return list;
        }

        public async Task<StudentDTO> GetStudentByIdAsync(int id)
        {
            var student = await _studentrepo.GetByIdAsync(id);
            if (student == null)
            {
                throw new KeyNotFoundException($"Student with id {id} not found.");
            }
            return _mapper.Map<StudentDTO>(student);
        }

        public async Task<Student> UpdateStudentAsync(StudentDTO student, int id)
        {
            var s = await _studentrepo.GetByIdAsync(id);
            if (s == null)
                return null;

            s.StudentCode = student.StudentCode;
            s.Fullname = student.Fullname;
            s.Age = student.Age;
            s.BloodType = student.BloodType;
            s.Gender = student.Gender;
            s.Dob = student.Dob;
            s.Classid = student.Classid;
            s.Parentid = student.Parentid;
            s.UpdatedAt = DateTime.Now;
            _studentrepo.Update(s);
            await _studentrepo.SaveChangesAsync();
            return s;
        }

        public async Task UploadStudentList(List<InsertStudent> studentlist)
        {
            try
            {
                var parentlist = await _parentrepo.GetAllAsync();
                var classlist = await _classroomrepo.GetAllAsync();
                foreach (var student in studentlist)
                {
                    if (student != null)
                    {
                        Classroom classroom = classlist.FirstOrDefault(c => c.Classname == student.className);
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


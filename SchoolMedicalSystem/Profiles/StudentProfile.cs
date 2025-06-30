using AutoMapper;
using DataAccessLayer.DTO.Students;
using DataAccessLayer.Entity;

namespace SchoolMedicalSystem.Profiles
{
    public class StudentProfile : Profile
    {
        public StudentProfile() {
            CreateMap<StudentDTO, Student>().ReverseMap();
            CreateMap<StudentParentDTO, Student>().ReverseMap();
            CreateMap<UpdateStudentDTo, Student>().ReverseMap();
        }
    }
}

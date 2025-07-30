using AutoMapper;
using DataAccessLayer.DTO;
using DataAccessLayer.DTO.Parents;
using DataAccessLayer.DTO.Students;
using DataAccessLayer.Entity;

namespace SchoolMedicalSystem.Profiles
{
    public class StudentProfile : Profile
    {
        public StudentProfile() {
            CreateMap<Student,StudentDTO>()
                .ForMember(dest => dest.Classname, opt => opt.MapFrom(src => src.Class.Classname))
                .ReverseMap();
            CreateMap<StudentParentDTO, Student>().ReverseMap();
            CreateMap<Student, StudentVaccineEvent>().ReverseMap();
            CreateMap<UpdateStudentDTo, Student>().ReverseMap();
            CreateMap<AddStudentDTO, Student>().ReverseMap();
        }
    }
}

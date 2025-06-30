using AutoMapper;
using DataAccessLayer.DTO;
using DataAccessLayer.DTO.Parents;
using DataAccessLayer.Entity;

namespace SchoolMedicalSystem.Profiles
{
    public class StudentProfile : Profile
    {
        public StudentProfile() {
            CreateMap<StudentDTO, Student>().ReverseMap();
            CreateMap<StudentParentDTO, Student>().ReverseMap();
            CreateMap<Student, StudentVaccineEvent>().ReverseMap();
        }
    }
}

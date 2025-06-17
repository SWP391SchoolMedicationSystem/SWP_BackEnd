using AutoMapper;
using DataAccessLayer.DTO;
using DataAccessLayer.Entity;

namespace SchoolMedicalSystem.Profiles
{
    public class StudentProfile : Profile
    {
        public StudentProfile() {
            CreateMap<StudentDTO, Student>().ReverseMap();
            
        
        }
    }
}

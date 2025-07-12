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
            CreateMap<Student, StudentDTO>()
                .ForMember(s => s.ParentName, opt => opt.MapFrom(scr => scr.Parent.Fullname))
                .ForMember(s => s.ClassName, opt => opt.MapFrom(scr => scr.Class.Classname))
                .ForMember(s => s.ParentPhone, opt => opt.MapFrom(scr => scr.Parent.Phone))
                .ForMember(dest => dest.CreatedByUserName, opt => opt.MapFrom(src => src.CreatedByUser.StaffUsers.FirstOrDefault(s => s.Userid == src.CreatedByUserId).Fullname))
                .ForMember(dest => dest.ModifiedByUserName, opt => opt.MapFrom(src => src.ModifiedByUser.StaffUsers.FirstOrDefault(s => s.Userid == src.ModifiedByUserId).Fullname))
                .ReverseMap();
            CreateMap<StudentParentDTO, Student>().ReverseMap();
            CreateMap<Student, StudentVaccineEvent>().ReverseMap();
            CreateMap<UpdateStudentDTo, Student>().ReverseMap();
        }
    }
}

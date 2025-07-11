using AutoMapper;
using DataAccessLayer.DTO.HealthChecks;
using DataAccessLayer.Entity;

namespace SchoolMedicalSystem.Profiles
{
    public class HealthCheckProfile : Profile
    {
        public HealthCheckProfile()
        {
            CreateMap<Healthcheck, HealthCheckDTO>().
                ForMember(dest => dest.StudentName, opt => opt.MapFrom(src => src.Student.Fullname))
                .ForMember(dest => dest.StaffName, opt => opt.MapFrom(src => src.Staff.Fullname))
                .ReverseMap();
        }

    }
}

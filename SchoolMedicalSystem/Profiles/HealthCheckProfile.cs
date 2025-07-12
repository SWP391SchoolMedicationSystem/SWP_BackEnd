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
                                .ForMember(dest => dest.CreatedByUserName, opt => opt.MapFrom(src => src.CreatedByUser.StaffUsers.FirstOrDefault(s => s.Userid == src.CreatedByUserId).Fullname))
                .ForMember(dest => dest.ModifiedByUserName, opt => opt.MapFrom(src => src.ModifiedByUser.StaffUsers.FirstOrDefault(s => s.Userid == src.ModifiedByUserId).Fullname))

                .ReverseMap();
            CreateMap<Healthcheck, AddHealthCheckDTO>().ReverseMap();
        }

    }
}

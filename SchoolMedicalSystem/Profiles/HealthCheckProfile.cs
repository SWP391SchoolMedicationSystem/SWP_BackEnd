using AutoMapper;
using DataAccessLayer.DTO;
using DataAccessLayer.DTO.HealthCheck;
using DataAccessLayer.Entity;

namespace SchoolMedicalSystem.Profiles
{
    public class HealthCheckProfile : Profile
    {
        public HealthCheckProfile()
        {
            CreateMap<Healthcheck, AddHealthCheckDto>()
                .ForMember(dest => dest.Eventid, opt => opt.Ignore())
                .ReverseMap();
            CreateMap<Healthcheck, HealthCheckDTO>()
                .ForMember(dest => dest.StudentName, opt => opt.MapFrom(src => src.Student.Fullname))
                .ReverseMap();
            CreateMap<AddHealthCheckEventDto, Healthcheckevent>()
                .ForMember(dest => dest.Documentfilename, opt => opt.Ignore())
                .ForMember(dest => dest.Documentaccesstoken, opt => opt.Ignore())
                .ReverseMap();
            CreateMap<Healthcheckevent, HeatlhCheckEventDto>().ReverseMap();
            CreateMap<Healthcheckrecordevent, HeatlhCheckRecordEventDto>().ForMember(dest => dest.Healthcheckevent.Healthcheckrecordevents, opt => opt.Ignore())
                .ForMember(dest => dest.Healthcheckrecord.Healthcheckrecordevents, opt => opt.Ignore()).ReverseMap();
            
        }

    }
}

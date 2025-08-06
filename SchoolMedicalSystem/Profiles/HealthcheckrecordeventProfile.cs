using AutoMapper;
using DataAccessLayer.DTO.HealthCheck;
using DataAccessLayer.Entity;

namespace SchoolMedicalSystem.Profiles
{
    public class HealthcheckrecordeventProfile : Profile
    {
        public HealthcheckrecordeventProfile()
        {
            CreateMap<AddHealthcheckrecordeventDTO, Healthcheckrecordevent>()
                .ForMember(dest => dest.Healthcheckrecordeventid, opt => opt.Ignore())
                .ForMember(dest => dest.Isdeleted, opt => opt.MapFrom(src => false))
                .ForMember(dest => dest.Healthcheckid, Map => Map.MapFrom(src => src.Healthcheckrecordid))
                .ForMember(dest => dest.Healthcheckeventid, Map => Map.MapFrom(src => src.Healthcheckeventid))
                .ReverseMap();
        }

    }
}

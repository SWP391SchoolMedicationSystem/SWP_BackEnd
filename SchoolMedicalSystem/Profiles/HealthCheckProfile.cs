using AutoMapper;
using DataAccessLayer.DTO.HealthChecks;
using DataAccessLayer.Entity;

namespace SchoolMedicalSystem.Profiles
{
    public class HealthCheckProfile : Profile
    {
        public HealthCheckProfile()
        {
            CreateMap<Healthcheck, HealthCheckDTO>().ReverseMap();
        }

    }
}

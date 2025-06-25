using AutoMapper;
using DataAccessLayer.DTO.HealthStatus;
using DataAccessLayer.Entity;

namespace SchoolMedicalSystem.Profiles
{
    public class HealthStatusProfile : Profile
    {
        public HealthStatusProfile() {
            CreateMap<Healthstatuscategory,HealthStatusCategoryDTO>().ReverseMap();
            CreateMap<HealthStatusDTO, Healthstatus>().ReverseMap();
        }
    }
}

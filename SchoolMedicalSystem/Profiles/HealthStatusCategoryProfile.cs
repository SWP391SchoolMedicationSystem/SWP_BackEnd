using AutoMapper;
using DataAccessLayer.DTO.HealthStatus;
using DataAccessLayer.Entity;

namespace SchoolMedicalSystem.Profiles
{
    public class HealthStatusCategoryProfile : Profile
    {
        public HealthStatusCategoryProfile() {
            CreateMap<Healthstatuscategory,HealthStatusCategoryDTO>().ReverseMap();
        }
    }
}

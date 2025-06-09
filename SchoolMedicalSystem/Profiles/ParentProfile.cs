using AutoMapper;
using DataAccessLayer.DTO;
using DataAccessLayer.Entity;

namespace SchoolMedicalSystem.Profiles
{
    public class ParentProfile : Profile
    {
        public ParentProfile()
        {
            CreateMap<ParentLoginDTO, Parent>().ReverseMap();
            CreateMap<ParentRegister, Parent>().ReverseMap();
            CreateMap<Parent, ParentDTO>().ReverseMap();
        }
    }
    
    
}

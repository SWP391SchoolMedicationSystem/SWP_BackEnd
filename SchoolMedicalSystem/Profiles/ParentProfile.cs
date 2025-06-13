using AutoMapper;
using DataAccessLayer.DTO;
using DataAccessLayer.Entity;

namespace SchoolMedicalSystem.Profiles
{
    public class ParentProfile : Profile
    {
        public ParentProfile()
        {
            CreateMap<LoginDTO, Parent>().ReverseMap();
            CreateMap<ParentRegister, Parent>().ReverseMap();
            CreateMap<Parent, ParentDTO>().ReverseMap();
            CreateMap<ParentUpdate, Parent>().ReverseMap();
        }
    }
    
    
}

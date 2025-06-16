using AutoMapper;
using DataAccessLayer.DTO;
using DataAccessLayer.DTO.Parents;
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
            CreateMap<Parent, ParentUpdate>().ReverseMap();
            CreateMap<Parent, LoginGoogleDTO>().ReverseMap();
        }


    }
}

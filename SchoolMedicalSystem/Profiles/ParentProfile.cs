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
            CreateMap<ParentDTO, Parent>().
                ForMember(dest => dest.Students, opt => opt.MapFrom(src => src.Students))
                .ReverseMap();
            CreateMap<ParentUpdate, Parent>().ReverseMap();
            CreateMap<Parent, ParentUpdate>().ReverseMap();
            CreateMap<Parent, LoginGoogleDTO>().ReverseMap();
            CreateMap<Parent, ParentStudent>().ReverseMap();
            CreateMap<Parent, ParentVaccineEvent>().ReverseMap();
        }


    }
}

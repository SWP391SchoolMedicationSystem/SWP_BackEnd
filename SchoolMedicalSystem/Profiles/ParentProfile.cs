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
            CreateMap<Parent, ParentDTO>().
                ForMember(p => p.Students, opt => opt.MapFrom(src => src.Students))
                .ForMember(dest => dest.CreatedByUserName, opt => opt.MapFrom(src => src.CreatedByUser.StaffUsers.FirstOrDefault(s => s.Userid == src.CreatedByUserId).Fullname))
                .ForMember(dest => dest.ModifiedByUserName, opt => opt.MapFrom(src => src.ModifiedByUser.StaffUsers.FirstOrDefault(s => s.Userid == src.ModifiedByUserId).Fullname))
                .ReverseMap();
            CreateMap<ParentUpdate, Parent>().ReverseMap();
            CreateMap<Parent, ParentUpdate>().ReverseMap();
            CreateMap<Parent, LoginGoogleDTO>().ReverseMap();
            CreateMap<Parent, ParentStudent>().ReverseMap();
            CreateMap<Parent, ParentVaccineEvent>().ReverseMap();
        }


    }
}

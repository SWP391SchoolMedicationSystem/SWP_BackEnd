using AutoMapper;
using DataAccessLayer.DTO;
using DataAccessLayer.Entity;

namespace SchoolMedicalSystem.Profiles
{
    public class FormProfile : Profile
    {
        public FormProfile()
        {
            CreateMap<FormDTO, Form>()
                .ForMember(dest => dest.Parentid, opt => opt.MapFrom(src => src.ParentId))
                .ReverseMap()
                .ForMember(dest => dest.ParentId, opt => opt.MapFrom(src => src.Parentid));

            CreateMap<CreateFormDTO, Form>().ReverseMap();
            CreateMap<UpdateFormDTO, Form>().ReverseMap();
            CreateMap<Form, ResponseFormDTO>()
                .ForMember(dest => dest.ParentName, opt => opt.MapFrom(src => src.Parent.Fullname))
                .ForMember(dest => dest.ParentEmail, opt => opt.MapFrom(src => src.Parent.Email));
        }
    }
}

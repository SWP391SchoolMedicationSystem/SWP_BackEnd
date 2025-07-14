using AutoMapper;
using DataAccessLayer.DTO.Consultations;
using DataAccessLayer.Entity;

namespace SchoolMedicalSystem.Profiles
{
    public class ConsultationProfile : Profile
    {
        public ConsultationProfile()
        {
            CreateMap<DataAccessLayer.Entity.Consultationrequest, CreateConsultationDTO>()
                .ReverseMap();
            CreateMap<DataAccessLayer.Entity.Consultationtype,CreateConsultationTypeDTO>().ReverseMap();
            CreateMap<DataAccessLayer.Entity.Consultationrequest, UpdateConsultationDTO>()
                .ReverseMap();
            //CreateMap<DataAccessLayer.Entity.Consultationrequest, ConsultationDTO>().
            //    ForMember(dest => dest.ParentName, opt => opt.Ignore()).ForMember(dest => dest.StudentName, opt => opt.Ignore())
            //    .ForMember(dest => dest.Staffname, opt => opt.Ignore())
            //    .ForMember(dest => dest.CreateByName, opt => opt.Ignore())
            //    .ReverseMap();
            CreateMap<ConsultationDTO,Consultationrequest>().ForSourceMember(dest => dest.ParentName, opt => opt.DoNotValidate())
                .ForSourceMember(dest => dest.StudentName, opt => opt.DoNotValidate())
                .ForSourceMember(dest => dest.Staffname, opt => opt.DoNotValidate())
                .ForSourceMember(dest => dest.CreateByName, opt => opt.DoNotValidate())
                .ReverseMap();
            CreateMap<Consultationrequest, ApprovalConsultationDTO>().ReverseMap();

        }
    }
}

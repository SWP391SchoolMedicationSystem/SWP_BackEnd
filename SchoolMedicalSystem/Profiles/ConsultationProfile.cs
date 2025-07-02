using AutoMapper;
using DataAccessLayer.DTO.Consultations;

namespace SchoolMedicalSystem.Profiles
{
    public class ConsultationProfile : Profile
    {
        public ConsultationProfile()
        {
            CreateMap<DataAccessLayer.Entity.Consultationrequest, CreateConsultationDTO>()
                .ReverseMap();
            CreateMap<DataAccessLayer.Entity.Consultationtype,CreateonsultationTypeDTO>().ReverseMap();
                
        }
    }
}

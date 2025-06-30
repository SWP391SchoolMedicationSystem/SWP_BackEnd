using AutoMapper;
using DataAccessLayer.DTO;

namespace SchoolMedicalSystem.Profiles
{
    public class ConsultationProfile : Profile
    {
        public ConsultationProfile()
        {
            CreateMap<DataAccessLayer.Entity.Consultationrequest, ConsultationRequestDTO>()
                .ReverseMap();
            CreateMap<DataAccessLayer.Entity.Consultationtype,ConsultationTypeDTO>().ReverseMap();
                
        }
    }
}

using AutoMapper;

namespace SchoolMedicalSystem.Profiles
{
    public class EmailProfile : Profile
    {
        public EmailProfile()
        { 
            CreateMap<DataAccessLayer.DTO.EmailDTO, DataAccessLayer.Entity.EmailTemplate>()
                .ReverseMap();
        }


    }
}

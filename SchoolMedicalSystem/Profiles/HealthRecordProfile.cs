using AutoMapper;
using DataAccessLayer.DTO;
using DataAccessLayer.Entity;

namespace SchoolMedicalSystem.Profiles
{
    public class HealthRecordProfile : Profile
    {
        public HealthRecordProfile()
        {
            CreateMap<Healthrecord, HealthRecordDTO>().ReverseMap();
            CreateMap<HealthRecordDTO, Healthrecord>().ReverseMap();
        }
    }

}

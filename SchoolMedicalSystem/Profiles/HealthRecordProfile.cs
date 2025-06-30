using AutoMapper;
using DataAccessLayer.DTO;
using DataAccessLayer.DTO.HealthRecords;
using DataAccessLayer.Entity;

namespace SchoolMedicalSystem.Profiles
{
    public class HealthRecordProfile : Profile
    {
        public HealthRecordProfile()
        {
            CreateMap<Healthrecord, HealthRecordDto>().ReverseMap();
            CreateMap<HealthRecordDto, Healthrecord>().ReverseMap();
            CreateMap<Healthrecord, CreateHealthRecordDTO>().ReverseMap();
            
        }
    }

}

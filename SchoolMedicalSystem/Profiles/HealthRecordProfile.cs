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
            CreateMap<HealthRecord, HealthRecordDto>().ReverseMap();
            CreateMap<HealthRecordDto, HealthRecord>().ReverseMap();
            CreateMap<HealthRecord, CreateHealthRecordDTO>().ReverseMap();
            
        }
    }

}

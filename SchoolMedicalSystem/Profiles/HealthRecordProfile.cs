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
            CreateMap<HealthRecord, HealthRecordDto>()
                .ForMember(destinationMember => destinationMember.StudentName,
                           opt => opt.MapFrom(sourceMember => sourceMember.Student.Fullname))
                .ForMember(destinationMember => destinationMember.staffName, destinationMember => destinationMember
                    .MapFrom(sourceMember => sourceMember.Staff.Fullname))
                .ReverseMap()
                ;
            CreateMap<HealthRecordDto, HealthRecord>().ReverseMap();
            CreateMap<HealthRecord, CreateHealthRecordDTO>().ReverseMap();
            
        }
    }

}

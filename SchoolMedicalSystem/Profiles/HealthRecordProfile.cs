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
                .ForMember(dest => dest.CreatedByUserName, opt => opt.MapFrom(src => src.CreatedByUser.StaffUsers.FirstOrDefault(s => s.Userid == src.CreatedByUserId).Fullname))
                .ForMember(dest => dest.ModifiedByUserName, opt => opt.MapFrom(src => src.ModifiedByUser.StaffUsers.FirstOrDefault(s => s.Userid == src.ModifiedByUserId).Fullname))

                .ReverseMap()
                ;
            CreateMap<HealthRecordDto, HealthRecord>().ReverseMap();
            CreateMap<HealthRecord, CreateHealthRecordDTO>().ReverseMap();
            
        }
    }

}

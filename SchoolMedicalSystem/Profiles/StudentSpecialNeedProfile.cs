using AutoMapper;
using DataAccessLayer.DTO.StudentSpecialNeeds;
using DataAccessLayer.Entity;

namespace SchoolMedicalSystem.Profiles
{
    public class StudentSpecialNeedProfile : Profile
    {
        public StudentSpecialNeedProfile()
        {
            CreateMap<StudentSpecialNeed, StudentSpecialNeedDTO>()
                .ForMember(dest => dest.CreatedByUserName, opt => opt.MapFrom(src => src.CreatedByUser.StaffUsers.FirstOrDefault(s => s.Userid == src.CreatedByUserId).Fullname))
                .ForMember(dest => dest.ModifiedByUserName, opt => opt.MapFrom(src => src.ModifiedByUser.StaffUsers.FirstOrDefault(s => s.Userid == src.ModifiedByUserId).Fullname))
                .ReverseMap();
            CreateMap<CreateSpecialStudentNeedDTO, StudentSpecialNeed>().ReverseMap();
            CreateMap<UpdateStudentSpecialNeedDTO, StudentSpecialNeed>().ReverseMap();
        }
    }
}

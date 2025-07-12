using AutoMapper;
using DataAccessLayer.DTO.SpecialNeedCategory;
using DataAccessLayer.DTO.StudentSpecialNeeds;
using DataAccessLayer.Entity;

namespace SchoolMedicalSystem.Profiles
{
    public class SpecialNeedCategoryProfile : Profile
    {
        public SpecialNeedCategoryProfile()
        {
            CreateMap<SpecialNeedsCategory, SpecialNeedCategoryDTO>()
                .ForMember(dest => dest.CreatedByUserName, opt => opt.MapFrom(src => src.CreatedByUser.StaffUsers.FirstOrDefault(s => s.Userid == src.CreatedByUserId).Fullname))
                .ForMember(dest => dest.ModifiedByUserName, opt => opt.MapFrom(src => src.ModifiedByUser.StaffUsers.FirstOrDefault(s => s.Userid == src.ModifiedByUserId).Fullname))
    .ReverseMap();
            CreateMap<CreateSpecialNeedCategoryDTO, SpecialNeedsCategory>().ReverseMap();
            CreateMap<UpdateSpecialNeedCategoryDTO, SpecialNeedsCategory>().ReverseMap();
        }
    }
}

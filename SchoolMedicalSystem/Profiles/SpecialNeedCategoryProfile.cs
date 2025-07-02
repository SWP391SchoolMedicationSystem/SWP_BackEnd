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
    .ReverseMap();
            CreateMap<CreateSpecialNeedCategoryDTO, SpecialNeedsCategory>().ReverseMap();
            CreateMap<UpdateSpecialNeedCategoryDTO, SpecialNeedsCategory>().ReverseMap();
        }
    }
}

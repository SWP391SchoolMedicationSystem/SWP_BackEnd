using AutoMapper;
using DataAccessLayer.DTO.MedicineCategory;
using DataAccessLayer.Entity;

namespace SchoolMedicalSystem.Profiles
{
    public class MedicineCategoryProfile : Profile
    {
        public MedicineCategoryProfile()
        {
            CreateMap<Medicinecategory, MedicineCategoryDTO>()
                .ReverseMap();
            CreateMap<CreateMedicineCategoryDTO, Medicinecategory>().ReverseMap();
            CreateMap<UpdateMedicineCategoryDTO, Medicinecategory>()
                .ReverseMap();
        }
    }
}

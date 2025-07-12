using AutoMapper;
using DataAccessLayer.DTO.Medicines;
using DataAccessLayer.DTO.PersonalMedicine;
using DataAccessLayer.DTO.Staffs;
using DataAccessLayer.Entity;

namespace SchoolMedicalSystem.Profiles
{
    public class MedicineProfile : Profile
    {
        public MedicineProfile()
        {
            CreateMap<MedicineCatalog, MedicineDTO>()
                .ForMember(dest => dest.CreatedByUserName, opt => opt.MapFrom(src => src.CreatedByUser.StaffUsers.FirstOrDefault(s => s.Userid == src.CreatedByUserId).Fullname))
                .ForMember(dest => dest.ModifiedByUserName, opt => opt.MapFrom(src => src.ModifiedByUser.StaffUsers.FirstOrDefault(s => s.Userid == src.ModifiedByUserId).Fullname))

                .ReverseMap();
            CreateMap<CreateMedicineDTO, MedicineCatalog>().ReverseMap();
            CreateMap<UpdateMedicineDTO, MedicineCatalog>().ReverseMap();
        }
    }
}

using AutoMapper;
using DataAccessLayer.DTO.Medicines;
using DataAccessLayer.DTO.PersonalMedicine;
using DataAccessLayer.Entity;

namespace SchoolMedicalSystem.Profiles
{
    public class MedicineProfile : Profile
    {
        public MedicineProfile()
        {
            CreateMap<MedicineDTO, MedicineCatalog>().ReverseMap();
            CreateMap<CreateMedicineDTO, MedicineCatalog>().ReverseMap();
            CreateMap<UpdateMedicineDTO, MedicineCatalog>().ReverseMap();
        }
    }
}

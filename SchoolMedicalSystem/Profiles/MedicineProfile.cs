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
            CreateMap<MedicineDTO, Medicine>().ReverseMap();
            CreateMap<CreateMedicineDTO, Medicine>().ReverseMap();
            CreateMap<UpdateMedicineDTO, Medicine>().ReverseMap();
        }
    }
}

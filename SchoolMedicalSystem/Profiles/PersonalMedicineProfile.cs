using AutoMapper;
using DataAccessLayer.DTO.PersonalMedicine;
using DataAccessLayer.Entity;

namespace SchoolMedicalSystem.Profiles
{
    public class PersonalMedicineProfile : Profile
    {
        public PersonalMedicineProfile()
        {
            CreateMap<PersonalMedicineDTO,Personalmedicine>().ReverseMap();
            CreateMap<UpdatePersonalMedicineDTO, Personalmedicine>().ReverseMap();
            CreateMap<AddPersonalMedicineDTO, Personalmedicine>().ReverseMap();
        }
    }
}

using AutoMapper;
using DataAccessLayer.DTO.Blogs;
using DataAccessLayer.DTO.PersonalMedicine;
using DataAccessLayer.Entity;

namespace SchoolMedicalSystem.Profiles
{
    public class PersonalMedicineProfile : Profile
    {
        public PersonalMedicineProfile()
        {
            CreateMap<Personalmedicine, AddPersonalMedicineDTO>().ReverseMap();
            CreateMap<Personalmedicine, UpdatePersonalMedicineDTO>().ReverseMap();
        }
    }
}

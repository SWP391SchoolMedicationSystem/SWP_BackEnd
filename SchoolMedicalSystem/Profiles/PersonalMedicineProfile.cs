using AutoMapper;
using DataAccessLayer.DTO;
using DataAccessLayer.DTO.PersonalMedicine;
using DataAccessLayer.Entity;

namespace SchoolMedicalSystem.Profiles
{
    public class PersonalMedicineProfile : Profile
    {
        public PersonalMedicineProfile()
        {
            CreateMap<Personalmedicine, PersonalMedicineDTO>()
                .ForMember(dest => dest.CreatedByUserName, opt => opt.MapFrom(src => src.CreatedByUser.StaffUsers.FirstOrDefault(s => s.Userid == src.CreatedByUserId).Fullname))
                .ForMember(dest => dest.ModifiedByUserName, opt => opt.MapFrom(src => src.ModifiedByUser.StaffUsers.FirstOrDefault(s => s.Userid == src.ModifiedByUserId).Fullname))
                .ReverseMap();
            CreateMap<UpdatePersonalMedicineDTO, Personalmedicine>().ReverseMap();
            CreateMap<AddPersonalMedicineDTO, Personalmedicine>().ReverseMap();
            CreateMap<MedicineScheduleLink, PersonalMedicineScheduleDTO>().ReverseMap();
        }
    }
}

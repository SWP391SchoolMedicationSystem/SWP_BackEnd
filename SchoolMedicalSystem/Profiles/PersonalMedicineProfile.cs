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
                .ForMember(dest => dest.ParentName, opt => opt.MapFrom(src => src.Parent != null ? src.Parent.Fullname : ""))
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Parent != null ? src.Parent.Phone : ""))
                .ForMember(dest => dest.StudentName, opt => opt.MapFrom(src => src.Student != null ? src.Student.Fullname : ""))
                .ForMember(des => des.MedicineName, opt => opt.MapFrom(src => src.Medicine != null ? src.Medicine.Medicinename : ""))
                .ReverseMap();
            CreateMap<UpdatePersonalMedicineDTO, Personalmedicine>().ReverseMap();
            CreateMap<AddPersonalMedicineDTO, Personalmedicine>().ReverseMap();
            CreateMap<Medicineschedule, PersonalMedicineScheduleDTO>().ReverseMap();
        }
    }
}

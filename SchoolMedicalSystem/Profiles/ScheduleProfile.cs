using AutoMapper;
using DataAccessLayer.DTO;
using DataAccessLayer.DTO.Medicines;
using DataAccessLayer.Entity;

namespace SchoolMedicalSystem.Profiles
{
    public class ScheduleProfile : Profile
    {
        public ScheduleProfile()
        {
            CreateMap<Scheduledetail, ScheduleDetailDTO>().ReverseMap();    
            CreateMap<MedicineScheduleDTO, Medicineschedule>().ReverseMap();
            CreateMap<AddScheduleMedicineDTO, Medicineschedule>()
                .ForMember(dest => dest.ScheduledetailsNavigation, opt => opt.Ignore())
                .ForMember(dest => dest.Personalmedicine, opt => opt.Ignore());
            CreateMap<Medicineschedule, AddScheduleMedicineDTO>().ReverseMap();
        }
    }
}

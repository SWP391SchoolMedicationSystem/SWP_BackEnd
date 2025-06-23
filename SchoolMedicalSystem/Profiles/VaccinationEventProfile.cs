using AutoMapper;
using DataAccessLayer.DTO;
using DataAccessLayer.Entity;

namespace SchoolMedicalSystem.Profiles
{
    public class VaccinationEventProfile : Profile
    {
        public VaccinationEventProfile() {
        CreateMap<Vaccinationevent, VaccinationEventDTO>().ReverseMap();

        }
    }
}

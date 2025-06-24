using AutoMapper;
using DataAccessLayer.DTO;
using DataAccessLayer.Entity;

namespace SchoolMedicalSystem.Profiles
{
    public class VaccinationRecordProfile : Profile
    {
        public VaccinationRecordProfile()
        {
            CreateMap<Vaccinationrecord, VaccinationRecordDTO>()
                .ReverseMap();
            CreateMap<VaccinationRecordStudentEvent,Vaccinationrecord>().ReverseMap();
        }
    }
}

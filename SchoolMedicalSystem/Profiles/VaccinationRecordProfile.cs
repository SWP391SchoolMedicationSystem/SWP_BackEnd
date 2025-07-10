using AutoMapper;
using DataAccessLayer.DTO;
using DataAccessLayer.Entity;

namespace SchoolMedicalSystem.Profiles
{
    public class VaccinationRecordProfile : Profile
    {
        public VaccinationRecordProfile()
        {
            CreateMap<StudentVaccinationRecord, VaccinationRecordDTO>()
                .ReverseMap();
            CreateMap<VaccinationRecordStudentEvent, StudentVaccinationRecord>().ReverseMap();
        }
    }
}

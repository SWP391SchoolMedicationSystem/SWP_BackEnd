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
                .ForMember(dest => dest.StudentName, opt => opt.MapFrom(src => src.Student.Fullname))
                .ForMember(dest => dest.VaccineName, opt => opt.MapFrom(src => src.Vaccine.VaccineName))
                .ForMember(dest => dest.EventName, opt => opt.MapFrom(src => src.Event.EventName))
                .ReverseMap();
            CreateMap<VaccinationRecordStudentEvent, StudentVaccinationRecord>().
                ForMember(dest => dest.Student, opt => opt.MapFrom(src => new Student { Studentid = src.StudentId }))
                .ForMember(dest => dest.Vaccine, opt => opt.MapFrom(src => new Vaccine { VaccineId = src.VaccineId }))
                .ReverseMap();
        }
    }
}

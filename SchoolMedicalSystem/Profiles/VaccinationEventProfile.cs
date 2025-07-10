using AutoMapper;
using DataAccessLayer.DTO;
using DataAccessLayer.Entity;

namespace SchoolMedicalSystem.Profiles
{
    public class VaccinationEventProfile : Profile
    {
        public VaccinationEventProfile()
        {
            // Entity to DTO mappings
            CreateMap<VaccinationEvent, VaccinationEventDTO>().ReverseMap();
            // DTO to Entity mappings
            CreateMap<CreateVaccinationEventDTO, VaccinationEvent>().ReverseMap();
            CreateMap<UpdateVaccinationEventDTO, VaccinationEvent>().ReverseMap();
            // VaccinationRecord mappings
            //CreateMap<Vaccinationrecord, ParentVaccinationResponseDTO>()
            //    .ForMember(dest => dest.ParentId, opt => opt.MapFrom(src => src.Student.Parentid))
            //    .ForMember(dest => dest.StudentId, opt => opt.MapFrom(src => src.Studentid))
            //    .ForMember(dest => dest.VaccinationEventId, opt => opt.MapFrom(src => src.Vaccinationeventid));

            //CreateMap<ParentVaccinationResponseDTO, Vaccinationrecord>()
            //    .ForMember(dest => dest.Studentid, opt => opt.MapFrom(src => src.StudentId))
            //    .ForMember(dest => dest.Vaccinationeventid, opt => opt.MapFrom(src => src.VaccinationEventId))
            //    .ForMember(dest => dest.Confirmedbyparent, opt => opt.MapFrom(src => src.WillAttend));
        }
    }
} 
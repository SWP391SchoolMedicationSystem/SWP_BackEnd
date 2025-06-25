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
            CreateMap<Vaccinationevent, VaccinationEventDTO>()
                .ForMember(dest => dest.VaccinationEventId, opt => opt.MapFrom(src => src.Vaccinationeventid))
                .ForMember(dest => dest.VaccinationEventName, opt => opt.MapFrom(src => src.Vaccinationeventname))
                .ForMember(dest => dest.OrganizedBy, opt => opt.MapFrom(src => src.Organizedby))
                .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => src.Createddate))
                .ForMember(dest => dest.ModifiedDate, opt => opt.MapFrom(src => src.Modifieddate))
                .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.Createdby))
                .ForMember(dest => dest.ModifiedBy, opt => opt.MapFrom(src => src.Modifiedby))
                .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(src => src.Isdeleted));

            // DTO to Entity mappings
            CreateMap<CreateVaccinationEventDTO, Vaccinationevent>()
                .ForMember(dest => dest.Vaccinationeventname, opt => opt.MapFrom(src => src.VaccinationEventName))
                .ForMember(dest => dest.Organizedby, opt => opt.MapFrom(src => src.OrganizedBy));

            CreateMap<UpdateVaccinationEventDTO, Vaccinationevent>()
                .ForMember(dest => dest.Vaccinationeventid, opt => opt.MapFrom(src => src.VaccinationEventId))
                .ForMember(dest => dest.Vaccinationeventname, opt => opt.MapFrom(src => src.VaccinationEventName))
                .ForMember(dest => dest.Organizedby, opt => opt.MapFrom(src => src.OrganizedBy));

            // VaccinationRecord mappings
            CreateMap<Vaccinationrecord, ParentVaccinationResponseDTO>()
                .ForMember(dest => dest.ParentId, opt => opt.MapFrom(src => src.Student.Parentid))
                .ForMember(dest => dest.StudentId, opt => opt.MapFrom(src => src.Studentid))
                .ForMember(dest => dest.VaccinationEventId, opt => opt.MapFrom(src => src.Vaccinationeventid));

            CreateMap<ParentVaccinationResponseDTO, Vaccinationrecord>()
                .ForMember(dest => dest.Studentid, opt => opt.MapFrom(src => src.StudentId))
                .ForMember(dest => dest.Vaccinationeventid, opt => opt.MapFrom(src => src.VaccinationEventId))
                .ForMember(dest => dest.Confirmedbyparent, opt => opt.MapFrom(src => src.WillAttend));
        }
    }
} 
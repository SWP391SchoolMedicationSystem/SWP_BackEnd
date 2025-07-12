using AutoMapper;
using DataAccessLayer.DTO;
using DataAccessLayer.DTO.Staffs;
using DataAccessLayer.Entity;

namespace SchoolMedicalSystem.Profiles
{
    public class StaffProfile : Profile
    {
        public StaffProfile()
        {
            CreateMap<LoginDTO, Staff>().ReverseMap();
            CreateMap<StaffRegister, Staff>().ReverseMap();
            CreateMap<Staff, StaffDTO>()
                .ForMember(dest => dest.CreatedByUserName, opt => opt.MapFrom(src => src.CreatedByUser.StaffUsers.FirstOrDefault(s => s.Userid == src.CreatedByUserId).Fullname))
                .ForMember(dest => dest.ModifiedByUserName, opt => opt.MapFrom(src => src.ModifiedByUser.StaffUsers.FirstOrDefault(s => s.Userid == src.ModifiedByUserId).Fullname))
                .ReverseMap();

            CreateMap<Staff, LoginGoogleDTO>().ReverseMap();
            CreateMap<StaffUpdate, Staff>().ReverseMap();
        }
    }
}

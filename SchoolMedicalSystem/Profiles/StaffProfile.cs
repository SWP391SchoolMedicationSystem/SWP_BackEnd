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
            CreateMap<Staff, StaffDTO>().ReverseMap();
            CreateMap<Staff, LoginGoogleDTO>().ReverseMap();
            CreateMap<StaffUpdate, Staff>().ReverseMap();
        }
    }
}

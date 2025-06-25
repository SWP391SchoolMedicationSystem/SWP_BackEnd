using AutoMapper;
using DataAccessLayer.DTO;
using DataAccessLayer.Entity;

namespace SchoolMedicalSystem.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserDTo, User>().ReverseMap();
            CreateMap<User, UserDTo>().ReverseMap();
        }
    }
}

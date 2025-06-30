using AutoMapper;
using DataAccessLayer.DTO;
using DataAccessLayer.Entity;

namespace SchoolMedicalSystem.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile() {
            CreateMap<UserDTO, User>().ReverseMap();
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<LoginGoogleDTO, User>().ReverseMap();
            CreateMap<LoginDTO, User>().ReverseMap();
        }
    }
}

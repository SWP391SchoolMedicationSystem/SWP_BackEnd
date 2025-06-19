using AutoMapper;
using DataAccessLayer.DTO;
using DataAccessLayer.DTO.Notifications;
using DataAccessLayer.Entity;

namespace SchoolMedicalSystem.Profiles
{
    public class NotificationProfile : Profile
    {
        public NotificationProfile()
        {
            CreateMap<Notification, NotificationDTO>().ReverseMap();
            CreateMap<NotificationDTO, Notification>().ForSourceMember(c=> c.Message, opt => opt.DoNotValidate()).ReverseMap();

            CreateMap<NotificationParentDetail, Notification>().ReverseMap();
            CreateMap<NotificationParentDetail, NotificationDTO>().ReverseMap();

        }
    }
}

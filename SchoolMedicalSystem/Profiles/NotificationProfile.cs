using AutoMapper;
using DataAccessLayer.DTO.Notifications;
using DataAccessLayer.Entity;

namespace SchoolMedicalSystem.Profiles
{
    public class NotificationProfile : Profile
    {
        public NotificationProfile()
        {
            CreateMap<Notification, NotificationDTO>().ReverseMap();
            CreateMap<NotificationDTO, Notification>().ReverseMap();
            CreateMap<NotificationParentDetail, Notification>().ReverseMap();
            CreateMap<Notification, NotificationParentDetail>().ReverseMap();
            CreateMap<NotificationParentDetail, NotificationDTO>().ReverseMap();
        }
    }
}

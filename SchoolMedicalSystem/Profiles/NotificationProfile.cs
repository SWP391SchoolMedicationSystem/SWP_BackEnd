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
            CreateMap<NotificationDTO, Notification>().ForSourceMember(c=> c.Message, opt => opt.DoNotValidate()).ReverseMap();
            CreateMap<NotificationParentDetail, Notification>().ReverseMap();
            CreateMap<Notification, NotificationParentDetail>().ReverseMap();
            CreateMap<NotificationParentDetail, NotificationDTO>().ReverseMap();
            CreateMap<CreateNotificationDTO, Notification>().ReverseMap();
            CreateMap<UpdateNotificationDTO, Notification>().ReverseMap();

        }
    }
}

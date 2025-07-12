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
            CreateMap<Notification, NotificationDTO>()
                .ForMember(dest => dest.CreatedByUserName, opt => opt.MapFrom(src => src.CreatedByUser.StaffUsers.FirstOrDefault(s => s.Userid == src.CreatedByUserId).Fullname))
                .ForMember(dest => dest.ModifiedByUserName, opt => opt.MapFrom(src => src.ModifiedByUser.StaffUsers.FirstOrDefault(s => s.Userid == src.ModifiedByUserId).Fullname))
                .ReverseMap();
            CreateMap<NotificationParentDetail, Notification>().ReverseMap();
            CreateMap<NotificationParentDetail, NotificationDTO>().ReverseMap();
            CreateMap<CreateNotificationDTO, Notification>().ReverseMap();
            CreateMap<UpdateNotificationDTO, Notification>().ReverseMap();

        }
    }
}

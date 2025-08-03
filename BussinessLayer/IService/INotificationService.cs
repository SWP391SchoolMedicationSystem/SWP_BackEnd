using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.DTO.Notifications;
using DataAccessLayer.Entity;

namespace BussinessLayer.IService
{
    public interface INotificationService
    {
        Task CreateNotification(CreateNotificationDTO dto);
        Task CreateNotificationForParent(CreateNotificationDTO dto);
        Task CreateNotificationForStaff(CreateNotificationDTO dto);
        List<Notification> GetAllNotifications();
        List<Notification> GetAllNotificationsForParent();
        List<Notification> GetAllNotificationsForStaff();
        void UpdateNotificationForParent(UpdateNotificationDTO dto, int id);
        void UpdateNotificationForStaff(UpdateNotificationDTO dto, int id);
        void DeleteNotification(int id);
        Task UpdateNotificationIsReadAsync(int notificationId);
        List<NotificationParentDetail> GetAllParentNotifcationUnread(int parentId);
        List<Notificationstaffdetail> GetAllStaffNotifcationUnread(int staffId);
        Task UpdateParentNotifciationRead(int parentID);
        Task UpdateStaffNotifciationRead(int staffID);

        //comment
    }
}

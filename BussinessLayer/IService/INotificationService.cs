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
        void CreateNotification(CreateNotificationDTO dto);
        void CreateNotificationForParent(CreateNotificationDTO dto);
        void CreateNotificationForStaff(CreateNotificationDTO dto);
        List<Notification> GetAllNotifications();
        List<Notification> GetAllNotificationsForParent();
        List<Notification> GetAllNotificationsForStaff();
        void UpdateNotificationForParent(UpdateNotificationDTO dto);
        void UpdateNotificationForStaff(UpdateNotificationDTO dto);
        void DeleteNotification(int id);
        //comment
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.DTO;
using DataAccessLayer.Entity;

namespace BussinessLayer.IService
{
    public interface INotificationService
    {
        void CreateNotification(NotificationDTO dto);
        void CreateNotificationForParent(NotificationDTO dto);
        void CreateNotificationForStaff(NotificationDTO dto);
        List<Notification> GetAllNotifications();
        void DeleteNotification(int id);

    }
}

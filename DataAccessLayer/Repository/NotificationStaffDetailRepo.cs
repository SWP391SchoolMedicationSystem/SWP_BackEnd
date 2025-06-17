using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Entity;
using DataAccessLayer.IRepository;
namespace DataAccessLayer.Repository
{
    public class NotificationStaffDetailRepo : GenericRepository<Notificationstaffdetail>, INotificationStaffDetailRepo
    {
        public NotificationStaffDetailRepo(SchoolMedicalSystemContext context) : base(context)
        {
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Entity;
using DataAccessLayer.IRepository;
using Microsoft.EntityFrameworkCore;
namespace DataAccessLayer.Repository
{
    public class NotificationRepo : GenericRepository<Notification>, INotificationRepo
    {
        private readonly SchoolMedicalSystemContext _context;
        public NotificationRepo(SchoolMedicalSystemContext context) : base(context)
        {
            _context = context;
        }
        public async Task<List<Notification>> GetAllNotificationsAsync()
        {
            return await _context.Notifications
                .Include(b => b.ModifiedByUser).ThenInclude(b => b.StaffUsers)
                .Include(b => b.CreatedByUser).ThenInclude(b => b.StaffUsers)
                .Where(n => !n.IsDeleted)
                .ToListAsync();
        }
    }
}

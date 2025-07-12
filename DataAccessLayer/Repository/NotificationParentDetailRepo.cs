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
    public class NotificationParentDetailRepo : GenericRepository<NotificationParentDetail>, INotificationParentDetailRepo
    {
        private readonly SchoolMedicalSystemContext _context;
        public NotificationParentDetailRepo(SchoolMedicalSystemContext context) : base(context)
        {
            _context = context;
        }
        public async Task<List<NotificationParentDetail>> GetAllAsync()
        {
            return await _context.NotificationParentDetails
                .Include(n => n.Notification)
                .Include(n => n.Parent)
                .Include(b => b.ModifiedByUser).ThenInclude(b => b.StaffUsers)
                .Include(b => b.CreatedByUser).ThenInclude(b => b.StaffUsers)

                .ToListAsync();
        }
    }
}

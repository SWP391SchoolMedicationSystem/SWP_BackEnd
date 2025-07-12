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
    public class NotificationStaffDetailRepo : GenericRepository<Notificationstaffdetail>, INotificationStaffDetailRepo
    {
        private readonly SchoolMedicalSystemContext _context;
        public NotificationStaffDetailRepo(SchoolMedicalSystemContext context) : base(context)
        {
            _context = context;
        }
        public async Task<List<Notificationstaffdetail>> GetAllNotificationsByStaffIdAsync(int staffId)
        {
            return await _context.Notificationstaffdetails
                .Include(b => b.ModifiedByUser).ThenInclude(b => b.StaffUsers)
                .Include(b => b.CreatedByUser).ThenInclude(b => b.StaffUsers)

                .OrderByDescending(n => n.CreatedAt)
                .ToListAsync();
        }
    }
}

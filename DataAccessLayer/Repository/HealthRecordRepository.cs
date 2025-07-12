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
    public class HealthRecordRepository : GenericRepository<HealthRecord>, IHealthRecordRepository
    {
        private DbSet<HealthRecord> _dbset;
        public HealthRecordRepository(SchoolMedicalSystemContext context) : base(context)
        {
            _dbset = context.Set<HealthRecord>();
        }
        public async Task<List<HealthRecord>> GetAllAsync()
        {
            return await _dbset
                .Include(hr => hr.Student)
                    .ThenInclude(s => s.Parent)
                .Include(hr => hr.Student)
                    .ThenInclude(s => s.Class)
                .Include(hr => hr.Staff)
                .Include(b => b.ModifiedByUser).ThenInclude(b => b.StaffUsers)
                .Include(b => b.CreatedByUser).ThenInclude(b => b.StaffUsers)

                .ToListAsync();
        }
    }
}

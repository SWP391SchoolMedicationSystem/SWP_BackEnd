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
    public class HealthCategoryRepository : GenericRepository<HealthRecordCategory>, IHealthRecordCategoryRepo
    {
        private readonly DbSet<HealthRecordCategory> _healthCategories;
        public HealthCategoryRepository(SchoolMedicalSystemContext context) : base(context)
        {
            _healthCategories = context.Set<HealthRecordCategory>();
        }
        public async Task<List<HealthRecordCategory>> GetAllAsync()
        {
            return await _healthCategories
                .Include(c => c.HealthRecords)
                .Include(b => b.ModifiedByUser).ThenInclude(b => b.StaffUsers)
                .Include(b => b.CreatedByUser).ThenInclude(b => b.StaffUsers)
                .ToListAsync();
        }
    }
}

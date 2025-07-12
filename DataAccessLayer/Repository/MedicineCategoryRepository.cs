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
    public class MedicineCategoryRepository : GenericRepository<MedicineCategory>, IMedicineCategoryRepository
    {
        private readonly DbSet<MedicineCategory> _dbSet;
        public MedicineCategoryRepository(SchoolMedicalSystemContext context) : base(context)
        {
            _dbSet = context.Set<MedicineCategory>();
        }
        public async Task<List<MedicineCategory>> GetAllAsync()
        {
            return await _dbSet
                .Include(b => b.ModifiedByUser).ThenInclude(b => b.StaffUsers)
                .Include(b => b.CreatedByUser).ThenInclude(b => b.StaffUsers)

                .ToListAsync();
        }
    }
}

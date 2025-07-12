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
    public class MedicineScheduleRepository : GenericRepository<MedicineScheduleLink>, IMedicineScheduleRepository
    {
        private readonly DbSet<MedicineScheduleLink> _dbSet;
        public MedicineScheduleRepository(SchoolMedicalSystemContext context) : base(context)
        {
            _dbSet = context.Set<MedicineScheduleLink>();
        }
        public async Task<List<MedicineScheduleLink>> GetAllAsync()
        {
            return await _dbSet
                .Include(ms => ms.PersonalMedicine)
                .Include(ms => ms.ScheduleDetail)
                .Include(b => b.ModifiedByUser).ThenInclude(b => b.StaffUsers)
                .Include(b => b.CreatedByUser).ThenInclude(b => b.StaffUsers)

                .ToListAsync();
        }
    }
}

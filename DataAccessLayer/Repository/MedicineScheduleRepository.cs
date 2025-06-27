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
    public class MedicineScheduleRepository : GenericRepository<Medicineschedule>, IMedicineScheduleRepository
    {
        private readonly DbSet<Medicineschedule> _dbSet;
        public MedicineScheduleRepository(SchoolMedicalSystemContext context) : base(context)
        {
            _dbSet = context.Set<Medicineschedule>();
        }
    }
}

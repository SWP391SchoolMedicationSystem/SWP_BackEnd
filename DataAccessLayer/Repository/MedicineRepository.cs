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
    public class MedicineRepository : GenericRepository<Medicine>, IMedicineRepository
    {
        private readonly DbSet<Medicine> _dbset;
        public MedicineRepository(SchoolMedicalSystemContext context) : base(context)
        {
            _dbset = context.Set<Medicine>();
        }
        public new Task<List<Medicine>> GetAllAsync()
        {
            return _dbset.Include(m => m.Medicinecategory)
                         .ToListAsync();
        }
    }
}

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
    public class MedicineRepository : GenericRepository<MedicineCatalog>, IMedicineRepository
    {
        private readonly DbSet<MedicineCatalog> _dbset;
        public MedicineRepository(SchoolMedicalSystemContext context) : base(context)
        {
            _dbset = context.Set<MedicineCatalog>();
        }
        public new Task<List<MedicineCatalog>> GetAllAsync()
        {
            return _dbset.Include(m => m.MedicineCategory)
                         .ToListAsync();
        }
    }
}

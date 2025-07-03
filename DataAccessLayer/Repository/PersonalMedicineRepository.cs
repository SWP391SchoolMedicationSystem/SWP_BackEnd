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
    public class PersonalMedicineRepository : GenericRepository<Personalmedicine>, IPersonalMedicineRepository
    {
        private readonly DbSet<Personalmedicine> _dbset;
        public PersonalMedicineRepository(SchoolMedicalSystemContext context) : base(context)
        {
            _dbset = context.Set<Personalmedicine>();
        }
        public Task<List<Personalmedicine>> GetAllAsync()
        {
            return _dbset.Include(p => p.Medicine)
                         .Include(p => p.Parent)
                         .Include(p => p.Student)
                         .ToListAsync();
        }
    }
}

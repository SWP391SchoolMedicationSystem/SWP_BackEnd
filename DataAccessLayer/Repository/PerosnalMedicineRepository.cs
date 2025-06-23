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
    public class PerosnalMedicineRepository : GenericRepository<Personalmedicine>, IPersonalMedicineRepository
    {
        private readonly DbSet<Personalmedicine> _personalMedicines;
        public PerosnalMedicineRepository(SchoolMedicalSystemContext context) : base(context)
        {
            _personalMedicines = context.Set<Personalmedicine>();
        }
    }
}

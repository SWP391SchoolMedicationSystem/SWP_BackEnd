using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Entity;
using DataAccessLayer.IRepository;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repository
{
    public class VaccinationEventRepository : GenericRepository<Vaccinationevent>, IVaccinationEventRepository
    {
        private readonly DbSet<Vaccinationevent> _dbSet;
        public VaccinationEventRepository(SchoolMedicalSystemContext context) : base(context)
        {
            _dbSet = context.Set<Vaccinationevent>();
        }
    }
}

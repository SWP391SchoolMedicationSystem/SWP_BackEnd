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
    public class HealthCheckEventRepository : GenericRepository<Healthcheckevent>, IHealthCheckEventRepository
    {
        private readonly DbSet<Healthcheckevent> _dbset;
        public HealthCheckEventRepository(SchoolMedicalSystemContext context) : base(context)
        {
            _dbset = context.Set<Healthcheckevent>();
        }
    }
}

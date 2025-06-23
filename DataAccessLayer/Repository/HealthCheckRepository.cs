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
    public class HealthCheckRepository : GenericRepository<Healthcheck>, IHealthCheckRepo
    {
        private readonly DbSet<Healthcheck> _healthChecks;
        public HealthCheckRepository(SchoolMedicalSystemContext context) : base(context) {
            _healthChecks = context.Set<Healthcheck>();
        }
    }
}

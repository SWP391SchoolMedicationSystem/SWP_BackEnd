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
    public class HealthStatusRepository : GenericRepository<Healthstatus>, IHealthStatusRepository
    {
        private readonly DbSet<Healthstatus> _healthStatuses;
        public HealthStatusRepository(SchoolMedicalSystemContext context) : base(context)
        {
            _healthStatuses = context.Set<Healthstatus>();
        }
    }
}

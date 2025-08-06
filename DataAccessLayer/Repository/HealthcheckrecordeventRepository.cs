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
    public class HealthcheckrecordeventRepository : GenericRepository<Healthcheckrecordevent>, IHealthcheckrecordeventRepository
    {
        private readonly DbSet<Healthcheckrecordevent> _dbset;
        public HealthcheckrecordeventRepository(SchoolMedicalSystemContext context) : base(context)
        {
            _dbset = context.Set<Healthcheckrecordevent>();
        }
        public async Task<List<Healthcheckrecordevent>> GetAllAsync()
        {
            return await _dbset
                .Include(h => h.Healthcheckevent)
                .Include(h => h.Healthcheck)
                    .ThenInclude(hr => hr.Student)
                        .ThenInclude(s => s.Class)
                // Do NOT include Student.Healthchecks or Class.Students
                .IgnoreAutoIncludes() // Ensures navigation properties not explicitly included are ignored
                .ToListAsync();
        }
    }
}

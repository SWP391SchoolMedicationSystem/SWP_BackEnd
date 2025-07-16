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
        private DbSet<Healthcheck> _dbset;
        public HealthCheckRepository(SchoolMedicalSystemContext context) : base(context)
        {
            _dbset = context.Set<Healthcheck>();
        }
        public async Task<List<Healthcheck>> GetHealthChecksByStudentIdAsync(int studentId)
        {
            return await _dbset
                .Where(h => h.Studentid == studentId)
                .OrderByDescending(h => h.Checkdate)
                .ToListAsync();
        }
        public async Task<Healthcheck?> GetHealthCheckByIdAsync(int checkId)
        {
            return await _dbset
                .Include(h => h.Student)
                    .ThenInclude(s => s.Parent)
                .FirstOrDefaultAsync(h => h.Checkid == checkId );
        }
    }
}

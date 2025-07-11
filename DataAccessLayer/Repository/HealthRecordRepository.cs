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
    public class HealthRecordRepository : GenericRepository<HealthRecord>, IHealthRecordRepository
    {
        private DbSet<HealthRecord> _dbset;
        public HealthRecordRepository(SchoolMedicalSystemContext context) : base(context)
        {
            _dbset = context.Set<HealthRecord>();
        }
    }
}

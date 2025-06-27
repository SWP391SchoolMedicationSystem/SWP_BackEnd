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
    public class ScheduleDetailRepo : GenericRepository<Scheduledetail>, IScheduleDetailRepo
    {
        private readonly DbSet<Scheduledetail> _dbSet;
        public ScheduleDetailRepo(SchoolMedicalSystemContext context) : base(context)
        {
            _dbSet = context.Set<Scheduledetail>();
        }
    }
}

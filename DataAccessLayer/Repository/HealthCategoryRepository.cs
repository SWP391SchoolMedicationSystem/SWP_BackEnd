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
    public class HealthCategoryRepository : GenericRepository<HealthRecordCategory>, IHealthRecordCategoryRepo
    {
        private readonly DbSet<HealthRecordCategory> _healthCategories;
        public HealthCategoryRepository(SchoolMedicalSystemContext context) : base(context)
        {
            _healthCategories = context.Set<HealthRecordCategory>();
        }
    }
}

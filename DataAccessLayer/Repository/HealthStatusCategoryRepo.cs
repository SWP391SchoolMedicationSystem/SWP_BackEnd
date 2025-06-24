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
    public class HealthStatusCategoryRepo : GenericRepository<Healthstatuscategory>, IHealthStatusCategoryRepository
    {
        private readonly DbSet<Healthstatuscategory> _dbSet;
        public HealthStatusCategoryRepo(SchoolMedicalSystemContext context) : base(context)
        {
            _dbSet = context.Set<Healthstatuscategory>();
        }
    }
}

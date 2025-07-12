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
    public class BlogRepo : GenericRepository<Blog>, IBlogRepo
    {
        private readonly DbSet<Blog> _dbSet;
        public BlogRepo(SchoolMedicalSystemContext context) : base(context)
        {
            _dbSet = context.Set<Blog>();
        }
        public async Task<List<Blog>> GetAllAsync()
        {
            return await _dbSet
                .Include(b => b.ModifiedByUser).ThenInclude(b => b.StaffUsers)
                .Include(b => b.CreatedByUser).ThenInclude(b => b.StaffUsers)
                .Include(b => b.ApprovedByNavigation)
                .ToListAsync();
        }

    }
}

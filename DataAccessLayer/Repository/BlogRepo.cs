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
    }
}

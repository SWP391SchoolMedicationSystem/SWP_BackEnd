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
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly DbSet<User> _dbset;
        public UserRepository(SchoolMedicalSystemContext context) : base(context)
        {
            _dbset = context.Set<User>();
        }
        public Task<List<User>> GetAllAsync()
        {
            return _dbset
                .Include(x => x.Parents)
                .Include(x => x.Staff)
                .ToListAsync();
        }
    }
}

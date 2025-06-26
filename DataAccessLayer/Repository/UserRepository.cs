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
    public class UserRepository(SchoolMedicalSystemContext context) : GenericRepository<User>(context), IUserRepository
    {
        private readonly DbSet<User> _dbset = context.Set<User>();

        public new Task<List<User>> GetAllAsync()
        {
            return _dbset
                .Include(x => x.Parents)
                .Include(x => x.Staff)
                .ToListAsync();
        }
    }
}

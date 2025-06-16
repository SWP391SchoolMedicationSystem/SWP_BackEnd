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
    public class RoleRepository : GenericRepository<Role>, IRoleRepository
    {
        private DbSet<Role> _dbset;
        public RoleRepository(SchoolMedicalSystemContext context) : base(context)
        {
            _dbset = context.Set<Role>
        }
    }
}

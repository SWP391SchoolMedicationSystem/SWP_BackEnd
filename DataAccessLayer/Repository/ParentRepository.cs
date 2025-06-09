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
    public class ParentRepository : GenericRepository<Parent>, IParentRepository
    {
        private DbSet<Parent> _dbset;
        public ParentRepository(SchoolMedicalSystemContext context) : base(context)
        {
            _dbset = context.Set<Parent>();
        }
    }
}

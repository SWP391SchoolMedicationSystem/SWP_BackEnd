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
    public class StaffRepository : GenericRepository<Staff>, IStaffRepository
    {
        private readonly DbSet<Staff> _dbset;

        public StaffRepository(SchoolMedicalSystemContext context) : base(context)
        {
            _dbset = context.Set<Staff>();
        }
    }
}

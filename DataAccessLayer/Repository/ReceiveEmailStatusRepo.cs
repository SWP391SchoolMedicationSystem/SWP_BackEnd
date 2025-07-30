using DataAccessLayer.Entity;
using DataAccessLayer.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repository
{
    public class ReceiveEmailStatusRepo : GenericRepository<Receiveemailstatus>, IReceiveEmailStatusRepo
    {
        private readonly DbSet<Receiveemailstatus> _dbset;
        public ReceiveEmailStatusRepo(SchoolMedicalSystemContext context) : base(context)
        {
            _dbset = context.Set<Receiveemailstatus>();
        }
    }
}

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
    public class ConsultationRepository : GenericRepository<Consultationrequest>, IConsulationRepository
    {
        private readonly DbSet<Consultationrequest> _dbSet;
        public ConsultationRepository(SchoolMedicalSystemContext context) : base(context)
        {
            _dbSet = context.Set<Consultationrequest>();
        }
    }
}

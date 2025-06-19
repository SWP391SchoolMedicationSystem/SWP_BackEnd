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
    public class ConsultationTypeRepo : GenericRepository<Consultationtype>, IConsultationTypeRepo
    {
        private readonly DbSet<Consultationtype> _dbset
            ;
        public ConsultationTypeRepo(SchoolMedicalSystemContext context) : base(context)
        {
            _dbset = context.Set<Consultationtype>();
        }
        // You can add any additional methods specific to ConsultationType here if needed.
    }
}

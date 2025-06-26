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
    public class MedicineDonationRepository : GenericRepository<Medicinedonation>, IMedicineDonationRepository
    {
        private readonly DbSet<Medicinedonation> _dbset;
        public MedicineDonationRepository(SchoolMedicalSystemContext context) : base(context)
        {
            _dbset = context.Set<Medicinedonation>();
        }
    }
}

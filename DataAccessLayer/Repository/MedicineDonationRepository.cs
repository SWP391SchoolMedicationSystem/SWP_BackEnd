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
    public class MedicineDonationRepository(SchoolMedicalSystemContext context) : GenericRepository<Medicinedonation>(context), IMedicineDonationRepository
    {
        private readonly DbSet<Medicinedonation> _medicinedonations = context.Set<Medicinedonation>();
    }
}

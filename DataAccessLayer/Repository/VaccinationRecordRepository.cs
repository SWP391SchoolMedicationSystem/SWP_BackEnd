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
    public class VaccinationRecordRepository : GenericRepository<Vaccinationrecord>, IVaccinationRecordRepo
    {
        private readonly DbSet<Vaccinationrecord> _vaccinationRecords;
        public VaccinationRecordRepository(SchoolMedicalSystemContext context) : base(context)
        {
            _vaccinationRecords = context.Set<Vaccinationrecord>();
        }
    }
}

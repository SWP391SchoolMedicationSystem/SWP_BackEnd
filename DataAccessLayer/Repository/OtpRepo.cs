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
    public class OtpRepo : GenericRepository<Otp>, IOtpRepo
    {
        private readonly DbSet<Otp> _dbset;
        public OtpRepo(SchoolMedicalSystemContext context) : base(context)
        {
            _dbset = context.Set<Otp>();
        }

    }
}

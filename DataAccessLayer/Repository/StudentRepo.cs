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
    public class StudentRepo : GenericRepository<Student>, IStudentRepo
    {
        private readonly DbSet<Student> _dbset;
        public StudentRepo(SchoolMedicalSystemContext context) : base(context)
        {
            _dbset = context.Set<Student>();
        }
    }
}

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

        public async Task<Parent> GetParentForEvent(string email)
        {
            var parent = await _dbset.Include(p => p.Students).FirstOrDefaultAsync(p => p.Email == email && !p.IsDeleted);

            if (parent == null)
            {
                return null;
            }
            return parent;
        }
        public async Task<Parent> GetByStudentIdAsync(int studentId)
        {
            var parent = await _dbset.Include(p => p.Students)
                                     .FirstOrDefaultAsync(p => p.Students.Any(s => s.Studentid == studentId) && !p.IsDeleted);
            return parent ?? throw new KeyNotFoundException($"Parent for student with id {studentId} not found.");
        }
    }
}

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
    public class StudentSpecialNeedRepo : GenericRepository<StudentSpecialNeed>, IStudentSpecialNeedRepo
    {
        private readonly SchoolMedicalSystemContext _context;
        public StudentSpecialNeedRepo(SchoolMedicalSystemContext context) : base(context)
        {
            _context = context;
        }
        public async Task<StudentSpecialNeed?> GetWithNavigationByIdAsync(int id)
        {
            return await _context.StudentSpecialNeeds
                .Include(b => b.ModifiedByUser).ThenInclude(b => b.StaffUsers)
                .Include(b => b.CreatedByUser).ThenInclude(b => b.StaffUsers)
                .Include(x => x.Student)
                .Include(x => x.SpecialNeedCategory)
                .FirstOrDefaultAsync(x => x.StudentSpecialNeedId == id);
        }
    }
}

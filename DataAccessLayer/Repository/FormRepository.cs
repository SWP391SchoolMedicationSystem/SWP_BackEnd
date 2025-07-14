using DataAccessLayer.Entity;
using DataAccessLayer.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccessLayer.Repository
{
    public class FormRepository : GenericRepository<Form>, IFormRepository
    {
        private readonly DbSet<Form> _dbset;

        public FormRepository(SchoolMedicalSystemContext context) : base(context)
        {
            _dbset = context.Set<Form>();
        }

        public async Task<List<Form>> GetFormsByParentIdAsync(int parentId)
        {
            return await _dbset
                .Include(f => f.Parent)
                .Where(f => f.Parentid == parentId)
                .ToListAsync();
        }

        public async Task<List<Form>> GetFormsByCategoryIdAsync(int categoryId)
        {
            return await _dbset
                .Include(f => f.Parent)
                .Where(f => f.FormcategoryId == categoryId)
                .ToListAsync();
        }

        public async Task<Form?> GetFormWithParentAsync(int formId)
        {
            return await _dbset
                .Include(f => f.Parent)
                .FirstOrDefaultAsync(f => f.FormId == formId);
        }
    }
}

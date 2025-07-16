using DataAccessLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.IRepository
{
    public interface IFormRepository : IGenericRepository<Form>
    {
        Task<List<Form>> GetFormsByParentIdAsync(int parentId);
        Task<List<Form>> GetFormsByCategoryIdAsync(int categoryId);
    }
}

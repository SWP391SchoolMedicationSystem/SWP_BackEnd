using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.DTO.HealthStatus;
using DataAccessLayer.Entity;

namespace BussinessLayer.IService
{
    public interface IHealthStatusCategoryService
    {
        Task<List<Healthstatuscategory>> GetHealthstatuscategories();
        Task<Healthstatuscategory> GetHealthstatuscategoryByID(int id);
        Task AddHealthStatusCategory(HealthStatusCategoryDTO healthstatuscategory);
        void UpdateHealthstatuscategory(HealthStatusCategoryDTO healthstatuscategory);
        void DeleteHealthstatuscategory(int id);
    }
}

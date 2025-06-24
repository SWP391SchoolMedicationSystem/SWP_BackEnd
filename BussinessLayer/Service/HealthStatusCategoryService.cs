using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BussinessLayer.IService;
using DataAccessLayer.DTO;
using DataAccessLayer.Entity;
using DataAccessLayer.IRepository;

namespace BussinessLayer.Service
{
    public class HealthStatusCategoryService(IHealthStatusCategoryRepository healthStatusCategoryRepository, IMapper mapper) : IHealthStatusCategoryService
    {
        public async Task AddHealthStatusCategory(HealthStatusCategoryDTO healthstatuscategorydto)
        {
            var healthstatus = mapper.Map<Healthstatuscategory>(healthstatuscategorydto);
            healthstatus.CreatedAt = DateTime.Now;
            await healthStatusCategoryRepository.AddAsync(healthstatus);
            healthStatusCategoryRepository.Save();
        }

        public async void DeleteHealthstatuscategory(int id)
        {
           var status = await GetHealthstatuscategoryByID(id);
            if (status != null)
            {
                status.IsDeleted = true;
                healthStatusCategoryRepository.Save();
            }
            else throw new Exception("HealthStatusCategory not found");
        }

        public Task<List<Healthstatuscategory>> GetHealthstatuscategories()
        {
            return healthStatusCategoryRepository.GetAllAsync();
        }

        public async Task<Healthstatuscategory> GetHealthstatuscategoryByID(int id)
        {
            return await healthStatusCategoryRepository.GetByIdAsync(id);
        }

        public void UpdateHealthstatuscategory(HealthStatusCategoryDTO dto)
        {
            var status = mapper.Map<Healthstatuscategory>(dto);
            status.UpdatedAt = DateTime.Now;
            healthStatusCategoryRepository.Update(status);
            healthStatusCategoryRepository.Save();
        }
    }
}

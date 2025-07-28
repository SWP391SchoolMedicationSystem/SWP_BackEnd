using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BussinessLayer.IService;
using DataAccessLayer.IRepository;

namespace BussinessLayer.Service
{
    public class HealthCheckEventService(IHealthCheckEventRepository healthCheckEventRepository) : IHealthCheckEventService
    {
        public async Task<List<DataAccessLayer.Entity.Healthcheckevent>> GetAllHealthCheckEventsAsync()
        {
            return await healthCheckEventRepository.GetAllAsync();
        }
        public async Task<DataAccessLayer.Entity.Healthcheckevent?> GetHealthCheckEventByIdAsync(int eventId)
        {
            return await healthCheckEventRepository.GetByIdAsync(eventId);
        }
        public async Task AddHealthCheckEventAsync(DataAccessLayer.Entity.Healthcheckevent healthCheckEvent)
        {
            await healthCheckEventRepository.AddAsync(healthCheckEvent);
        }
        public async Task UpdateHealthCheckEventAsync(DataAccessLayer.Entity.Healthcheckevent healthCheckEvent)
        {
            healthCheckEventRepository.Update(healthCheckEvent);
            await healthCheckEventRepository.SaveChangesAsync();
        }
        public async Task DeleteHealthCheckEventAsync(int eventId)
        {
            healthCheckEventRepository.Delete(eventId);
            await healthCheckEventRepository.SaveChangesAsync();
        }
    }
}

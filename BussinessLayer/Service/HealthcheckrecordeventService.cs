using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BussinessLayer.IService;
using DataAccessLayer.Entity;
using DataAccessLayer.IRepository;

namespace BussinessLayer.Service
{
    public class HealthcheckrecordeventService(IHealthCheckEventRepository healthCheckEventRepository) : IHealthCheckEventRecordService
    {
        public async Task<List<Healthcheckrecordevent>> GetAllHealthCheckRecordEventsAsync()
        {
            return await healthCheckEventRepository.GetAllAsync();
        }
        public async Task<Healthcheckrecordevent?> GetHealthCheckRecordEventByIdAsync(int eventId)
        {
            return await healthCheckEventRepository.GetByIdAsync(eventId);
        }
        public async Task AddHealthCheckRecordEventAsync(Healthcheckrecordevent healthCheckRecordEvent)
        {
            await healthCheckEventRepository.AddAsync(healthCheckRecordEvent);
        }
        public async Task UpdateHealthCheckRecordEventAsync(Healthcheckrecordevent healthCheckRecordEvent)
        {
            healthCheckEventRepository.Update(healthCheckRecordEvent);
            await healthCheckEventRepository.SaveChangesAsync();
        }
        public async Task DeleteHealthCheckRecordEventAsync(int eventId)
        {
            healthCheckEventRepository.Delete(eventId);
            await healthCheckEventRepository.SaveChangesAsync();
        }
    }
}

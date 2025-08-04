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
    public class HealthcheckrecordeventService(IHealthcheckrecordeventRepository healthCheckEventRepository, IMapper mapper) : IHealthCheckEventRecordService
    {
        public async Task<List<Healthcheckrecordevent>> GetAllHealthCheckRecordEventsAsync()
        {
            var list = await healthCheckEventRepository.GetAllAsync();
                return list.OrderBy(x => x.Healthcheckevent.Eventdate).Reverse().ToList();
        }
        public async Task<Healthcheckrecordevent?> GetHealthCheckRecordEventByIdAsync(int eventId)
        {
            return await healthCheckEventRepository.GetByIdAsync(eventId);
        }
        public async Task AddHealthCheckRecordEventAsync(AddHealthcheckrecordeventDTO healthCheckRecordEvent)
        {
            try
            {
                var record = mapper.Map<Healthcheckrecordevent>(healthCheckRecordEvent);
                await healthCheckEventRepository.AddAsync(record);
                await healthCheckEventRepository.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw new Exception($"Error adding Health Check and Event Together {e.Message}");
            }

        }
        public async Task UpdateHealthCheckRecordEventAsync(Healthcheckrecordevent healthCheckRecordEvent)
        {
            if (await healthCheckEventRepository.GetByIdAsync(healthCheckRecordEvent.Healthcheckrecordid) != null)
            {
                healthCheckEventRepository.Update(healthCheckRecordEvent);
                await healthCheckEventRepository.SaveChangesAsync();
            }
            else
            {
                throw new KeyNotFoundException("Healthcheckrecordevent not found.");
            }
        }
        public async Task DeleteHealthCheckRecordEventAsync(int eventId)
        {
            var healthcheck = await healthCheckEventRepository.GetByIdAsync(eventId);
            healthcheck.Isdeleted = true; // Soft delete
            healthCheckEventRepository.Update(healthcheck);
            await healthCheckEventRepository.SaveChangesAsync();
        }

        public async Task<List<Healthcheckrecordevent>> GetHealthCheckRecordEventsByStudentIdAsync(int studentId)
        {
            var list = await healthCheckEventRepository.GetAllAsync();
            return list.Where(x => x.Healthcheckrecord.Studentid == studentId).OrderBy(x => x.Healthcheckevent.Eventdate).Reverse().ToList();
        }

        public async Task<List<Healthcheckrecordevent>> GetHealthCheckRecordEventsByEventIdAsync(int eventId)
        {
            var list = await healthCheckEventRepository.GetAllAsync();
            return list.Where(x => x.Healthcheckevent.HealthcheckeventID == eventId).OrderBy(x => x.Healthcheckevent.Eventdate).Reverse().ToList();
        }
       
    }
}

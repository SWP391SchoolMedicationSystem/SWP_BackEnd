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
            return await healthCheckEventRepository.GetAllAsync();
        }
        public async Task<Healthcheckrecordevent?> GetHealthCheckRecordEventByIdAsync(int eventId)
        {
            return await healthCheckEventRepository.GetByIdAsync(eventId);
        }
        public async Task AddHealthCheckRecordEventAsync(AddHealthcheckrecordeventDTO healthCheckRecordEvent)
        {
            var record = mapper.Map<AddHealthcheckrecordeventDTO, Healthcheckrecordevent>(healthCheckRecordEvent);
            await healthCheckEventRepository.AddAsync(record);
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
            healthCheckEventRepository.Delete(eventId);
            await healthCheckEventRepository.SaveChangesAsync();
        }

        public async Task<List<Healthcheckrecordevent>> GetHealthCheckRecordEventsByStudentIdAsync(int studentId)
        {
            var list = await healthCheckEventRepository.GetAllAsync();
            return list.Where(x => x.Healthcheckrecord.Studentid == studentId).OrderBy(x => x.Healthcheckevent.Eventdate).ToList();
        }

        public Task<List<Healthcheckrecordevent>> GetHealthCheckRecordEventsByEventIdAsync(int eventId)
        {
            throw new NotImplementedException();
        }
    }
}

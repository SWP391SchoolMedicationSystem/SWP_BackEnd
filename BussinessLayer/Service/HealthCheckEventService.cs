using AutoMapper;
using Azure.Core;
using BussinessLayer.IService;
using DataAccessLayer.DTO.HealthCheck;
using DataAccessLayer.Entity;
using DataAccessLayer.IRepository;
using Microsoft.IdentityModel.Tokens;
using NPOI.SS.Formula.Functions;

namespace BussinessLayer.Service
{
    public class HealthCheckEventService(IHealthCheckEventRepository healthCheckEventRepository
        , IMapper mapper

        ) : IHealthCheckEventService
    {
        public async Task<List<DataAccessLayer.Entity.Healthcheckevent>> GetAllHealthCheckEventsAsync()
        {
            return await healthCheckEventRepository.GetAllAsync();
        }
        public async Task<DataAccessLayer.Entity.Healthcheckevent?> GetHealthCheckEventByIdAsync(int eventId)
        {
            return await healthCheckEventRepository.GetByIdAsync(eventId);
        }
        public async Task AddHealthCheckEventAsync(AddHealthCheckEventDto dto, string? storedFileName)
        {
            string? accessToken = null;

            if (!storedFileName.IsNullOrEmpty())
            {
                accessToken = Guid.NewGuid().ToString();
            }

            var healthCheckEvent = mapper.Map<Healthcheckevent>(dto);
            healthCheckEvent.Documentfilename = storedFileName;
            healthCheckEvent.Documentaccesstoken = accessToken;
            healthCheckEvent.Createddate = DateTime.Now;
            await healthCheckEventRepository.AddAsync(healthCheckEvent);
            await healthCheckEventRepository.SaveChangesAsync();
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

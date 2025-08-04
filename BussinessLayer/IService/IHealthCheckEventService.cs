using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.DTO.HealthCheck;
using DataAccessLayer.Entity;

namespace BussinessLayer.IService
{
    public interface IHealthCheckEventService
    {
        Task<List<HeatlhCheckEventDto>> GetAllHealthCheckEventsAsync();
        Task<HeatlhCheckEventDto?> GetHealthCheckEventByIdAsync(int eventId);
        Task AddHealthCheckEventAsync(AddHealthCheckEventDto healthCheckEvent, string? storedFileName);
        Task UpdateHealthCheckEventAsync(Healthcheckevent healthCheckEvent);
        Task DeleteHealthCheckEventAsync(int eventId);

    }
}

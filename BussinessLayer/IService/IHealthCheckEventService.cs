using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Entity;

namespace BussinessLayer.IService
{
    public interface IHealthCheckEventService
    {
        Task<List<Healthcheckevent>> GetAllHealthCheckEventsAsync();
        Task<Healthcheckevent?> GetHealthCheckEventByIdAsync(int eventId);
        Task AddHealthCheckEventAsync(Healthcheckevent healthCheckEvent);
        Task UpdateHealthCheckEventAsync(Healthcheckevent healthCheckEvent);
        Task DeleteHealthCheckEventAsync(int eventId);

    }
}

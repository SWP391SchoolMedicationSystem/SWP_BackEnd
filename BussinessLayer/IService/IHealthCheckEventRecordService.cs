using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Entity;

namespace BussinessLayer.IService
{
    public interface IHealthCheckEventRecordService 
    {
        Task<List<Healthcheckrecordevent>> GetAllHealthCheckRecordEventsAsync();
        Task<Healthcheckrecordevent?> GetHealthCheckRecordEventByIdAsync(int eventId);
        Task AddHealthCheckRecordEventAsync(Healthcheckrecordevent healthCheckRecordEvent);
        Task UpdateHealthCheckRecordEventAsync(Healthcheckrecordevent healthCheckRecordEvent);
        Task DeleteHealthCheckRecordEventAsync(int eventId);
    }
}

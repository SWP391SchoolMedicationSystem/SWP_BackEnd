using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.DTO;
using DataAccessLayer.DTO.HealthCheck;
using DataAccessLayer.Entity;

namespace BussinessLayer.IService
{
    public interface IHealthCheckEventRecordService 
    {
        Task<List<HealthCheckDtoIgnoreClass>> GetAllHealthCheckRecordEventsAsync();
        Task<Healthcheckrecordevent?> GetHealthCheckRecordEventByIdAsync(int eventId);
        Task AddHealthCheckRecordEventAsync(AddHealthcheckrecordeventDTO healthCheckRecordEvent);
        Task UpdateHealthCheckRecordEventAsync(Healthcheckrecordevent healthCheckRecordEvent);
        Task DeleteHealthCheckRecordEventAsync(int eventId);
        Task<List<HealthCheckDtoIgnoreClass>> GetHealthCheckRecordEventsByStudentIdAsync(int studentId);
        Task<List<Healthcheckrecordevent>> GetHealthCheckRecordEventsByEventIdAsync(int eventId);
    }
}

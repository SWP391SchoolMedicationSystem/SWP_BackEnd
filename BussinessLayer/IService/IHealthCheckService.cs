using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.DTO;
using DataAccessLayer.Entity;

namespace BussinessLayer.IService
{
    public interface IHealthCheckService
    {
        Task<Healthcheck> AddHealthCheckAsync(HealthCheckDTO healthCheckDto);
        Task<Healthcheck> UpdateHealthCheckAsync(HealthCheckDTO healthCheckDto);
        Task <bool> DeleteHealthCheckAsync(int checkId);
        Task<List<HealthCheckDTO>> GetAllHealthChecksAsync();
    }
}

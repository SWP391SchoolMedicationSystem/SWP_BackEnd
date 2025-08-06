using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.DTO.HealthCheck;
using DataAccessLayer.Entity;

namespace BussinessLayer.IService
{
    public interface IHealthCheckService
    {
        Task AddHealthCheckAsync(AddHealthCheckDto healthCheckDto);
        Task<Healthcheck> UpdateHealthCheckAsync(HealthCheckDTO healthCheckDto);
        Task <bool> DeleteHealthCheckAsync(int checkId);
        List<HealthCheckDTO> GetAllHealthChecksAsync();
        Task<Healthcheck> GetHealthCheckByIdAsync(int checkId);
        Task<List<HealthCheckDTO>> GetHealthChecksByStudentIdAsync(int studentId);

    }
}

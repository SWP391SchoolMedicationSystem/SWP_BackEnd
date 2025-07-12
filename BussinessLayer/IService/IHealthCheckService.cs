using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.DTO.HealthChecks;
using DataAccessLayer.Entity;

namespace BussinessLayer.IService
{
    public interface IHealthCheckService
    {
        Task<Healthcheck> AddHealthCheckAsync(AddHealthCheckDTO healthCheckDto);
        Task<Healthcheck> UpdateHealthCheckAsync(UpdateHealthCheckDTO healthCheckDto, int id);
        Task <bool> DeleteHealthCheckAsync(int checkId);
        Task<List<HealthCheckDTO>> GetAllHealthChecksAsync();
        Task<Healthcheck> GetHealthCheckByIdAsync(int checkId);
        Task<List<HealthCheckDTO>> GetHealthChecksByStudentIdAsync(int studentId);

    }
}

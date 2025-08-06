using AutoMapper;
using BussinessLayer.IService;
using DataAccessLayer.DTO.HealthCheck;
using Microsoft.AspNetCore.Mvc;

namespace SchoolMedicalSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HealthCheckController : ControllerBase
    {
        private readonly IHealthCheckService _healthCheckService;
        private readonly IMapper _mapper;
        public HealthCheckController(IHealthCheckService healthCheckService, IMapper mapper)
        {
            _healthCheckService = healthCheckService;
            _mapper = mapper;
        }
        [HttpGet]
        public Task<List<HealthCheckDTO>> GetAllHealthChecks()
        {
            var result = _healthCheckService.GetAllHealthChecksAsync();
            if (result == null || !result.Any())
                return Task.FromResult<List<HealthCheckDTO>>(null);
            return Task.FromResult(result);
        }


        [HttpPost]
        public async Task<IActionResult> AddHealthCheck([FromBody] AddHealthCheckDto healthCheckDto)
        {
            
                if (healthCheckDto == null)
                    return BadRequest("Health check data is null.");
                if (healthCheckDto.Checkdate < DateTime.Now)
                    return BadRequest("Date cannot be in the past.");

                await _healthCheckService.AddHealthCheckAsync(healthCheckDto);
                return Ok("Health check added successfully.");
            

        }
        [HttpPut]
        public async Task<IActionResult> UpdateHealthCheck([FromBody] HealthCheckDTO healthCheckDto)
        {
            if (healthCheckDto == null)
                return BadRequest("Health check data is null.");
            var result = await _healthCheckService.UpdateHealthCheckAsync(healthCheckDto);
            if (result == null)
                return NotFound("Health check not found.");
            return Ok(result);
        }
        [HttpDelete("{checkId}")]
        public async Task<IActionResult> DeleteHealthCheck(int checkId)
        {
            var result = await _healthCheckService.DeleteHealthCheckAsync(checkId);
            if (!result)
                return NotFound("Health check not found.");
            return Ok("Health check deleted successfully.");
        }

    }
}

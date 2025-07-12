using AutoMapper;
using BussinessLayer.IService;
using DataAccessLayer.DTO.HealthChecks;
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
            return result;
        }


        [HttpPost]
        public async Task<IActionResult> AddHealthCheck([FromBody] AddHealthCheckDTO healthCheckDto)
        {
            try
            {
                if (healthCheckDto == null)
                    return BadRequest("Health check data is null.");
                var result = await _healthCheckService.AddHealthCheckAsync(healthCheckDto);
                if (result == null)
                    return NotFound("Staff or Student not found.");

                return Ok(result);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        [HttpPut]
        [Route("update{id}")]
        public async Task<IActionResult> UpdateHealthCheck([FromBody] UpdateHealthCheckDTO healthCheckDto, int id)
        {
            if (healthCheckDto == null)
                return BadRequest("Health check data is null.");
            var result = await _healthCheckService.UpdateHealthCheckAsync(healthCheckDto, id);
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

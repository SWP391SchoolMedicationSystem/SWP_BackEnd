using BussinessLayer.IService;
using BussinessLayer.Utils;
using DataAccessLayer.DTO.HealthCheck;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SchoolMedicalSystem.Controllers
{
    [Route("api/")]
    [ApiController]
    public class HealthCheckEventController(IHealthCheckEventService healthCheckEventService, FileHandler fileHandler) : ControllerBase
    {
        [HttpGet]
        [Route("healthcheckevent")]
        public async Task<IActionResult> GetAllHealthCheckEvents()
        {
            var events = await healthCheckEventService.GetAllHealthCheckEventsAsync();
            return Ok(events);
        }
        [HttpGet]
        [Route("healthcheckevent/{eventId}")]
        public async Task<IActionResult> GetHealthCheckEventById(int eventId)
        {
            var healthCheckEvent = await healthCheckEventService.GetHealthCheckEventByIdAsync(eventId);
            if (healthCheckEvent == null)
            {
                return NotFound();
            }
            return Ok(healthCheckEvent);
        }
        [HttpPost]
        [Route("healthcheckevent")]
        public async Task<IActionResult> AddHealthCheckEvent([FromForm] AddHealthCheckEventDto healthCheckEvent)
        {
            string? storedFileName = null;

            if (healthCheckEvent == null)
            {
                return BadRequest("Health check event cannot be null.");
            }
            if (healthCheckEvent.DocumentFile != null)
            {
                var uploadResult = await fileHandler.UploadAsync(healthCheckEvent.DocumentFile);
                if (!uploadResult.Success)
                {
                    return BadRequest(new { message = uploadResult.ErrorMessage });
                }
                storedFileName = uploadResult.StoredFileName;
            }


            await healthCheckEventService.AddHealthCheckEventAsync(healthCheckEvent, storedFileName);
            return Ok(healthCheckEvent);
        }
        [HttpPut]
        [Route("healthcheckevent/{eventId}")]
        public async Task<IActionResult> UpdateHealthCheckEvent(int eventId, [FromBody] UpdateHeatlhCheckEventDto healthCheckEvent)
        {
            if (healthCheckEvent == null || healthCheckEvent.HealthcheckeventID != eventId)
            {
                return BadRequest("Health check event data is invalid.");
            }
            var existingEvent = await healthCheckEventService.GetHealthCheckEventByIdAsync(eventId);
            if (existingEvent == null)
            {
                return NotFound();
            }
            await healthCheckEventService.UpdateHealthCheckEventAsync(healthCheckEvent);
            return NoContent();
        }
        [HttpDelete]
        [Route("healthcheckevent/{eventId}")]
        public async Task<IActionResult> DeleteHealthCheckEvent(int eventId)
        {
            var existingEvent = await healthCheckEventService.GetHealthCheckEventByIdAsync(eventId);
            if (existingEvent == null)
            {
                return NotFound();
            }
            await healthCheckEventService.DeleteHealthCheckEventAsync(eventId);
            return NoContent();
        }
    }
}

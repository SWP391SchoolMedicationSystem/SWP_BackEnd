using BussinessLayer.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SchoolMedicalSystem.Controllers
{
    [Route("api/")]
    [ApiController]
    public class HealthCheckEventController(IHealthCheckEventService healthCheckEventService) : ControllerBase
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
        public async Task<IActionResult> AddHealthCheckEvent([FromBody] DataAccessLayer.Entity.Healthcheckevent healthCheckEvent)
        {
            if (healthCheckEvent == null)
            {
                return BadRequest("Health check event cannot be null.");
            }
            await healthCheckEventService.AddHealthCheckEventAsync(healthCheckEvent);
            return CreatedAtAction(nameof(GetHealthCheckEventById), new { eventId = healthCheckEvent.HealthcheckeventID }, healthCheckEvent);
        }
        [HttpPut]
        [Route("healthcheckevent/{eventId}")]
        public async Task<IActionResult> UpdateHealthCheckEvent(int eventId, [FromBody] DataAccessLayer.Entity.Healthcheckevent healthCheckEvent)
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

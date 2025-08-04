using BussinessLayer.IService;
using DataAccessLayer.DTO;
using DataAccessLayer.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SchoolMedicalSystem.Controllers
{
    [Route("api")]
    [ApiController]
    public class HealthcheckrecordeventController(IHealthCheckEventRecordService healthCheckEventRecordService) : ControllerBase
    {
        [HttpGet]
        [Route("healthcheckrecordevents")]
        public async Task<IActionResult> GetAllHealthCheckRecordEvents()
        {
            var events = await healthCheckEventRecordService.GetAllHealthCheckRecordEventsAsync();
            return Ok(events);
        }
        [HttpGet]
        [Route("healthcheckrecordevents/{id}")]
        public async Task<IActionResult> GetHealthCheckRecordEventById(int id)
        {
            var healthCheckRecordEvent = await healthCheckEventRecordService.GetHealthCheckRecordEventByIdAsync(id);
            if (healthCheckRecordEvent == null)
            {
                return NotFound();
            }
            return Ok(healthCheckRecordEvent);
        }
        [HttpGet]
        [Route("healthcheckrecordevents/student/{studentId}")]
        public async Task<IActionResult> GetHealthCheckRecordEventsByStudentId(int studentId)
        {
            var events = await healthCheckEventRecordService.GetHealthCheckRecordEventsByStudentIdAsync(studentId);
            if (events == null || !events.Any())
            {
                return NotFound();
            }
            return Ok(events);
        }
        [HttpGet]
        [Route("healthcheckrecordevents/event/{eventId}")]
        public async Task<IActionResult> GetHealthCheckRecordEventsByEventId(int eventId)
        {
            var events = await healthCheckEventRecordService.GetHealthCheckRecordEventsByEventIdAsync(eventId);
            if (events == null || !events.Any())
            {
                return NotFound();
            }
            return Ok(events);
        }
        [HttpPost]
        [Route("healthcheckrecordevents")]
        public async Task<IActionResult> AddHealthCheckRecordEvent([FromBody] AddHealthcheckrecordeventDTO healthCheckRecordEvent)
        {
            if (healthCheckRecordEvent == null)
            {
                return BadRequest("Invalid health check record event data.");
            }
            await healthCheckEventRecordService.AddHealthCheckRecordEventAsync(healthCheckRecordEvent);
            return Ok("HealthCheck and Event connected.");
        }
        [HttpPut]
        [Route("healthcheckrecordevents/{eventId}")]
        public async Task<IActionResult> UpdateHealthCheckRecordEvent(int eventId, [FromBody] Healthcheckrecordevent healthCheckRecordEvent)
        {
            if (healthCheckRecordEvent == null || healthCheckRecordEvent.Healthcheckrecordid != eventId)
            {
                return BadRequest("Invalid health check record event data.");
            }
            try
            {
                await healthCheckEventRecordService.UpdateHealthCheckRecordEventAsync(healthCheckRecordEvent);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }
        [HttpDelete]
        [Route("healthcheckrecordevents/{eventId}")]
        public async Task<IActionResult> DeleteHealthCheckRecordEvent(int eventId)
        {
            try
            {
                await healthCheckEventRecordService.DeleteHealthCheckRecordEventAsync(eventId);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }
    }
}

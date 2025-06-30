using AutoMapper;
using BussinessLayer.IService;
using DataAccessLayer.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace SchoolMedicalSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VaccinationEventController : ControllerBase
    {
        private readonly IVaccinationEventService _vaccinationEventService;
        private readonly IEmailService _emailService;
        private readonly IMapper _mapper;

        public VaccinationEventController(IVaccinationEventService vaccinationEventService, IMapper mapper, IEmailService emailService)
        {
            _vaccinationEventService = vaccinationEventService;
            _emailService = emailService;
            _mapper = mapper;
        }

        // GET: api/VaccinationEvent
        [HttpGet]
        public async Task<ActionResult<List<VaccinationEventDTO>>> GetAllEvents()
        {
            try
            {
                var events = await _vaccinationEventService.GetAllEventsAsync();
                return Ok(events);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/VaccinationEvent/5
        [HttpGet("{id}")]
        public async Task<ActionResult<VaccinationEventDTO>> GetEvent(int id)
        {
            try
            {
                var vaccinationEvent = await _vaccinationEventService.GetEventByIdAsync(id);
                if (vaccinationEvent == null)
                    return NotFound("Vaccination event not found.");

                return Ok(vaccinationEvent);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // POST: api/VaccinationEvent
        [HttpPost]
        public async Task<ActionResult<VaccinationEventDTO>> CreateEvent([FromBody] CreateVaccinationEventDTO dto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var createdBy = User.FindFirst(ClaimTypes.Name)?.Value ?? "System";
                var vaccinationEvent = await _vaccinationEventService.CreateEventAsync(dto, createdBy);

                return CreatedAtAction(nameof(GetEvent), new { id = vaccinationEvent.VaccinationEventId }, vaccinationEvent);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // PUT: api/VaccinationEvent/5
        [HttpPut]
        public async Task<IActionResult> UpdateEvent([FromBody] UpdateVaccinationEventDTO dto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var modifiedBy = User.FindFirst(ClaimTypes.Name)?.Value ?? "System";
                var vaccinationEvent = await _vaccinationEventService.UpdateEventAsync(dto, modifiedBy);

                return Ok(vaccinationEvent);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // DELETE: api/VaccinationEvent/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvent(int id)
        {
            try
            {
                var deletedBy = User.FindFirst(ClaimTypes.Name)?.Value ?? "System";
                var result = await _vaccinationEventService.DeleteEventAsync(id, deletedBy);

                if (!result)
                    return NotFound("Vaccination event not found.");

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/VaccinationEvent/upcoming
        [HttpGet("upcoming")]
        public async Task<ActionResult<List<VaccinationEventDTO>>> GetUpcomingEvents()
        {
            try
            {
                var events = await _vaccinationEventService.GetUpcomingEventsAsync();
                return Ok(events);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/VaccinationEvent/date-range
        [HttpGet("date-range")]
        public async Task<ActionResult<List<VaccinationEventDTO>>> GetEventsByDateRange(
            [FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            try
            {
                var events = await _vaccinationEventService.GetEventsByDateRangeAsync(startDate, endDate);
                return Ok(events);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/VaccinationEvent/5/summary
        [HttpGet("{id}/summary")]
        public async Task<ActionResult<VaccinationEventSummaryDTO>> GetEventSummary(int id)
        {
            try
            {
                var summary = await _vaccinationEventService.GetEventSummaryAsync(id);
                if (summary == null)
                    return NotFound("Vaccination event not found.");

                return Ok(summary);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/VaccinationEvent/id/responses
        // This endpoint is used to get student responses for a vaccination event
        [HttpGet("{id}/responses")]
        public async Task<ActionResult<List<StudentVaccinationStatusDTO>>> GetStudentResponses(int id)
        {
            try
            {
                var responses = await _vaccinationEventService.GetStudentResponsesForEventAsync(id);
                return Ok(responses);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // POST: api/VaccinationEvent/5/send-email
        // This endpoint is used to send vaccination emails to all parents
        [HttpPost("send-email")]
        public async Task<IActionResult> SendVaccinationEmail([FromBody] SendVaccinationEmailDTO dto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var result = await _vaccinationEventService.SendVaccinationEmailToAllParentsAsync(dto);

                if (result == null)
                    return BadRequest("Not found Event info or Email template");

                if(result.Any())
                    return BadRequest("Failed to send vaccination emails to some parents: " + string.Join(", ", result.Select(e => e.To)));

                return Ok("Vaccination emails sent successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // POST: api/VaccinationEvent/5/send-email-specific
        // This endpoint is used to send vaccination emails to specific parents
        [HttpPost("send-email-specific")]
        public async Task<IActionResult> SendVaccinationEmailToSpecificParents(
            [FromBody] SendVaccinationEmailDTO dto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                if (dto.parentIds == null || !dto.parentIds.Any())
                    return BadRequest("Parent IDs are required.");

                var result = await _vaccinationEventService.SendVaccinationEmailToSpecificParentsAsync(dto, dto.parentIds);

                if (result == null)
                    return BadRequest("Not found Event info or Email template");

                if (result.Any())
                    return BadRequest("Failed to send vaccination emails to some parents: " + string.Join(", ", result.Select(e => e.To)));

                return Ok("Vaccination emails sent successfully to specific parents.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // POST: api/VaccinationEvent/respond
        // This endpoint is used by parents to respond to vaccination events
        [HttpPost("respond")]
        public async Task<IActionResult> ProcessParentResponse([FromBody] ParentVaccinationResponseDTO dto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var result = await _vaccinationEventService.ProcessParentResponseAsync(dto);

                if (!result)
                    return BadRequest("Failed to process parent response.");

                return Ok("Parent response processed successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/VaccinationEvent/id/parent-responses
        [HttpGet("{id}/parent-responses")]
        public async Task<ActionResult<List<ParentVaccinationResponseDTO>>> GetParentResponses(int id)
        {
            try
            {
                var responses = await _vaccinationEventService.GetParentResponsesForEventAsync(id);
                return Ok(responses);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/VaccinationEvent/5/statistics
        [HttpGet("{id}/statistics")]
        public async Task<ActionResult<Dictionary<string, int>>> GetEventStatistics(int id)
        {
            try
            {
                var statistics = await _vaccinationEventService.GetEventStatisticsAsync(id);
                return Ok(statistics);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/VaccinationEvent/with-statistics
        [HttpGet("with-statistics")]
        public async Task<ActionResult<List<VaccinationEventDTO>>> GetEventsWithStatistics()
        {
            try
            {
                var events = await _vaccinationEventService.GetEventsWithStatisticsAsync();
                return Ok(events);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/VaccinationEvent/respond
        [HttpGet("respond")]
        public async Task<IActionResult> GetResponseForm([FromQuery] string email, [FromQuery] int eventId)
        {
            try
            {
                // Validate parameters
                if (string.IsNullOrEmpty(email) || eventId <= 0)
                    return BadRequest("Invalid email or event ID");

                // Get event information
                var eventInfo = await _vaccinationEventService.GetEventByIdAsync(eventId);
                if (eventInfo == null)
                    return NotFound("Vaccination event not found");

                // Get student information for this email
                var studentInfo = await _vaccinationEventService.GetStudentByParentEmailAsync(email, eventId);
                if (studentInfo == null || !studentInfo.Any())
                    return NotFound("No student found for this email address");

                // Return HTML form
                var htmlForm = await _vaccinationEventService.FillEmailTemplateData(email, eventInfo);

                return Content(htmlForm, "text/html");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
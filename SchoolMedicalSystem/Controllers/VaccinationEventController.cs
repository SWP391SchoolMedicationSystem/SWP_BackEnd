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
        private readonly IMapper _mapper;

        public VaccinationEventController(IVaccinationEventService vaccinationEventService, IMapper mapper)
        {
            _vaccinationEventService = vaccinationEventService;
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
        //[Authorize(Roles = "Admin,Manager")]
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
        [HttpPut("{id}")]
        //[Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> UpdateEvent(int id, [FromBody] UpdateVaccinationEventDTO dto)
        {
            try
            {
                if (id != dto.VaccinationEventId)
                    return BadRequest("ID mismatch");

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
        //[Authorize(Roles = "Admin,Manager")]
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

        // GET: api/VaccinationEvent/5/responses
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
        [HttpPost("{id}/send-email")]
        //[Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> SendVaccinationEmail(int id, [FromBody] SendVaccinationEmailDTO dto)
        {
            try
            {
                if (id != dto.VaccinationEventId)
                    return BadRequest("ID mismatch");

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var result = await _vaccinationEventService.SendVaccinationEmailToAllParentsAsync(dto);

                if (!result)
                    return BadRequest("Failed to send vaccination emails.");

                return Ok("Vaccination emails sent successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // POST: api/VaccinationEvent/5/send-email-specific
        [HttpPost("{id}/send-email-specific")]
        //[Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> SendVaccinationEmailToSpecificParents(
            int id, [FromBody] SendVaccinationEmailDTO dto, [FromQuery] List<int> parentIds)
        {
            try
            {
                if (id != dto.VaccinationEventId)
                    return BadRequest("ID mismatch");

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                if (parentIds == null || !parentIds.Any())
                    return BadRequest("Parent IDs are required.");

                var result = await _vaccinationEventService.SendVaccinationEmailToSpecificParentsAsync(dto, parentIds);

                if (!result)
                    return BadRequest("Failed to send vaccination emails.");

                return Ok("Vaccination emails sent successfully to specific parents.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // POST: api/VaccinationEvent/respond
        [HttpPost("respond")]
        //[AllowAnonymous] // Allow parents to respond without authentication
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

        // GET: api/VaccinationEvent/5/parent-responses
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
        //[AllowAnonymous]
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
                if (studentInfo == null)
                    return NotFound("No student found for this email address");

                // Return HTML form
                var htmlForm = $@"
<!DOCTYPE html>
<html lang='vi'>
<head>
    <meta charset='UTF-8'>
    <meta name='viewport' content='width=device-width, initial-scale=1.0'>
    <title>Phản hồi về sự kiện tiêm chủng</title>
    <style>
        body {{ font-family: Arial, sans-serif; line-height: 1.6; margin: 0; padding: 20px; background-color: #f5f5f5; }}
        .container {{ max-width: 600px; margin: 0 auto; background: white; padding: 30px; border-radius: 10px; box-shadow: 0 2px 10px rgba(0,0,0,0.1); }}
        h1 {{ color: #2c3e50; text-align: center; margin-bottom: 30px; }}
        .event-info {{ background-color: #f8f9fa; padding: 20px; border-radius: 8px; margin-bottom: 30px; }}
        .form-group {{ margin-bottom: 20px; }}
        label {{ display: block; margin-bottom: 5px; font-weight: bold; color: #333; }}
        input[type='text'], input[type='email'], textarea, select {{ width: 100%; padding: 10px; border: 1px solid #ddd; border-radius: 5px; font-size: 16px; }}
        textarea {{ height: 100px; resize: vertical; }}
        .radio-group {{ margin: 10px 0; }}
        .radio-group label {{ display: inline; margin-right: 20px; font-weight: normal; }}
        button {{ background-color: #3498db; color: white; padding: 12px 30px; border: none; border-radius: 5px; font-size: 16px; cursor: pointer; }}
        button:hover {{ background-color: #2980b9; }}
        .success {{ color: #27ae60; }}
        .error {{ color: #e74c3c; }}
        #reasonField {{ display: none; }}
    </style>
</head>
<body>
    <div class='container'>
        <h1>Phản hồi về sự kiện tiêm chủng</h1>
        
        <div class='event-info'>
            <h3>Sự kiện: {eventInfo.VaccinationEventName}</h3>
            <p><strong>Ngày diễn ra:</strong> {eventInfo.EventDate:dd/MM/yyyy}</p>
            <p><strong>Địa điểm:</strong> {eventInfo.Location}</p>
            <p><strong>Học sinh:</strong> {studentInfo.StudentName}</p>
            <p><strong>Phụ huynh:</strong> {studentInfo.ParentName}</p>
        </div>

        <form id='responseForm'>
            <input type='hidden' id='parentId' value='{studentInfo.ParentId}'>
            <input type='hidden' id='studentId' value='{studentInfo.StudentId}'>
            <input type='hidden' id='eventId' value='{eventId}'>
            <input type='hidden' id='email' value='{email}'>

            <div class='form-group'>
                <label>Bạn có muốn cho con tham gia tiêm chủng không?</label>
                <div class='radio-group'>
                    <label><input type='radio' name='willAttend' value='true' onchange='toggleReasonField()'> Có, tôi đồng ý</label>
                    <label><input type='radio' name='willAttend' value='false' onchange='toggleReasonField()'> Không, tôi không đồng ý</label>
                </div>
            </div>

            <div class='form-group' id='reasonField'>
                <label for='reasonForDecline'>Lý do không tham gia:</label>
                <textarea id='reasonForDecline' name='reasonForDecline' placeholder='Vui lòng nêu rõ lý do...'></textarea>
            </div>

            <div class='form-group'>
                <label>Xác nhận đồng ý:</label>
                <div class='radio-group'>
                    <label><input type='checkbox' id='parentConsent' name='parentConsent'> Tôi xác nhận đã đọc và hiểu thông tin về sự kiện tiêm chủng</label>
                </div>
            </div>

            <div class='form-group'>
                <button type='submit'>Gửi phản hồi</button>
            </div>
        </form>

        <div id='result'></div>
    </div>

    <script>
        function toggleReasonField() {{
            const willAttend = document.querySelector('input[name=""willAttend""]:checked').value;
            const reasonField = document.getElementById('reasonField');
            if (willAttend === 'false') {{
                reasonField.style.display = 'block';
            }} else {{
                reasonField.style.display = 'none';
            }}
        }}

        document.getElementById('responseForm').addEventListener('submit', async function(e) {{
            e.preventDefault();
            
            const formData = {{
                parentId: parseInt(document.getElementById('parentId').value),
                studentId: parseInt(document.getElementById('studentId').value),
                vaccinationEventId: parseInt(document.getElementById('eventId').value),
                willAttend: document.querySelector('input[name=""willAttend""]:checked').value === 'true',
                reasonForDecline: document.getElementById('reasonForDecline').value,
                parentConsent: document.getElementById('parentConsent').checked
            }};

            try {{
                const response = await fetch('/api/VaccinationEvent/respond', {{
                    method: 'POST',
                    headers: {{
                        'Content-Type': 'application/json',
                    }},
                    body: JSON.stringify(formData)
                }});

                const result = await response.text();
                const resultDiv = document.getElementById('result');
                
                if (response.ok) {{
                    resultDiv.innerHTML = '<p class=""success"">Phản hồi của bạn đã được gửi thành công. Cảm ơn bạn!</p>';
                    document.getElementById('responseForm').style.display = 'none';
                }} else {{
                    resultDiv.innerHTML = '<p class=""error"">Có lỗi xảy ra: ' + result + '</p>';
                }}
            }} catch (error) {{
                document.getElementById('result').innerHTML = '<p class=""error"">Có lỗi xảy ra khi gửi phản hồi.</p>';
            }}
        }});
    </script>
</body>
</html>";

                return Content(htmlForm, "text/html");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // POST: api/VaccinationEvent/email-reply
        [HttpPost("email-reply")]
        //[AllowAnonymous]
        public async Task<IActionResult> ProcessEmailReply([FromBody] EmailReplyDTO dto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var result = await _vaccinationEventService.ProcessEmailReplyAsync(dto.FromEmail, dto.Subject, dto.Body);

                if (!result)
                    return BadRequest("Failed to process email reply.");

                return Ok("Email reply processed successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
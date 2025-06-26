using AutoMapper;
using BussinessLayer.IService;
using BussinessLayer.QuartzJob.Scheduler;
using DataAccessLayer.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace SchoolMedicalSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IEmailService _email;
        private readonly IMapper _mapper;
        private readonly NotifyScheduler _notifyScheduler;

        public EmailController(IEmailService email, IMapper mapper, NotifyScheduler notifyScheduler)
        {
            _email = email;
            _mapper = mapper;
            _notifyScheduler = notifyScheduler;
        }

        [HttpPost("testemail")]
        public async Task<IActionResult> SendEmail([FromBody] EmailDTO request)
        {
            if (request == null)
                return BadRequest("Email request cannot be empty or null");
            await _email.SendEmailAsync(request);

            return Ok("Email sent successfully");
        }
        public class Notifyer()
        {
            public string id { get; set; }
            public EmailDTO request { get; set; }
            public DateTime time { get; set; }
        }
        [HttpPost("email/schedule")]
        public async Task<IActionResult> ScheduleEmail([FromBody] Notifyer noti)
        {
            try
            {
                if (noti == null)
                    return BadRequest("Email request cannot be empty or null");
                // Schedule the email to be sent at the specified time
                await _notifyScheduler.ScheduleNotifyJob(noti.id, noti.request, noti.time);
                return Ok("Email scheduled successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error scheduling email: {ex.Message}");
            }
        }

        [HttpPost("email/alluser")]
        public async Task<IActionResult> SendEmailToAllUsers([FromBody] int templateId)
        {
            var failList = await _email.SendEmailToAllUsersAsync(templateId);
            if(failList == null)
                return NotFound("No email template found or failed to send emails");

            if(failList.Any())
                return Ok("Email sent to all users with some failures: " + string.Join(", ", failList.Select(e => e.To)));

            return Ok("Email sent to all users successfully");
        }

        [HttpPost("email/list")]
        public async Task<IActionResult> SendEmailByList([FromBody] UserList request)
        {
            if (request == null || request.userIDs == null)
                return BadRequest("Email list cannot be empty or null");
            var result = await _email.SendEmailByListAsync(request.userIDs, request.emailTemplateID);
            if (result == null)
                return BadRequest("No email template found or failed to send emails");

            if (result.Any())
                return Ok("Emails sent successfully to the specified users: " + string.Join(", ", result.Select(e => e.To)));

            return Ok("Emails sent successfully");
        }

        [HttpPost("CreateEmailTemplate")]
        public async Task<IActionResult> CreateEmailTemplate([FromBody] EmailDTO request)
        {
            var emailTemplate = await _email.CreateEmailTemplate(request);
            if (emailTemplate == null)
                return BadRequest("Failed to create email template");

            return Ok(emailTemplate);
        }

        [HttpGet("GetEmailAllTemplate")]
        public async Task<IActionResult> GetAllEmailTemplate()
        {
            var emailTemplates = await _email.GetEmailAllTemplate();
            if (emailTemplates == null || !emailTemplates.Any())
                return NotFound("No email templates found");
            return Ok(emailTemplates);
        }

        [HttpPut("UpdateEmailTemplate")]
        public async Task<IActionResult> UpdateEmailTemplate([FromBody] UpdateEmail request)
        {
            if (request == null || request.Email == null)
                return BadRequest("Email template cannot be empty or null");
            var updatedTemplate = await _email.UpdateEmailTemplate(request.Email, request.Id);
            if (updatedTemplate == null)
                return BadRequest("Failed to update email template");

            var emailTemplate = _mapper.Map<EmailDTO>(updatedTemplate);
            return Ok(emailTemplate);
        }

        [HttpDelete("DeleteEmailTemplate/{id}")]
        public async Task<IActionResult> DeleteEmailTemplate(int id)
        {
            var result = await _email.DeleteEmailTemplate(id);
            if (!result)
                return NotFound("Email template not found or could not be deleted");
            return Ok("Email template deleted successfully");
        }

        [HttpGet("GetTemplateByID/{templateId}")]
        public IActionResult GetTemplateByID(int templateId)
        {
            var emailTemplate = _email.GetTemplateByID(templateId);
            if (emailTemplate == null)
                return NotFound("Email template not found");
            return Ok(emailTemplate);
        }
        [HttpGet("GetAllUserEmails")]
        public IActionResult GetAllUserEmails()
        {
            var emails = _email.GetAllUserEmails();
            if (emails == null || !emails.Any())
                return NotFound("No user emails found");
            return Ok(emails);
        }
    }
    public class UserList
    {
        public List<int>? userIDs { get; set; }
        public int emailTemplateID { get; set; }
    }

    public class UpdateEmail
    {
        public int Id { get; set; }
        public EmailDTO? Email { get; set; }
    }
}

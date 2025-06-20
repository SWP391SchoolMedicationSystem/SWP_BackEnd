using AutoMapper;
using BussinessLayer.IService;
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

        public EmailController(IEmailService email, IMapper mapper)
        {
            _email = email;
            _mapper = mapper;
        }

        [HttpPost("testemail")]
        public async Task<IActionResult> SendEmail([FromBody] EmailDTO request)
        {
            if (request == null)
                return BadRequest("Email request cannot be empty or null");
            await _email.SendEmailAsync(request);

            return Ok("Email sent successfully");
        }

        [HttpPost("email/alluser")]
        public async Task<IActionResult> SendEmailToAllUsers([FromBody] int templateId)
        {
            await _email.SendEmailToAllUsersAsync(templateId);
            return Ok("Email sent to all users successfully");
        }

        [HttpPost("email/list")]
        public async Task<IActionResult> SendEmailByList([FromBody] UserList request)
        {
            if (request == null || request.userIDs == null)
                return BadRequest("Email list cannot be empty or null");
            var result = await _email.SendEmailByListAsync(request.userIDs, request.emailTemplateID);
            if (!result)
                return BadRequest("Failed to send emails");
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
        public async Task<IActionResult> GetEmailAllTemplate()
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

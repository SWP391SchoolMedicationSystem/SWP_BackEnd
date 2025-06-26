using AutoMapper;
using BussinessLayer.IService;
using DataAccessLayer.DTO;
using Microsoft.AspNetCore.Mvc;

namespace SchoolMedicalSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController(IEmailService email) : ControllerBase
    {

        [HttpPost("sendEmail")]
        public async Task<IActionResult> SendEmail(EmailDTO request)
        {
            if (request == null)
                return BadRequest("Email request cannot be empty or null");
            await email.SendEmailAsync(request);

            return Ok("Email sent successfully");
        }

        [HttpPost("CreateEmailTemplate")]
        public async Task<IActionResult> CreateEmailTemplate(EmailDTO request)
        {
            var emailTemplate = await email.CreateEmailTemplate(request);
            if (emailTemplate == null)
                return BadRequest("Failed to create email template");

            return Ok(emailTemplate);
        }

        [HttpPost("SendEmailToAllUsers")]
        public async Task<IActionResult> SendEmailToAllUsers(int id)
        {
            await email.SendEmailToAllUsersAsync(id);
            return Ok("Email sent to all users successfully");
        }

        [HttpPost("SendEmailByList")]
        public async Task<IActionResult> SendEmailByList(UserList request)
        {
            if (request == null || request.UserIDs == null || request.UserIDs.Count == 0)
                return BadRequest("Email list cannot be empty or null");
            else
            {
                var result = await email.SendEmailByListAsync(request.UserIDs, request.EmailTemplate);
                if (!result)
                    return BadRequest("Failed to send emails");
                return Ok("Emails sent successfully");
            }
        }
    }
    public class UserList
    {
        public List<int>? UserIDs { get; set; }
        public int EmailTemplate { get; set; }
    }
}

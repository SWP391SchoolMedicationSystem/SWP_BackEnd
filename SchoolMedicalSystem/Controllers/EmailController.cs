using AutoMapper;
using BussinessLayer.IService;
using DataAccessLayer.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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

        [HttpPost("sendEmail")]
        public async Task<IActionResult> SendEmail(EmailDTO request)
        {
            if (request == null)
                return BadRequest("Email request cannot be empty or null");
            await _email.SendEmailAsync(request);

            return Ok("Email sent successfully");
        }

        [HttpPost("CreateEmailTemplate")]
        public async Task<IActionResult> CreateEmailTemplate(EmailDTO request)
        {
            var emailTemplate = await _email.CreateEmailTemplate(request);
            if (emailTemplate == null)
                return BadRequest("Failed to create email template");

            return Ok(emailTemplate);
        }

        [HttpPost("SendEmailToAllUsers")]
        public async Task<IActionResult> SendEmailToAllUsers(int id)
        {
            await _email.SendEmailToAllUsersAsync(id);
            return Ok("Email sent to all users successfully");
        }

        [HttpPost("SendEmailByList")]
        public async Task<IActionResult> SendEmailByList(UserList request)
        {
            if (request == null)
                return BadRequest("Email list cannot be empty or null");
            var result = await _email.SendEmailByListAsync(request.userIDs, request.emailTemplate);
            if (!result)
                return BadRequest("Failed to send emails");
            return Ok("Emails sent successfully");
        }
    }
    public class UserList
    {
        public List<int>? userIDs { get; set; }
        public int emailTemplate { get; set; }
    }
}

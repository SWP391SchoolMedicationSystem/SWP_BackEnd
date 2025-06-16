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
        public IActionResult SendEmail(EmailDTO request)
        {
            _email.SendEmail(request);

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
        public IActionResult SendEmailToAllUsers(int id)
        {
            _email.SendEmailToAllUsers(id);
            return Ok("Email sent to all users successfully");
        }
    }
}

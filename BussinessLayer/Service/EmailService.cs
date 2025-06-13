using MailKit.Security;
using MimeKit.Text;
using MimeKit;

using MailKit.Net.Smtp;

using Microsoft.Extensions.Configuration;
using DataAccessLayer.Entity;
using DataAccessLayer.DTO;
using AutoMapper;

namespace SchoolMedicalSystem.Services.EmailService
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;
        private readonly SchoolMedicalSystemContext _context;
        private readonly IMapper _mapper;

        public EmailService(IMapper mapper,
            IConfiguration config, SchoolMedicalSystemContext context)
        {
            _mapper = mapper;
            _config = config;
            _context = context;
        }

        public List<string> GetAllEmails()
        {
            return _context.Users.Select(e => e.Email).ToList();
        }

        public void SendEmail(EmailDTO request)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(_config.GetSection("EmailHost").Value));
            email.To.Add(MailboxAddress.Parse(request.To));
            email.Subject = request.Subject;
            email.Body = new TextPart(TextFormat.Html) { Text = request.Body };

            using var smtp = new SmtpClient();
            smtp.Connect(_config.GetSection("EmailHost").Value, 587, SecureSocketOptions.StartTls);
            smtp.Authenticate(_config.GetSection("EmailUserName").Value, _config.GetSection("EmailPassword").Value);
            smtp.Send(email);
            smtp.Disconnect(true);
        }

        public bool SendEmailToAllUsers(int id)
        {
            var emailTemplate = GetTemplateByID(id);
            if (emailTemplate == null)
                return false;

            var emails = GetAllEmails();
            foreach (var email in emails)
            {
                emailTemplate.To = email;
                SendEmail(emailTemplate);
            }

            return true;
        }

        public EmailDTO GetTemplateByID(int id)
        {
            var emailTemplate = from e in _context.EmailTemplates where e.EmailTemplateId == id select e;
            return (EmailDTO)emailTemplate;
        }

        public async Task<EmailTemplate> CreateEmailTemplate(EmailDTO request)
        {
            var emailTemplate = _mapper.Map<EmailTemplate>(request);
            emailTemplate.CreatedDate = DateTime.Now;
            emailTemplate.CreatedBy = 2;
            _context.EmailTemplates.Add(emailTemplate);
 // Assuming 1 is the ID of the user creating the template, adjust as necessary
            await _context.SaveChangesAsync();
            // Optionally, you can map the created EmailTemplate back to DTO if needed
            // var createdEmailTemplate = _mapper.Map<EmailTemplateDTO>(emailTemplate);
            // return createdEmailTemplate;

            return emailTemplate;
        }
    }
}

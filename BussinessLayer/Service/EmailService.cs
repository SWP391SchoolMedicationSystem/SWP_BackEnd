using MailKit.Security;
using MimeKit.Text;
using MimeKit;

using MailKit.Net.Smtp;

using Microsoft.Extensions.Configuration;
using DataAccessLayer.Entity;
using DataAccessLayer.DTO;
using AutoMapper;
using BussinessLayer.IService;
using Microsoft.EntityFrameworkCore;
using DataAccessLayer.IRepository;

namespace BussinessLayer.Service
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;
        private readonly SchoolMedicalSystemContext _context;
        private readonly IEmailRepo _emailRepository;
        private readonly IMapper _mapper;

        public EmailService(IMapper mapper,
            IConfiguration config, SchoolMedicalSystemContext context, IEmailRepo emailRepo)
        {
            _mapper = mapper;
            _config = config;
            _context = context;
            _emailRepository = emailRepo;
        }

        public List<string> GetAllUserEmails()
        {
            return _context.Users.Select(e => e.Email).ToList();
        }

        public List<string> GetEmailListByID(List<int> userIDs)
        {
            if (userIDs == null || !userIDs.Any())
                return new List<string>();
            return _context.Users
                .Where(u => userIDs.Contains(u.UserId))
                .Select(u => u.Email)
                .ToList();
        }

        public async Task SendEmailAsync(EmailDTO request)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(_config["EmailHost"]));
            email.To.Add(MailboxAddress.Parse(request.To));
            email.Subject = request.Subject;
            email.Body = new TextPart(TextFormat.Html) { Text = request.Body };

            using var smtp = new SmtpClient();
            await smtp.ConnectAsync(_config["EmailHost"], 587, SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync(_config["EmailUserName"], _config["EmailPassword"]);
            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);
        }

        public async Task<bool> SendEmailToAllUsersAsync(int templateId)
        {
            var emailTemplate = GetTemplateByID(templateId);
            if (emailTemplate == null)
                return false;

            var emails = GetAllUserEmails();
            foreach (var email in emails)
            {
                emailTemplate.To = email;
                await SendEmailAsync(emailTemplate);
            }

            return true;
        }

        public async Task<bool> SendEmailByListAsync(List<int> userIDs, int templateId)
        {
            var request = GetTemplateByID(templateId);
            if (request == null)
                return false;

            request.To = string.Empty;
            var emails = GetEmailListByID(userIDs);
            if (emails == null || !emails.Any())
                return false;

            foreach (var email in emails)
            {
                request.To = email;
                await SendEmailAsync(request);
            }
            return true;
        }

        public async Task<List<EmailTemplate>> GetEmailAllTemplate()
        {
            return await _context.EmailTemplates.ToListAsync();
        }

        public EmailDTO GetTemplateByID(int templateId)
        {
            var emailTemplate = _context.EmailTemplates.FirstOrDefault(e => e.EmailTemplateId == templateId);
            if (emailTemplate == null)
                return null;

            var emailTemplateDTO = new EmailDTO
            {
                To = emailTemplate.To,
                Subject = emailTemplate.Subject,
                Body = emailTemplate.Body
            };
            return emailTemplateDTO;
        }

        public async Task<EmailTemplate> CreateEmailTemplate(EmailDTO request)
        {
            var emailTemplate = _mapper.Map<EmailTemplate>(request);
            emailTemplate.CreatedDate = DateTime.Now;
            _context.EmailTemplates.Add(emailTemplate);
            await _context.SaveChangesAsync();

            return emailTemplate;
        }

        public async Task<EmailTemplate> UpdateEmailTemplate(EmailDTO request, int id)
        {
            var emailTemplate = await _context.EmailTemplates.FindAsync(id);
            if (emailTemplate == null)
                return null;
            emailTemplate.To = request.To;
            emailTemplate.Subject = request.Subject;
            emailTemplate.Body = request.Body;
            emailTemplate.UpdatedDate = DateTime.Now;
            _context.EmailTemplates.Update(emailTemplate);
            await _context.SaveChangesAsync();
            return emailTemplate;
        }

        public async Task<bool> DeleteEmailTemplate(int id)
        {
            var emailTemplate = await _context.EmailTemplates.FindAsync(id);
            if (emailTemplate == null)
                return false;
            emailTemplate.IsDeleted = true;
            _context.EmailTemplates.Update(emailTemplate);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}

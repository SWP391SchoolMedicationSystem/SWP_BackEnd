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
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Security.Cryptography;

namespace BussinessLayer.Service
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;
        private readonly SchoolMedicalSystemContext _context;
        private readonly IEmailRepo _emailRepository;
        private readonly IMapper _mapper;
        private readonly SemaphoreSlim _smtpSemaphore;
        private readonly int _maxConcurrentEmails;

        public EmailService(IMapper mapper,
            IConfiguration config, SchoolMedicalSystemContext context, IEmailRepo emailRepo)
        {
            _mapper = mapper;
            _config = config;
            _context = context;
            _emailRepository = emailRepo;
            _maxConcurrentEmails = int.TryParse(_config["EmailSettings:MaxConcurrentEmails"], out var max) ? max : 5;
            _smtpSemaphore = new SemaphoreSlim(_maxConcurrentEmails, _maxConcurrentEmails);
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
            await _smtpSemaphore.WaitAsync();
            try
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
            finally
            {
                _smtpSemaphore.Release();
            }
        }

        // Optimized method for sending multiple emails with connection pooling
        public async Task<bool> SendBulkEmailsAsync(List<EmailDTO> emails, int batchSize = 10)
        {
            if (emails == null || !emails.Any())
                return false;

            var batches = emails.Chunk(batchSize);
            var tasks = new List<Task>();

            foreach (var batch in batches)
            {
                var batchTask = ProcessEmailBatchAsync(batch);
                tasks.Add(batchTask);
            }

            await Task.WhenAll(tasks);
            return true;
        }

        private async Task ProcessEmailBatchAsync(IEnumerable<EmailDTO> emails)
        {
            using var smtp = new SmtpClient();
            try
            {
                await smtp.ConnectAsync(_config["EmailHost"], 587, SecureSocketOptions.StartTls);
                await smtp.AuthenticateAsync(_config["EmailUserName"], _config["EmailPassword"]);

                foreach (var emailDto in emails)
                {
                    var email = new MimeMessage();
                    email.From.Add(MailboxAddress.Parse(_config["EmailHost"]));
                    email.To.Add(MailboxAddress.Parse(emailDto.To));
                    email.Subject = emailDto.Subject;
                    email.Body = new TextPart(TextFormat.Html) { Text = emailDto.Body };

                    await smtp.SendAsync(email);
                }
            }
            finally
            {
                await smtp.DisconnectAsync(true);
            }
        }

        // Optimized method for sending emails to all users
        public async Task<bool> SendEmailToAllUsersAsync(int templateId)
        {
            var emailTemplate = GetTemplateByID(templateId);
            if (emailTemplate == null)
                return false;

            var emails = GetAllUserEmails();
            var emailDtos = emails.Select(email => new EmailDTO
            {
                To = email,
                Subject = emailTemplate.Subject,
                Body = emailTemplate.Body
            }).ToList();

            return await SendBulkEmailsAsync(emailDtos);
        }

        // Optimized method for sending emails to specific users
        public async Task<bool> SendEmailByListAsync(List<int> userIDs, int templateId)
        {
            var request = GetTemplateByID(templateId);
            if (request == null)
                return false;

            var emails = GetEmailListByID(userIDs);
            if (emails == null || !emails.Any())
                return false;

            var emailDtos = emails.Select(email => new EmailDTO
            {
                To = email,
                Subject = request.Subject,
                Body = request.Body
            }).ToList();

            return await SendBulkEmailsAsync(emailDtos);
        }

        // Optimized method for sending personalized emails
        public async Task<bool> SendPersonalizedEmailsAsync<T>(List<T> recipients, int templateId, 
            Func<T, EmailDTO> personalizationFunc, int batchSize = 10)
        {
            var emailTemplate = GetTemplateByID(templateId);
            if (emailTemplate == null)
                return false;

            var emailDtos = recipients.Select(recipient => personalizationFunc(recipient)).ToList();
            return await SendBulkEmailsAsync(emailDtos, batchSize);
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

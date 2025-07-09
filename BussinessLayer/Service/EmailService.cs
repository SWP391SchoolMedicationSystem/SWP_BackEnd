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
using Scriban;

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

        // Returns a list of all emails that failed to send across all batches.
        // An empty list means everything was successful.
        public async Task<List<EmailDTO>> SendBulkEmailsAsync(List<EmailDTO> emails, int batchSize = 10)
        {
            if (emails == null || !emails.Any())
                return new List<EmailDTO>(); // Nothing to do

            var allFailedEmails = new List<EmailDTO>();
            var batches = emails.Chunk(batchSize);
            var batchProcessingTasks = new List<Task<List<EmailDTO>>>();

            foreach (var batch in batches)
            {
                // Wait for a slot to become available
                await _smtpSemaphore.WaitAsync();

                // Start the task and add it to our list.
                // The ContinueWith ensures the semaphore is released.
                var task = ProcessEmailBatchAsync(batch)
                    .ContinueWith(t =>
                    {
                        _smtpSemaphore.Release();
                        return t.Result; // t.Result is the List<EmailDTO> of failed emails from the batch
                    });
                batchProcessingTasks.Add(task);
            }

            // Await all batch tasks to complete
            var results = await Task.WhenAll(batchProcessingTasks);

            // Aggregate the lists of ailed emails from all batches
            foreach (var failedBatch in results)
            {
                if (failedBatch.Any())
                {
                    allFailedEmails.AddRange(failedBatch);
                }
            }

            return allFailedEmails;
        }

        private async Task<List<EmailDTO>> ProcessEmailBatchAsync(IEnumerable<EmailDTO> emailBatch)
        {
            var failedEmails = new List<EmailDTO>();
            using var smtp = new SmtpClient();
            try
            {
                await smtp.ConnectAsync(_config["EmailHost"], 587, SecureSocketOptions.StartTls);
                await smtp.AuthenticateAsync(_config["EmailUserName"], _config["EmailPassword"]);

                foreach (var emailDto in emailBatch)
                {
                    try
                    {
                        var email = new MimeMessage();
                        email.From.Add(MailboxAddress.Parse(_config["EmailHost"]));
                        email.To.Add(MailboxAddress.Parse(emailDto.To));
                        email.Subject = emailDto.Subject;
                        email.Body = new TextPart(TextFormat.Html) { Text = emailDto.Body };

                        await smtp.SendAsync(email);
                    }
                    catch (Exception ex)
                    {
                        // A specific email failed. Log the exception and the recipient.
                        // For example: _logger.LogError(ex, "Failed to send email to {Recipient}", emailDto.To);
                        failedEmails.Add(emailDto);
                    }
                }
            }
            catch (Exception ex)
            {
                // A catastrophic failure occurred (e.g., couldn't connect or authenticate).
                // All emails in this batch are considered failed.
                // For example: _logger.LogError(ex, "Catastrophic failure in email batch processing.");
                return emailBatch.ToList(); // Return the whole batch as failed
            }
            finally
            {
                if (smtp.IsConnected)
                {
                    await smtp.DisconnectAsync(true);
                }
            }
            return failedEmails;
        }

        // Optimized method for sending emails to all users
        public async Task<List<EmailDTO>> SendEmailToAllUsersAsync(int templateId)
        {
            var emailTemplate = GetTemplateByID(templateId);
            if (emailTemplate == null)
                return null;

            var emails = GetAllUserEmails();
            var emailDtos = emails.Select(email => new EmailDTO
            {
                To = email,
                Subject = emailTemplate.Subject,
                Body = emailTemplate.Body
            }).ToList();

            var failedEmails = await SendBulkEmailsAsync(emailDtos);

            return failedEmails;
        }

        // Optimized method for sending emails to specific users
        public async Task<List<EmailDTO>> SendEmailByListAsync(List<int> userIDs, int templateId)
        {
            var request = GetTemplateByID(templateId);
            if (request == null)
                return null;

            var emails = GetEmailListByID(userIDs);
            if (emails == null || !emails.Any())
                return null;

            var emailDtos = emails.Select(email => new EmailDTO
            {
                To = email,
                Subject = request.Subject,
                Body = request.Body
            }).ToList();

            // Send the emails in bulk and return any that failed
            return await SendBulkEmailsAsync(emailDtos);
        }

        // Optimized method for sending personalized emails
        public async Task<List<EmailDTO>> SendPersonalizedEmailsAsync<T>(List<T> recipients, int templateId, 
            Func<T, EmailDTO> personalizationFunc, int batchSize = 10)
        {
            var emailTemplate = GetTemplateByID(templateId);
            if (emailTemplate == null)
                return null;

            var emailDtos = recipients.Select(recipient => personalizationFunc(recipient)).ToList();

            // Ensure each email has the template's subject and body
            return await SendBulkEmailsAsync(emailDtos, batchSize);
        }

        public async Task<List<VaccinationEmailTemplate>> GetEmailAllTemplate()
        {
            return await _context.VaccinationEmailTemplate.ToListAsync();
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

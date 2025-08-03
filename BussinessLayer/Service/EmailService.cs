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
        private List<EmailDTO> failEmailList;

        public EmailService(IMapper mapper,
            IConfiguration config, SchoolMedicalSystemContext context, IEmailRepo emailRepo)
        {
            _mapper = mapper;
            _config = config;
            _context = context;
            _emailRepository = emailRepo;
            _maxConcurrentEmails = int.TryParse(_config["EmailSettings:MaxConcurrentEmails"], out var max) ? max : 5;
            _smtpSemaphore = new SemaphoreSlim(_maxConcurrentEmails, _maxConcurrentEmails);
            failEmailList = new List<EmailDTO>();
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

        public async Task<EmailDTO> GetEmailByName(string name)
        {
            var emailTemplate = await _context.EmailTemplates
                .FirstOrDefaultAsync(e => e.Subject.Equals(name));
            if (emailTemplate == null)
                return null;
            return new EmailDTO
            {
                To = emailTemplate.To,
                Subject = emailTemplate.Subject,
                Body = emailTemplate.Body
            };
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
            failEmailList = new List<EmailDTO>();

            foreach (var batch in batches)
            {
                await _smtpSemaphore.WaitAsync();

                var task = ProcessEmailBatchAsync(batch)
                    .ContinueWith(t =>
                    {
                        _smtpSemaphore.Release();
                        return t.Result;
                    });
                batchProcessingTasks.Add(task);
            }

            _ = Task.Run(async () =>
            {
                try
                {
                    // Await all batch tasks. We don't need their results here, just their completion.
                    await Task.WhenAll(batchProcessingTasks);
                    Console.WriteLine("All email batch processing tasks have completed successfully.");
                }
                catch (AggregateException ae)
                {
                    // This catch block will execute if any of the *individual* tasks threw an unhandled exception.
                    Console.WriteLine($"An AggregateException occurred during bulk email processing:");
                    foreach (var ex in ae.InnerExceptions)
                    {
                        Console.WriteLine($"- Inner Exception: {ex.Message}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An unexpected error occurred in the background task: {ex.Message}");
                }
                finally
                {
                    await RetrySendEmailAsync(failEmailList); // Retry sending failed emails
                    Console.WriteLine("OnBulkEmailProcessingCompleted event raised.");
                }
            });

            return failEmailList; // Return the list of failed emails
        }

        private async Task RetrySendEmailAsync(List<EmailDTO> failList)
        {
            using var smtp = new SmtpClient();
            try
            {
                await smtp.ConnectAsync(_config["EmailHost"], 587, SecureSocketOptions.StartTls);
                await smtp.AuthenticateAsync(_config["EmailUserName"], _config["EmailPassword"]);

                foreach (var emailDto in failList)
                {
                    try
                    {
                        var email = new MimeMessage();
                        email.From.Add(MailboxAddress.Parse(_config["EmailHost"]));
                        email.To.Add(MailboxAddress.Parse(emailDto.To));
                        email.Subject = emailDto.Subject;
                        email.Body = new TextPart(TextFormat.Html) { Text = emailDto.Body };

                        await smtp.SendAsync(email);
                        failEmailList.Remove(emailDto); // Remove from failed list if sent successfully
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Failed to resend email to {emailDto.To}: {ex.Message}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"A catastrophic failure occurred while retrying emails: {ex.Message}");
            }
            finally
            {
                if (smtp.IsConnected)
                {
                    await smtp.DisconnectAsync(true);
                }
            }
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
                        failEmailList.Add(emailDto);
                    }
                }
            }
            catch (Exception ex)
            {
                return failedEmails; // Return the whole batch as failed
            }
            finally
            {
                if (smtp.IsConnected)
                {
                    await smtp.DisconnectAsync(true);
                }
            }
            return failEmailList;
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

        public async Task<EmailTemplate> CreateEmailTemplate(CreateEmailDTO request)
        {
            var emailTemplate = _mapper.Map<EmailTemplate>(request);
            emailTemplate.CreatedDate = DateTime.Now;
            _context.EmailTemplates.Add(emailTemplate);
            await _context.SaveChangesAsync();

            return emailTemplate;
        }

        public async Task<EmailTemplate> UpdateEmailTemplate(UpdateEmailDTO request)
        {
            var emailTemplate = await _context.EmailTemplates.FindAsync(request.Id);
            if (emailTemplate == null)
                return null;
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

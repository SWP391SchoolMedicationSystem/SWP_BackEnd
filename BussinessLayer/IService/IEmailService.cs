using DataAccessLayer.DTO;
using DataAccessLayer.Entity;

namespace BussinessLayer.IService
{
    public interface IEmailService
    {
        Task SendEmailAsync(EmailDTO request);
        Task<bool> SendBulkEmailsAsync(List<EmailDTO> emails, int batchSize = 10);
        Task<bool> SendPersonalizedEmailsAsync<T>(List<T> recipients, int templateId, 
            Func<T, EmailDTO> personalizationFunc, int batchSize = 10);
        Task<EmailTemplate> CreateEmailTemplate(EmailDTO request);
        Task<bool> SendEmailToAllUsersAsync(int id);
        List<string> GetAllUserEmails();
        Task<bool> SendEmailByListAsync(List<int> userIDs, int templateId);
        Task<List<EmailTemplate>> GetEmailAllTemplate();
        Task<EmailTemplate> UpdateEmailTemplate(EmailDTO request, int id);
        Task<bool> DeleteEmailTemplate(int id);
        public EmailDTO GetTemplateByID(int templateId);
    }
}

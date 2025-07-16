using DataAccessLayer.DTO;
using DataAccessLayer.Entity;

namespace BussinessLayer.IService
{
    public interface IEmailService
    {
        Task SendEmailAsync(EmailDTO request);
        Task<List<EmailDTO>> SendBulkEmailsAsync(List<EmailDTO> emails, int batchSize = 10);
        Task<List<EmailDTO>> SendPersonalizedEmailsAsync<T>(List<T> recipients, int templateId, 
            Func<T, EmailDTO> personalizationFunc, int batchSize = 10);
        Task<EmailTemplate> CreateEmailTemplate(EmailDTO request);
        Task<List<EmailDTO>> SendEmailToAllUsersAsync(int id);
        List<string> GetAllUserEmails();
        Task<List<EmailDTO>> SendEmailByListAsync(List<int> userIDs, int templateId);
        Task<List<EmailTemplate>> GetEmailAllTemplate();
        Task<EmailTemplate> UpdateEmailTemplate(EmailDTO request, int id);
        Task<bool> DeleteEmailTemplate(int id);
        EmailDTO GetTemplateByID(int templateId);
        Task<EmailDTO> GetEmailByName(string name);
    }
}

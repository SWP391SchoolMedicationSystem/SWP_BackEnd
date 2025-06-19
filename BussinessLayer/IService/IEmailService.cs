using DataAccessLayer.DTO;
using DataAccessLayer.Entity;


namespace BussinessLayer.IService
{
    public interface IEmailService
    {
        Task SendEmailAsync(EmailDTO request);
        Task<EmailTemplate> CreateEmailTemplate(EmailDTO request);
        Task<bool> SendEmailToAllUsersAsync(int id);
        List<string> GetAllUserEmails();
        Task<bool> SendEmailByListAsync(List<int> userIDs, int templateId);
        Task<List<EmailTemplate>> GetEmailAllTemplate();
        Task<EmailTemplate> UpdateEmailTemplate(EmailDTO request, int id);
        Task<bool> DeleteEmailTemplate(int id);
        Task<bool> ResetPassword(string email);
    }
}

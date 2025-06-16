using DataAccessLayer.DTO;
using DataAccessLayer.Entity;


namespace BussinessLayer.IService
{
    public interface IEmailService
    {
        Task SendEmailAsync(EmailDTO request);
        Task<EmailTemplate> CreateEmailTemplate(EmailDTO request);
        Task<bool> SendEmailToAllUsersAsync(int id);
        List<string> GetAllEmails();
        Task<bool> SendEmailByListAsync(List<int> userIDs, int templateId);
    }
}

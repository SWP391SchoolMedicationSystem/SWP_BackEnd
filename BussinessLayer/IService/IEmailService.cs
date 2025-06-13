using DataAccessLayer.DTO;
using DataAccessLayer.Entity;


namespace SchoolMedicalSystem.Services.EmailService
{
    public interface IEmailService
    {
        void SendEmail(EmailDTO request);
        Task<EmailTemplate> CreateEmailTemplate(EmailDTO request);
        bool SendEmailToAllUsers(int id);
        List<string> GetAllEmails();
    }
}

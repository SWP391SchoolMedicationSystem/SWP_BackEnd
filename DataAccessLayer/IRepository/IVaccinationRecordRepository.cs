using DataAccessLayer.Entity;

namespace DataAccessLayer.IRepository
{
    public interface IVaccinationRecordRepository : IGenericRepository<Vaccinationrecord>
    {
        Task<List<Vaccinationrecord>> GetRecordsByEventAsync(int eventId);
        Task<List<Vaccinationrecord>> GetRecordsByStudentAsync(int studentId);
        Task<Vaccinationrecord?> GetRecordByStudentAndEventAsync(int studentId, int eventId);
        Task<int> GetConfirmedCountAsync(int eventId);
        Task<int> GetDeclinedCountAsync(int eventId);
        Task<int> GetPendingCountAsync(int eventId);
    }
} 
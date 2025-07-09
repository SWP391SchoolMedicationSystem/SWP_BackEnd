using DataAccessLayer.Entity;

namespace DataAccessLayer.IRepository
{
    public interface IVaccinationRecordRepository : IGenericRepository<StudentVaccinationRecord>
    {
        Task<List<StudentVaccinationRecord>> GetRecordsByEventAsync(int eventId);
        Task<List<StudentVaccinationRecord>> GetRecordsByStudentAsync(int studentId);
        Task<StudentVaccinationRecord?> GetRecordByStudentAndEventAsync(int studentId, int eventId);
        Task<int> GetConfirmedCountAsync(int eventId);
        Task<int> GetDeclinedCountAsync(int eventId);
        Task<int> GetPendingCountAsync(int eventId);
    }
} 
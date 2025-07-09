using System;
using DataAccessLayer.DTO;
using DataAccessLayer.Entity;

namespace DataAccessLayer.IRepository
{
    public interface IVaccinationEventRepository : IGenericRepository<VaccinationEvent>
    {
        Task<List<VaccinationEvent>> GetAllActiveEventsAsync();
        Task<VaccinationEvent?> GetEventWithRecordsAsync(int eventId);
        Task<List<VaccinationEvent>> GetEventsByDateRangeAsync(DateTime startDate, DateTime endDate);
        Task<List<VaccinationEvent>> GetUpcomingEventsAsync();
        Task<bool> EventExistsAsync(int eventId);
        Task<List<StudentVaccinationRecord>> GetRecordsByEventAsync(int eventId);
        Task<List<StudentVaccinationRecord>> GetRecordsByStudentAsync(int studentId);
        Task<StudentVaccinationRecord?> GetRecordByStudentAndEventAsync(int studentId, int eventId);
        Task<List<StudentVaccinationStatusDTO>> GetStudentResponsesForEventAsync(int eventId);
        Task<VaccinationEventSummaryDTO> GetEventSummaryAsync(int eventId);
        Task<List<Parent>> GetParentsForEventAsync(int eventId);
        Task<int> GetTotalStudentsForEventAsync(int eventId);
        Task<List<Student>> GetStudentsForEventAsync(int eventId);
        Task<int> GetConfirmedCountAsync(int eventId);
        Task<int> GetDeclinedCountAsync(int eventId);
        Task<int> GetPendingCountAsync(int eventId);
    }
} 
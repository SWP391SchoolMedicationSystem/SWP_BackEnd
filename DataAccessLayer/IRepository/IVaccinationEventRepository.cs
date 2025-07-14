using System;
using DataAccessLayer.DTO;
using DataAccessLayer.Entity;

namespace DataAccessLayer.IRepository
{
    public interface IVaccinationEventRepository : IGenericRepository<Vaccinationevent>
    {
        Task<List<Vaccinationevent>> GetAllActiveEventsAsync();
        Task<Vaccinationevent?> GetEventByAccessTokenAsync(string accessToken);
        Task<Vaccinationevent?> GetEventWithRecordsAsync(int eventId);
        Task<List<Vaccinationevent>> GetEventsByDateRangeAsync(DateTime startDate, DateTime endDate);
        Task<List<Vaccinationevent>> GetUpcomingEventsAsync();
        Task<bool> EventExistsAsync(int eventId);
        Task<List<Vaccinationrecord>> GetRecordsByEventAsync(int eventId);
        Task<List<Vaccinationrecord>> GetRecordsByStudentAsync(int studentId);
        Task<Vaccinationrecord?> GetRecordByStudentAndEventAsync(int studentId, int eventId);
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
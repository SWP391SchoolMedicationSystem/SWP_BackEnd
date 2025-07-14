using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.DTO;
using DataAccessLayer.Entity;

namespace BussinessLayer.IService
{
    public interface IVaccinationEventService
    {
        // CRUD Operations
        Task<List<VaccinationEventDTO>> GetAllEventsAsync();
        Task<VaccinationEventDTO> GetEventByAccessToken(string accessToken);
        Task<VaccinationEventDTO?> GetEventByIdAsync(int eventId);
        Task<VaccinationEventDTO> CreateEventAsync(CreateVaccinationEventDTO dto, string createdBy);
        Task<VaccinationEventDTO> UpdateEventAsync(UpdateVaccinationEventDTO dto, string modifiedBy);
        Task<bool> DeleteEventAsync(int eventId, string deletedBy);
        
        // Event Management
        Task<List<VaccinationEventDTO>> GetUpcomingEventsAsync();
        Task<List<VaccinationEventDTO>> GetEventsByDateRangeAsync(DateTime startDate, DateTime endDate);
        Task<VaccinationEventSummaryDTO> GetEventSummaryAsync(int eventId);
        Task<List<StudentVaccinationStatusDTO>> GetStudentResponsesForEventAsync(int eventId);
        
        // Email Operations
        Task<List<EmailDTO>> SendVaccinationEmailToAllParentsAsync(SendVaccinationEmailDTO dto);
        Task<List<EmailDTO>> SendVaccinationEmailToSpecificParentsAsync(SendVaccinationEmailDTO dto, List<int> parentIds);
        Task<List<EmailDTO>> SendVaccinationEmailToSpecificStudentsAsync(SendVaccinationEmailDTO dto, List<int> studentIds);

        // Parent Response Handling
        Task<bool> ProcessParentResponseAsync(ParentVaccinationResponseDTO dto);
        Task<List<ParentVaccinationResponseDTO>> GetParentResponsesForEventAsync(int eventId);
        Task<string> FillEmailTemplateData(string email, VaccinationEventDTO eventInfo, string baseUrl);

        // Statistics
        Task<Dictionary<string, int>> GetEventStatisticsAsync(int eventId);
        Task<List<VaccinationEventDTO>> GetEventsWithStatisticsAsync();
        Task<List<StudentVaccinationStatusDTO>?> GetStudentByParentEmailAsync(string email, int eventId);


    }
} 
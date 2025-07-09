using DataAccessLayer.DTO;
using System;
using DataAccessLayer.Entity;
using DataAccessLayer.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;
using DataAccessLayer.Ultis;
namespace DataAccessLayer.Repository
{
    public class VaccinationEventRepository : GenericRepository<VaccinationEvent>, IVaccinationEventRepository
{    

        private readonly SchoolMedicalSystemContext _context;

        public VaccinationEventRepository(SchoolMedicalSystemContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<VaccinationEvent>> GetAllActiveEventsAsync()
        {
            return await _context.VaccinationEvents
                .Where(e => !e.IsDeleted)
                .OrderByDescending(e => e.EventDate)
                .ToListAsync();
        }

        public async Task<VaccinationEvent?> GetEventWithRecordsAsync(int eventId)
        {
            return await _context.VaccinationEvents
                .Include(e => e.StudentVaccinationRecords)
                    .ThenInclude(r => r.Student)
                        .ThenInclude(s => s.Parent)
                .Include(e => e.StudentVaccinationRecords)
                    .ThenInclude(r => r.Student)
                        .ThenInclude(s => s.Class)
                .FirstOrDefaultAsync(e => e.EventId == eventId && !e.IsDeleted);
        }

        public async Task<List<VaccinationEvent>> GetEventsByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            return await _context.VaccinationEvents
                .Where(e => !e.IsDeleted && e.EventDate >= startDate && e.EventDate <= endDate)
                .OrderBy(e => e.EventDate)
                .ToListAsync();
        }

        public async Task<List<VaccinationEvent>> GetUpcomingEventsAsync()
        {
            return await _context.VaccinationEvents
                .Where(e => !e.IsDeleted && e.EventDate > DateTime.Now)
                .OrderBy(e => e.EventDate)
                .ToListAsync();
        }

        public async Task<bool> EventExistsAsync(int eventId)
        {
            return await _context.VaccinationEvents
                .AnyAsync(e => e.EventId == eventId && !e.IsDeleted);
        }

        public async Task<List<StudentVaccinationRecord>> GetRecordsByEventAsync(int eventId)
        {
            return await _context.StudentVaccinationRecords
                .Include(r => r.Student)
                    .ThenInclude(s => s.Parent)
                .Include(r => r.Student)
                    .ThenInclude(s => s.Class)
                .Where(r => r.StudentVaccinationId == eventId && !r.IsDeleted)
                .ToListAsync();
        }

        public async Task<List<StudentVaccinationRecord>> GetRecordsByStudentAsync(int studentId)
        {
            return await _context.StudentVaccinationRecords
                .Include(r => r.Event)
                .Where(r => r.StudentId == studentId && !r.IsDeleted)
                .OrderByDescending(r => r.ConsentDate)
                .ToListAsync();
        }

        public async Task<StudentVaccinationRecord?> GetRecordByStudentAndEventAsync(int studentId, int eventId)
        {
            return await _context.StudentVaccinationRecords
                .Include(r => r.Student)
                    .ThenInclude(s => s.Parent)
                .Include(r => r.Event)
                .FirstOrDefaultAsync(r => r.StudentId == studentId && r.EventId == eventId && !r.IsDeleted);
        }

        public async Task<List<StudentVaccinationStatusDTO>> GetStudentResponsesForEventAsync(int eventId)
        {
            var query = from s in _context.Students
                       join p in _context.Parents on s.Parentid equals p.Parentid
                       join c in _context.Classrooms on s.Classid equals c.Classid
                       join vr in _context.StudentVaccinationRecords on s.Studentid equals vr.StudentId into vrGroup
                       from vr in vrGroup.DefaultIfEmpty()
                       where !s.IsDeleted && !p.IsDeleted && !c.IsDeleted
                       && (vr == null || (vr.EventId == eventId && !vr.IsDeleted))
                       select new StudentVaccinationStatusDTO
                       {
                           StudentId = s.Studentid,
                           ParentId = p.Parentid,
                           StudentName = s.Fullname,
                           ParentName = p.Fullname,
                           ParentEmail = p.Email ?? "",
                           ClassName = c.Classname,
                           ParentalConsentStatus = vr.ParentalConsentStatus,
                           ReasonForDecline = vr.Reasonfordecline,
                           ResponseDate = vr.Responsedate,
                           Status = vr == null ? "Pending" :
                                   vr.Willattend == true ? "Confirmed" :
                                   vr.Willattend == false ? "Declined" : "Pending"
                       };

            return await query.ToListAsync();
        }

        public async Task<VaccinationEventSummaryDTO> GetEventSummaryAsync(int eventId)
        {
            var eventInfo = await _context.Vaccinationevents
                .Where(e => e.EventId == eventId && !e.Isdeleted)
                .Select(e => new { e.Vaccinationeventname, e.Eventdate, e.Location })
                .FirstOrDefaultAsync();

            if (eventInfo == null)
                return null!;

            var studentResponses = await GetStudentResponsesForEventAsync(eventId);
            var totalStudents = studentResponses.Count;
            var confirmedCount = studentResponses.Count(s => s.Status == "Confirmed");
            var declinedCount = studentResponses.Count(s => s.Status == "Declined");
            var pendingCount = studentResponses.Count(s => s.Status == "Pending");

            return new VaccinationEventSummaryDTO
            {
                VaccinationEventId = eventId,
                VaccinationEventName = eventInfo.Vaccinationeventname,
                EventDate = eventInfo.Eventdate,
                Location = eventInfo.Location,
                TotalStudents = totalStudents,
                ConfirmedCount = confirmedCount,
                DeclinedCount = declinedCount,
                PendingCount = pendingCount,
                ConfirmationRate = totalStudents > 0 ? (double)confirmedCount / totalStudents * 100 : 0,
                StudentResponses = studentResponses
            };
        }

        public async Task<List<Parent>> GetParentsForEventAsync(int eventId)
        {
            return await _context.Students
                .Where(s => s.IsDeleted == false)
                .Select(s => s.Parent)
                .Where(p => p != null && p.IsDeleted == false)
                .Distinct()
                .ToListAsync();
        }

        public async Task<int> GetTotalStudentsForEventAsync(int eventId)
        {
            return await _context.Students
                .Where(s => s.IsDeleted == false)
                .CountAsync();
        }

        public async Task<List<Student>> GetStudentsForEventAsync(int eventId)
        {
            return await _context.Students
                .Include(s => s.Parent)
                .Include(s => s.Class)
                .Where(s => s.IsDeleted == false)
                .ToListAsync();
        }

        public async Task<int> GetConfirmedCountAsync(int eventId)
        {
            return await _context.StudentVaccinationRecords
                .CountAsync(r => r.EventId == eventId && r.ParentalConsentStatus == Constants.FormStatus.Accepted  && !r.IsDeleted);
        }

        public async Task<int> GetDeclinedCountAsync(int eventId)
        {
            return await _context.StudentVaccinationRecords
                .CountAsync(r => r.EventId == eventId && r.Willattend == false && !r.Isdeleted);
        }

        public async Task<int> GetPendingCountAsync(int eventId)
        {
            var totalStudents = await _context.Students.CountAsync(s => !s.IsDeleted);
            var respondedStudents = await _context.StudentVaccinationRecords
                .Where(r => r.EventId == eventId && !r.Isdeleted)
                .Select(r => r.Studentid)
                .Distinct()
                .CountAsync();

            return totalStudents - respondedStudents;
        }
    }
} 
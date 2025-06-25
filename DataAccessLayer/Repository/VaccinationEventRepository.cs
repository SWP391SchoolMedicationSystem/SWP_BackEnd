using DataAccessLayer.DTO;
using System;
using DataAccessLayer.Entity;
using DataAccessLayer.IRepository;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repository
{
    public class VaccinationEventRepository : GenericRepository<Vaccinationevent>, IVaccinationEventRepository
    {
        private readonly SchoolMedicalSystemContext _context;

        public VaccinationEventRepository(SchoolMedicalSystemContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Vaccinationevent>> GetAllActiveEventsAsync()
        {
            return await _context.Vaccinationevents
                .Where(e => !e.Isdeleted)
                .OrderByDescending(e => e.EventDate)
                .ToListAsync();
        }

        public async Task<Vaccinationevent?> GetEventWithRecordsAsync(int eventId)
        {
            return await _context.Vaccinationevents
                .Include(e => e.Vaccinationrecords)
                    .ThenInclude(r => r.Student)
                        .ThenInclude(s => s.Parent)
                .Include(e => e.Vaccinationrecords)
                    .ThenInclude(r => r.Student)
                        .ThenInclude(s => s.Class)
                .FirstOrDefaultAsync(e => e.Vaccinationeventid == eventId && !e.Isdeleted);
        }

        public async Task<List<Vaccinationevent>> GetEventsByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            return await _context.Vaccinationevents
                .Where(e => !e.Isdeleted && e.EventDate >= startDate && e.EventDate <= endDate)
                .OrderBy(e => e.EventDate)
                .ToListAsync();
        }

        public async Task<List<Vaccinationevent>> GetUpcomingEventsAsync()
        {
            return await _context.Vaccinationevents
                .Where(e => !e.Isdeleted && e.EventDate > DateTime.Now)
                .OrderBy(e => e.EventDate)
                .ToListAsync();
        }

        public async Task<bool> EventExistsAsync(int eventId)
        {
            return await _context.Vaccinationevents
                .AnyAsync(e => e.Vaccinationeventid == eventId && !e.Isdeleted);
        }

        public async Task<List<Vaccinationrecord>> GetRecordsByEventAsync(int eventId)
        {
            return await _context.Vaccinationrecords
                .Include(r => r.Student)
                    .ThenInclude(s => s.Parent)
                .Include(r => r.Student)
                    .ThenInclude(s => s.Class)
                .Where(r => r.Vaccinationeventid == eventId && !r.Isdeleted)
                .ToListAsync();
        }

        public async Task<List<Vaccinationrecord>> GetRecordsByStudentAsync(int studentId)
        {
            return await _context.Vaccinationrecords
                .Include(r => r.Vaccinationevent)
                .Where(r => r.Studentid == studentId && !r.Isdeleted)
                .OrderByDescending(r => r.Vaccinationdate)
                .ToListAsync();
        }

        public async Task<Vaccinationrecord?> GetRecordByStudentAndEventAsync(int studentId, int eventId)
        {
            return await _context.Vaccinationrecords
                .Include(r => r.Student)
                    .ThenInclude(s => s.Parent)
                .Include(r => r.Vaccinationevent)
                .FirstOrDefaultAsync(r => r.Studentid == studentId && r.Vaccinationeventid == eventId && !r.Isdeleted);
        }

        public async Task<List<StudentVaccinationStatusDTO>> GetStudentResponsesForEventAsync(int eventId)
        {
            var query = from s in _context.Students
                       join p in _context.Parents on s.Parentid equals p.Parentid
                       join c in _context.Classrooms on s.Classid equals c.Classid
                       join vr in _context.Vaccinationrecords on s.Studentid equals vr.Studentid into vrGroup
                       from vr in vrGroup.DefaultIfEmpty()
                       where !s.IsDeleted && !p.IsDeleted && !c.IsDeleted
                       && (vr == null || (vr.Vaccinationeventid == eventId && !vr.Isdeleted))
                       select new StudentVaccinationStatusDTO
                       {
                           StudentId = s.Studentid,
                           ParentId = p.Parentid,
                           StudentName = s.Fullname,
                           ParentName = p.Fullname,
                           ParentEmail = p.Email ?? "",
                           ClassName = c.Classname,
                           WillAttend = vr.WillAttend,
                           ReasonForDecline = vr.ReasonForDecline,
                           ResponseDate = vr.ResponseDate,
                           Status = vr == null ? "Pending" :
                                   vr.WillAttend == true ? "Confirmed" :
                                   vr.WillAttend == false ? "Declined" : "Pending"
                       };

            return await query.ToListAsync();
        }

        public async Task<VaccinationEventSummaryDTO> GetEventSummaryAsync(int eventId)
        {
            var eventInfo = await _context.Vaccinationevents
                .Where(e => e.Vaccinationeventid == eventId && !e.Isdeleted)
                .Select(e => new { e.Vaccinationeventname, e.EventDate, e.Location })
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
                EventDate = eventInfo.EventDate,
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
            return await _context.Vaccinationrecords
                .CountAsync(r => r.Vaccinationeventid == eventId && r.WillAttend == true && !r.Isdeleted);
        }

        public async Task<int> GetDeclinedCountAsync(int eventId)
        {
            return await _context.Vaccinationrecords
                .CountAsync(r => r.Vaccinationeventid == eventId && r.WillAttend == false && !r.Isdeleted);
        }

        public async Task<int> GetPendingCountAsync(int eventId)
        {
            var totalStudents = await _context.Students.CountAsync(s => !s.IsDeleted);
            var respondedStudents = await _context.Vaccinationrecords
                .Where(r => r.Vaccinationeventid == eventId && !r.Isdeleted)
                .Select(r => r.Studentid)
                .Distinct()
                .CountAsync();

            return totalStudents - respondedStudents;
        }
    }
} 
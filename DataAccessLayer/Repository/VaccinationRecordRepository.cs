using System;
using DataAccessLayer.Entity;
using DataAccessLayer.IRepository;
using Microsoft.EntityFrameworkCore;
using static DataAccessLayer.Ultis.Constants;

namespace DataAccessLayer.Repository
{
    public class VaccinationRecordRepository : GenericRepository<StudentVaccinationRecord>, IVaccinationRecordRepository
    {
        private readonly SchoolMedicalSystemContext _context;

        public VaccinationRecordRepository(SchoolMedicalSystemContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<StudentVaccinationRecord>> GetRecordsByEventAsync(int eventId)
        {
            return await _context.StudentVaccinationRecords
                .Include(r => r.Student)
                    .ThenInclude(s => s.Parent)
                .Include(r => r.Student)
                    .ThenInclude(s => s.Class)
                .Where(r => r.EventId == eventId && !r.IsDeleted)
                .ToListAsync();
        }

        public async Task<List<StudentVaccinationRecord>> GetRecordsByStudentAsync(int studentId)
        {
            return await _context.StudentVaccinationRecords
                .Include(r => r.Event)
                .Where(r => r.StudentId == studentId && !r.IsDeleted)
                .OrderByDescending(r => r.DateAdministered)
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

        public async Task<int> GetConfirmedCountAsync(int eventId)
        {
            return await _context.StudentVaccinationRecords
                .CountAsync(r => r.EventId == eventId && r.ParentalConsentStatus == FormStatus.Accepted && !r.IsDeleted);
        }

        public async Task<int> GetDeclinedCountAsync(int eventId)
        {
            return await _context.StudentVaccinationRecords
                .CountAsync(r => r.EventId == eventId && r.ParentalConsentStatus == FormStatus.Rejected && !r.IsDeleted);
        }

        public async Task<int> GetPendingCountAsync(int eventId)
        {
            return await _context.StudentVaccinationRecords
                .CountAsync(r => r.EventId == eventId && r.ParentalConsentStatus == FormStatus.Pending && !r.IsDeleted);
        }
    }
} 
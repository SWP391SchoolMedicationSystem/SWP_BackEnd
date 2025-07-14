using System;
using DataAccessLayer.Entity;
using DataAccessLayer.IRepository;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repository
{
    public class VaccinationRecordRepository : GenericRepository<Vaccinationrecord>, IVaccinationRecordRepository
    {
        private readonly SchoolMedicalSystemContext _context;

        public VaccinationRecordRepository(SchoolMedicalSystemContext context) : base(context)
        {
            _context = context;
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

        //public async Task<

        public async Task<int> GetConfirmedCountAsync(int eventId)
        {
            return await _context.Vaccinationrecords
                .CountAsync(r => r.Vaccinationeventid == eventId && r.Willattend == true && !r.Isdeleted);
        }

        public async Task<int> GetDeclinedCountAsync(int eventId)
        {
            return await _context.Vaccinationrecords
                .CountAsync(r => r.Vaccinationeventid == eventId && r.Willattend == false && !r.Isdeleted);
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
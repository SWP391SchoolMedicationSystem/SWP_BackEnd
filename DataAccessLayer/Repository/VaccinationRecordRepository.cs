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
                .Where(r => r.Studentid == studentId && !r.Isdeleted && r.IsDone)
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

        public async Task CreateRecordForAll(Vaccinationevent vaccineEvent)
        {
            var students = await _context.Students
                .Where(s => !s.IsDeleted)
                .ToListAsync();
            foreach (var student in students)
            {
                var existingRecord = await GetRecordByStudentAndEventAsync(student.Studentid, vaccineEvent.Vaccinationeventid);
                if (existingRecord == null)
                {
                    var newRecord = new Vaccinationrecord
                    {
                        Studentid = student.Studentid,
                        Vaccinationeventid = vaccineEvent.Vaccinationeventid,
                        IsDone = false,
                        Willattend = null,
                        Confirmedbyparent = false,
                        Vaccinename = "To be determined",
                        Dosenumber = 0,
                        Vaccinationdate = DateOnly.FromDateTime(vaccineEvent.Eventdate),
                        Isdeleted = false
                    };
                    await AddAsync(newRecord);
                }
            }

            await SaveChangesAsync();
        }

        public async Task CreateRecordForSpecificStudent(Vaccinationevent vaccineEvent, int parentID)
        {
            var students = await _context.Students.Where(s => s.Parentid == parentID && !s.IsDeleted).ToListAsync();
            foreach(var student in students)
            {
                var existingRecord = await GetRecordByStudentAndEventAsync(student.Studentid, vaccineEvent.Vaccinationeventid);
                if (existingRecord == null)
                {
                    var newRecord = new Vaccinationrecord
                    {
                        Studentid = student.Studentid,
                        Vaccinationeventid = vaccineEvent.Vaccinationeventid,
                        IsDone = false,
                        Willattend = null,
                        Confirmedbyparent = false,
                        Vaccinename = "To be determined",
                        Dosenumber = 0,
                        Vaccinationdate = DateOnly.FromDateTime(vaccineEvent.Eventdate),
                        Createdat = DateTime.Now,
                        Updatedat = DateTime.Now,
                        Createdby = "System",
                        Updatedby = "System",
                        Isdeleted = false
                    };
                    await AddAsync(newRecord);
                }
            }

            await SaveChangesAsync();
        }
    }
} 
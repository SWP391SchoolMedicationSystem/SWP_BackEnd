using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Entity;
using DataAccessLayer.IRepository;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repository
{
    public class UserRepository(SchoolMedicalSystemContext context) : GenericRepository<User>(context), IUserRepository
    {
        private readonly DbSet<User> _dbset = context.Set<User>();

        public new Task<List<User>> GetAllAsync()
        {
            return _dbset
.Include(u => u.AccidentReportCreatedByUsers)
.Include(u => u.AccidentReportModifiedByUsers)
.Include(u => u.BlogCreatedByUsers)
.Include(u => u.BlogModifiedByUsers)
.Include(u => u.ClassroomCreatedByUsers)
.Include(u => u.ClassroomModifiedByUsers)
.Include(u => u.EmailTemplateCreatedByUsers)
.Include(u => u.EmailTemplateModifiedByUsers)
.Include(u => u.FormCreatedByUsers)
.Include(u => u.FormModifiedByUsers)
.Include(u => u.FormSubmissionCategoryCreatedByUsers)
.Include(u => u.FormSubmissionCategoryModifiedByUsers)
.Include(u => u.HealthRecordCategoryCreatedByUsers)
.Include(u => u.HealthRecordCategoryModifiedByUsers)
.Include(u => u.HealthRecordCreatedByUsers)
.Include(u => u.HealthRecordModifiedByUsers)
.Include(u => u.HealthcheckCreatedByUsers)
.Include(u => u.HealthcheckModifiedByUsers)
.Include(u => u.MedicineCatalogCreatedByUsers)
.Include(u => u.MedicineCatalogModifiedByUsers)
.Include(u => u.MedicineCategoryCreatedByUsers)
.Include(u => u.MedicineCategoryModifiedByUsers)
.Include(u => u.MedicineScheduleLinkCreatedByUsers)
.Include(u => u.MedicineScheduleLinkModifiedByUsers)
.Include(u => u.MedicineStorageCreatedByUsers)
.Include(u => u.MedicineStorageModifiedByUsers)
.Include(u => u.NotificationCreatedByUsers)
.Include(u => u.NotificationModifiedByUsers)
.Include(u => u.NotificationParentDetailCreatedByUsers)
.Include(u => u.NotificationParentDetailModifiedByUsers)
.Include(u => u.NotificationstaffdetailCreatedByUsers)
.Include(u => u.NotificationstaffdetailModifiedByUsers)
.Include(u => u.ParentCreatedByUsers)
.Include(u => u.ParentModifiedByUsers)
.Include(u => u.ParentUsers)
.Include(u => u.PersonalmedicineCreatedByUsers)
.Include(u => u.PersonalmedicineModifiedByUsers)
.Include(u => u.RoleCreatedByUsers)
.Include(u => u.RoleModifiedByUsers)
.Include(u => u.ScheduleDetailCreatedByUsers)
.Include(u => u.ScheduleDetailModifiedByUsers)
.Include(u => u.SpecialNeedsCategoryCreatedByUsers)
.Include(u => u.SpecialNeedsCategoryModifiedByUsers)
.Include(u => u.StaffCreatedByUsers)
.Include(u => u.StaffModifiedByUsers)
.Include(u => u.StaffUsers)
.Include(u => u.StudentCreatedByUsers)
.Include(u => u.StudentModifiedByUsers)
.Include(u => u.StudentSpecialNeedCreatedByUsers)
.Include(u => u.StudentSpecialNeedModifiedByUsers)
.Include(u => u.StudentVaccinationRecordCreatedByUsers)
.Include(u => u.StudentVaccinationRecordModifiedByUsers)
.Include(u => u.VaccinationEventCreatedByUsers)
.Include(u => u.VaccinationEventModifiedByUsers)
.Include(u => u.VaccineCreatedByUsers)
.Include(u => u.VaccineModifiedByUsers)
                .ToListAsync();
        }
    }
}

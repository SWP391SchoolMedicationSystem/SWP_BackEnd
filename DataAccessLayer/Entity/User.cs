using System;
using System.Collections.Generic;

namespace DataAccessLayer.Entity;

public partial class User
{
    public int UserId { get; set; }

    public bool IsStaff { get; set; }

    public string Email { get; set; } = null!;

    public byte[] Hash { get; set; } = null!;

    public byte[] Salt { get; set; } = null!;

    public bool IsDeleted { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? ModifiedAt { get; set; }
    #region Collections
    public virtual ICollection<AccidentReport> AccidentReportCreatedByUsers { get; set; } = new List<AccidentReport>();

    public virtual ICollection<AccidentReport> AccidentReportModifiedByUsers { get; set; } = new List<AccidentReport>();

    public virtual ICollection<Blog> BlogCreatedByUsers { get; set; } = new List<Blog>();

    public virtual ICollection<Blog> BlogModifiedByUsers { get; set; } = new List<Blog>();

    public virtual ICollection<Classroom> ClassroomCreatedByUsers { get; set; } = new List<Classroom>();

    public virtual ICollection<Classroom> ClassroomModifiedByUsers { get; set; } = new List<Classroom>();

    public virtual ICollection<EmailTemplate> EmailTemplateCreatedByUsers { get; set; } = new List<EmailTemplate>();

    public virtual ICollection<EmailTemplate> EmailTemplateModifiedByUsers { get; set; } = new List<EmailTemplate>();

    public virtual ICollection<Form> FormCreatedByUsers { get; set; } = new List<Form>();

    public virtual ICollection<Form> FormModifiedByUsers { get; set; } = new List<Form>();

    public virtual ICollection<FormSubmissionCategory> FormSubmissionCategoryCreatedByUsers { get; set; } = new List<FormSubmissionCategory>();

    public virtual ICollection<FormSubmissionCategory> FormSubmissionCategoryModifiedByUsers { get; set; } = new List<FormSubmissionCategory>();

    public virtual ICollection<HealthRecordCategory> HealthRecordCategoryCreatedByUsers { get; set; } = new List<HealthRecordCategory>();

    public virtual ICollection<HealthRecordCategory> HealthRecordCategoryModifiedByUsers { get; set; } = new List<HealthRecordCategory>();

    public virtual ICollection<HealthRecord> HealthRecordCreatedByUsers { get; set; } = new List<HealthRecord>();

    public virtual ICollection<HealthRecord> HealthRecordModifiedByUsers { get; set; } = new List<HealthRecord>();

    public virtual ICollection<Healthcheck> HealthcheckCreatedByUsers { get; set; } = new List<Healthcheck>();

    public virtual ICollection<Healthcheck> HealthcheckModifiedByUsers { get; set; } = new List<Healthcheck>();

    public virtual ICollection<MedicineCatalog> MedicineCatalogCreatedByUsers { get; set; } = new List<MedicineCatalog>();

    public virtual ICollection<MedicineCatalog> MedicineCatalogModifiedByUsers { get; set; } = new List<MedicineCatalog>();

    public virtual ICollection<MedicineCategory> MedicineCategoryCreatedByUsers { get; set; } = new List<MedicineCategory>();

    public virtual ICollection<MedicineCategory> MedicineCategoryModifiedByUsers { get; set; } = new List<MedicineCategory>();

    public virtual ICollection<MedicineScheduleLink> MedicineScheduleLinkCreatedByUsers { get; set; } = new List<MedicineScheduleLink>();

    public virtual ICollection<MedicineScheduleLink> MedicineScheduleLinkModifiedByUsers { get; set; } = new List<MedicineScheduleLink>();

    public virtual ICollection<MedicineStorage> MedicineStorageCreatedByUsers { get; set; } = new List<MedicineStorage>();

    public virtual ICollection<MedicineStorage> MedicineStorageModifiedByUsers { get; set; } = new List<MedicineStorage>();

    public virtual ICollection<Notification> NotificationCreatedByUsers { get; set; } = new List<Notification>();

    public virtual ICollection<Notification> NotificationModifiedByUsers { get; set; } = new List<Notification>();

    public virtual ICollection<NotificationParentDetail> NotificationParentDetailCreatedByUsers { get; set; } = new List<NotificationParentDetail>();

    public virtual ICollection<NotificationParentDetail> NotificationParentDetailModifiedByUsers { get; set; } = new List<NotificationParentDetail>();

    public virtual ICollection<Notificationstaffdetail> NotificationstaffdetailCreatedByUsers { get; set; } = new List<Notificationstaffdetail>();

    public virtual ICollection<Notificationstaffdetail> NotificationstaffdetailModifiedByUsers { get; set; } = new List<Notificationstaffdetail>();

    public virtual ICollection<Parent> ParentCreatedByUsers { get; set; } = new List<Parent>();

    public virtual ICollection<Parent> ParentModifiedByUsers { get; set; } = new List<Parent>();

    public virtual ICollection<Parent> ParentUsers { get; set; } = new List<Parent>();

    public virtual ICollection<Personalmedicine> PersonalmedicineCreatedByUsers { get; set; } = new List<Personalmedicine>();

    public virtual ICollection<Personalmedicine> PersonalmedicineModifiedByUsers { get; set; } = new List<Personalmedicine>();

    public virtual ICollection<Role> RoleCreatedByUsers { get; set; } = new List<Role>();

    public virtual ICollection<Role> RoleModifiedByUsers { get; set; } = new List<Role>();

    public virtual ICollection<ScheduleDetail> ScheduleDetailCreatedByUsers { get; set; } = new List<ScheduleDetail>();

    public virtual ICollection<ScheduleDetail> ScheduleDetailModifiedByUsers { get; set; } = new List<ScheduleDetail>();

    public virtual ICollection<SpecialNeedsCategory> SpecialNeedsCategoryCreatedByUsers { get; set; } = new List<SpecialNeedsCategory>();

    public virtual ICollection<SpecialNeedsCategory> SpecialNeedsCategoryModifiedByUsers { get; set; } = new List<SpecialNeedsCategory>();

    public virtual ICollection<Staff> StaffCreatedByUsers { get; set; } = new List<Staff>();

    public virtual ICollection<Staff> StaffModifiedByUsers { get; set; } = new List<Staff>();

    public virtual ICollection<Staff> StaffUsers { get; set; } = new List<Staff>();

    public virtual ICollection<Student> StudentCreatedByUsers { get; set; } = new List<Student>();

    public virtual ICollection<Student> StudentModifiedByUsers { get; set; } = new List<Student>();

    public virtual ICollection<StudentSpecialNeed> StudentSpecialNeedCreatedByUsers { get; set; } = new List<StudentSpecialNeed>();

    public virtual ICollection<StudentSpecialNeed> StudentSpecialNeedModifiedByUsers { get; set; } = new List<StudentSpecialNeed>();

    public virtual ICollection<StudentVaccinationRecord> StudentVaccinationRecordCreatedByUsers { get; set; } = new List<StudentVaccinationRecord>();

    public virtual ICollection<StudentVaccinationRecord> StudentVaccinationRecordModifiedByUsers { get; set; } = new List<StudentVaccinationRecord>();

    public virtual ICollection<VaccinationEvent> VaccinationEventCreatedByUsers { get; set; } = new List<VaccinationEvent>();

    public virtual ICollection<VaccinationEvent> VaccinationEventModifiedByUsers { get; set; } = new List<VaccinationEvent>();

    public virtual ICollection<Vaccine> VaccineCreatedByUsers { get; set; } = new List<Vaccine>();

    public virtual ICollection<Vaccine> VaccineModifiedByUsers { get; set; } = new List<Vaccine>();
#endregion
}

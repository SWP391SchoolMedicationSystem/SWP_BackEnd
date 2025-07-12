using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Entity;

public partial class SchoolMedicalSystemContext : DbContext
{
    public SchoolMedicalSystemContext()
    {
    }

    public SchoolMedicalSystemContext(DbContextOptions<SchoolMedicalSystemContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AccidentReport> AccidentReports { get; set; }

    public virtual DbSet<Blog> Blogs { get; set; }

    public virtual DbSet<Classroom> Classrooms { get; set; }

    public virtual DbSet<EmailTemplate> EmailTemplates { get; set; }

    public virtual DbSet<Form> Forms { get; set; }

    public virtual DbSet<FormSubmissionCategory> FormSubmissionCategories { get; set; }

    public virtual DbSet<HealthRecord> HealthRecords { get; set; }

    public virtual DbSet<HealthRecordCategory> HealthRecordCategories { get; set; }

    public virtual DbSet<Healthcheck> Healthchecks { get; set; }

    public virtual DbSet<MedicineCatalog> MedicineCatalogs { get; set; }

    public virtual DbSet<MedicineCategory> MedicineCategories { get; set; }

    public virtual DbSet<MedicineScheduleLink> MedicineScheduleLinks { get; set; }

    public virtual DbSet<MedicineStorage> MedicineStorages { get; set; }

    public virtual DbSet<Notification> Notifications { get; set; }

    public virtual DbSet<NotificationParentDetail> NotificationParentDetails { get; set; }

    public virtual DbSet<Notificationstaffdetail> Notificationstaffdetails { get; set; }

    public virtual DbSet<Otp> Otps { get; set; }

    public virtual DbSet<Parent> Parents { get; set; }

    public virtual DbSet<Personalmedicine> Personalmedicines { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<ScheduleDetail> ScheduleDetails { get; set; }

    public virtual DbSet<SpecialNeedsCategory> SpecialNeedsCategories { get; set; }

    public virtual DbSet<Staff> Staff { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<StudentSpecialNeed> StudentSpecialNeeds { get; set; }

    public virtual DbSet<StudentVaccinationRecord> StudentVaccinationRecords { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<VaccinationEvent> VaccinationEvents { get; set; }

    public virtual DbSet<Vaccine> Vaccines { get; set; }

    public virtual DbSet<VaccineOfferedInEvent> VaccineOfferedInEvents { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AccidentReport>(entity =>
        {
            entity.HasKey(e => e.ReportId).HasName("PK__Accident__D5BD48E5CFEA68A8");

            entity.ToTable("AccidentReport");

            entity.Property(e => e.ReportId).HasColumnName("ReportID");
            entity.Property(e => e.AccidentType).HasMaxLength(100);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.CreatedByUserId).HasColumnName("CreatedByUserID");
            entity.Property(e => e.IsDeleted).HasColumnName("IS_DELETED");
            entity.Property(e => e.ModifiedAt).HasColumnType("datetime");
            entity.Property(e => e.ModifiedByUserId).HasColumnName("ModifiedByUserID");
            entity.Property(e => e.ParentNotificationDate).HasColumnType("datetime");
            entity.Property(e => e.ReportedByStaffId).HasColumnName("ReportedByStaffID");
            entity.Property(e => e.ReviewDate).HasColumnType("datetime");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasDefaultValue("Chờ xử lý");
            entity.Property(e => e.StudentId).HasColumnName("StudentID");
            entity.Property(e => e.Title).HasMaxLength(255);

            entity.HasOne(d => d.CreatedByUser).WithMany(p => p.AccidentReportCreatedByUsers)
                .HasForeignKey(d => d.CreatedByUserId)
                .HasConstraintName("FK_AccidentReport_CreatedByUser");

            entity.HasOne(d => d.ModifiedByUser).WithMany(p => p.AccidentReportModifiedByUsers)
                .HasForeignKey(d => d.ModifiedByUserId)
                .HasConstraintName("FK_AccidentReport_ModifiedByUser");

            entity.HasOne(d => d.ReportedByStaff).WithMany(p => p.AccidentReports)
                .HasForeignKey(d => d.ReportedByStaffId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AccidentReport_Staff");

            entity.HasOne(d => d.Student).WithMany(p => p.AccidentReports)
                .HasForeignKey(d => d.StudentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AccidentReport_Student");
        });

        modelBuilder.Entity<Blog>(entity =>
        {
            entity.HasKey(e => e.BlogId).HasName("PK__BLOG__F913A29D1E578196");

            entity.ToTable("BLOG");

            entity.Property(e => e.BlogId).HasColumnName("BLOG_ID");
            entity.Property(e => e.ApprovedBy).HasColumnName("APPROVED_BY");
            entity.Property(e => e.ApprovedOn)
                .HasColumnType("datetime")
                .HasColumnName("APPROVED_ON");
            entity.Property(e => e.Content).HasColumnName("CONTENT");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.CreatedByUserId).HasColumnName("CreatedByUserID");
            entity.Property(e => e.Image)
                .HasMaxLength(255)
                .HasColumnName("IMAGE");
            entity.Property(e => e.IsDeleted).HasColumnName("IS_DELETED");
            entity.Property(e => e.ModifiedAt).HasColumnType("datetime");
            entity.Property(e => e.ModifiedByUserId).HasColumnName("ModifiedByUserID");
            entity.Property(e => e.ReasonForDecline).HasMaxLength(255);
            entity.Property(e => e.ResponseDate).HasColumnType("datetime");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasDefaultValue("Bản nháp")
                .HasColumnName("STATUS");
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .HasColumnName("TITLE");

            entity.HasOne(d => d.ApprovedByNavigation).WithMany(p => p.Blogs)
                .HasForeignKey(d => d.ApprovedBy)
                .HasConstraintName("FK_BLOG_ApprovedBy");

            entity.HasOne(d => d.CreatedByUser).WithMany(p => p.BlogCreatedByUsers)
                .HasForeignKey(d => d.CreatedByUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BLOG_CreatedByUser");

            entity.HasOne(d => d.ModifiedByUser).WithMany(p => p.BlogModifiedByUsers)
                .HasForeignKey(d => d.ModifiedByUserId)
                .HasConstraintName("FK_BLOG_ModifiedByUser");
        });

        modelBuilder.Entity<Classroom>(entity =>
        {
            entity.HasKey(e => e.Classid).HasName("PK__CLASSROO__96D40B6CC1D56AAF");

            entity.ToTable("CLASSROOM");

            entity.Property(e => e.Classid).HasColumnName("CLASSID");
            entity.Property(e => e.Classname)
                .HasMaxLength(255)
                .HasColumnName("CLASSNAME");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.CreatedByUserId).HasColumnName("CreatedByUserID");
            entity.Property(e => e.Grade)
                .HasMaxLength(50)
                .HasColumnName("GRADE");
            entity.Property(e => e.IsDeleted).HasColumnName("IS_DELETED");
            entity.Property(e => e.ModifiedAt).HasColumnType("datetime");
            entity.Property(e => e.ModifiedByUserId).HasColumnName("ModifiedByUserID");
            entity.Property(e => e.Teachername)
                .HasMaxLength(255)
                .HasColumnName("TEACHERNAME");

            entity.HasOne(d => d.CreatedByUser).WithMany(p => p.ClassroomCreatedByUsers)
                .HasForeignKey(d => d.CreatedByUserId)
                .HasConstraintName("FK_CLASSROOM_CreatedByUser");

            entity.HasOne(d => d.ModifiedByUser).WithMany(p => p.ClassroomModifiedByUsers)
                .HasForeignKey(d => d.ModifiedByUserId)
                .HasConstraintName("FK_CLASSROOM_ModifiedByUser");
        });

        modelBuilder.Entity<EmailTemplate>(entity =>
        {
            entity.HasKey(e => e.EmailTemplateId).HasName("PK__EmailTem__BC0A3875E3BEE8EA");

            entity.ToTable("EmailTemplate");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.CreatedByUserId).HasColumnName("CreatedByUserID");
            entity.Property(e => e.ModifiedAt).HasColumnType("datetime");
            entity.Property(e => e.ModifiedByUserId).HasColumnName("ModifiedByUserID");

            entity.HasOne(d => d.CreatedByUser).WithMany(p => p.EmailTemplateCreatedByUsers)
                .HasForeignKey(d => d.CreatedByUserId)
                .HasConstraintName("FK_EmailTemplate_CreatedByUser");

            entity.HasOne(d => d.ModifiedByUser).WithMany(p => p.EmailTemplateModifiedByUsers)
                .HasForeignKey(d => d.ModifiedByUserId)
                .HasConstraintName("FK_EmailTemplate_ModifiedByUser");
        });

        modelBuilder.Entity<Form>(entity =>
        {
            entity.HasKey(e => e.FormId).HasName("PK__FORM__85052F6831128D91");

            entity.ToTable("FORM");

            entity.Property(e => e.FormId).HasColumnName("FORM_ID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.CreatedByUserId).HasColumnName("CreatedByUserID");
            entity.Property(e => e.FileData).HasColumnName("FILE_DATA");
            entity.Property(e => e.FormcategoryId).HasColumnName("FORMCATEGORY_ID");
            entity.Property(e => e.ModifiedAt).HasColumnType("datetime");
            entity.Property(e => e.ModifiedByUserId).HasColumnName("ModifiedByUserID");
            entity.Property(e => e.Parentid).HasColumnName("PARENTID");
            entity.Property(e => e.Reason)
                .HasMaxLength(255)
                .HasColumnName("REASON");
            entity.Property(e => e.Reasonfordecline)
                .HasMaxLength(255)
                .HasColumnName("REASONFORDECLINE");
            entity.Property(e => e.ResponseDate).HasColumnType("datetime");
            entity.Property(e => e.Staffid).HasColumnName("STAFFID");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasDefaultValue("Chờ phê duyệt");
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .HasColumnName("TITLE");

            entity.HasOne(d => d.CreatedByUser).WithMany(p => p.FormCreatedByUsers)
                .HasForeignKey(d => d.CreatedByUserId)
                .HasConstraintName("FK_FORM_CreatedByUser");

            entity.HasOne(d => d.Formcategory).WithMany(p => p.Forms)
                .HasForeignKey(d => d.FormcategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_FORM_CATEGORY");

            entity.HasOne(d => d.ModifiedByUser).WithMany(p => p.FormModifiedByUsers)
                .HasForeignKey(d => d.ModifiedByUserId)
                .HasConstraintName("FK_FORM_ModifiedByUser");

            entity.HasOne(d => d.Parent).WithMany(p => p.Forms)
                .HasForeignKey(d => d.Parentid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_FORM_PARENT");

            entity.HasOne(d => d.Staff).WithMany(p => p.Forms)
                .HasForeignKey(d => d.Staffid)
                .HasConstraintName("FK_FORM_STAFF");
        });

        modelBuilder.Entity<FormSubmissionCategory>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__FormSubm__19093A2B327513E6");

            entity.ToTable("FormSubmissionCategory");

            entity.Property(e => e.CategoryId).HasColumnName("CategoryID");
            entity.Property(e => e.CategoryName).HasMaxLength(255);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.CreatedByUserId).HasColumnName("CreatedByUserID");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("IS_DELETED");
            entity.Property(e => e.ModifiedAt).HasColumnType("datetime");
            entity.Property(e => e.ModifiedByUserId).HasColumnName("ModifiedByUserID");

            entity.HasOne(d => d.CreatedByUser).WithMany(p => p.FormSubmissionCategoryCreatedByUsers)
                .HasForeignKey(d => d.CreatedByUserId)
                .HasConstraintName("FK_FormSubmissionCategory_CreatedByUser");

            entity.HasOne(d => d.ModifiedByUser).WithMany(p => p.FormSubmissionCategoryModifiedByUsers)
                .HasForeignKey(d => d.ModifiedByUserId)
                .HasConstraintName("FK_FormSubmissionCategory_ModifiedByUser");
        });

        modelBuilder.Entity<HealthRecord>(entity =>
        {
            entity.HasKey(e => e.HealthRecordId).HasName("PK__HealthRe__3BE0B89D47D0D0A4");

            entity.ToTable("HealthRecord");

            entity.Property(e => e.HealthRecordId).HasColumnName("HealthRecordID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.CreatedByUserId).HasColumnName("CreatedByUserID");
            entity.Property(e => e.HealthCategoryId).HasColumnName("HealthCategoryID");
            entity.Property(e => e.HealthRecordDate).HasColumnType("datetime");
            entity.Property(e => e.HealthRecordTitle).HasMaxLength(255);
            entity.Property(e => e.ModifiedAt).HasColumnType("datetime");
            entity.Property(e => e.ModifiedByUserId).HasColumnName("ModifiedByUserID");
            entity.Property(e => e.StaffId).HasColumnName("StaffID");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasDefaultValue("Đã xác nhận");
            entity.Property(e => e.StudentId).HasColumnName("StudentID");

            entity.HasOne(d => d.CreatedByUser).WithMany(p => p.HealthRecordCreatedByUsers)
                .HasForeignKey(d => d.CreatedByUserId)
                .HasConstraintName("FK_HealthRecord_CreatedByUser");

            entity.HasOne(d => d.HealthCategory).WithMany(p => p.HealthRecords)
                .HasForeignKey(d => d.HealthCategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_HEALTHRECORD_HEALTHCATEGORY");

            entity.HasOne(d => d.ModifiedByUser).WithMany(p => p.HealthRecordModifiedByUsers)
                .HasForeignKey(d => d.ModifiedByUserId)
                .HasConstraintName("FK_HealthRecord_ModifiedByUser");

            entity.HasOne(d => d.Staff).WithMany(p => p.HealthRecords)
                .HasForeignKey(d => d.StaffId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_HEALTHRECORD_STAFF");

            entity.HasOne(d => d.Student).WithMany(p => p.HealthRecords)
                .HasForeignKey(d => d.StudentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_HEALTHRECORD_STUDENT");
        });

        modelBuilder.Entity<HealthRecordCategory>(entity =>
        {
            entity.HasKey(e => e.HealthCategoryId).HasName("PK__HealthRe__2552448EAE2DC243");

            entity.ToTable("HealthRecordCategory");

            entity.Property(e => e.HealthCategoryId).HasColumnName("HealthCategoryID");
            entity.Property(e => e.CategoryDescription).HasMaxLength(255);
            entity.Property(e => e.CategoryName).HasMaxLength(100);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.CreatedByUserId).HasColumnName("CreatedByUserID");
            entity.Property(e => e.IsDeleted).HasColumnName("IS_DELETED");
            entity.Property(e => e.ModifiedAt).HasColumnType("datetime");
            entity.Property(e => e.ModifiedByUserId).HasColumnName("ModifiedByUserID");

            entity.HasOne(d => d.CreatedByUser).WithMany(p => p.HealthRecordCategoryCreatedByUsers)
                .HasForeignKey(d => d.CreatedByUserId)
                .HasConstraintName("FK_HealthRecordCategory_CreatedByUser");

            entity.HasOne(d => d.ModifiedByUser).WithMany(p => p.HealthRecordCategoryModifiedByUsers)
                .HasForeignKey(d => d.ModifiedByUserId)
                .HasConstraintName("FK_HealthRecordCategory_ModifiedByUser");
        });

        modelBuilder.Entity<Healthcheck>(entity =>
        {
            entity.HasKey(e => e.Checkid).HasName("PK__HEALTHCH__7A9DCA675744B9BE");

            entity.ToTable("HEALTHCHECK");

            entity.Property(e => e.Checkid).HasColumnName("CHECKID");
            entity.Property(e => e.Bloodpressure)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("BLOODPRESSURE");
            entity.Property(e => e.Checkdate)
                .HasColumnType("datetime")
                .HasColumnName("CHECKDATE");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.CreatedByUserId).HasColumnName("CreatedByUserID");
            entity.Property(e => e.Height)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("HEIGHT");
            entity.Property(e => e.Isdeleted).HasColumnName("ISDELETED");
            entity.Property(e => e.ModifiedAt).HasColumnType("datetime");
            entity.Property(e => e.ModifiedByUserId).HasColumnName("ModifiedByUserID");
            entity.Property(e => e.Notes)
                .HasMaxLength(1000)
                .HasColumnName("NOTES");
            entity.Property(e => e.Staffid).HasColumnName("STAFFID");
            entity.Property(e => e.Studentid).HasColumnName("STUDENTID");
            entity.Property(e => e.Visionleft)
                .HasColumnType("decimal(4, 1)")
                .HasColumnName("VISIONLEFT");
            entity.Property(e => e.Visionright)
                .HasColumnType("decimal(4, 1)")
                .HasColumnName("VISIONRIGHT");
            entity.Property(e => e.Weight)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("WEIGHT");

            entity.HasOne(d => d.CreatedByUser).WithMany(p => p.HealthcheckCreatedByUsers)
                .HasForeignKey(d => d.CreatedByUserId)
                .HasConstraintName("FK_HEALTHCHECK_CreatedByUser");

            entity.HasOne(d => d.ModifiedByUser).WithMany(p => p.HealthcheckModifiedByUsers)
                .HasForeignKey(d => d.ModifiedByUserId)
                .HasConstraintName("FK_HEALTHCHECK_ModifiedByUser");

            entity.HasOne(d => d.Staff).WithMany(p => p.Healthchecks)
                .HasForeignKey(d => d.Staffid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_HEALTHCHECK_STAFF");

            entity.HasOne(d => d.Student).WithMany(p => p.Healthchecks)
                .HasForeignKey(d => d.Studentid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_HEALTHCHECK_STUDENT");
        });

        modelBuilder.Entity<MedicineCatalog>(entity =>
        {
            entity.HasKey(e => e.MedicineId).HasName("PK__Medicine__4F2128F038AE495C");

            entity.ToTable("MedicineCatalog");

            entity.Property(e => e.MedicineId).HasColumnName("MedicineID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.CreatedByUserId).HasColumnName("CreatedByUserID");
            entity.Property(e => e.DefaultDosage).HasMaxLength(255);
            entity.Property(e => e.MedicineCategoryId).HasColumnName("MedicineCategoryID");
            entity.Property(e => e.MedicineName).HasMaxLength(255);
            entity.Property(e => e.ModifiedAt).HasColumnType("datetime");
            entity.Property(e => e.ModifiedByUserId).HasColumnName("ModifiedByUserID");

            entity.HasOne(d => d.CreatedByUser).WithMany(p => p.MedicineCatalogCreatedByUsers)
                .HasForeignKey(d => d.CreatedByUserId)
                .HasConstraintName("FK_MedicineCatalog_CreatedByUser");

            entity.HasOne(d => d.MedicineCategory).WithMany(p => p.MedicineCatalogs)
                .HasForeignKey(d => d.MedicineCategoryId)
                .HasConstraintName("FK_MedicineCatalog_Category");

            entity.HasOne(d => d.ModifiedByUser).WithMany(p => p.MedicineCatalogModifiedByUsers)
                .HasForeignKey(d => d.ModifiedByUserId)
                .HasConstraintName("FK_MedicineCatalog_ModifiedByUser");
        });

        modelBuilder.Entity<MedicineCategory>(entity =>
        {
            entity.HasKey(e => e.MedicineCategoryId).HasName("PK__Medicine__28C9BEEA82AF2934");

            entity.ToTable("MedicineCategory");

            entity.Property(e => e.MedicineCategoryId).HasColumnName("MedicineCategoryID");
            entity.Property(e => e.CategoryName).HasMaxLength(100);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.CreatedByUserId).HasColumnName("CreatedByUserID");
            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.ModifiedAt).HasColumnType("datetime");
            entity.Property(e => e.ModifiedByUserId).HasColumnName("ModifiedByUserID");

            entity.HasOne(d => d.CreatedByUser).WithMany(p => p.MedicineCategoryCreatedByUsers)
                .HasForeignKey(d => d.CreatedByUserId)
                .HasConstraintName("FK_MedicineCategory_CreatedByUser");

            entity.HasOne(d => d.ModifiedByUser).WithMany(p => p.MedicineCategoryModifiedByUsers)
                .HasForeignKey(d => d.ModifiedByUserId)
                .HasConstraintName("FK_MedicineCategory_ModifiedByUser");
        });

        modelBuilder.Entity<MedicineScheduleLink>(entity =>
        {
            entity.HasKey(e => e.MedicineScheduleLinkId).HasName("PK__Medicine__A1BCB6BC92893C81");

            entity.ToTable("MedicineScheduleLink");

            entity.Property(e => e.MedicineScheduleLinkId).HasColumnName("MedicineScheduleLinkID");
            entity.Property(e => e.ApprovalDate).HasColumnType("datetime");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.CreatedByUserId).HasColumnName("CreatedByUserID");
            entity.Property(e => e.ModifiedAt).HasColumnType("datetime");
            entity.Property(e => e.ModifiedByUserId).HasColumnName("ModifiedByUserID");
            entity.Property(e => e.Note).HasColumnName("NOTE");
            entity.Property(e => e.PersonalMedicineId).HasColumnName("PersonalMedicineID");
            entity.Property(e => e.ReasonForDecline).HasMaxLength(255);
            entity.Property(e => e.ScheduleDetailId).HasColumnName("ScheduleDetailID");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasDefaultValue("Chờ phê duyệt");

            entity.HasOne(d => d.CreatedByUser).WithMany(p => p.MedicineScheduleLinkCreatedByUsers)
                .HasForeignKey(d => d.CreatedByUserId)
                .HasConstraintName("FK_MedicineScheduleLink_CreatedByUser");

            entity.HasOne(d => d.ModifiedByUser).WithMany(p => p.MedicineScheduleLinkModifiedByUsers)
                .HasForeignKey(d => d.ModifiedByUserId)
                .HasConstraintName("FK_MedicineScheduleLink_ModifiedByUser");

            entity.HasOne(d => d.PersonalMedicine).WithMany(p => p.MedicineScheduleLinks)
                .HasForeignKey(d => d.PersonalMedicineId)
                .HasConstraintName("FK_MedicineScheduleLink_PersonalMedicine");

            entity.HasOne(d => d.ScheduleDetail).WithMany(p => p.MedicineScheduleLinks)
                .HasForeignKey(d => d.ScheduleDetailId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MedicineScheduleLink_ScheduleDetail");
        });

        modelBuilder.Entity<MedicineStorage>(entity =>
        {
            entity.HasKey(e => e.StorageId).HasName("PK__Medicine__8A247E37424E4BAC");

            entity.ToTable("MedicineStorage");

            entity.Property(e => e.StorageId).HasColumnName("StorageID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.CreatedByUserId).HasColumnName("CreatedByUserID");
            entity.Property(e => e.LotNumber)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.MedicineId).HasColumnName("MedicineID");
            entity.Property(e => e.ModifiedAt).HasColumnType("datetime");
            entity.Property(e => e.ModifiedByUserId).HasColumnName("ModifiedByUserID");
            entity.Property(e => e.Notes).HasMaxLength(500);
            entity.Property(e => e.ReceivedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.StaffId).HasColumnName("StaffID");

            entity.HasOne(d => d.CreatedByUser).WithMany(p => p.MedicineStorageCreatedByUsers)
                .HasForeignKey(d => d.CreatedByUserId)
                .HasConstraintName("FK_MedicineStorage_CreatedByUser");

            entity.HasOne(d => d.Medicine).WithMany(p => p.MedicineStorages)
                .HasForeignKey(d => d.MedicineId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MedicineStorage_Medicine");

            entity.HasOne(d => d.ModifiedByUser).WithMany(p => p.MedicineStorageModifiedByUsers)
                .HasForeignKey(d => d.ModifiedByUserId)
                .HasConstraintName("FK_MedicineStorage_ModifiedByUser");

            entity.HasOne(d => d.Staff).WithMany(p => p.MedicineStorages)
                .HasForeignKey(d => d.StaffId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MedicineStorage_Staff");
        });

        modelBuilder.Entity<Notification>(entity =>
        {
            entity.HasKey(e => e.NotificationId).HasName("PK__Notifica__20CF2E12784D97AC");

            entity.ToTable("Notification");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.CreatedByUserId).HasColumnName("CreatedByUserID");
            entity.Property(e => e.IsDeleted).HasColumnName("isDeleted");
            entity.Property(e => e.ModifiedAt).HasColumnType("datetime");
            entity.Property(e => e.ModifiedByUserId).HasColumnName("ModifiedByUserID");
            entity.Property(e => e.Title).HasMaxLength(255);
            entity.Property(e => e.Type).HasMaxLength(50);

            entity.HasOne(d => d.CreatedByUser).WithMany(p => p.NotificationCreatedByUsers)
                .HasForeignKey(d => d.CreatedByUserId)
                .HasConstraintName("FK_Notification_CreatedByUser");

            entity.HasOne(d => d.ModifiedByUser).WithMany(p => p.NotificationModifiedByUsers)
                .HasForeignKey(d => d.ModifiedByUserId)
                .HasConstraintName("FK_Notification_ModifiedByUser");
        });

        modelBuilder.Entity<NotificationParentDetail>(entity =>
        {
            entity.HasKey(e => new { e.NotificationId, e.ParentId }).HasName("PK__Notifica__CDFCBB04C82CDF9F");

            entity.ToTable("NotificationParentDetail");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.CreatedByUserId).HasColumnName("CreatedByUserID");
            entity.Property(e => e.IsDeleted).HasColumnName("isDeleted");
            entity.Property(e => e.IsRead).HasColumnName("isRead");
            entity.Property(e => e.Message).HasMaxLength(500);
            entity.Property(e => e.ModifiedAt).HasColumnType("datetime");
            entity.Property(e => e.ModifiedByUserId).HasColumnName("ModifiedByUserID");

            entity.HasOne(d => d.CreatedByUser).WithMany(p => p.NotificationParentDetailCreatedByUsers)
                .HasForeignKey(d => d.CreatedByUserId)
                .HasConstraintName("FK_NotificationParentDetail_CreatedByUser");

            entity.HasOne(d => d.ModifiedByUser).WithMany(p => p.NotificationParentDetailModifiedByUsers)
                .HasForeignKey(d => d.ModifiedByUserId)
                .HasConstraintName("FK_NotificationParentDetail_ModifiedByUser");

            entity.HasOne(d => d.Notification).WithMany(p => p.NotificationParentDetails)
                .HasForeignKey(d => d.NotificationId)
                .HasConstraintName("FK_NotificationParentDetail_Notification");

            entity.HasOne(d => d.Parent).WithMany(p => p.NotificationParentDetails)
                .HasForeignKey(d => d.ParentId)
                .HasConstraintName("FK_NotificationParentDetail_Parent");
        });

        modelBuilder.Entity<Notificationstaffdetail>(entity =>
        {
            entity.HasKey(e => new { e.NotificationId, e.Staffid }).HasName("PK__NOTIFICA__82447E718FC69771");

            entity.ToTable("NOTIFICATIONSTAFFDETAILS");

            entity.Property(e => e.Staffid).HasColumnName("STAFFID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.CreatedByUserId).HasColumnName("CreatedByUserID");
            entity.Property(e => e.IsDeleted).HasColumnName("isDeleted");
            entity.Property(e => e.IsRead).HasColumnName("isRead");
            entity.Property(e => e.Message).HasMaxLength(500);
            entity.Property(e => e.ModifiedAt).HasColumnType("datetime");
            entity.Property(e => e.ModifiedByUserId).HasColumnName("ModifiedByUserID");

            entity.HasOne(d => d.CreatedByUser).WithMany(p => p.NotificationstaffdetailCreatedByUsers)
                .HasForeignKey(d => d.CreatedByUserId)
                .HasConstraintName("FK_NOTIFICATIONSTAFFDETAILS_CreatedByUser");

            entity.HasOne(d => d.ModifiedByUser).WithMany(p => p.NotificationstaffdetailModifiedByUsers)
                .HasForeignKey(d => d.ModifiedByUserId)
                .HasConstraintName("FK_NOTIFICATIONSTAFFDETAILS_ModifiedByUser");

            entity.HasOne(d => d.Notification).WithMany(p => p.Notificationstaffdetails)
                .HasForeignKey(d => d.NotificationId)
                .HasConstraintName("FK_NOTIFICATIONSTAFFDETAILS_Notification");

            entity.HasOne(d => d.Staff).WithMany(p => p.Notificationstaffdetails)
                .HasForeignKey(d => d.Staffid)
                .HasConstraintName("FK_NOTIFICATIONSTAFFDETAILS_STAFF");
        });

        modelBuilder.Entity<Otp>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Otp__3214EC07780498F2");

            entity.ToTable("Otp");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.ExpiresAt).HasColumnType("datetime");
            entity.Property(e => e.OtpCode)
                .HasMaxLength(10)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Parent>(entity =>
        {
            entity.HasKey(e => e.Parentid).HasName("PK__PARENT__7444E66A78A1ADDE");

            entity.ToTable("PARENT");

            entity.Property(e => e.Parentid).HasColumnName("PARENTID");
            entity.Property(e => e.Address)
                .HasMaxLength(500)
                .HasColumnName("ADDRESS");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.CreatedByUserId).HasColumnName("CreatedByUserID");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("EMAIL");
            entity.Property(e => e.Fullname)
                .HasMaxLength(255)
                .HasColumnName("FULLNAME");
            entity.Property(e => e.IsDeleted).HasColumnName("IS_DELETED");
            entity.Property(e => e.ModifiedAt).HasColumnType("datetime");
            entity.Property(e => e.ModifiedByUserId).HasColumnName("ModifiedByUserID");
            entity.Property(e => e.Phone)
                .HasMaxLength(255)
                .HasColumnName("PHONE");
            entity.Property(e => e.Userid).HasColumnName("USERID");

            entity.HasOne(d => d.CreatedByUser).WithMany(p => p.ParentCreatedByUsers)
                .HasForeignKey(d => d.CreatedByUserId)
                .HasConstraintName("FK_PARENT_CreatedByUser");

            entity.HasOne(d => d.ModifiedByUser).WithMany(p => p.ParentModifiedByUsers)
                .HasForeignKey(d => d.ModifiedByUserId)
                .HasConstraintName("FK_PARENT_ModifiedByUser");

            entity.HasOne(d => d.User).WithMany(p => p.ParentUsers)
                .HasForeignKey(d => d.Userid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PARENT_USERID");
        });

        modelBuilder.Entity<Personalmedicine>(entity =>
        {
            entity.HasKey(e => e.Personalmedicineid).HasName("PK__PERSONAL__E0FDAEFF5BE70DEF");

            entity.ToTable("PERSONALMEDICINE");

            entity.Property(e => e.Personalmedicineid).HasColumnName("PERSONALMEDICINEID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.CreatedByUserId).HasColumnName("CreatedByUserID");
            entity.Property(e => e.DeliveryStatus)
                .HasMaxLength(50)
                .HasDefaultValue("Chờ giao");
            entity.Property(e => e.Expirydate)
                .HasColumnType("datetime")
                .HasColumnName("EXPIRYDATE");
            entity.Property(e => e.Isdeleted).HasColumnName("ISDELETED");
            entity.Property(e => e.Medicinename)
                .HasMaxLength(100)
                .HasColumnName("MEDICINENAME");
            entity.Property(e => e.ModifiedAt).HasColumnType("datetime");
            entity.Property(e => e.ModifiedByUserId).HasColumnName("ModifiedByUserID");
            entity.Property(e => e.Note).HasColumnName("NOTE");
            entity.Property(e => e.Parentid).HasColumnName("PARENTID");
            entity.Property(e => e.Quantity).HasColumnName("QUANTITY");
            entity.Property(e => e.Receivedate)
                .HasColumnType("datetime")
                .HasColumnName("RECEIVEDATE");
            entity.Property(e => e.Staffid).HasColumnName("STAFFID");
            entity.Property(e => e.Studentid).HasColumnName("STUDENTID");

            entity.HasOne(d => d.CreatedByUser).WithMany(p => p.PersonalmedicineCreatedByUsers)
                .HasForeignKey(d => d.CreatedByUserId)
                .HasConstraintName("FK_PERSONALMEDICINE_CreatedByUser");

            entity.HasOne(d => d.ModifiedByUser).WithMany(p => p.PersonalmedicineModifiedByUsers)
                .HasForeignKey(d => d.ModifiedByUserId)
                .HasConstraintName("FK_PERSONALMEDICINE_ModifiedByUser");

            entity.HasOne(d => d.Parent).WithMany(p => p.Personalmedicines)
                .HasForeignKey(d => d.Parentid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PERSONALMEDICINE_PARENT");

            entity.HasOne(d => d.Staff).WithMany(p => p.Personalmedicines)
                .HasForeignKey(d => d.Staffid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PERSONALMEDICINE_STAFF");

            entity.HasOne(d => d.Student).WithMany(p => p.Personalmedicines)
                .HasForeignKey(d => d.Studentid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PERSONALMEDICINE_STUDENT");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Roleid).HasName("PK__ROLE__006568E992846827");

            entity.ToTable("ROLE");

            entity.Property(e => e.Roleid).HasColumnName("ROLEID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.CreatedByUserId).HasColumnName("CreatedByUserID");
            entity.Property(e => e.IsDeleted).HasColumnName("IS_DELETED");
            entity.Property(e => e.ModifiedAt).HasColumnType("datetime");
            entity.Property(e => e.ModifiedByUserId).HasColumnName("ModifiedByUserID");
            entity.Property(e => e.Rolename)
                .HasMaxLength(50)
                .HasColumnName("ROLENAME");

            entity.HasOne(d => d.CreatedByUser).WithMany(p => p.RoleCreatedByUsers)
                .HasForeignKey(d => d.CreatedByUserId)
                .HasConstraintName("FK_ROLE_CreatedByUser");

            entity.HasOne(d => d.ModifiedByUser).WithMany(p => p.RoleModifiedByUsers)
                .HasForeignKey(d => d.ModifiedByUserId)
                .HasConstraintName("FK_ROLE_ModifiedByUser");
        });

        modelBuilder.Entity<ScheduleDetail>(entity =>
        {
            entity.HasKey(e => e.ScheduleDetailId).HasName("PK__Schedule__921C9F75BC8B45F9");

            entity.ToTable("ScheduleDetail");

            entity.Property(e => e.ScheduleDetailId).HasColumnName("ScheduleDetailID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.CreatedByUserId).HasColumnName("CreatedByUserID");
            entity.Property(e => e.ModifiedAt).HasColumnType("datetime");
            entity.Property(e => e.ModifiedByUserId).HasColumnName("ModifiedByUserID");
            entity.Property(e => e.Notes).HasMaxLength(255);

            entity.HasOne(d => d.CreatedByUser).WithMany(p => p.ScheduleDetailCreatedByUsers)
                .HasForeignKey(d => d.CreatedByUserId)
                .HasConstraintName("FK_ScheduleDetail_CreatedByUser");

            entity.HasOne(d => d.ModifiedByUser).WithMany(p => p.ScheduleDetailModifiedByUsers)
                .HasForeignKey(d => d.ModifiedByUserId)
                .HasConstraintName("FK_ScheduleDetail_ModifiedByUser");
        });

        modelBuilder.Entity<SpecialNeedsCategory>(entity =>
        {
            entity.HasKey(e => e.SpecialNeedCategoryId).HasName("PK__SpecialN__BAED81C0342AE767");

            entity.ToTable("SpecialNeedsCategory");

            entity.Property(e => e.SpecialNeedCategoryId).HasColumnName("SpecialNeedCategoryID");
            entity.Property(e => e.CategoryName).HasMaxLength(150);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.CreatedByUserId).HasColumnName("CreatedByUserID");
            entity.Property(e => e.ModifiedAt).HasColumnType("datetime");
            entity.Property(e => e.ModifiedByUserId).HasColumnName("ModifiedByUserID");

            entity.HasOne(d => d.CreatedByUser).WithMany(p => p.SpecialNeedsCategoryCreatedByUsers)
                .HasForeignKey(d => d.CreatedByUserId)
                .HasConstraintName("FK_SpecialNeedsCategory_CreatedByUser");

            entity.HasOne(d => d.ModifiedByUser).WithMany(p => p.SpecialNeedsCategoryModifiedByUsers)
                .HasForeignKey(d => d.ModifiedByUserId)
                .HasConstraintName("FK_SpecialNeedsCategory_ModifiedByUser");
        });

        modelBuilder.Entity<Staff>(entity =>
        {
            entity.HasKey(e => e.Staffid).HasName("PK__STAFF__28B5063BF1EAB3AA");

            entity.ToTable("STAFF");

            entity.Property(e => e.Staffid).HasColumnName("STAFFID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.CreatedByUserId).HasColumnName("CreatedByUserID");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("EMAIL");
            entity.Property(e => e.Fullname)
                .HasMaxLength(255)
                .HasColumnName("FULLNAME");
            entity.Property(e => e.IsDeleted).HasColumnName("IS_DELETED");
            entity.Property(e => e.ModifiedAt).HasColumnType("datetime");
            entity.Property(e => e.ModifiedByUserId).HasColumnName("ModifiedByUserID");
            entity.Property(e => e.Phone)
                .HasMaxLength(255)
                .HasColumnName("PHONE");
            entity.Property(e => e.Roleid).HasColumnName("ROLEID");
            entity.Property(e => e.Userid).HasColumnName("USERID");

            entity.HasOne(d => d.CreatedByUser).WithMany(p => p.StaffCreatedByUsers)
                .HasForeignKey(d => d.CreatedByUserId)
                .HasConstraintName("FK_STAFF_CreatedByUser");

            entity.HasOne(d => d.ModifiedByUser).WithMany(p => p.StaffModifiedByUsers)
                .HasForeignKey(d => d.ModifiedByUserId)
                .HasConstraintName("FK_STAFF_ModifiedByUser");

            entity.HasOne(d => d.Role).WithMany(p => p.Staff)
                .HasForeignKey(d => d.Roleid)
                .HasConstraintName("FK_STAFF_ROLE");

            entity.HasOne(d => d.User).WithMany(p => p.StaffUsers)
                .HasForeignKey(d => d.Userid)
                .HasConstraintName("FK_STAFF_USER");
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.Studentid).HasName("PK__STUDENT__495196F03FA68415");

            entity.ToTable("STUDENT");

            entity.HasIndex(e => e.StudentCode, "UQ__STUDENT__7BC2836067B51705").IsUnique();

            entity.Property(e => e.Studentid).HasColumnName("STUDENTID");
            entity.Property(e => e.Age).HasColumnName("AGE");
            entity.Property(e => e.BloodType)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("BLOOD_TYPE");
            entity.Property(e => e.Classid).HasColumnName("CLASSID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.CreatedByUserId).HasColumnName("CreatedByUserID");
            entity.Property(e => e.Dob).HasColumnName("DOB");
            entity.Property(e => e.Fullname)
                .HasMaxLength(255)
                .HasColumnName("FULLNAME");
            entity.Property(e => e.Gender).HasColumnName("GENDER");
            entity.Property(e => e.IsDeleted).HasColumnName("IS_DELETED");
            entity.Property(e => e.ModifiedAt).HasColumnType("datetime");
            entity.Property(e => e.ModifiedByUserId).HasColumnName("ModifiedByUserID");
            entity.Property(e => e.Parentid).HasColumnName("PARENTID");
            entity.Property(e => e.StudentCode)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("STUDENT_CODE");

            entity.HasOne(d => d.Class).WithMany(p => p.Students)
                .HasForeignKey(d => d.Classid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_STUDENT_CLASSID");

            entity.HasOne(d => d.CreatedByUser).WithMany(p => p.StudentCreatedByUsers)
                .HasForeignKey(d => d.CreatedByUserId)
                .HasConstraintName("FK_STUDENT_CreatedByUser");

            entity.HasOne(d => d.ModifiedByUser).WithMany(p => p.StudentModifiedByUsers)
                .HasForeignKey(d => d.ModifiedByUserId)
                .HasConstraintName("FK_STUDENT_ModifiedByUser");

            entity.HasOne(d => d.Parent).WithMany(p => p.Students)
                .HasForeignKey(d => d.Parentid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_STUDENT_PARENTID");
        });

        modelBuilder.Entity<StudentSpecialNeed>(entity =>
        {
            entity.HasKey(e => e.StudentSpecialNeedId).HasName("PK__StudentS__00E0B0F5569DCFBE");

            entity.Property(e => e.StudentSpecialNeedId).HasColumnName("StudentSpecialNeedID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.CreatedByUserId).HasColumnName("CreatedByUserID");
            entity.Property(e => e.ModifiedAt).HasColumnType("datetime");
            entity.Property(e => e.ModifiedByUserId).HasColumnName("ModifiedByUserID");
            entity.Property(e => e.SpecialNeedCategoryId).HasColumnName("SpecialNeedCategoryID");
            entity.Property(e => e.StudentId).HasColumnName("StudentID");

            entity.HasOne(d => d.CreatedByUser).WithMany(p => p.StudentSpecialNeedCreatedByUsers)
                .HasForeignKey(d => d.CreatedByUserId)
                .HasConstraintName("FK_StudentSpecialNeeds_CreatedByUser");

            entity.HasOne(d => d.ModifiedByUser).WithMany(p => p.StudentSpecialNeedModifiedByUsers)
                .HasForeignKey(d => d.ModifiedByUserId)
                .HasConstraintName("FK_StudentSpecialNeeds_ModifiedByUser");

            entity.HasOne(d => d.SpecialNeedCategory).WithMany(p => p.StudentSpecialNeeds)
                .HasForeignKey(d => d.SpecialNeedCategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_StudentSpecialNeeds_Category");

            entity.HasOne(d => d.Student).WithMany(p => p.StudentSpecialNeeds)
                .HasForeignKey(d => d.StudentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_StudentSpecialNeeds_Student");
        });

        modelBuilder.Entity<StudentVaccinationRecord>(entity =>
        {
            entity.HasKey(e => e.StudentVaccinationId).HasName("PK__StudentV__940FB40D49E46B73");

            entity.ToTable("StudentVaccinationRecord");

            entity.Property(e => e.StudentVaccinationId).HasColumnName("StudentVaccinationID");
            entity.Property(e => e.AdministeredByStaffId).HasColumnName("AdministeredByStaffID");
            entity.Property(e => e.ConsentDate).HasColumnType("datetime");
            entity.Property(e => e.ConsentResponseDate).HasColumnType("datetime");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.CreatedByUserId).HasColumnName("CreatedByUserID");
            entity.Property(e => e.DateAdministered).HasColumnType("datetime");
            entity.Property(e => e.EventId).HasColumnName("EventID");
            entity.Property(e => e.IsDeleted).HasColumnName("IS_DELETED");
            entity.Property(e => e.ModifiedAt).HasColumnType("datetime");
            entity.Property(e => e.ModifiedByUserId).HasColumnName("ModifiedByUserID");
            entity.Property(e => e.ParentalConsentStatus)
                .HasMaxLength(50)
                .HasDefaultValue("Chờ xác nhận");
            entity.Property(e => e.ReasonForDecline).HasMaxLength(255);
            entity.Property(e => e.StudentId).HasColumnName("StudentID");
            entity.Property(e => e.VaccineId).HasColumnName("VaccineID");

            entity.HasOne(d => d.AdministeredByStaff).WithMany(p => p.StudentVaccinationRecords)
                .HasForeignKey(d => d.AdministeredByStaffId)
                .HasConstraintName("FK_StudentVaccination_Staff");

            entity.HasOne(d => d.CreatedByUser).WithMany(p => p.StudentVaccinationRecordCreatedByUsers)
                .HasForeignKey(d => d.CreatedByUserId)
                .HasConstraintName("FK_StudentVaccination_CreatedByUser");

            entity.HasOne(d => d.Event).WithMany(p => p.StudentVaccinationRecords)
                .HasForeignKey(d => d.EventId)
                .HasConstraintName("FK_StudentVaccination_Event");

            entity.HasOne(d => d.ModifiedByUser).WithMany(p => p.StudentVaccinationRecordModifiedByUsers)
                .HasForeignKey(d => d.ModifiedByUserId)
                .HasConstraintName("FK_StudentVaccination_ModifiedByUser");

            entity.HasOne(d => d.Student).WithMany(p => p.StudentVaccinationRecords)
                .HasForeignKey(d => d.StudentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_StudentVaccination_Student");

            entity.HasOne(d => d.Vaccine).WithMany(p => p.StudentVaccinationRecords)
                .HasForeignKey(d => d.VaccineId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_StudentVaccination_Vaccine");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__USER__1788CCAC05513F52");

            entity.ToTable("USER");

            entity.HasIndex(e => e.Email, "UQ__USER__A9D10534DF95891A").IsUnique();

            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.IsDeleted).HasColumnName("IS_DELETED");
            entity.Property(e => e.IsStaff).HasColumnName("isStaff");
            entity.Property(e => e.ModifiedAt).HasColumnType("datetime");
        });

        modelBuilder.Entity<VaccinationEvent>(entity =>
        {
            entity.HasKey(e => e.EventId).HasName("PK__Vaccinat__7944C87006F30BF8");

            entity.ToTable("VaccinationEvent");

            entity.Property(e => e.EventId).HasColumnName("EventID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.CreatedByUserId).HasColumnName("CreatedByUserID");
            entity.Property(e => e.EventDate).HasColumnType("datetime");
            entity.Property(e => e.EventName).HasMaxLength(255);
            entity.Property(e => e.IsDeleted).HasColumnName("IS_DELETED");
            entity.Property(e => e.Location).HasMaxLength(255);
            entity.Property(e => e.ModifiedAt).HasColumnType("datetime");
            entity.Property(e => e.ModifiedByUserId).HasColumnName("ModifiedByUserID");
            entity.Property(e => e.Organizer).HasMaxLength(255);

            entity.HasOne(d => d.CreatedByUser).WithMany(p => p.VaccinationEventCreatedByUsers)
                .HasForeignKey(d => d.CreatedByUserId)
                .HasConstraintName("FK_VaccinationEvent_CreatedByUser");

            entity.HasOne(d => d.ModifiedByUser).WithMany(p => p.VaccinationEventModifiedByUsers)
                .HasForeignKey(d => d.ModifiedByUserId)
                .HasConstraintName("FK_VaccinationEvent_ModifiedByUser");
        });

        modelBuilder.Entity<Vaccine>(entity =>
        {
            entity.HasKey(e => e.VaccineId).HasName("PK__Vaccine__45DC68E9F50294C1");

            entity.ToTable("Vaccine");

            entity.Property(e => e.VaccineId).HasColumnName("VaccineID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.CreatedByUserId).HasColumnName("CreatedByUserID");
            entity.Property(e => e.IsDeleted).HasColumnName("IS_DELETED");
            entity.Property(e => e.Manufacturer).HasMaxLength(255);
            entity.Property(e => e.ModifiedAt).HasColumnType("datetime");
            entity.Property(e => e.ModifiedByUserId).HasColumnName("ModifiedByUserID");
            entity.Property(e => e.MoreInfoUrl)
                .HasMaxLength(500)
                .HasColumnName("MoreInfoURL");
            entity.Property(e => e.TargetAudience).HasMaxLength(255);
            entity.Property(e => e.VaccineName).HasMaxLength(255);

            entity.HasOne(d => d.CreatedByUser).WithMany(p => p.VaccineCreatedByUsers)
                .HasForeignKey(d => d.CreatedByUserId)
                .HasConstraintName("FK_Vaccine_CreatedByUser");

            entity.HasOne(d => d.ModifiedByUser).WithMany(p => p.VaccineModifiedByUsers)
                .HasForeignKey(d => d.ModifiedByUserId)
                .HasConstraintName("FK_Vaccine_ModifiedByUser");
        });

        modelBuilder.Entity<VaccineOfferedInEvent>(entity =>
        {
            entity.HasKey(e => new { e.EventId, e.VaccineId }).HasName("PK__VaccineO__FD190EFEE544A182");

            entity.ToTable("VaccineOfferedInEvent");

            entity.Property(e => e.EventId).HasColumnName("EventID");
            entity.Property(e => e.VaccineId).HasColumnName("VaccineID");
            entity.Property(e => e.Fee).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Notes).HasMaxLength(500);

            entity.HasOne(d => d.Event).WithMany(p => p.VaccineOfferedInEvents)
                .HasForeignKey(d => d.EventId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_VaccineOffered_Event");

            entity.HasOne(d => d.Vaccine).WithMany(p => p.VaccineOfferedInEvents)
                .HasForeignKey(d => d.VaccineId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_VaccineOffered_Vaccine");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

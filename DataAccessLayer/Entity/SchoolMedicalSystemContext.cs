using System;
using System.Collections.Generic;
using DataAccessLayer.Entity;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Entity;

public partial class SchoolMedicalSystemContext: DbContext
{
    public SchoolMedicalSystemContext()
    {
    }

    public SchoolMedicalSystemContext(DbContextOptions<SchoolMedicalSystemContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Blog> Blogs { get; set; }

    public virtual DbSet<Classroom> Classrooms { get; set; }

    public virtual DbSet<EmailTemplate> EmailTemplates { get; set; }

    public virtual DbSet<Healthcategory> Healthcategories { get; set; }

    public virtual DbSet<Healthcheck> Healthchecks { get; set; }

    public virtual DbSet<Healthrecord> Healthrecords { get; set; }

    public virtual DbSet<Healthstatus> Healthstatuses { get; set; }

    public virtual DbSet<Healthstatuscategory> Healthstatuscategories { get; set; }

    public virtual DbSet<Notification> Notifications { get; set; }

    public virtual DbSet<NotificationParentDetail> NotificationParentDetails { get; set; }

    public virtual DbSet<Notificationstaffdetail> Notificationstaffdetails { get; set; }

    public virtual DbSet<Parent> Parents { get; set; }

    public virtual DbSet<Personalmedicine> Personalmedicines { get; set; }

    public virtual DbSet<Personalmedicineschedule> Personalmedicineschedules { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Staff> Staff { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<User> Users { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Blog>(entity =>
        {
            entity.HasKey(e => e.BlogId).HasName("PK__BLOG__F913A29D150AC718");

            entity.ToTable("BLOG");

            entity.Property(e => e.BlogId).HasColumnName("BLOG_ID");
            entity.Property(e => e.ApprovedBy).HasColumnName("APPROVED_BY");
            entity.Property(e => e.ApprovedOn)
                .HasColumnType("datetime")
                .HasColumnName("APPROVED_ON");
            entity.Property(e => e.Content).HasColumnName("CONTENT");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("CREATED_AT");
            entity.Property(e => e.CreatedBy).HasColumnName("CREATED_BY");
            entity.Property(e => e.Image)
                .HasMaxLength(255)
                .HasColumnName("IMAGE");
            entity.Property(e => e.IsDeleted).HasColumnName("IS_DELETED");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasDefaultValue("Draft")
                .HasColumnName("STATUS");
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .HasColumnName("TITLE");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("UPDATED_AT");
            entity.Property(e => e.UpdatedBy).HasColumnName("UPDATED_BY");
        });

        modelBuilder.Entity<Classroom>(entity =>
        {
            entity.HasKey(e => e.Classid).HasName("PK__CLASSROO__96D40B6C52E3F12F");

            entity.ToTable("CLASSROOM");

            entity.Property(e => e.Classid).HasColumnName("CLASSID");
            entity.Property(e => e.Classname)
                .HasMaxLength(255)
                .HasColumnName("CLASSNAME");
            entity.Property(e => e.Createdby)
                .HasMaxLength(255)
                .HasColumnName("CREATEDBY");
            entity.Property(e => e.Createddate)
                .HasColumnType("datetime")
                .HasColumnName("CREATEDDATE");
            entity.Property(e => e.Grade).HasColumnName("GRADE");
            entity.Property(e => e.IsDeleted).HasColumnName("IS_DELETED");
            entity.Property(e => e.Modifiedby)
                .HasMaxLength(255)
                .HasColumnName("MODIFIEDBY");
            entity.Property(e => e.Modifieddate)
                .HasColumnType("datetime")
                .HasColumnName("MODIFIEDDATE");
            entity.Property(e => e.Staffid).HasColumnName("STAFFID");

            entity.HasOne(d => d.Staff).WithMany(p => p.Classrooms)
                .HasForeignKey(d => d.Staffid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CLASSROOM_STAFF");
        });

        modelBuilder.Entity<EmailTemplate>(entity =>
        {
            entity.ToTable("EMAIL_TEMPLATE");

            entity.Property(e => e.EmailTemplateId).HasColumnName("EMAIL_TEMPLATE_ID");
            entity.Property(e => e.Body).HasColumnName("BODY");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(255)
                .HasColumnName("CREATED_BY");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("CREATED_DATE");
            entity.Property(e => e.Subject).HasColumnName("SUBJECT");
            entity.Property(e => e.To).HasColumnName("TO");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(255)
                .HasColumnName("UPDATED_BY");
            entity.Property(e => e.UpdatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("UPDATED_DATE");
        });

        modelBuilder.Entity<Healthcategory>(entity =>
        {
            entity.HasKey(e => e.Healthcategoryid).HasName("PK__HEALTHCA__21157DD6BC5269C5");

            entity.ToTable("HEALTHCATEGORY");

            entity.Property(e => e.Healthcategoryid).HasColumnName("HEALTHCATEGORYID");
            entity.Property(e => e.Createdby)
                .HasMaxLength(255)
                .HasColumnName("CREATEDBY");
            entity.Property(e => e.Createddate)
                .HasColumnType("datetime")
                .HasColumnName("CREATEDDATE");
            entity.Property(e => e.Healthcategorydescription)
                .HasMaxLength(255)
                .HasColumnName("HEALTHCATEGORYDESCRIPTION");
            entity.Property(e => e.Healthcategoryname)
                .HasMaxLength(100)
                .HasColumnName("HEALTHCATEGORYNAME");
            entity.Property(e => e.Isdeleted).HasColumnName("ISDELETED");
            entity.Property(e => e.Modifiedby)
                .HasMaxLength(255)
                .HasColumnName("MODIFIEDBY");
            entity.Property(e => e.Modifieddate)
                .HasColumnType("datetime")
                .HasColumnName("MODIFIEDDATE");
        });

        modelBuilder.Entity<Healthcheck>(entity =>
        {
            entity.HasKey(e => e.Checkid).HasName("PK__HEALTHCH__7A9DCA670F7A506C");

            entity.ToTable("HEALTHCHECK");

            entity.Property(e => e.Checkid).HasColumnName("CHECKID");
            entity.Property(e => e.Bloodpressure)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("BLOODPRESSURE");
            entity.Property(e => e.Checkdate)
                .HasColumnType("datetime")
                .HasColumnName("CHECKDATE");
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("CREATEDAT");
            entity.Property(e => e.Createdby)
                .HasMaxLength(255)
                .HasColumnName("CREATEDBY");
            entity.Property(e => e.Height)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("HEIGHT");
            entity.Property(e => e.Isdeleted).HasColumnName("ISDELETED");
            entity.Property(e => e.Notes)
                .HasMaxLength(1000)
                .HasColumnName("NOTES");
            entity.Property(e => e.Staffid).HasColumnName("STAFFID");
            entity.Property(e => e.Studentid).HasColumnName("STUDENTID");
            entity.Property(e => e.Updatedat)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("UPDATEDAT");
            entity.Property(e => e.Visionleft)
                .HasColumnType("decimal(3, 2)")
                .HasColumnName("VISIONLEFT");
            entity.Property(e => e.Visionright)
                .HasColumnType("decimal(3, 2)")
                .HasColumnName("VISIONRIGHT");
            entity.Property(e => e.Weight)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("WEIGHT");

            entity.HasOne(d => d.Staff).WithMany(p => p.Healthchecks)
                .HasForeignKey(d => d.Staffid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_HEALTHCHECK_STAFF");

            entity.HasOne(d => d.Student).WithMany(p => p.Healthchecks)
                .HasForeignKey(d => d.Studentid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_HEALTHCHECK_STUDENT");
        });

        modelBuilder.Entity<Healthrecord>(entity =>
        {
            entity.HasKey(e => e.Healthrecordid).HasName("PK__tmp_ms_x__05992FCEA73E1132");

            entity.ToTable("HEALTHRECORD");

            entity.Property(e => e.Healthrecordid).HasColumnName("HEALTHRECORDID");
            entity.Property(e => e.Createdby)
                .HasMaxLength(255)
                .HasColumnName("CREATEDBY");
            entity.Property(e => e.Createddate)
                .HasColumnType("datetime")
                .HasColumnName("CREATEDDATE");
            entity.Property(e => e.Healthcategoryid).HasColumnName("HEALTHCATEGORYID");
            entity.Property(e => e.Healthrecorddate)
                .HasColumnType("datetime")
                .HasColumnName("HEALTHRECORDDATE");
            entity.Property(e => e.Healthrecorddescription).HasColumnName("HEALTHRECORDDESCRIPTION");
            entity.Property(e => e.Healthrecordtitle)
                .HasMaxLength(255)
                .HasColumnName("HEALTHRECORDTITLE");
            entity.Property(e => e.Isconfirm).HasColumnName("ISCONFIRM");
            entity.Property(e => e.Modifiedby)
                .HasMaxLength(255)
                .HasColumnName("MODIFIEDBY");
            entity.Property(e => e.Modifieddate)
                .HasColumnType("datetime")
                .HasColumnName("MODIFIEDDATE");
            entity.Property(e => e.Staffid).HasColumnName("STAFFID");
            entity.Property(e => e.Studentid).HasColumnName("STUDENTID");

            entity.HasOne(d => d.Healthcategory).WithMany(p => p.Healthrecords)
                .HasForeignKey(d => d.Healthcategoryid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_HEALTHRECORD_HEALTHCATEGORY");

            entity.HasOne(d => d.Staff).WithMany(p => p.Healthrecords)
                .HasForeignKey(d => d.Staffid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_HEALTHRECORD_STAFF");

            entity.HasOne(d => d.Student).WithMany(p => p.Healthrecords)
                .HasForeignKey(d => d.Studentid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_HEALTHRECORD_STUDENT");
        });

        modelBuilder.Entity<Healthstatus>(entity =>
        {
            entity.HasKey(e => e.HealthId).HasName("PK__HEALTHST__D05E23F8037019AB");

            entity.ToTable("HEALTHSTATUS");

            entity.Property(e => e.HealthId).HasColumnName("HEALTH_ID");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(255)
                .HasColumnName("CREATED_BY");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("CREATED_DATE");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .HasColumnName("DESCRIPTION");
            entity.Property(e => e.HealthStatusCategory).HasColumnName("HEALTH_STATUS_CATEGORY");
            entity.Property(e => e.IsDeleted).HasColumnName("IS_DELETED");
            entity.Property(e => e.ModifiedBy)
                .HasMaxLength(255)
                .HasColumnName("MODIFIED_BY");
            entity.Property(e => e.ModifiedDate)
                .HasColumnType("datetime")
                .HasColumnName("MODIFIED_DATE");
            entity.Property(e => e.StaffId).HasColumnName("STAFF_ID");
            entity.Property(e => e.StudentId).HasColumnName("STUDENT_ID");

            entity.HasOne(d => d.HealthStatusCategoryNavigation).WithMany(p => p.Healthstatuses)
                .HasForeignKey(d => d.HealthStatusCategory)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_HEALTHSTATUS_CATEGORY");

            entity.HasOne(d => d.Staff).WithMany(p => p.Healthstatuses)
                .HasForeignKey(d => d.StaffId)
                .HasConstraintName("FK_HEALTHSTATUS_STAFF");

            entity.HasOne(d => d.Student).WithMany(p => p.Healthstatuses)
                .HasForeignKey(d => d.StudentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_HEALTHSTATUS_STUDENT");
        });

        modelBuilder.Entity<Healthstatuscategory>(entity =>
        {
            entity.HasKey(e => e.HealthStatusCategoryId).HasName("PK__HEALTHST__7C33ADE1A856D6A8");

            entity.ToTable("HEALTHSTATUSCATEGORY");

            entity.Property(e => e.HealthStatusCategoryId).HasColumnName("HEALTH_STATUS_CATEGORY_ID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("CREATED_AT");
            entity.Property(e => e.HealthStatusCategoryDescription)
                .HasMaxLength(255)
                .HasColumnName("HEALTH_STATUS_CATEGORY_DESCRIPTION");
            entity.Property(e => e.HealthStatusCategoryName)
                .HasMaxLength(100)
                .HasColumnName("HEALTH_STATUS_CATEGORY_NAME");
            entity.Property(e => e.IsDeleted).HasColumnName("IS_DELETED");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("UPDATED_AT");
        });

        modelBuilder.Entity<Notification>(entity =>
        {
            entity.HasKey(e => e.NotificationId).HasName("PK__Notifica__20CF2E12C31EBE6D");

            entity.ToTable("Notification");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Createdby)
                .HasMaxLength(255)
                .HasColumnName("CREATEDBY");
            entity.Property(e => e.Createddate)
                .HasColumnType("datetime")
                .HasColumnName("CREATEDDATE");
            entity.Property(e => e.IsDeleted).HasColumnName("isDeleted");
            entity.Property(e => e.Modifiedby)
                .HasMaxLength(255)
                .HasColumnName("MODIFIEDBY");
            entity.Property(e => e.Modifieddate)
                .HasColumnType("datetime")
                .HasColumnName("MODIFIEDDATE");
            entity.Property(e => e.Title).HasMaxLength(255);
            entity.Property(e => e.Type).HasMaxLength(50);
        });

        modelBuilder.Entity<NotificationParentDetail>(entity =>
        {
            entity.HasKey(e => new { e.NotificationId, e.ParentId });

            entity.ToTable("NotificationParentDetail");

            entity.Property(e => e.CreatedBy).HasMaxLength(255);
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.IsDeleted).HasColumnName("isDeleted");
            entity.Property(e => e.IsRead).HasColumnName("isRead");
            entity.Property(e => e.Message).HasMaxLength(500);
            entity.Property(e => e.ModifiedBy).HasMaxLength(255);
            entity.Property(e => e.ModifiedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Notification).WithMany(p => p.NotificationParentDetails)
                .HasForeignKey(d => d.NotificationId)
                .HasConstraintName("FK_NotificationParentDetail_Notification");

            entity.HasOne(d => d.Parent).WithMany(p => p.NotificationParentDetails)
                .HasForeignKey(d => d.ParentId)
                .HasConstraintName("FK_NotificationParentDetail_Parent");
        });

        modelBuilder.Entity<Notificationstaffdetail>(entity =>
        {
            entity.HasKey(e => new { e.NotificationId, e.Staffid });

            entity.ToTable("NOTIFICATIONSTAFFDETAILS");

            entity.Property(e => e.Staffid).HasColumnName("STAFFID");
            entity.Property(e => e.CreatedBy).HasMaxLength(255);
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.IsDeleted).HasColumnName("isDeleted");
            entity.Property(e => e.IsRead).HasColumnName("isRead");
            entity.Property(e => e.Message).HasMaxLength(500);
            entity.Property(e => e.ModifiedBy).HasMaxLength(255);
            entity.Property(e => e.ModifiedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Notification).WithMany(p => p.Notificationstaffdetails)
                .HasForeignKey(d => d.NotificationId)
                .HasConstraintName("FK_NOTIFICATIONSTAFFDETAILS_Notification");

            entity.HasOne(d => d.Staff).WithMany(p => p.Notificationstaffdetails)
                .HasForeignKey(d => d.Staffid)
                .HasConstraintName("FK_NOTIFICATIONSTAFFDETAILS_STAFF");
        });

        modelBuilder.Entity<Parent>(entity =>
        {
            entity.HasKey(e => e.Parentid).HasName("PK__tmp_ms_x__7444E66AE19BE58D");

            entity.ToTable("PARENT");

            entity.Property(e => e.Parentid).HasColumnName("PARENTID");
            entity.Property(e => e.Address)
                .HasMaxLength(500)
                .HasColumnName("ADDRESS");
            entity.Property(e => e.CreatedBy).HasMaxLength(255);
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("EMAIL");
            entity.Property(e => e.Fullname)
                .HasMaxLength(255)
                .HasColumnName("FULLNAME");
            entity.Property(e => e.IsDeleted).HasColumnName("IS_DELETED");
            entity.Property(e => e.ModifiedBy).HasMaxLength(255);
            entity.Property(e => e.ModifiedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Phone).HasColumnName("PHONE");
            entity.Property(e => e.Userid).HasColumnName("USERID");

            entity.HasOne(d => d.User).WithMany(p => p.Parents)
                .HasForeignKey(d => d.Userid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PARENT_USERID");
        });

        modelBuilder.Entity<Personalmedicine>(entity =>
        {
            entity.HasKey(e => e.Personalmedicineid).HasName("PK__PERSONAL__183A5A640F08CC21");

            entity.ToTable("PERSONALMEDICINE");

            entity.Property(e => e.Personalmedicineid).HasColumnName("PERSONALMEDICINEID");
            entity.Property(e => e.Createdby)
                .HasMaxLength(255)
                .HasColumnName("CREATEDBY");
            entity.Property(e => e.Createddate)
                .HasColumnType("datetime")
                .HasColumnName("CREATEDDATE");
            entity.Property(e => e.Expirydate)
                .HasColumnType("datetime")
                .HasColumnName("EXPIRYDATE");
            entity.Property(e => e.Isdeleted).HasColumnName("ISDELETED");
            entity.Property(e => e.Medicinename)
                .HasMaxLength(100)
                .HasColumnName("MEDICINENAME");
            entity.Property(e => e.Modifiedby)
                .HasMaxLength(255)
                .HasColumnName("MODIFIEDBY");
            entity.Property(e => e.Modifieddate)
                .HasColumnType("datetime")
                .HasColumnName("MODIFIEDDATE");
            entity.Property(e => e.Quanttiy).HasColumnName("QUANTTIY");
            entity.Property(e => e.Receivedate)
                .HasColumnType("datetime")
                .HasColumnName("RECEIVEDATE");
            entity.Property(e => e.Staffid).HasColumnName("STAFFID");
            entity.Property(e => e.Studentid).HasColumnName("STUDENTID");

            entity.HasOne(d => d.Staff).WithMany(p => p.Personalmedicines)
                .HasForeignKey(d => d.Staffid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PERSONALMEDICINE_STAFF");

            entity.HasOne(d => d.Student).WithMany(p => p.Personalmedicines)
                .HasForeignKey(d => d.Studentid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PERSONALMEDICINE_STUDENT");
        });

        modelBuilder.Entity<Personalmedicineschedule>(entity =>
        {
            entity.HasKey(e => e.Personalmedicineid).HasName("PK__PERSONAL__E0FDAEFF0607CB1B");

            entity.ToTable("PERSONALMEDICINESCHEDULE");

            entity.Property(e => e.Personalmedicineid)
                .ValueGeneratedNever()
                .HasColumnName("PERSONALMEDICINEID");
            entity.Property(e => e.CreatedBy).HasMaxLength(255);
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Dose)
                .HasMaxLength(50)
                .HasColumnName("DOSE");
            entity.Property(e => e.Isconfirm).HasColumnName("ISCONFIRM");
            entity.Property(e => e.ModifiedBy).HasMaxLength(255);
            entity.Property(e => e.ModifiedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Reason).HasColumnName("REASON");
            entity.Property(e => e.Scheduletime)
                .HasColumnType("datetime")
                .HasColumnName("SCHEDULETIME");

            entity.HasOne(d => d.Personalmedicine).WithOne(p => p.Personalmedicineschedule)
                .HasForeignKey<Personalmedicineschedule>(d => d.Personalmedicineid)
                .HasConstraintName("FK_PERSONALMEDICINESCHEDULE_PERSONALMEDICINEID");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Roleid).HasName("PK__ROLE__006568E91292435B");

            entity.ToTable("ROLE");

            entity.Property(e => e.Roleid).HasColumnName("ROLEID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("CREATED_AT");
            entity.Property(e => e.IsDeleted).HasColumnName("IS_DELETED");
            entity.Property(e => e.Rolename)
                .HasMaxLength(50)
                .HasColumnName("ROLENAME");
        });

        modelBuilder.Entity<Staff>(entity =>
        {
            entity.HasKey(e => e.Staffid).HasName("PK__tmp_ms_x__28B5063B6FB4213C");

            entity.ToTable("STAFF");

            entity.Property(e => e.Staffid).HasColumnName("STAFFID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("CREATED_AT");
            entity.Property(e => e.CreatedBy).HasMaxLength(255);
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("EMAIL");
            entity.Property(e => e.Fullname)
                .HasMaxLength(255)
                .HasColumnName("FULLNAME");
            entity.Property(e => e.IsDeleted).HasColumnName("IS_DELETED");
            entity.Property(e => e.ModifiedBy).HasMaxLength(255);
            entity.Property(e => e.Phone).HasColumnName("PHONE");
            entity.Property(e => e.Roleid).HasColumnName("ROLEID");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("UPDATED_AT");
            entity.Property(e => e.Userid).HasColumnName("USERID");

            entity.HasOne(d => d.Role).WithMany(p => p.Staff)
                .HasForeignKey(d => d.Roleid)
                .HasConstraintName("FK_STAFF_ROLE");

            entity.HasOne(d => d.User).WithMany(p => p.Staff)
                .HasForeignKey(d => d.Userid)
                .HasConstraintName("FK_STAFF_USER");
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.Studentid).HasName("PK__STUDENT__495196F0E731BCB6");

            entity.ToTable("STUDENT");

            entity.Property(e => e.Studentid).HasColumnName("STUDENTID");
            entity.Property(e => e.Age).HasColumnName("AGE");
            entity.Property(e => e.BloodType)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("BLOOD_TYPE");
            entity.Property(e => e.Classid).HasColumnName("CLASSID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("CREATED_AT");
            entity.Property(e => e.Dob).HasColumnName("DOB");
            entity.Property(e => e.Fullname)
                .HasMaxLength(255)
                .HasColumnName("FULLNAME");
            entity.Property(e => e.Gender).HasColumnName("GENDER");
            entity.Property(e => e.IsDeleted).HasColumnName("IS_DELETED");
            entity.Property(e => e.Parentid).HasColumnName("PARENTID");
            entity.Property(e => e.StudentCode)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("STUDENT_CODE");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("UPDATED_AT");

            entity.HasOne(d => d.Class).WithMany(p => p.Students)
                .HasForeignKey(d => d.Classid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_STUDENT_CLASSID");

            entity.HasOne(d => d.Parent).WithMany(p => p.Students)
                .HasForeignKey(d => d.Parentid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_STUDENT_PARENTID");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("USER");

            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Hash).HasMaxLength(255);
            entity.Property(e => e.IsStaff).HasColumnName("isStaff");
            entity.Property(e => e.Salt).HasMaxLength(255);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

﻿using System;
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

    public virtual DbSet<Blog> Blogs { get; set; }

    public virtual DbSet<Classroom> Classrooms { get; set; }

    public virtual DbSet<Consultationrequest> Consultationrequests { get; set; }

    public virtual DbSet<Consultationtype> Consultationtypes { get; set; }

    public virtual DbSet<EmailTemplate> EmailTemplates { get; set; }

    public virtual DbSet<Form> Forms { get; set; }

    public virtual DbSet<FormCategory> FormCategories { get; set; }

    public virtual DbSet<Formcategory1> Formcategories1 { get; set; }

    public virtual DbSet<Healthcategory> Healthcategories { get; set; }

    public virtual DbSet<Healthcheck> Healthchecks { get; set; }

    public virtual DbSet<Healthrecord> Healthrecords { get; set; }

    public virtual DbSet<Healthstatus> Healthstatuses { get; set; }

    public virtual DbSet<Healthstatuscategory> Healthstatuscategories { get; set; }

    public virtual DbSet<MedicalForm> MedicalForms { get; set; }

    public virtual DbSet<Medicine> Medicines { get; set; }

    public virtual DbSet<Medicinecategory> Medicinecategories { get; set; }

    public virtual DbSet<Medicineschedule> Medicineschedules { get; set; }

    public virtual DbSet<Notification> Notifications { get; set; }

    public virtual DbSet<NotificationParentDetail> NotificationParentDetails { get; set; }

    public virtual DbSet<Notificationstaffdetail> Notificationstaffdetails { get; set; }

    public virtual DbSet<Otp> Otps { get; set; }

    public virtual DbSet<Parent> Parents { get; set; }

    public virtual DbSet<Personalmedicine> Personalmedicines { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Scheduledetail> Scheduledetails { get; set; }

    public virtual DbSet<SpecialNeedsCategory> SpecialNeedsCategories { get; set; }

    public virtual DbSet<Staff> Staff { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<StudentSpecialNeed> StudentSpecialNeeds { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Vaccinationevent> Vaccinationevents { get; set; }

    public virtual DbSet<Vaccinationrecord> Vaccinationrecords { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Blog>(entity =>
        {
            entity.HasKey(e => e.BlogId).HasName("PK__BLOG__F913A29D2FA57285");

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
            entity.HasKey(e => e.Classid).HasName("PK__CLASSROO__96D40B6CA87AEB0D");

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
            entity.Property(e => e.Teachername)
                .HasMaxLength(255)
                .HasColumnName("TEACHERNAME");
        });

        modelBuilder.Entity<Consultationrequest>(entity =>
        {
            entity.HasKey(e => e.Consultationid).HasName("PK__CONSULTA__D2399067AC386CBF");

            entity.ToTable("CONSULTATIONREQUEST");

            entity.Property(e => e.Consultationid).HasColumnName("CONSULTATIONID");
            entity.Property(e => e.Createdby).HasColumnName("CREATEDBY");
            entity.Property(e => e.Createddate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("CREATEDDATE");
            entity.Property(e => e.Description).HasColumnName("DESCRIPTION");
            entity.Property(e => e.Isdelete).HasColumnName("ISDELETE");
            entity.Property(e => e.Modifiedby).HasColumnName("MODIFIEDBY");
            entity.Property(e => e.Modifieddate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("MODIFIEDDATE");
            entity.Property(e => e.Parentid).HasColumnName("PARENTID");
            entity.Property(e => e.Requestdate)
                .HasColumnType("datetime")
                .HasColumnName("REQUESTDATE");
            entity.Property(e => e.Requesttypeid).HasColumnName("REQUESTTYPEID");
            entity.Property(e => e.Scheduledate)
                .HasColumnType("datetime")
                .HasColumnName("SCHEDULEDATE");
            entity.Property(e => e.Staffid).HasColumnName("STAFFID");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasColumnName("STATUS");
            entity.Property(e => e.Studentid).HasColumnName("STUDENTID");
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .HasColumnName("TITLE");

            entity.HasOne(d => d.Parent).WithMany(p => p.Consultationrequests)
                .HasForeignKey(d => d.Parentid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ConsultationRequest_Parent");

            entity.HasOne(d => d.Requesttype).WithMany(p => p.Consultationrequests)
                .HasForeignKey(d => d.Requesttypeid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ConsultationRequest_RequestType");

            entity.HasOne(d => d.Staff).WithMany(p => p.Consultationrequests)
                .HasForeignKey(d => d.Staffid)
                .HasConstraintName("FK_ConsultationRequest_Staff");

            entity.HasOne(d => d.Student).WithMany(p => p.Consultationrequests)
                .HasForeignKey(d => d.Studentid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ConsultationRequest_Student");
        });

        modelBuilder.Entity<Consultationtype>(entity =>
        {
            entity.HasKey(e => e.Typeid).HasName("PK__CONSULTA__B2802A015360D0D1");

            entity.ToTable("CONSULTATIONTYPE");

            entity.Property(e => e.Typeid)
                .ValueGeneratedNever()
                .HasColumnName("TYPEID");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .HasColumnName("DESCRIPTION");
            entity.Property(e => e.Isdeleted).HasColumnName("ISDELETED");
            entity.Property(e => e.Typename)
                .HasMaxLength(50)
                .HasColumnName("TYPENAME");
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

        modelBuilder.Entity<Form>(entity =>
        {
            entity.HasKey(e => e.FormId).HasName("PK__FORM__85052F68E5023AF2");

            entity.ToTable("FORM");

            entity.Property(e => e.FormId).HasColumnName("FORM_ID");
            entity.Property(e => e.Createdby)
                .HasMaxLength(255)
                .HasColumnName("CREATEDBY");
            entity.Property(e => e.Createddate)
                .HasColumnType("datetime")
                .HasColumnName("CREATEDDATE");
            entity.Property(e => e.File).HasColumnName("FILE");
            entity.Property(e => e.FormcategoryId).HasColumnName("FORMCATEGORY_ID");
            entity.Property(e => e.Isaccepted)
                .HasDefaultValue(false)
                .HasColumnName("ISACCEPTED");
            entity.Property(e => e.Modifiedby)
                .HasMaxLength(255)
                .HasColumnName("MODIFIEDBY");
            entity.Property(e => e.Modifieddate)
                .HasColumnType("datetime")
                .HasColumnName("MODIFIEDDATE");
            entity.Property(e => e.Parentid).HasColumnName("PARENTID");
            entity.Property(e => e.Reason)
                .HasMaxLength(255)
                .HasColumnName("REASON");
            entity.Property(e => e.Reasonfordecline)
                .HasMaxLength(255)
                .HasColumnName("REASONFORDECLINE");
            entity.Property(e => e.Staffid).HasColumnName("STAFFID");
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .HasColumnName("TITLE");

            entity.HasOne(d => d.Formcategory).WithMany(p => p.Forms)
                .HasForeignKey(d => d.FormcategoryId)
                .HasConstraintName("FK__FORM__FORMCATEGO__5224328E");

            entity.HasOne(d => d.Parent).WithMany(p => p.Forms)
                .HasForeignKey(d => d.Parentid)
                .HasConstraintName("FK__FORM__PARENTID__531856C7");

            entity.HasOne(d => d.Staff).WithMany(p => p.Forms)
                .HasForeignKey(d => d.Staffid)
                .HasConstraintName("FK__FORM__STAFFID__540C7B00");
        });

        modelBuilder.Entity<FormCategory>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__FormCate__19093A2B1228B687");

            entity.HasIndex(e => e.CategoryName, "UQ__FormCate__8517B2E093CC4633").IsUnique();

            entity.Property(e => e.CategoryId).HasColumnName("CategoryID");
            entity.Property(e => e.CategoryName).HasMaxLength(100);
        });

        modelBuilder.Entity<Formcategory1>(entity =>
        {
            entity.HasKey(e => e.Categoryid).HasName("PK__FORMCATE__A50F989615FD6522");

            entity.ToTable("FORMCATEGORY");

            entity.Property(e => e.Categoryid).HasColumnName("CATEGORYID");
            entity.Property(e => e.Categoryname)
                .HasMaxLength(255)
                .HasColumnName("CATEGORYNAME");
            entity.Property(e => e.Isdeleted)
                .HasDefaultValue(false)
                .HasColumnName("ISDELETED");
        });

        modelBuilder.Entity<Healthcategory>(entity =>
        {
            entity.HasKey(e => e.Healthcategoryid).HasName("PK__HEALTHCA__21157DD6C2A3E815");

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
            entity.HasKey(e => e.Checkid).HasName("PK__HEALTHCH__7A9DCA678C4D1842");

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
            entity.HasKey(e => e.Healthrecordid).HasName("PK__HEALTHRE__05992FCE37808896");

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
            entity.Property(e => e.Isdeleted).HasColumnName("ISDELETED");
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
            entity.HasKey(e => e.HealthId).HasName("PK__HEALTHST__D05E23F816F450D1");

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
            entity.HasKey(e => e.HealthStatusCategoryId).HasName("PK__HEALTHST__7C33ADE1A29DC230");

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

        modelBuilder.Entity<MedicalForm>(entity =>
        {
            entity.HasKey(e => e.FormId).HasName("PK__MedicalF__FB05B7BDEC7279B2");

            entity.Property(e => e.FormId).HasColumnName("FormID");
            entity.Property(e => e.CategoryId).HasColumnName("CategoryID");
            entity.Property(e => e.ResponderId).HasColumnName("ResponderID");
            entity.Property(e => e.ResponseTimestamp).HasColumnType("datetime");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasDefaultValue("Chờ duyệt");
            entity.Property(e => e.StudentId).HasColumnName("StudentID");
            entity.Property(e => e.SubmissionTimestamp)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Category).WithMany(p => p.MedicalForms)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MedicalForms_Category");

            entity.HasOne(d => d.Student).WithMany(p => p.MedicalForms)
                .HasForeignKey(d => d.StudentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MedicalForms_Student");
        });

        modelBuilder.Entity<Medicine>(entity =>
        {
            entity.HasKey(e => e.Medicineid).HasName("PK__Medicine__FA10A9A3835E2AA7");

            entity.ToTable("Medicine");

            entity.Property(e => e.Medicineid).HasColumnName("MEDICINEID");
            entity.Property(e => e.Createdat)
                .HasColumnType("datetime")
                .HasColumnName("CREATEDAT");
            entity.Property(e => e.Createdby)
                .HasMaxLength(255)
                .HasColumnName("CREATEDBY");
            entity.Property(e => e.Medicinecategoryid).HasColumnName("MEDICINECATEGORYID");
            entity.Property(e => e.Medicinename)
                .HasMaxLength(255)
                .HasColumnName("MEDICINENAME");
            entity.Property(e => e.Quantity).HasColumnName("QUANTITY");
            entity.Property(e => e.Type)
                .HasMaxLength(255)
                .HasColumnName("TYPE");
            entity.Property(e => e.Updatedat)
                .HasColumnType("datetime")
                .HasColumnName("UPDATEDAT");
            entity.Property(e => e.Updatedby)
                .HasMaxLength(255)
                .HasColumnName("UPDATEDBY");
            entity.Property(e => e.IsDeleted).HasColumnName("ISDELETED");


            entity.HasOne(d => d.Medicinecategory).WithMany(p => p.Medicines)
                .HasForeignKey(d => d.Medicinecategoryid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Medicine__MEDICI__58D1301D");
        });

        modelBuilder.Entity<Medicinecategory>(entity =>
        {
            entity.HasKey(e => e.Medicinecategoryid).HasName("PK__MEDICINE__786AB630025DCC51");

            entity.ToTable("MEDICINECATEGORY");

            entity.Property(e => e.Medicinecategoryid).HasColumnName("MEDICINECATEGORYID");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .HasColumnName("DESCRIPTION");
            entity.Property(e => e.Medicinecategoryname)
                .HasMaxLength(255)
                .HasColumnName("MEDICINECATEGORYNAME");
        });

        modelBuilder.Entity<Medicineschedule>(entity =>
        {
            entity.HasKey(e => e.Schedulemedicineid).HasName("PK__MEDICINE__E8D9781A4A457221");

            entity.ToTable("MEDICINESCHEDULE");

            entity.Property(e => e.Schedulemedicineid).HasColumnName("SCHEDULEMEDICINEID");
            entity.Property(e => e.Duration).HasColumnName("DURATION");
            entity.Property(e => e.Notes).HasColumnName("NOTES");
            entity.Property(e => e.Personalmedicineid).HasColumnName("PERSONALMEDICINEID");
            entity.Property(e => e.Scheduledetails).HasColumnName("SCHEDULEDETAILS");
            entity.Property(e => e.Startdate)
                .HasColumnType("datetime")
                .HasColumnName("STARTDATE");

            entity.HasOne(d => d.Personalmedicine).WithMany(p => p.Medicineschedules)
                .HasForeignKey(d => d.Personalmedicineid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MEDICINESCHEDULE_PERSONALMEDICINEID");

            entity.HasOne(d => d.ScheduledetailsNavigation).WithMany(p => p.Medicineschedules)
                .HasForeignKey(d => d.Scheduledetails)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MEDICINESCHEDULE_SCHEDULEDETAILS");
        });

        modelBuilder.Entity<Notification>(entity =>
        {
            entity.HasKey(e => e.NotificationId).HasName("PK__Notifica__20CF2E1269A01088");

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

        modelBuilder.Entity<Otp>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tmp_ms_x__3214EC277320E3D4");

            entity.ToTable("Otp");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getutcdate())");
            entity.Property(e => e.Email).HasMaxLength(256);
            entity.Property(e => e.OtpCode).HasMaxLength(20);
        });

        modelBuilder.Entity<Parent>(entity =>
        {
            entity.HasKey(e => e.Parentid).HasName("PK__PARENT__7444E66A89D5B72E");

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
            entity.Property(e => e.Phone)
                .HasMaxLength(255)
                .HasColumnName("PHONE");
            entity.Property(e => e.Userid).HasColumnName("USERID");

            entity.HasOne(d => d.User).WithMany(p => p.Parents)
                .HasForeignKey(d => d.Userid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PARENT_USERID");
        });

        modelBuilder.Entity<Personalmedicine>(entity =>
        {
            entity.HasKey(e => e.Personalmedicineid).HasName("PK__PERSONAL__E0FDAEFF972A0A4C");

            entity.ToTable("PERSONALMEDICINE");

            entity.Property(e => e.Personalmedicineid).HasColumnName("PERSONALMEDICINEID");
            entity.Property(e => e.Approvedby)
                .HasMaxLength(255)
                .IsFixedLength()
                .HasColumnName("APPROVEDBY");
            entity.Property(e => e.Createdby)
                .HasMaxLength(50)
                .HasColumnName("CREATEDBY");
            entity.Property(e => e.Createddate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("CREATEDDATE");
            entity.Property(e => e.ExpiryDate).HasColumnType("datetime");
            entity.Property(e => e.Isapproved).HasColumnName("ISAPPROVED");
            entity.Property(e => e.Isdeleted).HasColumnName("ISDELETED");
            entity.Property(e => e.Medicineid).HasColumnName("MEDICINEID");
            entity.Property(e => e.Modifiedby)
                .HasMaxLength(50)
                .HasColumnName("MODIFIEDBY");
            entity.Property(e => e.Modifieddate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("MODIFIEDDATE");
            entity.Property(e => e.Note)
                .HasMaxLength(500)
                .HasColumnName("NOTE");
            entity.Property(e => e.Parentid).HasColumnName("PARENTID");
            entity.Property(e => e.Quantity).HasColumnName("QUANTITY");
            entity.Property(e => e.Receiveddate)
                .HasColumnType("datetime")
                .HasColumnName("RECEIVEDDATE");
            entity.Property(e => e.Status)
                .HasDefaultValue(true)
                .HasColumnName("STATUS");
            entity.Property(e => e.Studentid).HasColumnName("STUDENTID");

            entity.HasOne(d => d.Medicine).WithMany(p => p.Personalmedicines)
                .HasForeignKey(d => d.Medicineid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PERSONALM__MEDIC__7755B73D");

            entity.HasOne(d => d.Parent).WithMany(p => p.Personalmedicines)
                .HasForeignKey(d => d.Parentid)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__PERSONALM__PAREN__7849DB76");

            entity.HasOne(d => d.Student).WithMany(p => p.Personalmedicines)
                .HasForeignKey(d => d.Studentid)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__PERSONALM__STUDE__793DFFAF");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Roleid).HasName("PK__ROLE__006568E9F8FBF902");

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

        modelBuilder.Entity<Scheduledetail>(entity =>
        {
            entity.HasKey(e => e.Scheduledetailid).HasName("PK__SCHEDULE__466875F61980F5BB");

            entity.ToTable("SCHEDULEDETAILS");

            entity.Property(e => e.Scheduledetailid).HasColumnName("SCHEDULEDETAILID");
            entity.Property(e => e.Dayinweek).HasColumnName("DAYINWEEK");
            entity.Property(e => e.Endtime)
                .HasPrecision(0)
                .HasColumnName("ENDTIME");
            entity.Property(e => e.Isdeleted).HasColumnName("ISDELETED");
            entity.Property(e => e.Modifiedby)
                .HasMaxLength(50)
                .HasColumnName("MODIFIEDBY");
            entity.Property(e => e.Modifieddate)
                .HasColumnType("datetime")
                .HasColumnName("MODIFIEDDATE");
            entity.Property(e => e.Starttime)
                .HasPrecision(0)
                .HasColumnName("STARTTIME");
        });

        modelBuilder.Entity<SpecialNeedsCategory>(entity =>
        {
            entity.HasKey(e => e.SpecialNeedCategoryId).HasName("PK__SpecialN__BAED81C08E28A5A5");

            entity.HasIndex(e => e.CategoryName, "UQ__SpecialN__8517B2E096E4C7CF").IsUnique();

            entity.Property(e => e.SpecialNeedCategoryId).HasColumnName("SpecialNeedCategoryID");
            entity.Property(e => e.CategoryName).HasMaxLength(150);
            entity.Property(e => e.IsDelete).HasColumnName("ISDELETED");

        });

        modelBuilder.Entity<Staff>(entity =>
        {
            entity.HasKey(e => e.Staffid).HasName("PK__STAFF__28B5063BD3A1C1CF");

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
            entity.Property(e => e.Phone)
                .HasMaxLength(255)
                .HasColumnName("PHONE");
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
            entity.HasKey(e => e.Studentid).HasName("PK__STUDENT__495196F08119CB32");

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

        modelBuilder.Entity<StudentSpecialNeed>(entity =>
        {
            entity.HasKey(e => e.StudentSpecialNeedId).HasName("PK__StudentS__00E0B0F5120DA1CF");

            entity.Property(e => e.StudentSpecialNeedId).HasColumnName("StudentSpecialNeedID");
            entity.Property(e => e.SpecialNeedCategoryId).HasColumnName("SpecialNeedCategoryID");
            entity.Property(e => e.StudentId).HasColumnName("StudentID");
            entity.Property(e => e.IsDelete).HasColumnName("ISDELETED");


            entity.HasOne(d => d.SpecialNeedCategory).WithMany(p => p.StudentSpecialNeeds)
                .HasForeignKey(d => d.SpecialNeedCategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_StudentSpecialNeeds_Category");

            entity.HasOne(d => d.Student).WithMany(p => p.StudentSpecialNeeds)
                .HasForeignKey(d => d.StudentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_StudentSpecialNeeds_Student");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("USER");

            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Hash).HasMaxLength(255);
            entity.Property(e => e.IsDeleted).HasColumnName("IS_DELETED");
            entity.Property(e => e.IsStaff).HasColumnName("isStaff");
            entity.Property(e => e.Salt).HasMaxLength(255);
        });

        modelBuilder.Entity<Vaccinationevent>(entity =>
        {
            entity.HasKey(e => e.Vaccinationeventid).HasName("PK__tmp_ms_x__E5A79A9A5557AF9A");

            entity.ToTable("VACCINATIONEVENT");

            entity.Property(e => e.Vaccinationeventid).HasColumnName("VACCINATIONEVENTID");
            entity.Property(e => e.Createdby)
                .HasMaxLength(100)
                .HasColumnName("CREATEDBY");
            entity.Property(e => e.Createddate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("CREATEDDATE");
            entity.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("DESCRIPTION");
            entity.Property(e => e.Eventdate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("EVENTDATE");
            entity.Property(e => e.Isdeleted).HasColumnName("ISDELETED");
            entity.Property(e => e.Location)
                .HasMaxLength(100)
                .HasColumnName("LOCATION");
            entity.Property(e => e.Modifiedby)
                .HasMaxLength(100)
                .HasColumnName("MODIFIEDBY");
            entity.Property(e => e.Modifieddate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("MODIFIEDDATE");
            entity.Property(e => e.Organizedby)
                .HasMaxLength(100)
                .HasColumnName("ORGANIZEDBY");
            entity.Property(e => e.Vaccinationeventname)
                .HasMaxLength(100)
                .HasColumnName("VACCINATIONEVENTNAME");
        });

        modelBuilder.Entity<Vaccinationrecord>(entity =>
        {
            entity.HasKey(e => e.Vaccinationrecordid).HasName("PK__tmp_ms_x__BCD6C54803C01586");

            entity.ToTable("VACCINATIONRECORD");

            entity.Property(e => e.Vaccinationrecordid).HasColumnName("VACCINATIONRECORDID");
            entity.Property(e => e.Confirmedbyparent).HasColumnName("CONFIRMEDBYPARENT");
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("CREATEDAT");
            entity.Property(e => e.Createdby)
                .HasMaxLength(50)
                .HasColumnName("CREATEDBY");
            entity.Property(e => e.Dosenumber).HasColumnName("DOSENUMBER");
            entity.Property(e => e.Isdeleted).HasColumnName("ISDELETED");
            entity.Property(e => e.Parentconsent)
                .HasDefaultValue(false)
                .HasColumnName("PARENTCONSENT");
            entity.Property(e => e.Reasonfordecline)
                .HasMaxLength(500)
                .HasColumnName("REASONFORDECLINE");
            entity.Property(e => e.Responsedate)
                .HasColumnType("datetime")
                .HasColumnName("RESPONSEDATE");
            entity.Property(e => e.Studentid).HasColumnName("STUDENTID");
            entity.Property(e => e.Updatedat)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("UPDATEDAT");
            entity.Property(e => e.Updatedby)
                .HasMaxLength(50)
                .HasColumnName("UPDATEDBY");
            entity.Property(e => e.Vaccinationdate).HasColumnName("VACCINATIONDATE");
            entity.Property(e => e.Vaccinationeventid).HasColumnName("VACCINATIONEVENTID");
            entity.Property(e => e.Vaccinename)
                .HasMaxLength(100)
                .HasColumnName("VACCINENAME");
            entity.Property(e => e.Willattend)
                .HasDefaultValue(false)
                .HasColumnName("WILLATTEND");

            entity.HasOne(d => d.Student).WithMany(p => p.Vaccinationrecords)
                .HasForeignKey(d => d.Studentid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_VACCINATIONRECORD_STUDENT");

            entity.HasOne(d => d.Vaccinationevent).WithMany(p => p.Vaccinationrecords)
                .HasForeignKey(d => d.Vaccinationeventid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_VACCINATIONRECORD_VACCINATIONEVENT");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

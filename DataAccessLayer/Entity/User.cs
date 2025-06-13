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

    public virtual ICollection<Class> ClassCreatedbyNavigations { get; set; } = new List<Class>();

    public virtual ICollection<Class> ClassModifiedbyNavigations { get; set; } = new List<Class>();

    public virtual ICollection<EmailTemplate> EmailTemplateCreatedByNavigations { get; set; } = new List<EmailTemplate>();

    public virtual ICollection<EmailTemplate> EmailTemplateUpdatedByNavigations { get; set; } = new List<EmailTemplate>();

    public virtual ICollection<Healthcategory> HealthcategoryCreatedbyNavigations { get; set; } = new List<Healthcategory>();

    public virtual ICollection<Healthcategory> HealthcategoryModifiedbyNavigations { get; set; } = new List<Healthcategory>();

    public virtual ICollection<Healthstatus> HealthstatusCreatedByNavigations { get; set; } = new List<Healthstatus>();

    public virtual ICollection<Healthstatus> HealthstatusModifiedByNavigations { get; set; } = new List<Healthstatus>();

    public virtual ICollection<Notification> NotificationCreatedbyNavigations { get; set; } = new List<Notification>();

    public virtual ICollection<Notification> NotificationModifiedbyNavigations { get; set; } = new List<Notification>();

    public virtual ICollection<Parent> ParentCreatedByNavigations { get; set; } = new List<Parent>();

    public virtual ICollection<Parent> ParentModifiedByNavigations { get; set; } = new List<Parent>();

    public virtual ICollection<Parent> ParentUsers { get; set; } = new List<Parent>();

    public virtual ICollection<Personalmedicineschedule> PersonalmedicinescheduleCreatedByNavigations { get; set; } = new List<Personalmedicineschedule>();

    public virtual ICollection<Personalmedicineschedule> PersonalmedicinescheduleModifiedByNavigations { get; set; } = new List<Personalmedicineschedule>();

    public virtual ICollection<Staff> StaffCreatedByNavigations { get; set; } = new List<Staff>();

    public virtual ICollection<Staff> StaffModifiedByNavigations { get; set; } = new List<Staff>();

    public virtual ICollection<Staff> StaffUsers { get; set; } = new List<Staff>();
}

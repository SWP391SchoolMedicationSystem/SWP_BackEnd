using System;
using System.Collections.Generic;

namespace SchoolMedicalSystem.Entity;

public partial class Notification
{
    public int NotificationId { get; set; }

    public string Title { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public string Type { get; set; } = null!;

    public bool IsDeleted { get; set; }

    public int? CreatedByUserId { get; set; }

    public virtual ICollection<NotificationParentDetail> NotificationParentDetails { get; set; } = new List<NotificationParentDetail>();

    public virtual ICollection<Notificationstaffdetail> Notificationstaffdetails { get; set; } = new List<Notificationstaffdetail>();
}

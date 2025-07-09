using System;
using System.Collections.Generic;

namespace SchoolMedicalSystem.Entity;

public partial class Notificationstaffdetail
{
    public int NotificationId { get; set; }

    public int Staffid { get; set; }

    public string? Message { get; set; }

    public bool IsRead { get; set; }

    public bool IsDeleted { get; set; }

    public DateTime? CreatedDate { get; set; }

    public virtual Notification Notification { get; set; } = null!;

    public virtual Staff Staff { get; set; } = null!;
}

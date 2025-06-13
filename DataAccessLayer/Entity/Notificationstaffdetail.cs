using System;
using System.Collections.Generic;

namespace DataAccessLayer.Entity;

public partial class Notificationstaffdetail
{
    public int NotificationId { get; set; }

    public int Staffid { get; set; }

    public string? Message { get; set; }

    public bool IsRead { get; set; }

    public bool IsDeleted { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public int CreatedBy { get; set; }

    public int? ModifiedBy { get; set; }

    public virtual User CreatedByNavigation { get; set; } = null!;

    public virtual User ModifiedByNavigation { get; set; } = null!;

    public virtual Notification Notification { get; set; } = null!;

    public virtual Staff Staff { get; set; } = null!;
}

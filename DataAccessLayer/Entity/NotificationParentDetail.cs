using System;
using System.Collections.Generic;

namespace DataAccessLayer.Entity;

public partial class NotificationParentDetail
{
    public int NotificationId { get; set; }

    public int ParentId { get; set; }

    public string? Message { get; set; }

    public bool IsRead { get; set; }

    public bool IsDeleted { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public int? CreatedBy { get; set; }

    public int? ModifiedBy { get; set; }

    public virtual User? CreatedByNavigation { get; set; }

    public virtual User? ModifiedByNavigation { get; set; }

    public virtual Notification Notification { get; set; } = null!;

    public virtual Parent Parent { get; set; } = null!;
}

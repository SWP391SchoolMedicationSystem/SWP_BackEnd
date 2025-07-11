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

    public DateTime? CreatedAt { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public int? CreatedByUserId { get; set; }

    public int? ModifiedByUserId { get; set; }

    public virtual Notification Notification { get; set; } = null!;

    public virtual Parent Parent { get; set; } = null!;
}

using System;
using System.Collections.Generic;

namespace DataAccessLayer.Entity;

public partial class Notification
{
    public int NotificationId { get; set; }

    public string Title { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public string Type { get; set; } = null!;

    public bool IsDeleted { get; set; }

    public int? Createdby { get; set; }

    public DateTime? Createddate { get; set; }

    public int? Modifiedby { get; set; }

    public DateTime? Modifieddate { get; set; }

    public virtual User? CreatedbyNavigation { get; set; }

    public virtual User? ModifiedbyNavigation { get; set; }
}

using System;
using System.Collections.Generic;

namespace DataAccessLayer.Entity;

public class Notification
{
    public int NotificationId { get; set; }
    public string Title { get; set; }
    public DateTime CreatedAt { get; set; }
    public string Type { get; set; }
    public bool IsDeleted { get; set; }
    public string? Createdby { get; set; }
    public DateTime? Createddate { get; set; }
    public string? Modifiedby { get; set; }
    public DateTime? Modifieddate { get; set; }
    public virtual ICollection<NotificationParentDetail> NotificationParentDetails { get; set; }
    public virtual ICollection<Notificationstaffdetail> Notificationstaffdetails { get; set; } // Add this missing property  
}

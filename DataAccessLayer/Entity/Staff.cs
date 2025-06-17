using System;
using System.Collections.Generic;

namespace DataAccessLayer.Entity;

public class Staff
{
    public int Staffid { get; set; }
    public string Fullname { get; set; }
    public string? Email { get; set; }
    public int? Phone { get; set; }
    public int Roleid { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string? CreatedBy { get; set; }
    public string? ModifiedBy { get; set; }
    public bool IsDeleted { get; set; }
    public int Userid { get; set; }
    public virtual ICollection<Class> Classes { get; set; }
    public virtual ICollection<Healthrecord> Healthrecords { get; set; }
    public virtual ICollection<Healthstatus> Healthstatuses { get; set; }
    public virtual ICollection<Personalmedicine> Personalmedicines { get; set; }
    public virtual ICollection<Notificationstaffdetail> Notificationstaffdetails { get; set; } // Added this property  
    public virtual Role Role { get; set; }
    public virtual User User { get; set; }
}

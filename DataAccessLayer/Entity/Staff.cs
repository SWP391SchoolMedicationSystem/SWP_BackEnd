using System;
using System.Collections.Generic;

namespace DataAccessLayer.Entity;

public partial class Staff
{
    public int Staffid { get; set; }

    public string Fullname { get; set; } = null!;

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public int Roleid { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string? CreatedBy { get; set; }

    public string? ModifiedBy { get; set; }

    public bool IsDeleted { get; set; }

    public int Userid { get; set; }

    public virtual ICollection<Consultationrequest> Consultationrequests { get; set; } = new List<Consultationrequest>();

    public virtual ICollection<Healthcheck> Healthchecks { get; set; } = new List<Healthcheck>();

    public virtual ICollection<Healthrecord> Healthrecords { get; set; } = new List<Healthrecord>();

    public virtual ICollection<Healthstatus> Healthstatuses { get; set; } = new List<Healthstatus>();

    public virtual ICollection<Notificationstaffdetail> Notificationstaffdetails { get; set; } = new List<Notificationstaffdetail>();

    public virtual ICollection<Personalmedicine> Personalmedicines { get; set; } = new List<Personalmedicine>();

    public virtual Role Role { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}

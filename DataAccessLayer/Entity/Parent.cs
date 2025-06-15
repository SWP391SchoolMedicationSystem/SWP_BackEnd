using System;
using System.Collections.Generic;

namespace DataAccessLayer.Entity;

public partial class Parent
{
    public int Parentid { get; set; }

    public string Fullname { get; set; } = null!;

    public string? Email { get; set; }

    public int? Phone { get; set; }

    public string Address { get; set; } = null!;

    public DateTime? CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public string? CreatedBy { get; set; }

    public string? ModifiedBy { get; set; }

    public bool IsDeleted { get; set; }

    public int Userid { get; set; }

    public virtual ICollection<NotificationParentDetail> NotificationParentDetails { get; set; } = new List<NotificationParentDetail>();

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();

    public virtual User User { get; set; } = null!;
}

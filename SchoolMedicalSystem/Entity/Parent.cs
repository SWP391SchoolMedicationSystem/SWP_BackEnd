using System;
using System.Collections.Generic;

namespace SchoolMedicalSystem.Entity;

public partial class Parent
{
    public int Parentid { get; set; }

    public string Fullname { get; set; } = null!;

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public string Address { get; set; } = null!;

    public int Userid { get; set; }

    public bool IsDeleted { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public int? CreatedByUserId { get; set; }

    public int? ModifiedByUserId { get; set; }

    public virtual ICollection<Form> Forms { get; set; } = new List<Form>();

    public virtual ICollection<NotificationParentDetail> NotificationParentDetails { get; set; } = new List<NotificationParentDetail>();

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();

    public virtual User User { get; set; } = null!;
}

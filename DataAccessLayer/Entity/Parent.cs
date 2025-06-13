using System;
using System.Collections.Generic;

namespace DataAccessLayer.Entity;

public partial class Parent
{
    public int Parentid { get; set; }

    public string Fullname { get; set; } = null!;

    public string? Email { get; set; }

    public int Phone { get; set; }

    public string Address { get; set; } = null!;

    public DateTime? CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public int? CreatedBy { get; set; }

    public int? ModifiedBy { get; set; }

    public bool IsDeleted { get; set; }

    public int Userid { get; set; }

    public virtual User? CreatedByNavigation { get; set; }

    public virtual User? ModifiedByNavigation { get; set; }

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();

    public virtual User User { get; set; } = null!;
}

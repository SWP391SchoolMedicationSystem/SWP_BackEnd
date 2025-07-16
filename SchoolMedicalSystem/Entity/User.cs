using System;
using System.Collections.Generic;

namespace SchoolMedicalSystem.Entity;

public partial class User
{
    public int UserId { get; set; }

    public bool IsStaff { get; set; }

    public string Email { get; set; } = null!;

    public byte[] Hash { get; set; } = null!;

    public byte[] Salt { get; set; } = null!;

    public bool IsDeleted { get; set; }

    public virtual ICollection<Parent> Parents { get; set; } = new List<Parent>();

    public virtual ICollection<Staff> Staff { get; set; } = new List<Staff>();
}

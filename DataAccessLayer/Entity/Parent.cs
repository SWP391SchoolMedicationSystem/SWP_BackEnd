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

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public bool IsDeleted { get; set; }

    public byte[] PasswordHash { get; set; } = null!;

    public byte[] PasswordSalt { get; set; } = null!;

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();
}

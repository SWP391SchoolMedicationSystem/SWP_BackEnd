using System;
using System.Collections.Generic;

namespace DataAccessLayer.Entity;

public partial class Staff
{
    public int Staffid { get; set; }

    public string Fullname { get; set; } = null!;

    public string Email { get; set; } = null!;

    public int Phone { get; set; }

    public int Roleid { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public bool IsDeleted { get; set; }

    public byte[] PasswordHash { get; set; } = null!;

    public byte[] PasswordSalt { get; set; } = null!;

    public virtual ICollection<Class> Classes { get; set; } = new List<Class>();

    public virtual Role Role { get; set; } = null!;
}

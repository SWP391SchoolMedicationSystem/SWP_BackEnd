using System;
using System.Collections.Generic;

namespace DataAccessLayer.Entity;

public partial class Role
{
    public int Roleid { get; set; }

    public string Rolename { get; set; } = null!;

    public bool IsDeleted { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual ICollection<Staff> Staff { get; set; } = new List<Staff>();
}

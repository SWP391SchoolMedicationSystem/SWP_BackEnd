using System;
using System.Collections.Generic;

namespace DataAccessLayer.Entity;

public partial class Consultationtype
{
    public int Typeid { get; set; }

    public string Typename { get; set; } = null!;

    public string? Description { get; set; }

    public bool Isdeleted { get; set; }

    public virtual ICollection<Consultationrequest> Consultationrequests { get; set; } = new List<Consultationrequest>();
}

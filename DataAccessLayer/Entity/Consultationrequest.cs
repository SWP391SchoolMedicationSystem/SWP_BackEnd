using System;
using System.Collections.Generic;

namespace DataAccessLayer.Entity;

public partial class Consultationrequest
{
    public int Consultationid { get; set; }

    public int Parentid { get; set; }

    public int Studentid { get; set; }

    public int Requesttypeid { get; set; }

    public DateTime Requestdate { get; set; }

    public DateTime? Scheduledate { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public string Status { get; set; } = null!;

    public int? Staffid { get; set; }

    public bool Isdelete { get; set; }

    public int Createdby { get; set; }

    public DateTime Createddate { get; set; }

    public int? Modifiedby { get; set; }

    public DateTime? Modifieddate { get; set; }

    public virtual Parent Parent { get; set; } = null!;

    public virtual Consultationtype Requesttype { get; set; } = null!;

    public virtual Staff? Staff { get; set; }

    public virtual Student Student { get; set; } = null!;
}

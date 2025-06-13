using System;
using System.Collections.Generic;

namespace DataAccessLayer.Entity;
public partial class Healthrecord
{
    public int Healthrecordid { get; set; }

    public int Studentid { get; set; }

    public int Healthcategoryid { get; set; }

    public DateTime Healthrecorddate { get; set; }

    public string Healthrecordtitle { get; set; } = null!;

    public string? Healthrecorddescription { get; set; }

    public int Staffid { get; set; }

    public bool Isconfirm { get; set; }

    public int? Createdby { get; set; }

    public DateTime? Createddate { get; set; }

    public int? Modifiedby { get; set; }

    public DateTime? Modifieddate { get; set; }

    public virtual Staff? CreatedbyNavigation { get; set; }

    public virtual Healthcategory Healthcategory { get; set; } = null!;

    public virtual Staff? ModifiedbyNavigation { get; set; }

    public virtual Staff Staff { get; set; } = null!;

    public virtual Student Student { get; set; } = null!;
}

using System;
using System.Collections.Generic;

namespace SchoolMedicalSystem.Entity;

public partial class Healthcategory
{
    public int Healthcategoryid { get; set; }

    public string Healthcategoryname { get; set; } = null!;

    public string? Healthcategorydescription { get; set; }

    public bool Isdeleted { get; set; }

    public string? Createdby { get; set; }

    public DateTime? Createddate { get; set; }

    public string? Modifiedby { get; set; }

    public DateTime? Modifieddate { get; set; }

    public virtual ICollection<Healthrecord> Healthrecords { get; set; } = new List<Healthrecord>();
}

using System;
using System.Collections.Generic;

namespace DataAccessLayer.Entity;

public partial class Healthcategory
{
    public int Healthcategoryid { get; set; }

    public string Healthcategoryname { get; set; } = null!;

    public string? Healthcategorydescription { get; set; }

    public bool Isdeleted { get; set; }

    public int Createdby { get; set; }

    public DateTime Createddate { get; set; }

    public int? Modifiedby { get; set; }

    public DateTime? Modifieddate { get; set; }

    public virtual User CreatedbyNavigation { get; set; } = null!;

    public virtual ICollection<Healthrecord> Healthrecords { get; set; } = new List<Healthrecord>();

    public virtual User? ModifiedbyNavigation { get; set; }
}

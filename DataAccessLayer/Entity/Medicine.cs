using System;
using System.Collections.Generic;

namespace DataAccessLayer.Entity;

public partial class Medicine
{
    public int Medicineid { get; set; }

    public string Medicinename { get; set; } = null!;

    public int Medicinecategoryid { get; set; }

    public string Type { get; set; } = null!;

    public int Quantity { get; set; }

    public DateTime? Createdat { get; set; }

    public DateTime? Updatedat { get; set; }

    public string? Createdby { get; set; }

    public string? Updatedby { get; set; }

    public bool IsDeleted { get; set; }

    public virtual Medicinecategory Medicinecategory { get; set; } = null!;

    public virtual ICollection<Personalmedicine> Personalmedicines { get; set; } = new List<Personalmedicine>();
}

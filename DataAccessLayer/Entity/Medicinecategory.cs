using System;
using System.Collections.Generic;

namespace DataAccessLayer.Entity;

public partial class Medicinecategory
{
    public int Medicinecategoryid { get; set; }

    public string Medicinecategoryname { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<Medicine> Medicines { get; set; } = new List<Medicine>();
}

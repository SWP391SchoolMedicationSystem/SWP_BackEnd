using System;
using System.Collections.Generic;

namespace DataAccessLayer.Entity;

public partial class MedicineCategory
{
    public int MedicineCategoryId { get; set; }

    public string CategoryName { get; set; } = null!;

    public string? Description { get; set; }

    public bool IsDeleted { get; set; }

    public virtual ICollection<MedicineCatalog> MedicineCatalogs { get; set; } = new List<MedicineCatalog>();
}

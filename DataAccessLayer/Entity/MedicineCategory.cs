using System;
using System.Collections.Generic;

namespace DataAccessLayer.Entity;

public partial class MedicineCategory
{
    public int MedicineCategoryId { get; set; }

    public string CategoryName { get; set; } = null!;

    public string? Description { get; set; }

    public bool IsDeleted { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public int? CreatedByUserId { get; set; }

    public int? ModifiedByUserId { get; set; }

    public virtual User? CreatedByUser { get; set; }

    public virtual ICollection<MedicineCatalog> MedicineCatalogs { get; set; } = new List<MedicineCatalog>();

    public virtual User? ModifiedByUser { get; set; }
}

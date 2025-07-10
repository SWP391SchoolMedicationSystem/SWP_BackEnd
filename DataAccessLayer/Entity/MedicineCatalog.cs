using System;
using System.Collections.Generic;

namespace DataAccessLayer.Entity;

public partial class MedicineCatalog
{
    public int MedicineId { get; set; }

    public string MedicineName { get; set; } = null!;

    public int? MedicineCategoryId { get; set; }

    public string? Usage { get; set; }

    public string? DefaultDosage { get; set; }

    public string? SideEffects { get; set; }

    public bool IsDeleted { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public int? CreatedByUserId { get; set; }

    public int? ModifiedByUserId { get; set; }

    public virtual MedicineCategory? MedicineCategory { get; set; }

    public virtual ICollection<MedicineStorage> MedicineStorages { get; set; } = new List<MedicineStorage>();
}

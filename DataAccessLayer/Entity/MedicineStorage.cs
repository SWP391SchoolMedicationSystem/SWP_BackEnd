using System;
using System.Collections.Generic;

namespace DataAccessLayer.Entity;

public partial class MedicineStorage
{
    public int StorageId { get; set; }

    public int MedicineId { get; set; }

    public int Quantity { get; set; }

    public DateOnly ExpiryDate { get; set; }

    public string? LotNumber { get; set; }

    public DateTime ReceivedDate { get; set; }

    public int StaffId { get; set; }

    public string? Notes { get; set; }

    public virtual MedicineCatalog Medicine { get; set; } = null!;

    public virtual Staff Staff { get; set; } = null!;
}

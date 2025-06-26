using System;
using System.Collections.Generic;

namespace DataAccessLayer.Entity;

public partial class Medicinedonation
{
    public int Donationid { get; set; }

    public int Medicineid { get; set; }

    public int? Parentid { get; set; }

    public int Quantity { get; set; }

    public DateTime Donationdate { get; set; }

    public DateTime? ExpiryDate { get; set; }

    public bool Status { get; set; }

    public string? Note { get; set; }

    public string Createdby { get; set; } = null!;

    public DateTime Createddate { get; set; }

    public string? Modifiedby { get; set; }

    public DateTime? Modifieddate { get; set; }

    public bool Isdeleted { get; set; }

    public virtual Medicine Medicine { get; set; } = null!;

    public virtual Parent? Parent { get; set; }
}

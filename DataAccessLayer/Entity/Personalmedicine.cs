using System;
using System.Collections.Generic;

namespace DataAccessLayer.Entity;

public partial class Personalmedicine
{
    public int Personalmedicineid { get; set; }

    public int Studentid { get; set; }

    public int Parentid { get; set; }

    public string Medicinename { get; set; } = null!;

    public int Quantity { get; set; }

    public string? Note { get; set; }

    public DateTime Receivedate { get; set; }

    public DateTime Expirydate { get; set; }

    public int Staffid { get; set; }

    public string DeliveryStatus { get; set; } = null!;

    public bool Isdeleted { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public int? CreatedByUserId { get; set; }

    public int? ModifiedByUserId { get; set; }

    public virtual User? CreatedByUser { get; set; }

    public virtual ICollection<MedicineScheduleLink> MedicineScheduleLinks { get; set; } = new List<MedicineScheduleLink>();

    public virtual User? ModifiedByUser { get; set; }

    public virtual Parent Parent { get; set; } = null!;

    public virtual Staff Staff { get; set; } = null!;

    public virtual Student Student { get; set; } = null!;
}

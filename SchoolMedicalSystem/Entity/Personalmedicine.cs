using System;
using System.Collections.Generic;

namespace SchoolMedicalSystem.Entity;

public partial class Personalmedicine
{
    public int Personalmedicineid { get; set; }

    public int Studentid { get; set; }

    public string Medicinename { get; set; } = null!;

    public int Quantity { get; set; }

    public DateTime Receivedate { get; set; }

    public DateTime Expirydate { get; set; }

    public int Staffid { get; set; }

    public string DeliveryStatus { get; set; } = null!;

    public bool Isdeleted { get; set; }

    public DateTime? Createddate { get; set; }

    public DateTime? Modifieddate { get; set; }

    public int? ModifiedByUserId { get; set; }

    public virtual ICollection<MedicineScheduleLink> MedicineScheduleLinks { get; set; } = new List<MedicineScheduleLink>();

    public virtual Staff Staff { get; set; } = null!;

    public virtual Student Student { get; set; } = null!;
}

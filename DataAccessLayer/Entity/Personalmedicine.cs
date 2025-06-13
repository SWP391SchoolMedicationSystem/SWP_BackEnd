using System;
using System.Collections.Generic;

namespace DataAccessLayer.Entity;

public partial class Personalmedicine
{
    public int Personalmedicineid { get; set; }

    public int Studentid { get; set; }

    public string Medicinename { get; set; } = null!;

    public int Quanttiy { get; set; }

    public DateTime Receivedate { get; set; }

    public DateTime Expirydate { get; set; }

    public int Staffid { get; set; }

    public bool Isdeleted { get; set; }

    public int Createdby { get; set; }

    public DateTime Createddate { get; set; }

    public int? Modifiedby { get; set; }

    public DateTime? Modifieddate { get; set; }

    public virtual Personalmedicineschedule? Personalmedicineschedule { get; set; }

    public virtual Staff Staff { get; set; } = null!;

    public virtual Student Student { get; set; } = null!;
}

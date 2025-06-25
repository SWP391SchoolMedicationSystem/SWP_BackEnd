using System;
using System.Collections.Generic;

namespace DataAccessLayer.Entity;

public partial class Vaccinationrecord
{
    public int Vaccinationrecordid { get; set; }

    public int Studentid { get; set; }

    public int Vaccinationeventid { get; set; }

    public string Vaccinename { get; set; } = null!;

    public int Dosenumber { get; set; }

    public DateOnly Vaccinationdate { get; set; }

    public bool Confirmedbyparent { get; set; }

    public bool? WillAttend { get; set; }

    public string? ReasonForDecline { get; set; }

    public bool? ParentConsent { get; set; }

    public DateTime? ResponseDate { get; set; }

    public bool Isdeleted { get; set; }

    public DateTime Createdat { get; set; }

    public DateTime Updatedat { get; set; }

    public string Createdby { get; set; } = null!;

    public string Updatedby { get; set; } = null!;

    public virtual Student Student { get; set; } = null!;

    public virtual Vaccinationevent Vaccinationevent { get; set; } = null!;
}

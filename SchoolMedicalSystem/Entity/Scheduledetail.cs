using System;
using System.Collections.Generic;

namespace SchoolMedicalSystem.Entity;

public partial class Scheduledetail
{
    public int Scheduledetailid { get; set; }

    public int Dayinweek { get; set; }

    public TimeOnly Starttime { get; set; }

    public TimeOnly Endtime { get; set; }

    public bool Isdeleted { get; set; }

    public string? Modifiedby { get; set; }

    public DateTime? Modifieddate { get; set; }

    public virtual ICollection<Medicineschedule> Medicineschedules { get; set; } = new List<Medicineschedule>();
}

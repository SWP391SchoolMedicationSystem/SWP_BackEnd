using System;
using System.Collections.Generic;

namespace DataAccessLayer.Entity;

public partial class Medicineschedule
{
    public int Schedulemedicineid { get; set; }

    public int Scheduledetails { get; set; }

    public int Personalmedicineid { get; set; }

    public string? Notes { get; set; }

    public int? Duration { get; set; }

    public DateTime? Startdate { get; set; }

    public virtual Personalmedicine Personalmedicine { get; set; } = null!;

    public virtual Scheduledetail ScheduledetailsNavigation { get; set; } = null!;
}

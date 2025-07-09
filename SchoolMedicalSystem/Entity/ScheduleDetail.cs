using System;
using System.Collections.Generic;

namespace SchoolMedicalSystem.Entity;

public partial class ScheduleDetail
{
    public int ScheduleDetailId { get; set; }

    public int DayOfWeek { get; set; }

    public TimeOnly TimeSlot { get; set; }

    public virtual ICollection<MedicineScheduleLink> MedicineScheduleLinks { get; set; } = new List<MedicineScheduleLink>();
}

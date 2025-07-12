using System;
using System.Collections.Generic;

namespace DataAccessLayer.Entity;

public partial class ScheduleDetail
{
    public int ScheduleDetailId { get; set; }

    public int DayOfWeek { get; set; }

    public TimeOnly StartTime { get; set; }

    public TimeOnly EndTime { get; set; }

    public string? Notes { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public int? CreatedByUserId { get; set; }

    public int? ModifiedByUserId { get; set; }

    public virtual User? CreatedByUser { get; set; }

    public virtual ICollection<MedicineScheduleLink> MedicineScheduleLinks { get; set; } = new List<MedicineScheduleLink>();

    public virtual User? ModifiedByUser { get; set; }
}

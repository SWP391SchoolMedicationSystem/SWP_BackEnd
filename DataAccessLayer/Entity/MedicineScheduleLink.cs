using System;
using System.Collections.Generic;

namespace DataAccessLayer.Entity;

public partial class MedicineScheduleLink
{
    public int MedicineScheduleLinkId { get; set; }

    public int PersonalMedicineId { get; set; }

    public int ScheduleDetailId { get; set; }

    public DateOnly StartDate { get; set; }

    public int DurationInWeeks { get; set; }

    public string? Note { get; set; }

    public string Status { get; set; } = null!;

    public DateTime? ApprovalDate { get; set; }

    public string? ReasonForDecline { get; set; }

    public bool IsDeleted { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public int? CreatedByUserId { get; set; }

    public int? ModifiedByUserId { get; set; }

    public virtual User? CreatedByUser { get; set; }

    public virtual User? ModifiedByUser { get; set; }

    public virtual Personalmedicine PersonalMedicine { get; set; } = null!;

    public virtual ScheduleDetail ScheduleDetail { get; set; } = null!;
}

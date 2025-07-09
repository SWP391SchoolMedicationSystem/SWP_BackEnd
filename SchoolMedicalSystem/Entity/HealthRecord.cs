using System;
using System.Collections.Generic;

namespace SchoolMedicalSystem.Entity;

public partial class HealthRecord
{
    public int HealthRecordId { get; set; }

    public int StudentId { get; set; }

    public int HealthCategoryId { get; set; }

    public DateTime HealthRecordDate { get; set; }

    public string HealthRecordTitle { get; set; } = null!;

    public string? HealthRecordDescription { get; set; }

    public int StaffId { get; set; }

    public string Status { get; set; } = null!;

    public DateTime? Createddate { get; set; }

    public DateTime? Modifieddate { get; set; }

    public int? ModifiedByUserId { get; set; }

    public virtual HealthRecordCategory HealthCategory { get; set; } = null!;

    public virtual Staff Staff { get; set; } = null!;

    public virtual Student Student { get; set; } = null!;
}

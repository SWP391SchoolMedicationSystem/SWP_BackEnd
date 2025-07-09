using System;
using System.Collections.Generic;

namespace SchoolMedicalSystem.Entity;

public partial class Vaccine
{
    public int VaccineId { get; set; }

    public string VaccineName { get; set; } = null!;

    public string TargetDisease { get; set; } = null!;

    public string? Manufacturer { get; set; }

    public bool IsMandatory { get; set; }

    public string? TargetAudience { get; set; }

    public string? Description { get; set; }

    public string? MoreInfoUrl { get; set; }

    public bool IsDeleted { get; set; }

    public int? CreatedByUserId { get; set; }

    public DateTime? CreatedDate { get; set; }

    public int? ModifiedByUserId { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public virtual ICollection<StudentVaccinationRecord> StudentVaccinationRecords { get; set; } = new List<StudentVaccinationRecord>();

    public virtual ICollection<VaccineOfferedInEvent> VaccineOfferedInEvents { get; set; } = new List<VaccineOfferedInEvent>();
}

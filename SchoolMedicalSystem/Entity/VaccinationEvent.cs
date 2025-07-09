using System;
using System.Collections.Generic;

namespace SchoolMedicalSystem.Entity;

public partial class VaccinationEvent
{
    public int EventId { get; set; }

    public string EventName { get; set; } = null!;

    public DateTime EventDate { get; set; }

    public string Location { get; set; } = null!;

    public string? Description { get; set; }

    public bool IsDeleted { get; set; }

    public int? CreatedByUserId { get; set; }

    public DateTime? CreatedDate { get; set; }

    public int? ModifiedByUserId { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public virtual ICollection<StudentVaccinationRecord> StudentVaccinationRecords { get; set; } = new List<StudentVaccinationRecord>();

    public virtual ICollection<VaccineOfferedInEvent> VaccineOfferedInEvents { get; set; } = new List<VaccineOfferedInEvent>();
}

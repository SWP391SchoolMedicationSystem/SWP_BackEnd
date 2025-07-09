using System;
using System.Collections.Generic;

namespace DataAccessLayer.Entity;

public partial class VaccineOfferedInEvent
{
    public int EventId { get; set; }

    public int VaccineId { get; set; }

    public decimal Fee { get; set; }

    public string? Notes { get; set; }

    public virtual VaccinationEvent Event { get; set; } = null!;

    public virtual Vaccine Vaccine { get; set; } = null!;
}

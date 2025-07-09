using System;
using System.Collections.Generic;

namespace SchoolMedicalSystem.Entity;

public partial class Healthcheck
{
    public int Checkid { get; set; }

    public int Studentid { get; set; }

    public DateTime Checkdate { get; set; }

    public int Staffid { get; set; }

    public decimal? Height { get; set; }

    public decimal? Weight { get; set; }

    public decimal? Visionleft { get; set; }

    public decimal? Visionright { get; set; }

    public string? Bloodpressure { get; set; }

    public string? Notes { get; set; }

    public bool Isdeleted { get; set; }

    public DateTime? Createdat { get; set; }

    public DateTime? Updatedat { get; set; }

    public int? ModifiedByUserId { get; set; }

    public virtual Staff Staff { get; set; } = null!;

    public virtual Student Student { get; set; } = null!;
}

using System;
using System.Collections.Generic;

namespace DataAccessLayer.Entity;

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

    public DateTime? CreatedAt { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public int? CreatedByUserId { get; set; }

    public int? ModifiedByUserId { get; set; }

    public virtual User? CreatedByUser { get; set; }

    public virtual User? ModifiedByUser { get; set; }

    public virtual Staff Staff { get; set; } = null!;

    public virtual Student Student { get; set; } = null!;
}

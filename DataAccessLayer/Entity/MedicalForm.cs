using System;
using System.Collections.Generic;

namespace DataAccessLayer.Entity;

public partial class MedicalForm
{
    public int FormId { get; set; }

    public int StudentId { get; set; }

    public int CategoryId { get; set; }

    public DateTime SubmissionTimestamp { get; set; }

    public string Details { get; set; } = null!;

    public string Status { get; set; } = null!;

    public string? AdminResponse { get; set; }

    public int? ResponderId { get; set; }

    public DateTime? ResponseTimestamp { get; set; }

    public virtual FormCategory Category { get; set; } = null!;

    public virtual Student Student { get; set; } = null!;
}

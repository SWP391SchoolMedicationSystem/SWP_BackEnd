using System;
using System.Collections.Generic;

namespace DataAccessLayer.Entity;

public partial class AccidentReport
{
    public int ReportId { get; set; }

    public int StudentId { get; set; }

    public int ReportedByStaffId { get; set; }

    public DateTime ReportDate { get; set; }

    public string AccidentType { get; set; } = null!;

    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string? ActionTaken { get; set; }

    public string Status { get; set; } = null!;

    public DateTime? ReviewDate { get; set; }

    public bool IsNotifiedParent { get; set; }

    public DateTime? ParentNotificationDate { get; set; }

    public bool IsDeleted { get; set; }

    public int? ModifiedByUserId { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public virtual Staff ReportedByStaff { get; set; } = null!;

    public virtual Student Student { get; set; } = null!;
}

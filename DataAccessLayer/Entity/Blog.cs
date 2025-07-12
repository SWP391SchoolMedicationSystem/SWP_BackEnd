using System;
using System.Collections.Generic;

namespace DataAccessLayer.Entity;

public partial class Blog
{
    public int BlogId { get; set; }

    public string Title { get; set; } = null!;

    public string Content { get; set; } = null!;

    public int? ApprovedBy { get; set; }

    public DateTime? ApprovedOn { get; set; }

    public DateTime? ResponseDate { get; set; }

    public string? ReasonForDecline { get; set; }

    public int CreatedByUserId { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public int? ModifiedByUserId { get; set; }

    public string Status { get; set; } = null!;

    public bool IsDeleted { get; set; }

    public string? Image { get; set; }

    public virtual Staff? ApprovedByNavigation { get; set; }

    public virtual User CreatedByUser { get; set; } = null!;

    public virtual User? ModifiedByUser { get; set; }
}

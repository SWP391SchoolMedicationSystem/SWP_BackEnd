using System;
using System.Collections.Generic;

namespace DataAccessLayer.Entity;

public partial class EmailTemplate
{
    public int EmailTemplateId { get; set; }

    public string? To { get; set; }

    public string? Subject { get; set; }

    public string? Body { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime UpdatedDate { get; set; }

    public int CreatedBy { get; set; }

    public int? UpdatedBy { get; set; }

    public virtual User CreatedByNavigation { get; set; } = null!;

    public virtual User UpdatedByNavigation { get; set; } = null!;
}

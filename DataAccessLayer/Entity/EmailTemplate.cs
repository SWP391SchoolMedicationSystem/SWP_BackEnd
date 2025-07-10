using System;
using System.Collections.Generic;

namespace DataAccessLayer.Entity;

public partial class EmailTemplate
{
    public int EmailTemplateId { get; set; }

    public string? To { get; set; }

    public string? Subject { get; set; }

    public string? Body { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public int? CreatedByUserId { get; set; }

    public int? ModifiedByUserId { get; set; }

    public bool IsDeleted { get; set; }
}

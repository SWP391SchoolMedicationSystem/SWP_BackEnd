using System;
using System.Collections.Generic;

namespace DataAccessLayer.Entity;

public partial class EmailTemplate
{
    public int EmailTemplateId { get; set; }

    public string? To { get; set; }

    public string? Subject { get; set; }

    public string? Body { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public string? CreatedBy { get; set; }

    public string? UpdatedBy { get; set; }

    public bool IsDeleted { get; set; }
}

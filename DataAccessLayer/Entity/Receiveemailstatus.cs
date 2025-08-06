using System;
using System.Collections.Generic;

namespace DataAccessLayer.Entity;

public partial class Receiveemailstatus
{
    public int Id { get; set; }

    public int TemplateId { get; set; }

    public string Email { get; set; } = null!;

    public string Status { get; set; } = null!;

    public DateTime? Senddate { get; set; }

    public virtual EmailTemplate Template { get; set; } = null!;
}

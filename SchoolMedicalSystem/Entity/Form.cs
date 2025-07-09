using System;
using System.Collections.Generic;

namespace SchoolMedicalSystem.Entity;

public partial class Form
{
    public int FormId { get; set; }

    public int Parentid { get; set; }

    public int FormcategoryId { get; set; }

    public string Title { get; set; } = null!;

    public string Reason { get; set; } = null!;

    public byte[]? FileData { get; set; }

    public int? Staffid { get; set; }

    public string Status { get; set; } = null!;

    public string? Reasonfordecline { get; set; }

    public DateTime? Createddate { get; set; }

    public DateTime? Modifieddate { get; set; }

    public virtual FormSubmissionCategory Formcategory { get; set; } = null!;

    public virtual Parent Parent { get; set; } = null!;

    public virtual Staff? Staff { get; set; }
}

using System;
using System.Collections.Generic;

namespace DataAccessLayer.Entity;

public partial class Form
{
    public int Parentid { get; set; }

    public int FormId { get; set; }

    public int? FormcategoryId { get; set; }

    public string Title { get; set; } = null!;

    public string Reason { get; set; } = null!;

    public string? Originalfilename { get; set; } = null!;
    public string? Storedpath { get; set; } = null!;

    public int? Staffid { get; set; }

    public bool? Isaccepted { get; set; }

    public string? Reasonfordecline { get; set; }

    public string? Createdby { get; set; }

    public DateTime? Createddate { get; set; }

    public string? Modifiedby { get; set; }

    public DateTime? Modifieddate { get; set; }

    public virtual Formcategory1? Formcategory { get; set; }

    public virtual Parent? Parent { get; set; }

    public virtual Staff? Staff { get; set; }
}

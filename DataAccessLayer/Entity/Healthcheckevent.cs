using System;
using System.Collections.Generic;

namespace DataAccessLayer.Entity;

public partial class Healthcheckevent
{
    public int Healthcheckeventid { get; set; }

    public string Healthcheckeventname { get; set; } = null!;

    public string? Healthcheckeventdescription { get; set; }

    public string Location { get; set; } = null!;

    public DateTime Eventdate { get; set; }

    public string Createdby { get; set; } = null!;

    public DateTime Createddate { get; set; }

    public string? Modifiedby { get; set; }

    public DateTime? Modifieddate { get; set; }

    public string? Documentaccesstoken { get; set; }

    public string? Documentfilename { get; set; }

    public bool Isdeleted { get; set; }

    public virtual ICollection<Healthcheckrecordevent> Healthcheckrecordevents { get; set; } = new List<Healthcheckrecordevent>();
}

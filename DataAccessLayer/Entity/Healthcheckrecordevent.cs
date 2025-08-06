using System;
using System.Collections.Generic;

namespace DataAccessLayer.Entity;

public partial class Healthcheckrecordevent
{
    public int Healthcheckrecordeventid { get; set; }

    public int Healthcheckid { get; set; }

    public int Healthcheckeventid { get; set; }

    public bool Isdeleted { get; set; }

    public virtual Healthcheck Healthcheck { get; set; } = null!;

    public virtual Healthcheckevent Healthcheckevent { get; set; } = null!;
}

using System;
using System.Collections.Generic;

namespace DataAccessLayer.Entity;

public partial class Healthcheckrecordevent
{
    public int Healthcheckrecordeventid { get; set; }

    public int Healthcheckeventid { get; set; }

    public int Healthcheckrecordid { get; set; }

    public bool Isdeleted { get; set; }

    public virtual Healthcheckevent Healthcheckevent { get; set; } = null!;

    public virtual Healthcheck Healthcheckrecord { get; set; } = null!;
}

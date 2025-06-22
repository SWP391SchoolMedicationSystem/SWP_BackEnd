﻿using System;
using System.Collections.Generic;

namespace DataAccessLayer.Entity;

public partial class Vaccinationevent
{
    public int Vaccinationeventid { get; set; }

    public string Vaccinationeventname { get; set; } = null!;

    public string Location { get; set; } = null!;

    public string Organizedby { get; set; } = null!;

    public DateTime EventDate { get; set; }

    public string Description { get; set; } = null!;

    public DateTime Createddate { get; set; }

    public DateTime Modifieddate { get; set; }

    public string Createdby { get; set; } = null!;

    public string Modifiedby { get; set; } = null!;

    public bool Isdeleted { get; set; }

    public virtual ICollection<Vaccinationrecord> Vaccinationrecords { get; set; } = new List<Vaccinationrecord>();
}

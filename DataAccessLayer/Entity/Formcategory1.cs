using System;
using System.Collections.Generic;

namespace DataAccessLayer.Entity;

public partial class Formcategory1
{
    public int Categoryid { get; set; }

    public string Categoryname { get; set; } = null!;

    public bool? Isdeleted { get; set; }

    public virtual ICollection<Form> Forms { get; set; } = new List<Form>();
}

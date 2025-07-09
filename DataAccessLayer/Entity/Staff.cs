using System;
using System.Collections.Generic;

namespace DataAccessLayer.Entity;

public partial class Staff
{
    public int Staffid { get; set; }

    public string Fullname { get; set; } = null!;

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public int Roleid { get; set; }

    public int Userid { get; set; }

    public bool IsDeleted { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public int? CreatedByUserId { get; set; }

    public int? ModifiedByUserId { get; set; }

    public virtual ICollection<AccidentReport> AccidentReports { get; set; } = new List<AccidentReport>();

    public virtual ICollection<Form> Forms { get; set; } = new List<Form>();

    public virtual ICollection<HealthRecord> HealthRecords { get; set; } = new List<HealthRecord>();

    public virtual ICollection<Healthcheck> Healthchecks { get; set; } = new List<Healthcheck>();

    public virtual ICollection<MedicineStorage> MedicineStorages { get; set; } = new List<MedicineStorage>();

    public virtual ICollection<Notificationstaffdetail> Notificationstaffdetails { get; set; } = new List<Notificationstaffdetail>();

    public virtual ICollection<Personalmedicine> Personalmedicines { get; set; } = new List<Personalmedicine>();

    public virtual Role Role { get; set; } = null!;

    public virtual ICollection<StudentVaccinationRecord> StudentVaccinationRecords { get; set; } = new List<StudentVaccinationRecord>();

    public virtual User User { get; set; } = null!;
}

using System;
using System.Collections.Generic;

namespace SchoolMedicalSystem.Entity;

public partial class Student
{
    public int Studentid { get; set; }

    public string Fullname { get; set; } = null!;

    public int Age { get; set; }

    public DateOnly Dob { get; set; }

    public bool Gender { get; set; }

    public int Classid { get; set; }

    public int Parentid { get; set; }

    public bool IsDeleted { get; set; }

    public string? StudentCode { get; set; }

    public string? BloodType { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public int? CreatedByUserId { get; set; }

    public int? ModifiedByUserId { get; set; }

    public virtual ICollection<AccidentReport> AccidentReports { get; set; } = new List<AccidentReport>();

    public virtual Classroom Class { get; set; } = null!;

    public virtual ICollection<HealthRecord> HealthRecords { get; set; } = new List<HealthRecord>();

    public virtual ICollection<Healthcheck> Healthchecks { get; set; } = new List<Healthcheck>();

    public virtual Parent Parent { get; set; } = null!;

    public virtual ICollection<Personalmedicine> Personalmedicines { get; set; } = new List<Personalmedicine>();

    public virtual ICollection<StudentVaccinationRecord> StudentVaccinationRecords { get; set; } = new List<StudentVaccinationRecord>();
}

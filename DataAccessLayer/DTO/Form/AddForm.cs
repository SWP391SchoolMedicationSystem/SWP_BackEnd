using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DTO.Form
{
    public class AddFormMedicine
    {
        public int ParentId { get; set; }
        public int StudentId { get; set; }
        public string Title { get; set; } = null!;

        public string MedicineName { get; set; } = null!;
        public string? MedicineDescription { get; set; } = null!;
        public string? Reason { get; set; } = null!;
        public string CreatedBy { get; set; } = null!;
    }
    public class AddFormAbsent
    {
        public int ParentId { get; set; }
        public int StudentId { get; set; }
        public string Title { get; set; } = null!;
        public string? ReasonForAbsent { get; set; } = null!;
        public string AbsentDate { get; set; } = null!;

        public string CreatedBy { get; set; } = null!;
    }
    public class AddFormChronicIllness
    {
        public int ParentId { get; set; }
        public int StudentId { get; set; }
        public string Title { get; set; } = null!;
        public string? ChronicIllnessName { get; set; } = null!;
        public string? ChronicIllnessDescription { get; set; } = null!;
        public string? Systoms { get; set; } = null!;
        public string? ActionRequired { get; set; } = null!;
        public string CreatedBy { get; set; } = null!;
    }
    public class AddFormPhysicalActivityModification
    {
        public int StudentID { get; set; }
        public int HealthCategoryID { get; set; }
        public DateTime HealthRecordDate { get; set; }
        public string Healthrecordtitle { get; set; } = null!;
        public string Healthrecorddescription { get; set; } = null!;
        public int Staffid { get; set; }
        public bool IsConfirm { get; set; }
        public string CreatedBy { get; set; } = null!;
        public DateTime? CreatedDate { get; set; } = DateTime.Now;
    }

}

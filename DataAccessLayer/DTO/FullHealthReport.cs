using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.DTO.HealthRecords;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Identity.Client;

namespace DataAccessLayer.DTO
{
    public class FullHealthReport
    {
        //Student Information
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public string DOB { get; set; }
        public string Classroom { get; set; }
        public string Address { get; set; }
        //Parent Information
        public string ParentName { get; set; }
        public string ParentContact { get; set; }
        public string ParentEmail { get;set; }
        //Health Form Record Information
        public int IllnessCount { get; set; }
        public int AbsenceRequestCount { get; set; }
        public int MedicineSentCount { get; set; }
        public int MedicationScheduleCount { get; set; }
        public int SeriousIncidentCount { get; set; }
        //Count of Accidents By Dates
        public int NormalAccidentCountOnMonday { get; set; }
        public int SeriousAccidentCountOnMonday { get; set; }
        public int NormalAccidentCountOnTuesday { get; set; }
        public int SeriousAccidentCountOnTuesday { get; set; }
        public int NormalAccidentCountOnWednesday { get; set; }
        public int SeriousAccidentCountOnWednesday { get; set; }
        public int NormalAccidentCountOnThursday { get; set; }
        public int SeriousAccidentCountOnThursday { get; set; }
        public int NormalAccidentCountOnFriday { get; set; }
        public int SeriousAccidentCountOnFriday { get; set; }
        public int NormalAccidentCountOnSaturday { get; set; }
        public int SeriousAccidentCountOnSaturday { get; set; }
        //HealthRecord
        public string HealthCategoryName { get; set; } = null!;

        public string HealthRecordTitle { get; set; } = null!;
        public string HealthRecordDescription { get; set; } = null!;
        //Vaccination 
        public string Vaccinename { get; set; } = null!;
        public int Dosenumber { get; set; }
        public DateOnly Vaccinationdate { get; set; }
        //Special Needs
        public string SpecialNeedsCategory { get; set; } = null!;
        public string SpecialNeedsNotes { get; set; } = null!;



    }
}

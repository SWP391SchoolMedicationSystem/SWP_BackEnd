using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DTO.HealthRecords
{
    public class HealthRecordStudentCheck
    {
        public string StudentName { get; set; }
        public string HealthCategory { get; set; }
        public DateTime HealthRecordDate { get; set; }
        public string Healthrecordtitle { get; set; } = null!;
        public string Healthrecorddescription { get; set; } = null!;
        public string StaffName { get; set; }
        public List<VaccinationRecordDTO>? VaccinationRecords { get; set; } = new List<VaccinationRecordDTO>();
        public List<HealthCheckDTO>? HealthChecks { get; set; } = new List<HealthCheckDTO>();
        public string Status { get; set; }
    }
}

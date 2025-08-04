using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.DTO.HealthCheck;

namespace DataAccessLayer.DTO.HealthRecords
{
    public class HealthRecordStudentCheck
    {
        public int HealthRecordId { get; set; }
        public string StudentName { get; set; }
        public string HealthCategory { get; set; }
        public DateTime HealthRecordDate { get; set; }
        public string Healthrecordtitle { get; set; } = null!;
        public string Healthrecorddescription { get; set; } = null!;
        public string StaffName { get; set; }
        public List<VaccinationRecordDTO> VaccinationRecords { get; set; } = new List<VaccinationRecordDTO>();
        public HealthCheckDtoIgnoreClass HealthChecks { get; set; } = new HealthCheckDtoIgnoreClass();
        public bool IsConfirm { get; set; }
    }
}

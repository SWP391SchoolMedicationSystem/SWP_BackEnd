using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DTO.HealthRecords
{
    public class UpdateHealthRecordDTO
    {
        public int StudentID { get; set; }
        public int HealthCategoryID { get; set; }
        public DateTime HealthRecordDate { get; set; }
        public string Healthrecordtitle { get; set; }
        public string Healthrecorddescription { get; set; }
        public int Staffid { get; set; }
        public bool IsConfirm { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; } = DateTime.Now;
    }
}

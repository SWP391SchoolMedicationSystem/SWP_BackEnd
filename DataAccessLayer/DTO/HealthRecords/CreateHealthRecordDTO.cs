using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DTO.HealthRecords
{
    public class CreateHealthRecordDTO
    {
        public int StudentID { get; set; }
        public int HealthCategoryID { get; set; }
        public DateTime HealthRecordDate { get; set; }
        public string Healthrecordtitle { get; set; }
        public string Healthrecorddescription { get; set; }
        public int Staffid { get; set; }
        public bool IsConfirm { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; } = DateTime.Now;
    }
}

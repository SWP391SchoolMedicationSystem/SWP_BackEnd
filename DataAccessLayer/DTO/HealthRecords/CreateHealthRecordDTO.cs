using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DTO.HealthRecords
{
    public class CreateHealthRecordDTO
    {
        public int StudentId { get; set; }

        public int HealthCategoryId { get; set; }

        public DateTime HealthRecordDate { get; set; }

        public string HealthRecordTitle { get; set; } = null!;

        public string? HealthRecordDescription { get; set; }

        public int StaffId { get; set; }
        public int? CreatedByUserId { get; set; }
    }
}

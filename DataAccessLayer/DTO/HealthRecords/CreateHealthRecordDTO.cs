using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DTO.HealthRecords
{
    public class CreateHealthRecordDTO
    {
        public int studentID { get; set; }

        public int HealthCategoryID { get; set; }

        public DateTime HealthRecordDate { get; set; }

        public string healthrecordtitle { get; set; } = null!;

        public string? healthrecorddescription { get; set; }


        public int? CreatedByUserId { get; set; }
    }
}

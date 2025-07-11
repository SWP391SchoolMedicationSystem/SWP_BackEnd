using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DTO.HealthRecords
{
    public class UpdateHealthRecordDTO
    {
        public int healthrecordid { get; set; }

        public int studentid { get; set; }

        public int healthcategoryid { get; set; }

        public DateTime healthrecorddate { get; set; }

        public string healthrecordtitle { get; set; } = null!;

        public string? healthrecorddescription { get; set; }

        public int staffid { get; set; }

        public string Status { get; set; } = null!;
        public int? ModifiedByUserId { get; set; }
    }
}

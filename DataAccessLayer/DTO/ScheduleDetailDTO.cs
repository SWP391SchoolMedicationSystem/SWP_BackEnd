using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DTO
{
    public class ScheduleDetailDTO
    {
        public int Dayinweek { get; set; }

        public TimeOnly Starttime { get; set; }
        public TimeOnly Endtime { get; set; }

        public bool Isdeleted { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? ModifiedAt { get; set; }

        public int? CreatedByUserId { get; set; }

        public string? CreatedByUserName { get; set; } = null!;

        public int? ModifiedByUserId { get; set; }

        public string? ModifiedByUserName { get; set; } = null!;
        public string? Notes { get; set; }


    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DTO.HealthChecks
{
    public class HealthCheckDTO
    {
        public int Checkid { get; set; }
        public int Studentid { get; set; }
        public string StudentName { get; set; } = null!;

        public DateTime Checkdate { get; set; }

        public int Staffid { get; set; }
        public string StaffName { get; set; } = null!;

        public decimal? Height { get; set; }

        public decimal? Weight { get; set; }

        public decimal? Visionleft { get; set; }

        public decimal? Visionright { get; set; }

        public string? Bloodpressure { get; set; }

        public string? Notes { get; set; }
        public bool Isdeleted { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? ModifiedAt { get; set; }

        public int? CreatedByUserId { get; set; }

        public string? CreatedByUserName { get; set; } = null!;

        public int? ModifiedByUserId { get; set; }

        public string? ModifiedByUserName { get; set; } = null!;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DTO.HealthCheck
{
    public class HealthCheckDTO
    {
        public int Checkid { get; set; }
        public int Studentid { get; set; }
        public string StudentName { get; set; } = null!;

        public DateTime Checkdate { get; set; }

        public int Staffid { get; set; }

        public decimal? Height { get; set; }

        public decimal? Weight { get; set; }

        public decimal? Visionleft { get; set; }

        public decimal? Visionright { get; set; }

        public string? Bloodpressure { get; set; }

        public string? Notes { get; set; }
        public bool? Isdeleted { get; set; }

    }
}

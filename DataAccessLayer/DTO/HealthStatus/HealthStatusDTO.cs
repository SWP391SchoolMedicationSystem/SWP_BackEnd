using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DTO.HealthStatus
{
    public class HealthStatusDTO
    {
        public int StudentId { get; set; }

        public int? StaffId { get; set; }

        public int HealthStatusCategory { get; set; }

        public string? Description { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace DataAccessLayer.DTO.HealthCheck
{
    public class AddHealthCheckEventDto
    {
        public string Healthcheckeventname { get; set; } = null!;

        public string? Healthcheckeventdescription { get; set; }

        public string? Location { get; set; }

        public DateTime Eventdate { get; set; }

        public DateTime Eventtime { get; set; }
        public IFormFile? DocumentFile { get; set; }

        public string? Createdby { get; set; }



    }
}

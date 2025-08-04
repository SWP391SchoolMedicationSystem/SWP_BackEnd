using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DTO.HealthCheck
{
    public class HeatlhCheckEventDto
    {
        
    public int HealthcheckeventID { get; set; }

        public string Healthcheckeventname { get; set; } = null!;

        public string? Healthcheckeventdescription { get; set; }

        public string? Location { get; set; }

        public DateTime Eventdate { get; set; }

        public string? Documentfilename { get; set; }

        public string? Documentaccesstoken { get; set; }

        public string? Createdby { get; set; }

        public DateTime Createddate { get; set; }

        public bool Isdeleted { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Entity;

namespace DataAccessLayer.DTO.HealthCheck
{
    public class HeatlhCheckRecordEventDto
    {
        public int Healthcheckrecordeventid { get; set; }

        public int Healthcheckeventid { get; set; }

        public int Healthcheckrecordid { get; set; }

        public bool Isdeleted { get; set; }

        public virtual Healthcheckevent Healthcheckevent { get; set; } = null!;

        public virtual Healthcheck Healthcheckrecord { get; set; } = null!;
    }
}

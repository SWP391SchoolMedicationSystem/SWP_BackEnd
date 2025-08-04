using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.DTO.Students;
using DataAccessLayer.Entity;

namespace DataAccessLayer.DTO.HealthCheck
{
    public class HealthCheckDtoIgnoreClass
    {
        public int Healthcheckrecordeventid { get; set; }

        public int Healthcheckeventid { get; set; }

        public int Healthcheckrecordid { get; set; }

        public bool Isdeleted { get; set; }

        public virtual Healthcheckevent Healthcheckevent { get; set; } = null!;
        public StudentDTO student { get; set; } = new StudentDTO();

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DTO.Consultations
{
    public class ApprovalConsultationDTO
    {
        public int Consultationid { get; set; }

        public int? Staffid { get; set; }

        public int? Modifiedby { get; set; }

    }
}

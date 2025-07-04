using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DTO.Consultations
{
    public class UpdateConsultationDTO
    {
        public int Consultationid { get; set; }

        public int Parentid { get; set; }

        public int Studentid { get; set; }

        public int Requesttypeid { get; set; }

        public DateTime Requestdate { get; set; }

        public DateTime? Scheduledate { get; set; }

        public string Title { get; set; } = null!;

        public string? Description { get; set; }

        public string Status { get; set; } = null!;

        public int? Staffid { get; set; }

        public bool Isdelete { get; set; }

        public int Createdby { get; set; }
        public int? Modifiedby { get; set; }


    }
}

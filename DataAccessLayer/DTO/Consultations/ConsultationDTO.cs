using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DTO.Consultations
{
    public class ConsultationDTO
    {
        public int Consultationid { get; set; }

        public int Parentid { get; set; }
        public string ParentName { get; set; } = null!;
        public int? Staffid { get; set; }
        public string Staffname { get; set; } = null!;
        public int Studentid { get; set; }
        public string StudentName { get; set; } = null!;

        public int Requesttypeid { get; set; }

        public DateTime Requestdate { get; set; }

        public DateTime? Scheduledate { get; set; }

        public string Title { get; set; } = null!;

        public string? Description { get; set; }

        public string Status { get; set; } = null!;


        public bool Isdelete { get; set; }

        public int Createdby { get; set; }
        public string CreateByName { get; set; } = null!;

        public DateTime Createddate { get; set; }

        public int? Modifiedby { get; set; }

        public DateTime? Modifieddate { get; set; }
    }
}

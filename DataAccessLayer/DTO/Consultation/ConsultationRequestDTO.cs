using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DTO.Consultation
{
    public class ConsultationRequestDTO
    {

        public int Parentid { get; set; }

        public int Studentid { get; set; }

        public int Requesttypeid { get; set; }

        public string Title { get; set; } = null!;

        public string? Description { get; set; }

        public string Status { get; set; } = null!;


    }
}

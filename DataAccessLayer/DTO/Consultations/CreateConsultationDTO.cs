using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DTO.Consultations
{
    public class CreateConsultationDTO
    {

        public int Parentid { get; set; }

        public int Studentid { get; set; }

        public int Requesttypeid { get; set; }

        public string Title { get; set; } = null!;

        public string? Description { get; set; }


    }
}

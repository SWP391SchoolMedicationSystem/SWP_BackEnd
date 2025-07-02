using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DTO.Consultations
{
    public class CreateonsultationTypeDTO
    {
        public string Typename { get; set; } = null!;

        public string? Description { get; set; }
    }
}

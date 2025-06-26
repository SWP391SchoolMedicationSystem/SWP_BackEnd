using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DTO.PersonalMedicine
{
    public class AddPersonalMedicineDTO
    {
        public int Medicineid { get; set; }

        public int? Parentid { get; set; }

        public int? Studentid { get; set; }

        public int Quantity { get; set; }

        public DateTime Receiveddate { get; set; }

        public DateTime? ExpiryDate { get; set; }

        public bool Status { get; set; }

        public string? Note { get; set; }

    }
}

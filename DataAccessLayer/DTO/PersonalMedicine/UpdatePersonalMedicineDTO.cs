using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DTO.PersonalMedicine
{
    public class UpdatePersonalMedicineDTO
    {
        public int Personalmedicineid { get; set; }

        public int Studentid { get; set; }

        public int Parentid { get; set; }

        public string Medicinename { get; set; } = null!;

        public int Quantity { get; set; }

        public string Note { get; set; } = null!;


        public DateTime Receivedate { get; set; }

        public DateTime Expirydate { get; set; }

        public int Staffid { get; set; }

        public string DeliveryStatus { get; set; } = null!;

        public bool Isdeleted { get; set; }

        public int? ModifiedByUserId { get; set; }

    }
}

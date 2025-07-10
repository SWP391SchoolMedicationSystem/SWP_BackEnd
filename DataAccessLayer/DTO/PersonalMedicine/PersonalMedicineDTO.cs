using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Entity;

namespace DataAccessLayer.DTO.PersonalMedicine
{
    public class PersonalMedicineDTO
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

        public DateTime? CreatedAt { get; set; }

        public DateTime? ModifiedAt { get; set; }

        public int? CreatedByUserId { get; set; }

        public int? ModifiedByUserId { get; set; }

    }
}

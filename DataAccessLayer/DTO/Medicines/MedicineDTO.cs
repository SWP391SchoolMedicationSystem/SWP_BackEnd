using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DTO.Medicines
{
    public class MedicineDTO
    {
        public int Medicineid { get; set; }

        public string Medicinename { get; set; } = null!;

        public int Medicinecategoryid { get; set; }

        public string Type { get; set; } = null!;

        public int Quantity { get; set; }

        public DateTime? Createdat { get; set; }

        public DateTime? Updatedat { get; set; }

        public string? Createdby { get; set; }

        public string? Updatedby { get; set; }
    }
}

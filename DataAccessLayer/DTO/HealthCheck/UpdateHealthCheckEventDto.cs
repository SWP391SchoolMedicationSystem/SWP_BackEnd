using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DTO.HealthCheck
{
    public class UpdateHeatlhCheckEventDto
    {
        
    public int HealthcheckeventID { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tên sự kiện")]

        public string Healthcheckeventname { get; set; } = null!;

        public string? Healthcheckeventdescription { get; set; }

        public string? Location { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập ngày sự kiện")]
        [DataType(DataType.DateTime, ErrorMessage = "Ngày sự kiện không hợp lệ")]
        public DateTime Eventdate { get; set; }

        public bool Isdeleted { get; set; }
    }
}

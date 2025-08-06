using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DTO.HealthCheck
{
    public class AddHealthCheckDto
    {
        public int Eventid { get; set; }
        public int Studentid { get; set; }

        public DateTime Checkdate { get; set; }

        public int Staffid { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập chiều cao")]
        [Range(0, 300, ErrorMessage = "Chiều cao phải từ 0 đến 300 cm")]
        public decimal? Height { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập cân nặng")]
        [Range(0, 500, ErrorMessage = "Cân nặng phải từ 0 đến 500 kg")]
        public decimal? Weight { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập mắt trái")]
        [Range(0, 10, ErrorMessage = "Mắt trái phải từ 0 đến 10")]
        public decimal? Visionleft { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập mắt phải")]
        [Range(0, 10, ErrorMessage = "Mắt phải phải từ 0 đến 10")]
        public decimal? Visionright { get; set; }

        public string? Bloodpressure { get; set; }

        public string? Notes { get; set; }

    }
}

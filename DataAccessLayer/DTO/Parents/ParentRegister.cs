using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DTO.Parents
{
    public class ParentRegister
    {

        public string Fullname { get; set; } = null!;

        public string? Email { get; set; }

        [Required(ErrorMessage = "Phone number is required.")]

        public string Phone { get; set; } = null!;

        [Required(ErrorMessage = "Password is required.")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[A-Za-z\d]{8,}$",
            ErrorMessage = "Password must be at least 8 characters long," +
            " contain at least one uppercase letter, one lowercase letter, and one number.")]
        public string Password { get; set; } = null!;

        [Required(ErrorMessage = "Address is required.")]

        public string Address { get; set; } = null!;
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DTO
{
    public class EmailDTO
    {
        [Required(ErrorMessage = "From email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string? To { get; set; }
        [Required(ErrorMessage = "Title is required")]
        public string? Subject { get; set; }

        public string? Body { get; set; }

    }
}

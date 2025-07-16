using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DTO
{
    public class FileDTO
    {
        public string? OriginalFileName { get; set; }
        public string? StoredFileName { get; set; }
        public bool Success { get; set; }
        public string? ErrorMessage { get; set; }
    }
}

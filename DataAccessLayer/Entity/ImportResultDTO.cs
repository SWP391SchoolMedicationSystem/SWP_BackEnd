using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entity
{
    public class ImportResultDTO
    {
        public bool Success { get; set; }
        public int ImportedCount { get; set; }
        public string Message { get; set; }
        public List<ImportErrorDto> Errors { get; set; } = new List<ImportErrorDto>();
    }
}

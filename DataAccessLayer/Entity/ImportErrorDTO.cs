using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entity
{
    public class ImportErrorDto
    {
        public int Row { get; set; }
        public string Field { get; set; }
        public string Message { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DTO
{
    public class ScheduleDetailDTO
    {
        public int Dayinweek { get; set; }

        public TimeOnly Starttime { get; set; }

        public TimeOnly Endtime { get; set; }
    }
}

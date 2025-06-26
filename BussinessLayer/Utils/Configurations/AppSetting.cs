using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.Utils.Configurations
{
    public class AppSetting
    {
        public string SecretKey { get; set; } = null!;
        public string GoogleClientId { get; set; } = null!;
    }
}

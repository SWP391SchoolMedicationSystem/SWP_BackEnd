using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.Utils
{
    public static class Constants
    {
        public static class FormStatus
        {
            public readonly static string Pending = "Chờ phê duyệt";
            public readonly static string Accepted = "Đồng ý";
            public readonly static string Rejected = "Từ chối";
            public readonly static string Deleted = "Deleted";
        }

    }
}

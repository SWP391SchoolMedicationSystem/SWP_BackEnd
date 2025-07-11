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
        }
        public static class BlogStatus
        {
            public readonly static string Accepted = "Đã Duyệt";
            public readonly static string Rejected = "Từ chối";
            public readonly static string Draft = "Bản thảo";
        }

        public static class PersonalMedicineStatus
        {
            public readonly static string Pending = "Chờ phê duyệt";
            public readonly static string Accepted = "Đã Duyệt";
            public readonly static string Rejected = "Từ chối";
            public readonly static string Delivered = "Giao thành công";
        }
        public static class GenderStatus
        {
            public readonly static string Male = "Nam";
            public readonly static string Female = "Nữ";
            public readonly static string Other = "Khác";
        }
    }
}

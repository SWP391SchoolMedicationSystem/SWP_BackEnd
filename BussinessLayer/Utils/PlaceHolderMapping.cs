using DataAccessLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.Utils
{
    public class PlaceHolderMapping
    {
        public static readonly Dictionary<string, Func<object, string>> ParentPlaceholders = new()
        {
            { "{FULLNAME}", obj => ((Parent)obj).Fullname },
            { "{EMAIL}", obj => ((Parent) obj).Email ?? "" },
            { "{PHONE}", obj => ((Parent) obj).Phone ?? "" },
            { "{ADDRESS}", obj => ((Parent)obj).Address },
            { "{CREATED_DATE}", obj => ((Parent)obj).CreatedDate?.ToString("yyyy-MM-dd") ?? "" },
            { "{MODIFIED_DATE}", obj => ((Parent)obj).ModifiedDate?.ToString("yyyy-MM-dd") ?? "" },
            { "{USER_ID}", obj => ((Parent)obj).Userid.ToString() }
        };

        public static readonly Dictionary<string, Func<object, string>> ParentStudentPlaceholders = new()
        {
            { "{FULLNAME}", obj => ((Parent)obj).Fullname },
            { "{EMAIL}", obj => ((Parent) obj).Email ?? "" },
            { "{PHONE}", obj => ((Parent) obj).Phone ?? "" },
            { "{ADDRESS}", obj => ((Parent)obj).Address },
            { "{CREATED_DATE}", obj => ((Parent)obj).CreatedDate?.ToString("yyyy-MM-dd") ?? "" },
            { "{MODIFIED_DATE}", obj => ((Parent)obj).ModifiedDate?.ToString("yyyy-MM-dd") ?? "" },
            { "{USER_ID}", obj => ((Parent)obj).Userid.ToString() }
        };

        public static readonly Dictionary<Type, Dictionary<string, Func<object, string>>> AllMappings = new()
        {
            { typeof(Parent), ParentPlaceholders },
            //{ typeof(ParentStudent), ParentStudentPlaceholders }
            //{ typeof(Staff), StaffPlaceholders }
        };
    }
}

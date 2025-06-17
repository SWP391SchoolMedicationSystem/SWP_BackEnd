using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.Utils
{
    public class EmailDataFill
    {
        public static string FillEmailTemplate(string template, Dictionary<string, string> replacements)
        {
            if (string.IsNullOrEmpty(template) || replacements == null || !replacements.Any())
                return template;
            foreach (var kvp in replacements)
            {
                template = template.Replace($"{{{{{kvp.Key}}}}}", kvp.Value);
            }
            return template;
        }
    }
}

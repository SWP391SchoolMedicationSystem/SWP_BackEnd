using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.Utils
{
    public static class EmailDataFill
    {
        public static string FillEmailTemplate(string template, object data)
        {
            var type = data.GetType();
            if (!PlaceHolderMapping.AllMappings.TryGetValue(type, out var placeholders))
                throw new InvalidOperationException($"No placeholder mappings found for type: {type.Name}");

            StringBuilder body = new StringBuilder(template);
            foreach (var kvp in placeholders)
            {
                var placeholder = kvp.Key;
                var value = kvp.Value(data) ?? "";
                body.Replace(placeholder, value);
            }

            return body.ToString();
        }
    }
}

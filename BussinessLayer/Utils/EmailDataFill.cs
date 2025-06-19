using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.Utils
{
    public static class EmailDataFill
    {
        public static string FillEmailTemplate(string template, params object[] dataObjects)
        {
            var allPlaceholders = new Dictionary<string, string>();

            foreach (var obj in dataObjects)
            {
                var type = obj.GetType();
                if (!PlaceHolderMapping.AllMappings.TryGetValue(type, out var mappings))
                    throw new InvalidOperationException($"No mappings for {type.Name}");

                foreach (var kvp in mappings)
                {
                    if (!allPlaceholders.ContainsKey(kvp.Key))
                        allPlaceholders[kvp.Key] = kvp.Value(obj) ?? "";
                }
            }

            var body = new StringBuilder(template);
            foreach (var kvp in allPlaceholders)
            {
                body.Replace(kvp.Key, kvp.Value);
            }

            return body.ToString();
        }
    }
}

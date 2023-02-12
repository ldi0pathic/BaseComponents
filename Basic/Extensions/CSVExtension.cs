using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Extensions
{
    public static class CSVExtension
    {
        public static IEnumerable<string> ToCsv<T>(this IEnumerable<T> objectlist, string separator = ";", string separatorReplace = ",", bool header = true)
        {
            FieldInfo[] fields = typeof(T).GetFields();
            PropertyInfo[] properties = typeof(T).GetProperties();
            if (header)
            {
                yield return string.Join(separator, fields.Select(f => f.Name).Union(properties.Select(p => p.GetPropertyDisplayName().Replace(separator, separatorReplace))).ToArray());
            }
            foreach (var o in objectlist)
            {
                yield return string.Join(separator, fields.Select(f => (f.GetValue(o) ?? "").ToString().Replace(separator, separatorReplace))
                    .Union(properties.Select(p => (p.GetValue(o, null) ?? "").ToString().Replace(separator, separatorReplace))).ToArray());
            }
        }

        public static IEnumerable<string> ToCsv<T,T2>(this IDictionary<T,T2> objectlist, string separator = ";", string separatorReplace = ",", bool header = true)
        {
            FieldInfo[] fields = typeof(T2).GetFields();
            PropertyInfo[] properties = typeof(T2).GetProperties();
            if (header)
            {
                yield return string.Join(separator, new[] { "Key" }.Concat(fields.Select(f => f.Name).Union(properties.Select(p => p.GetPropertyDisplayName().Replace(separator, separatorReplace)))).ToArray());
            }
            foreach (var o in objectlist)
            {
                yield return string.Join(separator, new[] { o.Key.ToString() }.Concat(fields.Select(f => (f.GetValue(o.Value) ?? "").ToString().Replace(separator, separatorReplace))
                    .Union(properties.Select(p => (p.GetValue(o.Value, null) ?? "").ToString().Replace(separator, separatorReplace)))).ToArray());
            }
        }

        private static string GetPropertyDisplayName(this PropertyInfo pi)
        {
            var dp = pi.GetCustomAttributes(typeof(DisplayNameAttribute), true).Cast<DisplayNameAttribute>().SingleOrDefault();
            return dp != null ? dp.DisplayName : pi.Name;
        }

    }
}

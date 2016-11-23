using ObjectToQuery.Internal;
using System;

namespace ObjectToQuery
{
    public static class ObjectToQueryExtentions
    {
        public static string AppendObject<T>(this string source, T obj) where T : class
        {
            if (obj == null)
            {
                return source;
            }

            string delimiter = source.IndexOf("?", StringComparison.Ordinal) >= 0 ? "&" : "?";
            var query = obj.ToQuery();
            return string.IsNullOrWhiteSpace(query) ? source : $"{source}{delimiter}{query}";
        }

        public static string ToQuery<T>(this T filter, ToQueryOptions options=null) where T : class
        {
            return filter.ConvertToQuery(options);
        }
    }
}
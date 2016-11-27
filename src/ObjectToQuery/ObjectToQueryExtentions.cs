using ObjectToQuery.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ObjectToQuery
{
    public static class ObjectToQueryExtentions
    {
        /// <summary>
        /// Convert object to querystring and append to other string
        /// </summary>
        public static string AppendObject<T>(this string source, T obj, ToQueryOptions options = null) where T : class
        {
            if (obj == null)
            {
                return source;
            }

            string delimiter = source.IndexOf("?", StringComparison.Ordinal) >= 0 ? "&" : "?";
            var query = obj.ToQuery(options);
            return string.IsNullOrWhiteSpace(query) ? source : $"{source}{delimiter}{query}";
        }

        /// <summary>
        /// Convert object to querystirng
        /// </summary>
        public static string ToQuery<T>(this T filter, ToQueryOptions options = null) where T : class
        {
            return filter.ConvertToQuery(options ?? new ToQueryOptions());
        }

        public static string ToCacheKey<T>(this T filter, CacheKeyOptions options = null) where T : class
        {
            return filter.ConvertToQuery(options ?? new CacheKeyOptions());
        }

        /// <summary>
        /// Optional warmup of types for better performance
        /// </summary>
        public static async Task WarmUpAsync(params Type[] types)
        {
            var p = new PreLoader();
            List<Task> tasks = new List<Task> {
                p.PreloadDecimal(),
                p.PreloadGuid(),
                p.PreloadInt(),
                p.PreloadString()
            };

            tasks.AddRange(types.Select(p.PreloadType));
            await Task.WhenAll(tasks);
        }
    }
}
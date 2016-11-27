using System;
using System.Threading.Tasks;

namespace ObjectToQuery
{
    public interface IObjectToQueryConverter
    {
        /// <summary>
        /// Convert object to querystring and append to other string
        /// </summary>
        string AppendObject<T>(string source, T obj) where T : class;

        /// <summary>
        /// Convert object to querystirng
        /// </summary>
        string ToQuery<T>(T obj, ToQueryOptions options = null) where T : class;

        /// <summary>
        /// Convert object to querystirng
        /// </summary>
        string ToCacheKey<T>(T obj, CacheKeyOptions options = null) where T : class;

        /// <summary>
        /// Optional warmup of types for better performance
        /// </summary>
        Task WarmUpAsync(params Type[] types);
    }

    public class ObjectToQueryConverter : IObjectToQueryConverter
    {
        /// <summary>
        /// Convert object to querystring and append to other string
        /// </summary>
        public string AppendObject<T>(string source, T obj) where T : class
        {
            return source.AppendObject(obj);
        }

        /// <summary>
        /// Convert object to querystirng
        /// </summary>
        public string ToQuery<T>(T obj, ToQueryOptions options = null) where T : class
        {
            return obj.ToQuery(options);
        }

        public string ToCacheKey<T>(T obj, CacheKeyOptions options = null) where T : class
        {
            return obj.ToCacheKey(options);
        }

        /// <summary>
        /// Optional warmup of types for better performance
        /// </summary>
        public async Task WarmUpAsync(params Type[] types)
        {
            await ObjectToQueryExtentions.WarmUpAsync(types);
        }
    }
}
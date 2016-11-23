namespace ObjectToQuery
{
    public interface IObjectToQueryConverter
    {
        string AppendObject<T>(string source, T obj) where T : class;

        string ToQuery<T>(T filter, ToQueryOptions options = null) where T : class;
    }

    public class ObjectToQueryConverter : IObjectToQueryConverter
    {
        public string AppendObject<T>(string source, T obj) where T : class
        {
            return source.AppendObject(obj);
        }

        public string ToQuery<T>(T filter, ToQueryOptions options=null) where T : class
        {
            return filter.ToQuery(options);
        }
    }
}
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ObjectToQuery.Internal
{
    internal static class ObjectExtensions
    {
        private static readonly ConcurrentDictionary<Type, IReadOnlyList<PropertyInfo>> PropertyDictionary = new ConcurrentDictionary<Type, IReadOnlyList<PropertyInfo>>();

        internal static List<PropertyKeyValue> GetCachedProperties<T>(this T obj)
            where T : class
        {
            var type = obj?.GetType() ?? typeof(T);

            if (typeof(IEnumerable).GetTypeInfo().IsAssignableFrom(type))
            {
                throw new Exception("Lists and other enumerables are not supported.");
            }

            IReadOnlyList<PropertyInfo> properties = GetPropertiesForType(type);
            return properties.Select(property => new PropertyKeyValue { Key = property.Name, Value = property.GetValue(obj), Type = property.PropertyType }).ToList();
        }

        internal static IReadOnlyList<PropertyInfo> GetPropertiesForType(this Type type)
        {
            IReadOnlyList<PropertyInfo> properties;

            if (!PropertyDictionary.TryGetValue(type, out properties))
            {
                properties = type.GetTypeInfo().GetProperties().Where(property => property.CanRead).ToList();
                PropertyDictionary.TryAdd(type, properties);
            }

            return properties;
        }
    }
}
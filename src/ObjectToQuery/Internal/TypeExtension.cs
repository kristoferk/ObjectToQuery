using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reflection;

namespace ObjectToQuery.Internal
{
    internal static class TypeExtension
    {
        //a thread-safe way to hold default instances created at run-time
        private static readonly ConcurrentDictionary<Type, object> typeDefaults = new ConcurrentDictionary<Type, object>();

        public static object GetDefaultValue(this Type type)
        {
            return type.GetTypeInfo().IsValueType
                ? typeDefaults.GetOrAdd(type, Activator.CreateInstance)
                : null;
        }

        public static Type GetItemType<T>(this IEnumerable<T> enumerable)
        {
            return typeof(T);
        }
    }
}
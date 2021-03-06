﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace ObjectToQuery.Internal
{
    internal static class ObjectToQueryFunctions
    {
        internal static string ConvertToQuery<T>(this T filter, BaseOptions options) where T : class
        {
            if (filter == null)
            {
                return string.Empty;
            }

            if (options == null)
            {
                throw new ArgumentNullException(nameof(options), "Options must have value");
            }

            List<string> stringValues = new List<string>();

            List<PropertyKeyValue> properties = filter.GetCachedProperties();
            if (options.SortKeys)
            {
                foreach (var property in properties.OrderBy(p => p.Key))
                {
                    BuildParams(stringValues, property.Type, property.Key, property.Value, false, options);
                }
            }
            else
            {
                foreach (var property in properties)
                {
                    BuildParams(stringValues, property.Type, property.Key, property.Value, false, options);
                }
            }

            string result = string.Join("&", stringValues);
            if (options.ReplaceSpaceWithPlus)
            {
                result = result.Replace("%20", "+");
            }

            return result;
        }

        internal static string FormatKey(string key, BaseOptions options)
        {
            key = ChangeCase(key, options.KeyCase);

            if (options.KeyCase == KeyCase.LowerCase)
            {
                key = key.ToLowerInvariant();
            }

            if (!options.SkipEncoding)
            {
                key = Uri.EscapeDataString(key);
            }

            return key;
        }

        private static string ChangeCase(string key, KeyCase keyCase)
        {
            if (keyCase == KeyCase.CamelCase)
            {
                key = char.ToLowerInvariant(key[0]) + key.Substring(1);
            }

            if (keyCase == KeyCase.PascalCase)
            {
                key = char.ToUpperInvariant(key[0]) + key.Substring(1);
            }

            return key;
        }

        internal static string FormatValue(string value, BaseOptions options)
        {
            if (!options.SkipEncoding)
            {
                value = Uri.EscapeDataString(value);
            }

            if (options.ValueCase == ValueCase.LowerCase)
            {
                value = value.ToLowerInvariant();
            }

            return value;
        }

        internal static void Add(List<string> stringValues, string key, string value, BaseOptions options)
        {
            //Remove null values
            if (options.RemoveValues != RemoveValues.None && value == null)
            {
                return;
            }

            //Remove empty strings
            if ((options.RemoveValues == RemoveValues.NullOrEmpty ||
                 options.RemoveValues == RemoveValues.NullDefaultOrEmpty)
                && string.IsNullOrEmpty(value))
            {
                return;
            }

            stringValues.Add($"{FormatKey(key, options)}={FormatValue(value ?? string.Empty, options)}");
        }

        internal static bool AddPrimative(List<string> stringValues, string key, object value, bool isList, BaseOptions options)
        {
            var valueAsDateTime = value as DateTime?;
            if (valueAsDateTime.HasValue)
            {
                var dateFormat = options.ToCacheKey ? "yyyy-MM-ddTHH:mm:00.0000000Z" : "O";
                Add(stringValues, key, valueAsDateTime.Value.ToString(dateFormat), options);
                return true;
            }

            var valueAsDateTimeOffset = value as DateTimeOffset?;
            if (valueAsDateTimeOffset.HasValue)
            {
                var dateFormat = options.ToCacheKey ? "yyyy-MM-ddTHH:mm:00.0000000zzz" : "O";
                Add(stringValues, key, valueAsDateTimeOffset.Value.ToString(dateFormat), options);
                return true;
            }

            //var valueAsUrl = value as Url;
            //if (valueAsUrl != null)
            //{
            //    Add(stringValues, key, valueAsUrl.Value, options);
            //    return true;
            //}

            var valueAsUri = value as Uri;
            if (valueAsUri != null)
            {
                Add(stringValues, key, valueAsUri.ToString(), options);
                return true;
            }

            if (value is Enum)
            {
                var enumValue = options.EnumAsString ? value.ToString() : Convert.ToInt32(value).ToString();
                Add(stringValues, key, enumValue, options);
                return true;
            }

            var valueAsTimeSpan = value as TimeSpan?;
            if (valueAsTimeSpan.HasValue)
            {
                Add(stringValues, key, valueAsTimeSpan.Value.TotalMilliseconds.ToString(CultureInfo.InvariantCulture), options);
                return true;
            }

            var valueAsCultureInfo = value as CultureInfo;
            if (valueAsCultureInfo != null)
            {
                Add(stringValues, key, valueAsCultureInfo.Name, options);
                return true;
            }

            var guidValue = value as Guid?;
            if (guidValue.HasValue)
            {
                Add(stringValues, key, guidValue.Value.ToString(), options);
                return true;
            }

            var boolValue = value as bool?;
            if (boolValue.HasValue)
            {
                Add(stringValues, key, boolValue.Value.ToString().ToLowerInvariant(), options);
                return true;
            }

            var valueAsString = value as string;
            if (valueAsString != null)
            {
                Add(stringValues, key, valueAsString, options);
                return true;
            }

            return false;
        }

        internal static List<string> BuildParams(List<string> stringValues, Type valueType, string key, object value, bool isList, BaseOptions options)
        {
            //Handle primitive types
            if (AddPrimative(stringValues, key, value, isList, options))
            {
                return stringValues;
            }

            //Handle arrays and lists
            IEnumerable list = value as IEnumerable;
            if (!(value is string) && list != null)
            {
                var enumerable = list.Cast<object>().ToList();
                if (enumerable.Any())
                {
                    foreach (var listOject in enumerable)
                    {
                        var args = valueType.GetTypeInfo().GenericTypeArguments.FirstOrDefault();
                        BuildParams(stringValues, args, key, listOject, true, options);
                    }
                }

                return stringValues;
            }

            //Handle arrays and lists that has value null
            if (valueType != null &&
                typeof(IEnumerable).GetTypeInfo().IsAssignableFrom(valueType) &&
                !typeof(string).GetTypeInfo().IsAssignableFrom(valueType) &&
                value == null &&
                options.ToCacheKey)
            {
                Add(stringValues, key, "Null", options);
                return stringValues;
            }

            //Handle objects
            if (value != null)
            {
                var typeInfo = value.GetType().GetTypeInfo();
                if (!typeInfo.IsPrimitive && !(value is string) && !(value is decimal))
                {
                    if (options.SortKeys)
                    {
                        foreach (var property in value.GetCachedProperties().OrderBy(p => p.Key))
                        {
                            string propValue = ChangeCase(property.Key, options.KeyCase);
                            BuildParams(stringValues, property.Type, key + "." + propValue, property.Value, isList, options);
                        }
                    }
                    else
                    {
                        foreach (var property in value.GetCachedProperties())
                        {
                            string propValue = ChangeCase(property.Key, options.KeyCase);
                            BuildParams(stringValues, property.Type, key + "." + propValue, property.Value, false, options);
                        }
                    }

                    return stringValues;
                }

                if (options.RemoveValues == RemoveValues.NullOrDefault ||
                    options.RemoveValues == RemoveValues.NullDefaultOrEmpty)
                {
                    if (value.Equals(value.GetType().GetDefaultValue()))
                    {
                        return stringValues;
                    }
                }
            }

            //Fallback
            Add(stringValues, key, value?.ToString(), options);
            return stringValues;
        }
    }
}
﻿using System;

namespace ObjectToQuery
{
    public class ToQueryOptions : BaseOptions
    {
        public ToQueryOptions()
        {
            ToCacheKey = false;
        }
    }

    public class CacheKeyOptions : BaseOptions
    {
        public CacheKeyOptions()
        {
            ToCacheKey = true;
        }
    }

    public class BaseOptions
    {
        public RemoveValues RemoveValues { get; set; } = RemoveValues.Null;

        public bool SkipEncoding { get; set; }

        public KeyCase KeyCase { get; set; }

        public bool SortKeys { get; set; }

        public ValueCase ValueCase { get; set; }

        public bool EnumAsString { get; set; }

        public bool ReplaceSpaceWithPlus { get; set; }

        internal bool ToCacheKey { get; set; }

        //public KeyListStyle KeyListStyle { get; set; }

        //public ToQueryEvents Events { get; set; }
    }

    public class ToQueryEvents
    {
        public Action<string> FormatValue;
    }

    public enum KeyCase
    {
        None = 0,
        PascalCase = 1,
        CamelCase = 2,
        LowerCase = 3
    }

    public enum SortKeys
    {
        None = 0,
        Ascending = 1
    }

    public enum KeyListStyle
    {
        Default = 0,
        Array = 1
    }

    public enum ValueCase
    {
        None = 0,
        LowerCase = 1
    }

    public enum RemoveValues
    {
        None = 0,
        Null = 1,
        NullOrDefault = 2,
        NullOrEmpty = 4,
        NullDefaultOrEmpty = Null | NullOrDefault | NullOrEmpty
    }
}
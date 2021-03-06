﻿using System;
using System.Collections.Generic;
using System.Globalization;

namespace UnitTests.TestData
{
    public class MyObject
    {
        public int Id { get; set; } = 1;

        public string Name { get; set; } = "Example";

        public List<string> Tags { get; set; } = new List<string> { "a", "b" };

        public NestedObject NestedObject { get; set; } = new NestedObject();
    }

    public class CacheTest
    {
        public int? Age { get; set; } = 0;

        public string Name { get; set; } = string.Empty;

        public List<string> Tags { get; set; } = new List<string>();

        public List<int> Ids { get; set; } = new List<int>();
    }

    public class CacheTestInt
    {
        public int? Age { get; set; } = 0;
    }

    public class CacheTestDate
    {
        public DateTime? Date1 { get; set; }

        public DateTime Date2 { get; set; }

        public DateTimeOffset? Date3 { get; set; }

        public DateTimeOffset Date4 { get; set; }
    }

    public class CacheTestString
    {
        public string Name { get; set; } = string.Empty;
    }

    public class CacheTestNullableIntList
    {
        public List<int?> Ids { get; set; } = new List<int?>();
    }

    public class CacheTestIntList
    {
        public List<int> Ids { get; set; } = new List<int>();
    }

    public class CacheTestNestedIntList
    {
        public CacheTestIntList Obj { get; set; } = new CacheTestIntList();
    }

    public class CacheTestNestedStringList
    {
        public CacheTestStringList Obj { get; set; } = new CacheTestStringList();
    }

    public class CacheTestListNestedStringList
    {
        public List<CacheTestStringList> List { get; set; } = new List<CacheTestStringList>();
    }

    public class CacheTestListOfList
    {
        public List<List<string>> List { get; set; } = new List<List<string>>();
    }

    public class CacheTestStringList
    {
        public List<string> Tags { get; set; } = new List<string>();
    }

    public class InheritTest : MyObject
    {
        public int Page { get; set; } = 1;

        public int PageSize { get; set; } = 20;
    }

    public class NestedObject
    {
        public int Id { get; set; } = 2;
    }

    public class TestNestedObject
    {
        public NestedObject NestedObject { get; set; } = new NestedObject();
    }

    public class TestNestedNestedObject
    {
        public TestNestedObject NestedObject { get; set; } = new TestNestedObject();
    }

    public class ListObject
    {
        public List<int> List { get; set; } = new List<int> { 2, 4 };

        public List<int> EmptyIntList { get; set; } = new List<int>();

        public List<string> EmptyStringList { get; set; } = new List<string>();
    }

    public class CaseTestDataObject
    {
        public string Name { get; set; } = "Example";
    }

    public class TestEnumObject
    {
        public TestEnum TestEnum { get; set; } = TestEnum.Test;
    }

    public class SpecialTypeObject
    {
        public TimeSpan TimeSpan { get; set; } = new TimeSpan(0, 1, 1, 1, 2);

        public CultureInfo CultureInfo { get; set; } = new CultureInfo("en-US");

        public bool Removed { get; set; } = false;

        public double Double { get; set; } = 2.5;

        public Dictionary<string, string> Dictionary { get; set; } = new Dictionary<string, string> {
            { "a", "1" },
            { "b", "2" }
        };

        public Guid Guid { get; set; } = Guid.Parse("61fbde9f-b69b-4c37-ab6c-3212b02b8c0e");
    }

    public class TestDataObject
    {
        public int Id { get; set; } = 2;

        public int? UserAge { get; set; } = null;

        public decimal UserHeight { get; set; } = 80.5M;

        public float ShoeSize { get; set; } = 10.4F;

        public string FirstName { get; set; } = "Luigi";

        public string LastName { get; set; } = "";

        public decimal? LuckyNumber { get; set; } = null;

        public List<string> Tags { get; set; } = new List<string> { "C", "d" };

        public DateTimeOffset Birth { get; set; } = new DateTimeOffset(new DateTime(2018, 1, 1, 0, 0, 0, DateTimeKind.Utc), TimeSpan.Zero);

        public DateTimeOffset? Died { get; set; } = new DateTimeOffset(new DateTime(2030, 1, 1, 0, 0, 0, DateTimeKind.Utc), TimeSpan.Zero);

        public DateTimeOffset? Rebirth { get; set; } = null;

        public int DefaultInt { get; set; } = 0;

        public DateTime LastAnniversary { get; set; } = new DateTime(2018, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public DateTime? NextAnniversary { get; set; } = new DateTime(2018, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public string[] Categories { get; set; } = { "a", "B" };

        public TestDataProfile Profile { get; set; } = new TestDataProfile();
    }

    public class TestDataProfile
    {
        public TestDataProfile()
        {
            Email = "firstname.lastname@company.com";
        }

        public string Email { get; set; }

        public string Alias { get; set; } = "super man";

        //public Url HomePage { get; set; } = new Url("https://github.com/?Test=2");

        public Uri FavoritePage { get; set; } = new Uri("https://github.com/?Test=2", UriKind.Absolute);

        public List<DeepTestObject> Subs { get; set; } = new List<DeepTestObject> { new DeepTestObject { Id = 4 }, new DeepTestObject { Id = 5 } };

        public string[] Categories { get; set; } = { "a", "B" };

        public DeepTestObject Sub { get; set; } = new DeepTestObject();
    }

    public class DeepTestObject
    {
        public int Id { get; set; } = 6;

        public string Name { get; set; } = "Test";
    }

    public enum TestEnum
    {
        None = 0,
        Test = 1
    }

    public class TestDataFactory
    {
        public static TestDataObject CreateTestObject()
        {
            return new TestDataObject();
        }
    }
}
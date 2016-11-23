﻿using System;
using System.Collections.Generic;

namespace UnitTests.TestData
{
    public class CaseTestDataObject
    {
        public string Name { get; set; } = "Example";
    }

    public class TestEnumObject
    {
        public TestEnum TestEnum { get; set; } = TestEnum.Test;
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

        public DateTimeOffset Birth { get; set; } = new DateTimeOffset(new DateTime(2018,1,1,0,0,0, DateTimeKind.Utc), TimeSpan.Zero);

        public DateTimeOffset? Died { get; set; } = new DateTimeOffset(new DateTime(2030, 1, 1, 0, 0, 0, DateTimeKind.Utc), TimeSpan.Zero);

        public DateTimeOffset? Rebirth { get; set; } = null;

        public int DefaultInt { get; set; } = 0;

        public DateTime LastAnniversary { get; set; } = new DateTime(2018, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public DateTime? NextAnniversary { get; set; } = new DateTime(2018, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public string[] Categories { get; set; } = {"a", "B"};

        public TestDataProfile Profile { get; set; } = new TestDataProfile();
    }

    public class TestDataProfile
    {
        public string Email { get; set; }

        public string Alias { get; set; } = "super man";

        //public Url HomePage { get; set; } = new Url("https://github.com/?Test=2");

        public Uri FavoritePage { get; set; } = new Uri("https://github.com/?Test=2", UriKind.Absolute);

        public List<DeepTestObject> Subs { get; set; } = new List<DeepTestObject> { new DeepTestObject { Id = 4 }, new DeepTestObject { Id = 5 } };

        public string[] Categories { get; set; } = { "a", "B" };

        public DeepTestObject Sub { get; set; } = new DeepTestObject();

        public TestDataProfile()
        {
            Email = "firstname.lastname@company.com";
        }
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
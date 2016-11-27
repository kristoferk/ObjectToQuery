using ObjectToQuery;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnitTests.TestData;
using Xunit;

namespace UnitTests
{
    public class ObjectToQueryTests
    {
        [Fact]
        public void TestAppend()
        {
            string url = "https://test.se/".AppendObject(new CaseTestDataObject());
            string expectedQuery = "https://test.se/?Name=Example";
            Assert.Equal(expectedQuery, url);
        }

        [Fact]
        public void TestAppend2()
        {
            string url = "https://test.se/?test=2".AppendObject(new CaseTestDataObject());
            string expectedQuery = "https://test.se/?test=2&Name=Example";
            Assert.Equal(expectedQuery, url);
        }

        [Fact]
        public void TestAppend3()
        {
            const CaseTestDataObject t = null;
            string url = "https://test.se/".AppendObject(t);
            string expectedQuery = "https://test.se/";
            Assert.Equal(expectedQuery, url);
        }

        [Fact]
        public void TestConverter()
        {
            var testData = TestDataFactory.CreateTestObject();
            IObjectToQueryConverter converter = new ObjectToQueryConverter();
            var query = converter.ToQuery(testData);

            string expectedQuery =
                "Id=2" +
                //"&UserAge=" +
                "&UserHeight=80%2C5" +
                "&ShoeSize=10%2C4" +
                "&FirstName=Luigi" +
                "&LastName=" +
                //"&LuckyNumber=" +
                "&Tags=C&Tags=d" +
                "&Birth=2018-01-01T00%3A00%3A00.0000000%2B00%3A00" +
                "&Died=2030-01-01T00%3A00%3A00.0000000%2B00%3A00" +
                //"&Rebirth=" +
                "&DefaultInt=0" +
                "&LastAnniversary=2018-01-01T00%3A00%3A00.0000000Z" +
                "&NextAnniversary=2018-01-01T00%3A00%3A00.0000000Z" +
                "&Categories=a" +
                "&Categories=B" +
                "&Profile.Email=firstname.lastname%40company.com" +
                "&Profile.Alias=super%20man" +
                "&Profile.FavoritePage=https%3A%2F%2Fgithub.com%2F%3FTest%3D2" +
                "&Profile.Subs.Id=4" +
                "&Profile.Subs.Name=Test" +
                "&Profile.Subs.Id=5" +
                "&Profile.Subs.Name=Test" +
                "&Profile.Categories=a" +
                "&Profile.Categories=B" +
                "&Profile.Sub.Id=6" +
                "&Profile.Sub.Name=Test";

            Assert.Equal(expectedQuery, query);
        }

        [Fact]
        public void TestDefault()
        {
            var testData = TestDataFactory.CreateTestObject();

            var query = testData.ToQuery();

            string expectedQuery =
                "Id=2" +
                //"&UserAge=" +
                "&UserHeight=80%2C5" +
                "&ShoeSize=10%2C4" +
                "&FirstName=Luigi" +
                "&LastName=" +
                //"&LuckyNumber=" +
                "&Tags=C&Tags=d" +
                "&Birth=2018-01-01T00%3A00%3A00.0000000%2B00%3A00" +
                "&Died=2030-01-01T00%3A00%3A00.0000000%2B00%3A00" +
                //"&Rebirth=" +
                "&DefaultInt=0" +
                "&LastAnniversary=2018-01-01T00%3A00%3A00.0000000Z" +
                "&NextAnniversary=2018-01-01T00%3A00%3A00.0000000Z" +
                "&Categories=a" +
                "&Categories=B" +
                "&Profile.Email=firstname.lastname%40company.com" +
                "&Profile.Alias=super%20man" +
                "&Profile.FavoritePage=https%3A%2F%2Fgithub.com%2F%3FTest%3D2" +
                "&Profile.Subs.Id=4" +
                "&Profile.Subs.Name=Test" +
                "&Profile.Subs.Id=5" +
                "&Profile.Subs.Name=Test" +
                "&Profile.Categories=a" +
                "&Profile.Categories=B" +
                "&Profile.Sub.Id=6" +
                "&Profile.Sub.Name=Test";

            Assert.Equal(expectedQuery, query);
        }

        [Fact]
        public void TestEnumString()
        {
            var obj = new TestEnumObject();
            var query = obj.ToQuery(new ToQueryOptions {
                EnumAsString = true
            });
            string expectedQuery = "TestEnum=Test";
            Assert.Equal(expectedQuery, query);
        }

        [Fact]
        public void TestEnumValue()
        {
            var obj = new TestEnumObject();
            var query = obj.ToQuery();
            string expectedQuery = "TestEnum=1";
            Assert.Equal(expectedQuery, query);
        }

        [Fact]
        public void TestExample()
        {
            var myObject = new MyObject {
                Id = 1,
                Name = "Example",
                Tags = new List<string> { "a", "b" },
                NestedObject = new NestedObject {
                    Id = 2
                }
            };

            string queryString = myObject.ToQuery();

            //Result: Id=1&Name=Example&Tags=a&Tags=b&NestedObject.Id=2
            string expectedQuery = "Id=1&Name=Example&Tags=a&Tags=b&NestedObject.Id=2";
            Assert.Equal(expectedQuery, queryString);
        }

        [Fact]
        public void TestExample2()
        {
            var myObject = new MyObject {
                Id = 1,
                Name = "Example",
                Tags = new List<string> { "a", "b" },
                NestedObject = new NestedObject {
                    Id = 2
                }
            };

            string queryString = myObject.ToQuery(new ToQueryOptions {
                EnumAsString = true,
                KeyCase = KeyCase.CamelCase,
                RemoveValues = RemoveValues.NullDefaultOrEmpty,
                SortKeys = true,
                ValueCase = ValueCase.LowerCase,
                SkipEncoding = false,
                ReplaceSpaceWithPlus = false
            });

            //Result: id=1&name=example&nestedObject.id=2&tags=a&tags=b
            string expectedQuery = "id=1&name=example&nestedObject.id=2&tags=a&tags=b";
            Assert.Equal(expectedQuery, queryString);
        }

        [Fact]
        public void TestExample3()
        {
            var myObject = new MyObject { Id = 1, Name = "Example" };

            IObjectToQueryConverter objectToQueryConverter = new ObjectToQueryConverter();
            string queryString = objectToQueryConverter.ToQuery(myObject);

            //Result: Id=1&Name=Example&Tags=a&Tags=b&NestedObject.Id=2
            string expectedQuery = "Id=1&Name=Example&Tags=a&Tags=b&NestedObject.Id=2";
            Assert.Equal(expectedQuery, queryString);
        }

        [Fact]
        public void TestInheritTest()
        {
            var query = new InheritTest().ToQuery(new ToQueryOptions());
            string expectedQuery = "Page=1&PageSize=20&Id=1&Name=Example&Tags=a&Tags=b&NestedObject.Id=2";
            Assert.Equal(expectedQuery, query);
        }

        [Fact]
        public void TestKeyCamelCase()
        {
            var testData = TestDataFactory.CreateTestObject();

            var query = testData.ToQuery(new ToQueryOptions {
                KeyCase = KeyCase.CamelCase
            });

            string expectedQuery =
                "id=2" +
                //"&UserAge=" +
                "&userHeight=80%2C5" +
                "&shoeSize=10%2C4" +
                "&firstName=Luigi" +
                "&lastName=" +
                //"&LuckyNumber=" +
                "&tags=C&tags=d" +
                "&birth=2018-01-01T00%3A00%3A00.0000000%2B00%3A00" +
                "&died=2030-01-01T00%3A00%3A00.0000000%2B00%3A00" +
                //"&Rebirth=" +
                "&defaultInt=0" +
                "&lastAnniversary=2018-01-01T00%3A00%3A00.0000000Z" +
                "&nextAnniversary=2018-01-01T00%3A00%3A00.0000000Z" +
                "&categories=a" +
                "&categories=B" +
                "&profile.email=firstname.lastname%40company.com" +
                "&profile.alias=super%20man" +
                "&profile.favoritePage=https%3A%2F%2Fgithub.com%2F%3FTest%3D2" +
                "&profile.subs.id=4" +
                "&profile.subs.name=Test" +
                "&profile.subs.id=5" +
                "&profile.subs.name=Test" +
                "&profile.categories=a" +
                "&profile.categories=B" +
                "&profile.sub.id=6" +
                "&profile.sub.name=Test";

            Assert.Equal(expectedQuery, query);
        }

        [Fact]
        public void TestKeyLowerCase()
        {
            var testData = TestDataFactory.CreateTestObject();

            var query = testData.ToQuery(new ToQueryOptions {
                KeyCase = KeyCase.LowerCase
            });

            string expectedQuery =
                "id=2" +
                //"&UserAge=" +
                "&userheight=80%2C5" +
                "&shoesize=10%2C4" +
                "&firstname=Luigi" +
                "&lastname=" +
                //"&LuckyNumber=" +
                "&tags=C&tags=d" +
                "&birth=2018-01-01T00%3A00%3A00.0000000%2B00%3A00" +
                "&died=2030-01-01T00%3A00%3A00.0000000%2B00%3A00" +
                //"&Rebirth=" +
                "&defaultint=0" +
                "&lastanniversary=2018-01-01T00%3A00%3A00.0000000Z" +
                "&nextanniversary=2018-01-01T00%3A00%3A00.0000000Z" +
                "&categories=a" +
                "&categories=B" +
                "&profile.email=firstname.lastname%40company.com" +
                "&profile.alias=super%20man" +
                "&profile.favoritepage=https%3A%2F%2Fgithub.com%2F%3FTest%3D2" +
                "&profile.subs.id=4" +
                "&profile.subs.name=Test" +
                "&profile.subs.id=5" +
                "&profile.subs.name=Test" +
                "&profile.categories=a" +
                "&profile.categories=B" +
                "&profile.sub.id=6" +
                "&profile.sub.name=Test";

            Assert.Equal(expectedQuery, query);
        }

        [Fact]
        public void TestListObject()
        {
            var query = new ListObject().ToQuery();
            string expectedQuery = "List=2&List=4";
            Assert.Equal(expectedQuery, query);
        }

        [Fact]
        public void TestRemoveAll()
        {
            var testData = TestDataFactory.CreateTestObject();

            var query = testData.ToQuery(new ToQueryOptions {
                RemoveValues = RemoveValues.NullDefaultOrEmpty
            });

            string expectedQuery =
                "Id=2" +
                //"&UserAge=" +
                "&UserHeight=80%2C5" +
                "&ShoeSize=10%2C4" +
                "&FirstName=Luigi" +
                //"&LastName=" +
                //"&LuckyNumber=" +
                "&Tags=C&Tags=d" +
                "&Birth=2018-01-01T00%3A00%3A00.0000000%2B00%3A00" +
                "&Died=2030-01-01T00%3A00%3A00.0000000%2B00%3A00" +
                //"&Rebirth=" +
                //"&DefaultInt=0" +
                "&LastAnniversary=2018-01-01T00%3A00%3A00.0000000Z" +
                "&NextAnniversary=2018-01-01T00%3A00%3A00.0000000Z" +
                "&Categories=a" +
                "&Categories=B" +
                "&Profile.Email=firstname.lastname%40company.com" +
                "&Profile.Alias=super%20man" +
                //"&Profile.HomePage=https%3A%2F%2Fgithub.com%2F%3FTest=2" +
                "&Profile.FavoritePage=https%3A%2F%2Fgithub.com%2F%3FTest%3D2" +
                "&Profile.Subs.Id=4" +
                "&Profile.Subs.Name=Test" +
                "&Profile.Subs.Id=5" +
                "&Profile.Subs.Name=Test" +
                "&Profile.Categories=a" +
                "&Profile.Categories=B" +
                "&Profile.Sub.Id=6" +
                "&Profile.Sub.Name=Test";

            Assert.Equal(expectedQuery, query);
        }

        [Fact]
        public void TestRemoveDefault()
        {
            var testData = TestDataFactory.CreateTestObject();

            var query = testData.ToQuery(new ToQueryOptions {
                RemoveValues = RemoveValues.NullOrDefault
            });

            string expectedQuery =
                "Id=2" +
                //"&UserAge=" +
                "&UserHeight=80%2C5" +
                "&ShoeSize=10%2C4" +
                "&FirstName=Luigi" +
                "&LastName=" +
                //"&LuckyNumber=" +
                "&Tags=C&Tags=d" +
                "&Birth=2018-01-01T00%3A00%3A00.0000000%2B00%3A00" +
                "&Died=2030-01-01T00%3A00%3A00.0000000%2B00%3A00" +
                //"&Rebirth=" +
                //"&DefaultInt=0" +
                "&LastAnniversary=2018-01-01T00%3A00%3A00.0000000Z" +
                "&NextAnniversary=2018-01-01T00%3A00%3A00.0000000Z" +
                "&Categories=a" +
                "&Categories=B" +
                "&Profile.Email=firstname.lastname%40company.com" +
                "&Profile.Alias=super%20man" +
                //"&Profile.HomePage=https%3A%2F%2Fgithub.com%2F%3FTest=2" +
                "&Profile.FavoritePage=https%3A%2F%2Fgithub.com%2F%3FTest%3D2" +
                "&Profile.Subs.Id=4" +
                "&Profile.Subs.Name=Test" +
                "&Profile.Subs.Id=5" +
                "&Profile.Subs.Name=Test" +
                "&Profile.Categories=a" +
                "&Profile.Categories=B" +
                "&Profile.Sub.Id=6" +
                "&Profile.Sub.Name=Test";

            Assert.Equal(expectedQuery, query);
        }

        [Fact]
        public void TestRemoveEmptyValues()
        {
            var testData = TestDataFactory.CreateTestObject();

            var query = testData.ToQuery(new ToQueryOptions {
                RemoveValues = RemoveValues.NullOrEmpty
            });

            string expectedQuery =
                "Id=2" +
                //"&UserAge=" +
                "&UserHeight=80%2C5" +
                "&ShoeSize=10%2C4" +
                "&FirstName=Luigi" +
                //"&LastName=" +
                //"&LuckyNumber=" +
                "&Tags=C&Tags=d" +
                "&Birth=2018-01-01T00%3A00%3A00.0000000%2B00%3A00" +
                "&Died=2030-01-01T00%3A00%3A00.0000000%2B00%3A00" +
                //"&Rebirth=" +
                "&DefaultInt=0" +
                "&LastAnniversary=2018-01-01T00%3A00%3A00.0000000Z" +
                "&NextAnniversary=2018-01-01T00%3A00%3A00.0000000Z" +
                "&Categories=a" +
                "&Categories=B" +
                "&Profile.Email=firstname.lastname%40company.com" +
                "&Profile.Alias=super%20man" +
                //"&Profile.HomePage=https%3A%2F%2Fgithub.com%2F%3FTest=2" +
                "&Profile.FavoritePage=https%3A%2F%2Fgithub.com%2F%3FTest%3D2" +
                "&Profile.Subs.Id=4" +
                "&Profile.Subs.Name=Test" +
                "&Profile.Subs.Id=5" +
                "&Profile.Subs.Name=Test" +
                "&Profile.Categories=a" +
                "&Profile.Categories=B" +
                "&Profile.Sub.Id=6" +
                "&Profile.Sub.Name=Test";

            Assert.Equal(expectedQuery, query);
        }

        [Fact]
        public void TestRemoveNone()
        {
            var testData = TestDataFactory.CreateTestObject();

            var query = testData.ToQuery(new ToQueryOptions {
                RemoveValues = RemoveValues.None
            });

            string expectedQuery =
                "Id=2" +
                "&UserAge=" +
                "&UserHeight=80%2C5" +
                "&ShoeSize=10%2C4" +
                "&FirstName=Luigi" +
                "&LastName=" +
                "&LuckyNumber=" +
                "&Tags=C&Tags=d" +
                "&Birth=2018-01-01T00%3A00%3A00.0000000%2B00%3A00" +
                "&Died=2030-01-01T00%3A00%3A00.0000000%2B00%3A00" +
                "&Rebirth=" +
                "&DefaultInt=0" +
                "&LastAnniversary=2018-01-01T00%3A00%3A00.0000000Z" +
                "&NextAnniversary=2018-01-01T00%3A00%3A00.0000000Z" +
                "&Categories=a" +
                "&Categories=B" +
                "&Profile.Email=firstname.lastname%40company.com" +
                "&Profile.Alias=super%20man" +
                "&Profile.FavoritePage=https%3A%2F%2Fgithub.com%2F%3FTest%3D2" +
                "&Profile.Subs.Id=4" +
                "&Profile.Subs.Name=Test" +
                "&Profile.Subs.Id=5" +
                "&Profile.Subs.Name=Test" +
                "&Profile.Categories=a" +
                "&Profile.Categories=B" +
                "&Profile.Sub.Id=6" +
                "&Profile.Sub.Name=Test";

            Assert.Equal(expectedQuery, query);
        }

        [Fact]
        public void TestReplaceSpaceWithPlus()
        {
            var testData = TestDataFactory.CreateTestObject();

            var query = testData.ToQuery(new ToQueryOptions {
                ReplaceSpaceWithPlus = true
            });

            string expectedQuery =
                "Id=2" +
                //"&UserAge=" +
                "&UserHeight=80%2C5" +
                "&ShoeSize=10%2C4" +
                "&FirstName=Luigi" +
                "&LastName=" +
                //"&LuckyNumber=" +
                "&Tags=C&Tags=d" +
                "&Birth=2018-01-01T00%3A00%3A00.0000000%2B00%3A00" +
                "&Died=2030-01-01T00%3A00%3A00.0000000%2B00%3A00" +
                //"&Rebirth=" +
                "&DefaultInt=0" +
                "&LastAnniversary=2018-01-01T00%3A00%3A00.0000000Z" +
                "&NextAnniversary=2018-01-01T00%3A00%3A00.0000000Z" +
                "&Categories=a" +
                "&Categories=B" +
                "&Profile.Email=firstname.lastname%40company.com" +
                "&Profile.Alias=super+man" +
                "&Profile.FavoritePage=https%3A%2F%2Fgithub.com%2F%3FTest%3D2" +
                "&Profile.Subs.Id=4" +
                "&Profile.Subs.Name=Test" +
                "&Profile.Subs.Id=5" +
                "&Profile.Subs.Name=Test" +
                "&Profile.Categories=a" +
                "&Profile.Categories=B" +
                "&Profile.Sub.Id=6" +
                "&Profile.Sub.Name=Test";

            Assert.Equal(expectedQuery, query);
        }

        [Fact]
        public void TestSkipEncoding()
        {
            var testData = TestDataFactory.CreateTestObject();

            var query = testData.ToQuery(new ToQueryOptions {
                SkipEncoding = true
            });

            string expectedQuery =
                "Id=2" +
                //"&UserAge=" +
                "&UserHeight=80,5" +
                "&ShoeSize=10,4" +
                "&FirstName=Luigi" +
                "&LastName=" +
                //"&LuckyNumber=" +
                "&Tags=C&Tags=d" +
                "&Birth=2018-01-01T00:00:00.0000000+00:00" +
                "&Died=2030-01-01T00:00:00.0000000+00:00" +
                //"&Rebirth=" +
                "&DefaultInt=0" +
                "&LastAnniversary=2018-01-01T00:00:00.0000000Z" +
                "&NextAnniversary=2018-01-01T00:00:00.0000000Z" +
                "&Categories=a" +
                "&Categories=B" +
                "&Profile.Email=firstname.lastname@company.com" +
                "&Profile.Alias=super man" +
                "&Profile.FavoritePage=https://github.com/?Test=2" +
                "&Profile.Subs.Id=4" +
                "&Profile.Subs.Name=Test" +
                "&Profile.Subs.Id=5" +
                "&Profile.Subs.Name=Test" +
                "&Profile.Categories=a" +
                "&Profile.Categories=B" +
                "&Profile.Sub.Id=6" +
                "&Profile.Sub.Name=Test";

            Assert.Equal(expectedQuery, query);
        }

        [Fact]
        public void TestSortKeys()
        {
            var testData = TestDataFactory.CreateTestObject();

            var query = testData.ToQuery(new ToQueryOptions {
                SortKeys = true
            });

            string expectedQuery =
                "Birth=2018-01-01T00%3A00%3A00.0000000%2B00%3A00" +
                "&Categories=a" +
                "&Categories=B" +
                "&DefaultInt=0" +
                "&Died=2030-01-01T00%3A00%3A00.0000000%2B00%3A00" +
                "&FirstName=Luigi" +
                "&Id=2" +
                "&LastAnniversary=2018-01-01T00%3A00%3A00.0000000Z" +
                "&LastName=" +
                "&NextAnniversary=2018-01-01T00%3A00%3A00.0000000Z" +
                "&Profile.Alias=super%20man" +
                "&Profile.Categories=a" +
                "&Profile.Categories=B" +
                "&Profile.Email=firstname.lastname%40company.com" +
                "&Profile.FavoritePage=https%3A%2F%2Fgithub.com%2F%3FTest%3D2" +
                "&Profile.Sub.Id=6" +
                "&Profile.Sub.Name=Test" +
                "&Profile.Subs.Id=4" +
                "&Profile.Subs.Name=Test" +
                "&Profile.Subs.Id=5" +
                "&Profile.Subs.Name=Test" +
                "&ShoeSize=10%2C4" +
                "&Tags=C&Tags=d" +
                "&UserHeight=80%2C5";

            Assert.Equal(expectedQuery, query);
        }

        //[Fact]
        //public void TestListObject2()
        //{
        //    var query = new ListObject().ToQuery(new ToQueryOptions {
        //        KeyListStyle = KeyListStyle.Array
        //    });
        //    string expectedQuery = "List[]=2&List[]=4&EmptyIntList[]=";
        //    Assert.Equal(expectedQuery, query);
        //}

        [Fact]
        public void TestSpecialTypes()
        {
            var query = new SpecialTypeObject().ToQuery(new ToQueryOptions());
            string expectedQuery = $"TimeSpan={new TimeSpan(0, 1, 1, 1, 2).TotalMilliseconds}&CultureInfo=en-US&Removed=false&Double=2%2C5&Dictionary.Key=a&Dictionary.Value=1&Dictionary.Key=b&Dictionary.Value=2&Guid=61fbde9f-b69b-4c37-ab6c-3212b02b8c0e";
            Assert.Equal(expectedQuery, query);
        }

        [Fact]
        public void TestValueLowerCase()
        {
            var query = new CaseTestDataObject().ToQuery(new ToQueryOptions {
                ValueCase = ValueCase.LowerCase
            });
            string expectedQuery = "Name=example";
            Assert.Equal(expectedQuery, query);
        }

        [Fact]
        public async Task TestWarmUp()
        {
            await ObjectToQueryExtentions.WarmUpAsync(typeof(CaseTestDataObject));

            var query = new CaseTestDataObject().ToQuery(new ToQueryOptions {
                ValueCase = ValueCase.LowerCase
            });
            string expectedQuery = "Name=example";
            Assert.Equal(expectedQuery, query);
        }
    }
}
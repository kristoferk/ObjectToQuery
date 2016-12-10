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
        public void TestCache()
        {
            CacheTest notNull = new CacheTest {
                Age = 0,
                Name = string.Empty,
                Tags = new List<string>(),
                Ids = new List<int>()
            };

            string expectedQuery = "Age=0&Name=";
            Assert.Equal(expectedQuery, notNull.ToCacheKey());

            CacheTest nullable = new CacheTest {
                Age = null,
                Name = null,
                Tags = null,
                Ids = null
            };

            string expectedQuery2 = "Tags=Null&Ids=Null";
            Assert.Equal(expectedQuery2, nullable.ToCacheKey());

            CacheTest defaultTest = new CacheTest {
                Age = 0,
                Name = string.Empty,
                Tags = new List<string> { string.Empty },
                Ids = new List<int> { 0 }
            };

            string expectedQuery3 = "Age=0&Name=&Tags=&Ids=0";
            Assert.Equal(expectedQuery3, defaultTest.ToCacheKey());

            Assert.NotEqual(nullable.ToCacheKey(), defaultTest.ToCacheKey());
            Assert.NotEqual(notNull.ToCacheKey(), defaultTest.ToCacheKey());
            Assert.NotEqual(notNull.ToCacheKey(), nullable.ToCacheKey());
        }

        [Fact]
        public void TestCacheInt()
        {
            CacheTestInt notNull = new CacheTestInt { Age = 0 };
            Assert.Equal("Age=0", notNull.ToCacheKey());
            CacheTestInt objNull = new CacheTestInt { Age = null };
            Assert.Equal(string.Empty, objNull.ToCacheKey());
            Assert.NotEqual(notNull.ToCacheKey(), objNull.ToCacheKey());
        }

        [Fact]
        public void TestCacheIntList()
        {
            CacheTestIntList objNull = new CacheTestIntList { Ids = null };
            Assert.Equal("Ids=Null", objNull.ToCacheKey());
            CacheTestIntList notNull = new CacheTestIntList { Ids = new List<int> { 0 } };
            Assert.Equal("Ids=0", notNull.ToCacheKey());
            CacheTestIntList emptyList = new CacheTestIntList { Ids = new List<int>() };
            Assert.Equal(string.Empty, emptyList.ToCacheKey());

            Assert.NotEqual(notNull.ToCacheKey(), objNull.ToCacheKey());
            Assert.NotEqual(emptyList.ToCacheKey(), objNull.ToCacheKey());
            Assert.NotEqual(emptyList.ToCacheKey(), notNull.ToCacheKey());
        }

        [Fact]
        public void TestCacheNestedStringList()
        {
            CacheTestNestedStringList objNull = new CacheTestNestedStringList { Obj = new CacheTestStringList { Tags = null } };
            Assert.Equal("Obj.Tags=Null", objNull.ToCacheKey());
            CacheTestNestedStringList notNull = new CacheTestNestedStringList { Obj = new CacheTestStringList { Tags = new List<string> { string.Empty } } };
            Assert.Equal("Obj.Tags=", notNull.ToCacheKey());
            CacheTestNestedStringList emptyList = new CacheTestNestedStringList { Obj = new CacheTestStringList { Tags = new List<string>() } };
            Assert.Equal(string.Empty, emptyList.ToCacheKey());

            Assert.NotEqual(notNull.ToCacheKey(), objNull.ToCacheKey());
            Assert.NotEqual(emptyList.ToCacheKey(), objNull.ToCacheKey());
            Assert.NotEqual(emptyList.ToCacheKey(), notNull.ToCacheKey());
        }

        [Fact]
        public void TestCacheNullableIntList()
        {
            CacheTestNullableIntList objNull = new CacheTestNullableIntList { Ids = null };
            Assert.Equal("Ids=Null", objNull.ToCacheKey());
            CacheTestNullableIntList notNull = new CacheTestNullableIntList { Ids = new List<int?> { 0 } };
            Assert.Equal("Ids=0", notNull.ToCacheKey());
            CacheTestNullableIntList emptyList = new CacheTestNullableIntList { Ids = new List<int?>() };
            Assert.Equal(string.Empty, emptyList.ToCacheKey());
            CacheTestNullableIntList nullList = new CacheTestNullableIntList { Ids = new List<int?> { null } };
            Assert.Equal(string.Empty, nullList.ToCacheKey());

            Assert.NotEqual(notNull.ToCacheKey(), objNull.ToCacheKey());
            Assert.NotEqual(emptyList.ToCacheKey(), objNull.ToCacheKey());
            Assert.NotEqual(emptyList.ToCacheKey(), notNull.ToCacheKey());
            Assert.Equal(emptyList.ToCacheKey(), nullList.ToCacheKey());
        }

        [Fact]
        public void TestCacheString()
        {
            CacheTestString notNull = new CacheTestString { Name = string.Empty };
            Assert.Equal("Name=", notNull.ToCacheKey());
            CacheTestString objNull = new CacheTestString { Name = null };
            Assert.Equal(string.Empty, objNull.ToCacheKey());
            Assert.NotEqual(notNull.ToCacheKey(), objNull.ToCacheKey());
        }

        [Fact]
        public void TestCacheStringList()
        {
            CacheTestStringList objNull = new CacheTestStringList { Tags = null };
            Assert.Equal("Tags=Null", objNull.ToCacheKey());
            CacheTestStringList notNull = new CacheTestStringList { Tags = new List<string> { string.Empty } };
            Assert.Equal("Tags=", notNull.ToCacheKey());
            CacheTestStringList emptyList = new CacheTestStringList { Tags = new List<string>() };
            Assert.Equal(string.Empty, emptyList.ToCacheKey());

            Assert.NotEqual(notNull.ToCacheKey(), objNull.ToCacheKey());
            Assert.NotEqual(emptyList.ToCacheKey(), objNull.ToCacheKey());
            Assert.NotEqual(emptyList.ToCacheKey(), notNull.ToCacheKey());
        }

        [Fact]
        public void TestCacheTestListNestedStringList()
        {
            CacheTestListNestedStringList objNull = new CacheTestListNestedStringList { List = new List<CacheTestStringList> { new CacheTestStringList { Tags = null } } };
            Assert.Equal("List.Tags=Null", objNull.ToCacheKey());
            CacheTestListNestedStringList notNull = new CacheTestListNestedStringList { List = new List<CacheTestStringList> { new CacheTestStringList { Tags = new List<string> { string.Empty } } } };
            Assert.Equal("List.Tags=", notNull.ToCacheKey());
            CacheTestListNestedStringList emptyList = new CacheTestListNestedStringList { List = new List<CacheTestStringList> { new CacheTestStringList { Tags = new List<string>() } } };
            Assert.Equal(string.Empty, emptyList.ToCacheKey());
            CacheTestListNestedStringList listOfNullList = new CacheTestListNestedStringList { List = new List<CacheTestStringList> { new CacheTestStringList { Tags = new List<string> { null } } } };
            Assert.Equal(string.Empty, listOfNullList.ToCacheKey());

            Assert.NotEqual(notNull.ToCacheKey(), objNull.ToCacheKey());
            Assert.NotEqual(emptyList.ToCacheKey(), objNull.ToCacheKey());
            Assert.NotEqual(emptyList.ToCacheKey(), notNull.ToCacheKey());
            Assert.Equal(emptyList.ToCacheKey(), listOfNullList.ToCacheKey());
        }

        [Fact]
        public void TestCacheTestListOfList()
        {
            CacheTestListOfList objNull = new CacheTestListOfList { List = new List<List<string>> { null } };
            Assert.Equal("List=Null", objNull.ToCacheKey());
            CacheTestListOfList notNull = new CacheTestListOfList { List = new List<List<string>> { new List<string>() } };
            Assert.Equal(string.Empty, notNull.ToCacheKey());

            CacheTestListOfList notNull2 = new CacheTestListOfList { List = new List<List<string>> { new List<string> { string.Empty } } };
            Assert.Equal("List=", notNull2.ToCacheKey());

            CacheTestListOfList notNull3 = new CacheTestListOfList { List = new List<List<string>> { new List<string> { null } } };
            Assert.Equal(string.Empty, notNull3.ToCacheKey());

            CacheTestListOfList notNull4 = new CacheTestListOfList { List = new List<List<string>> { new List<string> { "A" } } };
            Assert.Equal("List=A", notNull4.ToCacheKey());
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
        public void TestDynamic()
        {
            IObjectToQueryConverter objectToQueryConverter = new ObjectToQueryConverter();

            string queryString = objectToQueryConverter.ToQuery(new { Id = 1, Name = "Example" });
            string expectedQuery = "Id=1&Name=Example";
            Assert.Equal(expectedQuery, queryString);

            queryString = objectToQueryConverter.ToQuery(new { Id = 1, Name = "Example" });
            Assert.Equal(expectedQuery, queryString);

            string queryString2 = objectToQueryConverter.ToQuery(new { Id2 = 1, Name2 = "Example" });
            string expectedQuery2 = "Id2=1&Name2=Example";
            Assert.Equal(expectedQuery2, queryString2);

            string queryString3 = objectToQueryConverter.ToQuery(new { Id = 3, Name = "Example3" });
            string expectedQuery3 = "Id=3&Name=Example3";
            Assert.Equal(expectedQuery3, queryString3);
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
        public void TestNestedNestedObject()
        {
            var query = new TestNestedNestedObject {
                NestedObject = new TestNestedObject {
                    NestedObject = null
                }
            }.ToQuery();

            string expectedQuery = string.Empty;
            Assert.Equal(expectedQuery, query);

            var query2 = new TestNestedNestedObject {
                NestedObject = new TestNestedObject {
                    NestedObject = new NestedObject {
                        Id = 0
                    }
                }
            }.ToQuery();

            string expectedQuery2 = "NestedObject.NestedObject.Id=0";
            Assert.Equal(expectedQuery2, query2);
        }

        [Fact]
        public void TestNestedObject()
        {
            var query = new TestNestedObject {
                NestedObject = null
            }.ToQuery();
            string expectedQuery = string.Empty;
            Assert.Equal(expectedQuery, query);

            var query2 = new TestNestedObject {
                NestedObject = new NestedObject {
                    Id = 0
                }
            }.ToQuery();
            string expectedQuery2 = "NestedObject.Id=0";
            Assert.Equal(expectedQuery2, query2);
        }

        [Fact]
        public void TestQueryInsteadOfCache()
        {
            CacheTest notNull = new CacheTest {
                Age = 0,
                Name = string.Empty,
                Tags = new List<string>(),
                Ids = new List<int>()
            };

            string expectedQuery = "Age=0&Name=";
            Assert.Equal(expectedQuery, notNull.ToQuery());

            CacheTest nullable = new CacheTest {
                Age = null,
                Name = null,
                Tags = null,
                Ids = null
            };

            string expectedQuery2 = string.Empty;
            Assert.Equal(expectedQuery2, nullable.ToQuery());

            CacheTest defaultTest = new CacheTest {
                Age = 0,
                Name = string.Empty,
                Tags = new List<string> { string.Empty },
                Ids = new List<int> { 0 }
            };

            string expectedQuery3 = "Age=0&Name=&Tags=&Ids=0";
            Assert.Equal(expectedQuery3, defaultTest.ToQuery());

            Assert.NotEqual(nullable.ToQuery(), defaultTest.ToQuery());
            Assert.NotEqual(notNull.ToQuery(), defaultTest.ToQuery());
            Assert.NotEqual(notNull.ToQuery(), nullable.ToQuery());
        }

        [Fact]
        public void TestQueryInsteadOfCacheInt()
        {
            CacheTestInt notNull = new CacheTestInt { Age = 0 };
            Assert.Equal("Age=0", notNull.ToQuery());
            CacheTestInt objNull = new CacheTestInt { Age = null };
            Assert.Equal(string.Empty, objNull.ToQuery());
            Assert.NotEqual(notNull.ToQuery(), objNull.ToQuery());
        }

        [Fact]
        public void TestQueryInsteadOfCacheIntList()
        {
            CacheTestIntList objNull = new CacheTestIntList { Ids = null };
            Assert.Equal(string.Empty, objNull.ToQuery());
            CacheTestIntList notNull = new CacheTestIntList { Ids = new List<int> { 0 } };
            Assert.Equal("Ids=0", notNull.ToQuery());
            CacheTestIntList emptyList = new CacheTestIntList { Ids = new List<int>() };
            Assert.Equal(string.Empty, emptyList.ToQuery());

            Assert.NotEqual(notNull.ToQuery(), objNull.ToQuery());
            Assert.Equal(emptyList.ToQuery(), objNull.ToQuery());
            Assert.NotEqual(emptyList.ToQuery(), notNull.ToQuery());
        }

        [Fact]
        public void TestQueryInsteadOfCacheString()
        {
            CacheTestString notNull = new CacheTestString { Name = string.Empty };
            Assert.Equal("Name=", notNull.ToQuery());
            CacheTestString objNull = new CacheTestString { Name = null };
            Assert.Equal(string.Empty, objNull.ToQuery());
            Assert.NotEqual(notNull.ToQuery(), objNull.ToQuery());
        }

        [Fact]
        public void TestQueryInsteadOfCacheStringList()
        {
            CacheTestStringList objNull = new CacheTestStringList { Tags = null };
            Assert.Equal(string.Empty, objNull.ToQuery());
            CacheTestStringList notNull = new CacheTestStringList { Tags = new List<string> { string.Empty } };
            Assert.Equal("Tags=", notNull.ToQuery());
            CacheTestStringList emptyList = new CacheTestStringList { Tags = new List<string>() };
            Assert.Equal(string.Empty, emptyList.ToQuery());

            Assert.NotEqual(notNull.ToQuery(), objNull.ToQuery());
            Assert.Equal(emptyList.ToQuery(), objNull.ToQuery());
            Assert.NotEqual(emptyList.ToQuery(), notNull.ToQuery());
        }

        [Fact]
        public void TestQueryTestListOfList()
        {
            CacheTestListOfList objNull = new CacheTestListOfList { List = new List<List<string>> { null } };
            Assert.Equal(string.Empty, objNull.ToQuery());
            CacheTestListOfList notNull = new CacheTestListOfList { List = new List<List<string>> { new List<string>() } };
            Assert.Equal(string.Empty, notNull.ToQuery());

            CacheTestListOfList notNull2 = new CacheTestListOfList { List = new List<List<string>> { new List<string> { string.Empty } } };
            Assert.Equal("List=", notNull2.ToQuery());

            CacheTestListOfList notNull3 = new CacheTestListOfList { List = new List<List<string>> { new List<string> { null } } };
            Assert.Equal(string.Empty, notNull3.ToQuery());

            CacheTestListOfList notNull4 = new CacheTestListOfList { List = new List<List<string>> { new List<string> { "A" } } };
            Assert.Equal("List=A", notNull4.ToQuery());
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
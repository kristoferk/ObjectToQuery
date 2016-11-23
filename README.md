# ObjectToQuery
Converts a C# object to a url query string. Supports nested objects and lists.

## Installation
To install, run the following command in the Package Manager Console:
````csharp

PM> Install-Package ObjectToQuery

````

## Usage
````csharp

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

````


## Support for options

````csharp

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

````


| Property             | Default           | Description                                                 |
| -------------------- |:-----------------:| ----------------------------------------------------------- |
| EnumAsString         | false             | Convert enum values to text instead of numbers              |
| KeyCase              | None              | Change case of keys in querystring                          |
| RemoveValues         | RemoveValues.Null | Do not include values that are null, empty or default value |
| SortKeys             | false             | Sort keys ascending in alphabetically                       |
| ValueCase            | ValueCase.None    | Change case of values in querystring                        |
| SkipEncoding         | false             | If querystring should be encoded or not                     |
| ReplaceSpaceWithPlus | false             | If %20 should be replaced by +                              |



## Support for dependency injection
````csharp

var myObject = new MyObject { Id = 1, Name = "Example" };

IObjectToQueryConverter objectToQueryConverter = new ObjectToQueryConverter();
string queryString = objectToQueryConverter.ToQuery(myObject);

````

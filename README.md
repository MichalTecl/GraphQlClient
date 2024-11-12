

# GraphQL Client Documentation

## Introduction

GraphQlClient is a powerful tool for consuming GraphQL APIs within .NET in a modern way. It leverages .NET’s type system, classic "POCO" data modelling, and expression trees to offer a combination of productivity, flexibility, and performance.

### Prerequisites


This documentation assumes a basic understanding of [GraphQL](https://graphql.org/) and familiarity with .NET [Expression Trees](https://learn.microsoft.com/en-us/dotnet/csharp/advanced-topics/expression-trees/expression-trees-explained)

To get started, install the client via NuGet: [GraphQlClient on NuGet](https://www.nuget.org/packages/MTecl.GraphQlClient/). You can also check out [examples](https://github.com/MichalTecl/GraphQlClient/blob/main/MTecl.GraphQlClient.IntegrationTests/TestsWithCountries/CountriesQueryingTests.cs) and explore the full [source code on GitHub](https://github.com/MichalTecl/GraphQlClient).

---

## Queries and Mutations

### Model

The fundamental building block of a GraphQL query in this library is the model, which consists of data classes 
representing the types in the GraphQL schema and interfaces defining the operations.



```csharp
public class Country
{
    public string Name { get; set; }
    public string Capital { get; set; }
}

public interface IQuery
{
    [Gql("countries")]
    List<Country> GetCountries();                       
}
```
> There is one nice public & free GraphQL service https://countries.trevorblades.com/graphql which we will use in our tour. Please take a look there and get a bit familiar with their data schema.

>**Naming Tip**: I recommend naming methods according to typical C# conventions (e.g., GetSomething, FindSomething) and specify their GraphQL name with the Gql attribute. For data models, however, it’s better to align with the naming conventions used by the GraphQL API itself, even if they’re not quite C#-ish. By default, all property names are converted to camelCase, and deserialization is case-insensitive, so PascalCase vs. camelCase is fine, but GraphQL APIs sometimes use various notations with underscores etc, which you have to reflect in your property names to avoid unpleasant surprises during de/serializaition.

---

### Building Expressions

With a model reflecting the API’s data schema, we can start building a query. For this, we’ll need an instance 
of the generic `GraphQlQueryBuilder`, with our query interface as the generic argument:

```csharp
var builder = new GraphQlQueryBuilder<IQuery>();
```

The builder provides various properties for customizing query compilation and behavior. In most cases, the default settings should be sufficient, but I'll explain each option in detail [later in this document](#query-compilation-customization).

To build a query, we use a C# expression:

```csharp
var allCountriesQuery = builder.Build(q => q.GetCountries());
```

The resulting object represents the query. 

>Note that building a query from expressions uses lot of reflection and 
string manipulation, so consider the built query as a valuable resource and avoid creating it repeatedly. 
In real-world projects, you would typically store compiled queries as static fields.

---

### Execution

Our query is now ready for execution, i.e., sending it to the server and processing the response.

```csharp
var customers = await allCountriesQuery.ExecuteAsync(new HttpClient());
```
Of course, this one-liner won’t work right away—you need the API's address, possibly authentication, etc. 
Let me introduce you to [`GqlRequestOptions`](#query-execution-customization):

```csharp
static async Task Main(string[] args)
{
	var builder = new GraphQlQueryBuilder<IQuery>();

	var allCountriesQuery = builder.Build(q => q.GetCountries());

	var options = new GqlRequestOptions
	{
		RequestUri = new Uri("https://countries.trevorblades.com/graphql")                
	};

	using var httpClient = new HttpClient();

	var countries = await allCountriesQuery.WithOptions(options).ExecuteAsync(httpClient);

	foreach (var c in countries)
		Console.WriteLine($"{c.Name}\t{c.Capital}");
}

public class Country
{
	public string Name { get; set; }
	public string Capital { get; set; }
}

public interface IQuery
{
	[Gql("countries")]
	List<Country> GetCountries();                       
}
```


Note that I've added "WithOptions" to the query execution line. So now we have working API client! That was pretty easy, right? ✨ 


---

### Fields Inclusion


So, we can already retrieve a list of countries and even learn the names of their capital cities! But, just like me, I’m sure you're super curious about what languages are spoken in each country. According to the schema, it looks like we need to tweak our model a bit:

```csharp
public class Language 
{
    public string Name { get; set; }
    public string Native { get; set; }
}

public class Country
{            
    public string Name { get; set; }
    public string Capital { get; set; }

    public List<Language> Languages { get; set; }
}
```
If you just tried to run the query again, you might be a bit disappointed - yes, all Country.Languages are null. And that's actually fine. It’s important to understand that by default, GraphQlClient only includes fields with primitive types in the query. For more complex nested objects, we need to explicitly request them. And for that, the magical `With` method comes to the rescue! 

Here’s a full example with execution:
```csharp
var allCountriesQuery = builder.Build(q => q.GetCountries().With(c => c.Languages));
```
So now we said that we want to include country.languages in query results. Let's see (or even run) the complete example:

```csharp
static async Task Main(string[] args)
{
    var builder = new GraphQlQueryBuilder<IQuery>();
    var allCountriesQuery = builder.Build(q => q.GetCountries().With(c => c.Languages));
    
    var options = new GqlRequestOptions
    {
        RequestUri = new Uri("https://countries.trevorblades.com/graphql")
    };
    
    using var httpClient = new HttpClient();
    var countries = await allCountriesQuery.WithOptions(options).ExecuteAsync(httpClient);
    
    foreach (var c in countries)
        Console.WriteLine($"{c.Name}: {string.Join(", ", c.Languages.Select(l => l.Name))}");
}

public class Language 
{
	public string Name { get; set; }
	public string Native { get; set; }
}

public class Country
{            
	public string Name { get; set; }
	public string Capital { get; set; }

	public List<Language> Languages { get; set; }
}

public interface IQuery
{
	[Gql("countries")]
	List<Country> GetCountries();                       
}
```

~~It's not really true we speak Slovak in Czech republic, but that's not so important right now:~~  It works!

---

Using the `With` method, you can include as many properties as you need, diving deeper and deeper into nested structures, like so:

```csharp
var allCountriesQuery = builder.Build(q => 
    q.GetCountries().With(
        c => c.Languages, 
        c => c.Cities.With(
            city => city.Landmarks, 
            city => city.Districts.With(
                district => district.Neighborhoods
            )
        )
    )
);
```
Just for fun - this expression is compiled into this nice GraphQL:

```graphql
query {
	  countries {
		name
		capital
		languages {
		  name
		  native
		}
		cities {
		  name
		  landmarks {
			name
			description
			yearEstablished
		  }
		  districts {
			name
			population
			neighborhoods {
			  name
			  isResidential
			  population
			}
		  }
		}
	  }
	}
```



---

### Field Inclusion by Attribute

Sometimes, a property is so essential to an object that it makes sense to always include it in the query. To save ourselves a few calls to `With`, we can mark such properties with the `[Gql(InclusionMode = FieldInclusionMode.Include)]` attribute. For example, without a `Location`, a `PointOfInterest` wouldn’t make much sense:

```csharp
public class Location
{
    public double Latitude { get; set; }
    public double Longitude { get; set; }
}

public class PointOfInterest
{
    public string Name { get; set; }

    [Gql(InclusionMode = FieldInclusionMode.Include)]
    public Location Location { get; set; }
}
```
On the other hand, sometimes a property might be a primitive type, but we don’t always want it in the query. This might be the case if it holds a large value we won’t often need. In these situations, the `[Gql(InclusionMode = FieldInclusionMode.Exclude)]` attribute will exclude it by default, unless we specifically include it using `With`:

```csharp
public class User
{
    public string Name { get; set; }

    [Gql(InclusionMode = FieldInclusionMode.Exclude)]
    public string Base64ProfilePicture { get; set; }
}
```
---

### Query Arguments
Of course we can specify which data we want by using arguments. The simplest (though usually not the best) approach is to pass a value directly as a method argument. Let’s modify our query interface to have a method with a parameter:
```csharp
public interface IQuery
{
    [Gql("country")]
    Country GetCountry(string code);
}
```
... and pass the value there:
```csharp
static async Task Main(string[] args)
{
    var builder = new GraphQlQueryBuilder<IQuery>();

    var czQuery = builder.Build(q => q.GetCountry("CZ"));

    var options = new GqlRequestOptions
    {
        RequestUri = new Uri("https://countries.trevorblades.com/graphql")
    };

    using var httpClient = new HttpClient();

    var czechRepublic = await czQuery.WithOptions(options).ExecuteAsync(httpClient);

    Console.WriteLine($"I ♥ {czechRepublic.Capital}");
}

public class Country
{
    public string Name { get; set; }
    public string Capital { get; set; }        
}

public interface IQuery
{
    [Gql("country")]
    Country GetCountry(string code);
}
```

---
### Variables
Remember, I mentioned that query compilation can be resource-intensive, so we want to recycle queries as much as possible. To avoid hardcoding argument values directly into the compiled query, it’s highly recommended to use variables:

```csharp
var countryByCode = builder
	.Build(q => q.GetCountry(QueryVariable.Pass<string>("$countryCode", "ID")));

var czechRepublic = await countryByCode
	.WithOptions(options)
	.WithVariable("$countryCode", "CZ")
	.ExecuteAsync(httpClient);
	
var australia = await countryByCode
	.WithOptions(options)
	.WithVariable("$countryCode", "AU")
	.ExecuteAsync(httpClient);

Console.WriteLine($"It's really far from {czechRepublic.Capital} to {australia.Capital}");
```
In this example, we use the same compiled query twice to get two different results by passing variables. Notice that the `Pass` method specifies both the variable name and its type, `"ID"`, as required by the API. Since we can’t inherit directly from `string` in C#, we need to specify the type this way. When you’ll use complex types as parameters, you can omit the type in `Pass` and rely on the correct naming of your model class. Alternatively, you can specify a name with the `GqlType` attribute, as shown here:
```csharp
[GqlType("CountryFilterInput")]
public class CountryFilter 
{
    public StringFilterInput code { get; set; }
    public StringFilterInput continent { get; set; }
    public StringFilterInput currency { get; set; }
    public StringFilterInput name { get; set; }
}
```

You can also provide variable values as a `Dictionary`:
```csharp
var results = await searchQuery
	.WithVariables(new Dictionary<string, object> { { "query", "abc" }
	                                              , { "resultsLimit", 10 }})
	.ExecuteAsync(httpClient);
```

Or as a (even anonymous) object:
```csharp
var results = await searchQuery
	.WithVariables(new { query = "abc", resultsLimit = 10 })
	.ExecuteAsync(httpClient);
```

---
### Arguments without Methods
Methods typically represent operations defined by the API, but sometimes it’s useful to parameterize something without specifying a method—of course where the GraphQL schema supports it. It might seem a bit weird in this case, but nothing’s stopping you from defining a query interface with no methods at all:
```csharp
public interface IQuery
{
    Country Country { get; }
}
```
Then, we can instruct the query builder to add an argument with the name `"code"` and assign it the `$countryCode` variable:
```csharp
var czQuery = builder
	.Build(q => q.Country
		.Argument("code",
		          QueryVariable.Pass<string>("$countryCode", "ID")));

var czechRepublic = await czQuery
	.WithOptions(options)
	.WithVariable("$countryCode", "CZ")
	.ExecuteAsync(httpClient);
	
var australia = await czQuery
	.WithOptions(options)
	.WithVariable("$countryCode", "AU")
	.ExecuteAsync(httpClient);

Console.WriteLine($"It's really far from {czechRepublic.Capital} to {australia.Capital}");
```

Of course, you can also add hardcoded values this way, and you can define as many arguments as you need:
```csharp
var mostRecentBookOfAuthor = builder.Build(q => q.Books
    .Argument("author", QueryVariable.Pass<int>("$authorId"))
    .Argument("sortDirection", "DESC")
    .Argument("limit", 1));
```
---
### Field Aliases
Sometimes, it’s useful to give a field a nickname - alias. This is easy to set up using method IsAliasFor:
```csharp
var countryQuery = builder
	.Build(q => q.GetCountry(QueryVariable.Pass<string>("$countryCode", "ID"))
               .With(c => c.NameOfCapitalCity.IsAliasFor("capital")));

var czechRepublic = await countryQuery
	.WithOptions(options)
	.WithVariable("$countryCode", "CZ")
	.ExecuteAsync(httpClient);
	
var australia = await countryQuery
	.WithOptions(options)
	.WithVariable("$countryCode", "AU")
	.ExecuteAsync(httpClient);

Console.WriteLine($"It's really far from {czechRepublic.NameOfCapitalCity} to {australia.NameOfCapitalCity}");
```
```csharp
public class Country
{
    public string NameOfCapitalCity { get; set; }
}
```
---
### Parameterized Aliases
By combining aliases and arguments, we can do some really interesting things:
```csharp
static async Task Main(string[] args)
{
    var builder = new GraphQlQueryBuilder<IQuery>();
    var options = new GqlRequestOptions
    {
        RequestUri = new Uri("https://countries.trevorblades.com/graphql")
    };

    using var httpClient = new HttpClient();

    var countriesQuery = builder.Build(q => q.GetCountries()
        .With(c => c.EnglishName.IsAliasFor("name").Argument("lang", "en"),
              c => c.GermanName.IsAliasFor("name").Argument("lang", "de")));

    var countries = await countriesQuery.WithOptions(options).ExecuteAsync(httpClient);

    foreach (var c in countries)
        Console.WriteLine($"{c.EnglishName}\t{c.GermanName}");
}

public class Country
{
    public string EnglishName { get; set; }
    public string GermanName { get; set; }        
}

public interface IQuery
{
    [Gql("countries")]
    List<Country> GetCountries();
}
```
There’s even a shorthand version if you want to save keystrokes:
```csharp
var countriesQuery = builder.Build(q => q.GetCountries()
    .With(c => c.EnglishName.IsAliasFor("name", "lang", "en"),
          c => c.GermanName.IsAliasFor("name", "lang", "de")));

```

---
### Aliases by Attribute
There’s another way to set up a field alias, using the `IsAliasFor` property in the `Gql` attribute:
```csharp
public class Country
{
    [Gql(IsAliasFor = "capital")]
    public string NameOfCapitalCity { get; set; }
}

```
In this case, you won’t need to specify the alias in the query:
```csharp
var countryQuery = builder
	.Build(q => q.GetCountry(QueryVariable.Pass<string>("$countryCode", "ID")));	
```

---

### Inline Fragments
Sometimes, a field in the query results can represent multiple types ([see union types in GraphQL](https://graphql.org/learn/schema/#union-types)). To handle such cases, we need to deserialize into an object that combines properties of each of the types in the union. In the query, however, we need to use inline fragments to specify which fields we expect for each type. 

We define membership in each type using C# interfaces, marked with the `GqlTypeFragment` attribute. Here’s how we’d structure the model for the [example linked above](https://graphql.org/learn/schema/#union-types):

```csharp
[GqlTypeFragment("Human")]
public interface IHuman
{
    string Name { get; set; }
    double Height { get; set; }
}

[GqlTypeFragment("Droid")]
public interface IDroid
{
    string Name { get; set; }
    string PrimaryFunction { get; set; }
}

[GqlTypeFragment("Starship")]
public interface IStarship
{
    string Name { get; set; }
    double Length { get; set; }
}

public class SearchResult : IHuman, IDroid, IStarship
{
    public string __typename { get; set; } // see https://spec.graphql.org/draft/#sec-Type-Name-Introspection 
    public string Name { get; set; }
    public double Height { get; set; }
    public string PrimaryFunction { get; set; }
    public double Length { get; set; }
}

public interface IQuery
{            
    List<SearchResult> Search(string text);
}
```
If you check how the builder constructs the query (yes, `ToString()` will always output the query text—just hover over it in VS), you’ll see that we get the exact query as shown in the linked example:
![Inline Fragment Example](https://github.com/MichalTecl/GraphQlClient/blob/main/Doc/inlinefragment.png?raw=true)

---

### Mutations
Yep, I haven't talked about mutations yet. But there’s really not much to cover—just use the `GraphQlQueryBuilder.BuildMutation` method, and everything works just like with queries.

---
### Named Queries
Sometimes, it’s a good idea to give individual queries and mutations a name. This can be especially useful for tracking in logs. The `BuildQuery` and `BuildMutation` methods have overloads that accept a name as the first argument.

---
## Advanced Techniques

### Query Compilation Customization
The `GraphQlQueryBuilder` class has several properties that allow you to customize query compilation and, to some extent, execution:


####  DateTimeConversionMode

By default, DateTime is serialized and deserialized according to [ISO 8601](https://learn.microsoft.com/en-us/dotnet/standard/base-types/standard-date-and-time-format-strings#Roundtrip). It's very easy to change the desired DateTime format for serialization:

```csharp
private static readonly GraphQlQueryBuilder<ISomeApi> _builder 
	= new GraphQlQueryBuilder<ISomeApi>()
{
    DateTimeConversionMode = DateTimeConverter.StringConversionMode("yyyy-MM-dd HH:mm:ss")
};
```

If this isn’t enough and you need for example to represent DateTime as a numeric value, you have to implement the `DateTimeConverter.IDateTimeConversionMode` interface in your own class to handle both serialization and deserialization:

```csharp
internal class UnixDateTimeMode : DateTimeConverter.IDateTimeConversionMode
{
    public DateTime Read(ref Utf8JsonReader reader, JsonSerializerOptions options)
        => DateTimeOffset.FromUnixTimeSeconds(reader.GetInt64()).UtcDateTime;

    public void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        => writer.WriteNumberValue(new DateTimeOffset(value).ToUnixTimeSeconds());
}

private static readonly GraphQlQueryBuilder<ISomeApi> _builder 
	= new GraphQlQueryBuilder<ISomeApi>()
	{
	    DateTimeConversionMode = new UnixDateTimeMode()
	};
```

#### JsonSerializerOptions

This property lets you modify JSON serialization and deserialization behavior. Changes should be approached with caution, as JSON de/serialization is heavily relied on throughout the system, and many parts expect certain behaviors. Default settings can be reviewed in the [`GraphQlQueryBuilder` constructor](https://github.com/MichalTecl/GraphQlClient/blob/main/MTecl.GraphQlClient/GraphQlQueryBuilder.cs#L20).
 
#### CustomizeQueryGraph

In very special cases, it might be necessary to adjust the compiled query graph to influence rendering. For example, you could insert a custom node type representing a comment within the query body:

![Customize Query Graph Example](https://github.com/MichalTecl/GraphQlClient/raw/main/Doc/customizegraph.png?raw=true)

#### CustomizeQueryText
This property allows you to make modifications to the rendered query string.

---
### Query Execution Customization

In addition to the `GraphQlBuilder` parameters intended for compilation customization, you may (and usually will) also need to influence query execution behavior. This is where the `GqlRequestOptions` class comes into play. Insert it into query processing via the `.WithOptions` method:

```csharp
var customers = await allCustomersQuery.WithOptions(options).ExecuteAsync(httpClient);
```

The key properties in `GqlRequestOptions` are:

- **RequestUri**: Specifies the GraphQL API endpoint address. (Note: This information can also be stored in `HttpClient.BaseAddress`. In that case, you might be able to avoid using `options` entirely.)
- **CustomRequestHeaders**: Often required to add headers with access tokens or other information that the server expects.
- **Encoding**: Used in case the default UTF-8 encoding does not meet your needs.
- **RawRequestPeek and RawResponsePeek**: These allow you to “eavesdrop” on the request and response content, typically for debugging or logging purposes. For example:

    ```csharp
    var options = new GqlRequestOptions
    {
        RequestUri = new Uri("https://countries.trevorblades.com/graphql"),
        RawRequestPeek = Console.WriteLine,
        RawResponsePeek = Console.WriteLine
    };
    ```

- ***RequestMessageBuilder, ResponseDeserializer, and CreateResponseException***: These remaining properties can be used for fundamental changes to the client’s communication layer implementation, out of scope of this doc.
---
## Conclusion
I originally developed this library to meet the needs of my own projects, but I’m thrilled to share it with anyone seeking a modern, robust, and user-friendly tool for consuming GraphQL APIs in .NET. Whether you’re just testing it out or building something critical, I’d love to hear your thoughts and feedback. Let's make this library even better together – reach out, ask questions, or just share your experiences.

If you’re interested in diving deeper, discussing ideas, or need guidance, don’t hesitate to contact me at mtecl.prg@gmail.com. Looking forward to connecting and collaborating with you!


using FluentAssertions;
using MTecl.GraphQlClient.Execution;
using MTecl.GraphQlClient.ObjectMapping.GraphModel.Variables;

namespace MTecl.GraphQlClient.IntegrationTests.TestsWithCountries
{
    public class CountriesQueryingTests
    {
        private static readonly GraphQlQueryBuilder<IQuery> _builder = new GraphQlQueryBuilder<IQuery>();
        private static readonly GqlRequestOptions _options; 

        static CountriesQueryingTests()
        {
            _options = new GqlRequestOptions
            {
                RequestUri = new Uri("https://countries.trevorblades.com/"),
                RawRequestPeek = r =>
                {
                    Console.WriteLine(r);
                },
                RawResponsePeek = r =>
                {
                    Console.WriteLine(r);
                }
            };

            //_options.CustomRequestHeaders["Accept"] = "application/json, multipart/mixed";
            //_options.CustomRequestHeaders["Accept-Encoding"] = "utf-8";            
        }

        [Fact]
        public async Task SimpleQueryOneLevelWithoutVariables()
        {
            var query = _builder.Build("getEuQuery", q => q.GetContinent("EU"));

            var eu = await Execute(query);

            eu.Should().NotBeNull();
            eu.code.Should().Be("EU");
        }
                
        [Fact]
        public async Task QueryWithVariable()
        {
            var query = _builder.Build(q => q.GetContinent(QueryVariable.Pass<string>("$code", "ID"))).WithVariable("code", "NA");

            var northAmerica = await Execute(query);

            northAmerica.Should().NotBeNull();
            northAmerica.code.Should().Be("NA");
        }

        [Fact]
        public async Task QueryWithVariablesAsDictionary()
        {
            var query = _builder.Build(q => q.GetContinent(QueryVariable.Pass<string>("$code", "ID")))
                .WithVariables(new Dictionary<string, object> { { "code", "NA" } });

            var northAmerica = await Execute(query);

            northAmerica.Should().NotBeNull();
            northAmerica.code.Should().Be("NA");
        }

        [Fact]
        public async Task QueryWithVariablesObject()
        {
            var query = _builder.Build(q => q.GetContinent(QueryVariable.Pass<string>("$code", "ID")))
                .WithVariables(new { code = "NA" });

            var northAmerica = await Execute(query);

            northAmerica.Should().NotBeNull();
            northAmerica.code.Should().Be("NA");
        }

        [Fact]
        public async Task QueryWithHardcodedInputType()
        {
            var query = _builder.Build(q => q.GetCountries(new CountryFilter { code = new StringFilterInput { @in = new List<string>() { "CZ", "JP" } } }));

            var czAndJp = await Execute(query);

            czAndJp.Should().NotBeNull();
            czAndJp.Should().HaveCount(2);
        }

        [Fact]
        public async Task QueryWithPassedComplexVariable_TypeNameFromAttribute()
        {
            var query = _builder.Build(q => q.GetCountries(QueryVariable.Pass<CountryFilter>("filter")))
                .WithVariable("filter", new CountryFilter { code = new StringFilterInput { @in = ["CZ", "UA"] } });

            var czAndUa = await Execute(query);

            czAndUa.Should().NotBeNull();
            czAndUa.Should().HaveCount(2);
        }

        [Fact]
        public async Task QueryWithPassedComplexVariable_TypeNameFromPassArgument()
        {
            var query = _builder.Build(q => q.GetLanguages(QueryVariable.Pass<LanguageFilter>("filter", "LanguageFilterInput")))
                .WithVariable("filter", new LanguageFilter { code = new StringFilterInput { @in = ["en", "de"] } });

            var enAndDe = await Execute(query);

            enAndDe.Should().NotBeNull();
            enAndDe.Should().HaveCount(2);
        }

        [Fact]
        public async Task QueryWithPassedComplexVariable_TypeNameFromTypeName()
        {
            var query = _builder.Build(q => q.GetContinents(QueryVariable.Pass<ContinentFilterInput>("filter")))
            .WithVariable("filter", new ContinentFilterInput { code = new StringFilterInput { @in = ["AF", "AS"] } });

            var afAndAs = await Execute(query);

            afAndAs.Should().NotBeNull();
            afAndAs.Should().HaveCount(2);                
        }


        private static async Task<T> Execute<T>(IQuery<T> query)
        {
            using (var client = new HttpClient())
            {
                return await query.WithOptions(_options).ExecuteAsync(client);
            }
        }
    }
}

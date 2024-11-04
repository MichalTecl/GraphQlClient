using FluentAssertions;
using MTecl.GraphQlClient.Execution;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTecl.GraphQlClient.IntegrationTests.TestsWithCountries
{
    public class CountriesQueryingTests
    {
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

            _options.CustomRequestHeaders["Accept"] = "application/json, multipart/mixed";
            _options.CustomRequestHeaders["Accept-Encoding"] = "utf-8";            
        }

        [Fact]
        public async Task SimpleQueryOneLevelWithoutVariables()
        {
            var eu = await Execute(Queries.EuropeQuery);

            eu.Should().NotBeNull();
            eu.code.Should().Be("EU");
        }
                
        [Fact]
        public async Task QueryWithVariable()
        {
            var northAmerica = await Execute(Queries.ContinentByCodeQuery.WithVariable("code", "NA"));

            northAmerica.Should().NotBeNull();
            northAmerica.code.Should().Be("NA");
        }

        [Fact]
        public async Task QueryWithHardcodedInputType()
        {
            var czAndJp = await Execute(Queries.CzechAndJapanQuery);

            czAndJp.Should().NotBeNull();
            czAndJp.Should().HaveCount(2);
        }
        
        private static async Task<T> Execute<T>(IExecutionData<T> query)
        {
            using (var client = new HttpClient())
            {
                return await query.WithOptions(_options).ExecuteAsync(client);
            }
        }
    }
}

using MTecl.GraphQlClient;
using MTecl.GraphQlClient.Execution;
using MTecl.GraphQlClient.ObjectMapping.GraphModel;
using MTecl.GraphQlClient.ObjectMapping.GraphModel.Variables;
using System.Text;

namespace Playground
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var builder = new GraphQlQueryBuilder<IQuery>();
            var options = new GqlRequestOptions
            {
                RequestUri = new Uri("https://countries.trevorblades.com/graphql")
            };

            using var httpClient = new HttpClient();


            

            var searchQuery = builder.Build(q => q.Search("an"));            
        }


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



    }
}

/*
 [Gql("customer")]
            Customer GetCustomer(int id);
 */
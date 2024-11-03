using MTecl.GraphQlClient.ObjectMapping.GraphModel.Variables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTecl.GraphQlClient.IntegrationTests.TestsWithCountries
{
    internal class Queries
    {
        public static readonly GqlQuery<Continent> EuropeQuery = GqlQuery<Continent>.Build<IQuery>(q => q.GetContinent("EU"));

        public static readonly GqlQuery<Continent> ContinentByCodeQuery = GqlQuery<Continent>.Build<IQuery>(q => q.GetContinent(QueryVariable.Pass<string>("$code", "ID")));
    }
}

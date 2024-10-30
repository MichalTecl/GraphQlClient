using FluentAssertions.Equivalency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTecl.GraphQlClient.IntegrationTests.TestsWithCountries
{
    public class Continent
    {
        public string code { get; set; } // ID type
        public List<Country> countries { get; set; }
        public string name { get; set; }
    }

    public class Country
    {
        public string awsRegion { get; set; }
        public string capital { get; set; }
        public string code { get; set; } // ID type
        public Continent continent { get; set; }
        public List<string> currencies { get; set; }
        public string currency { get; set; }
        public string emoji { get; set; }
        public string emojiU { get; set; }
        public List<Language> languages { get; set; }
        public string name { get; set; }
        public string native { get; set; }
        public string phone { get; set; }
        public List<string> phones { get; set; }
        public List<State> states { get; set; }
        public List<Subdivision> subdivisions { get; set; }
    }

    public class Language
    {
        public string code { get; set; } // ID type
        public string name { get; set; }
        public string native { get; set; }
        public bool rtl { get; set; }
    }

    public class State
    {
        public string code { get; set; }
        public Country country { get; set; }
        public string name { get; set; }
    }

    public class Subdivision
    {
        public string code { get; set; } // ID type
        public string emoji { get; set; }
        public string name { get; set; }
    }

    public class StringFilterInput
    {

    }

    public interface Query
    {
        [Gql("continent")]
        Continent GetContinent(string code);

        [Gql("continents")]
        List<Continent> Continents(StringFilterInput filter);
    }

    public class ContinentResponse { public Continent Continent { get; set; } }
    public class ContinentsResponse { public List<Continent> Continents { get; set; } }

}

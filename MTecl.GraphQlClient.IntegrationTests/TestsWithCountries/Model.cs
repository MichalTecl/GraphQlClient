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
        public string eq { get; set; }

        [Gql("in")]
        public List<string> @in { get; set; }

        public List<string> nin { get; set; }

        public string ne { get; set; }

        public string regex { get; set; }
    }

    [GqlType("CountryFilterInput")]
    public class CountryFilter 
    {
        public StringFilterInput code { get; set; }

        public StringFilterInput continent { get; set; }

        public StringFilterInput currency { get; set; }

        public StringFilterInput name { get; set; }
    }

    public class ContinentFilterInput
    {
        public StringFilterInput code { get; set; }
    }

    public class LanguageFilter
    {
        public StringFilterInput code { get; set; }
    }

    public interface IQuery
    {
        [Gql("continent")]
        Continent GetContinent(string code);

        [Gql("continents")]
        List<Continent> GetContinents(ContinentFilterInput filter);

        [Gql("countries")]
        List<Country> GetCountries(CountryFilter filter);

        [Gql("languages")]
        List<Language> GetLanguages(LanguageFilter filter);
    }

    public class ContinentResponse { public Continent Continent { get; set; } }
    public class ContinentsResponse { public List<Continent> Continents { get; set; } }

}

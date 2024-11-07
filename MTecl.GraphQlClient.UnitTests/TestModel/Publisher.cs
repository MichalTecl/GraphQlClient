namespace MTecl.GraphQlClient.UnitTests.TestModel
{
    public class Author
    {
        public string Nationality { get; set; }
        public int YearOfBirth { get; set; }

        public Author(string nationality, int yearOfBirth)
        {
            Nationality = nationality;
            YearOfBirth = yearOfBirth;
        }
    }

    public class Book
    {
        [Gql("title")]
        public string Title { get; set; }

        [Gql("authors")]
        public List<Author> Authors { get; set; }

        [Gql("language")]
        public string Language { get; set; }

        [Gql("genre")]
        public string Genre { get; set; }

        [Gql("dimensions")]
        public Dimensions Dimensions { get; set; }
    }

    public class Publisher
    {
        [Gql("name")]
        public string Name { get; set; }

        [Gql("publishedBooks")]
        public List<Book> PublishedBooks { get; set; }

        [Gql("publishedBooks")]
        public List<Book> GetPublishedBooks(string name) { return PublishedBooks; }
    }

    public class Dimensions
    {
        [Gql("weight")]
        public decimal Weight { get; set; }

        [Gql("height")]
        public decimal Height { get; set; }

        [Gql("width")]
        public int Width { get; set; }

        [Gql("numberOfPages")]
        public int NumberOfPages { get; set;}

        [Gql("coverType")]
        public CoverType CoverType { get; set; }
    }

    public class CoverType
    {
        [Gql("name")]
        public string Name { get; set; }

        [Gql("paperThickness")]
        public int PaperThickness { get; set; }
    }

    public interface IQuery
    {
        [Gql("publishers")]
        List<Publisher> GetPublishers(string nameFilter, int intArgument, string stringArgument);
    }

}

using BwApiClient;

namespace ConsoleApp1
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var token = File.ReadAllText(@"c:\DEV\bwtoken.txt").Trim();

            var bw = new BwClient("https://snap0905-ana.flox.cz/api/graphql", token, () => new HttpClient());

            //bw.Options.RawRequestPeek = (r) => 
            //{ 
            //    Console.WriteLine(r); 
            //};

            //bw.Options.RawResponsePeek = (r) =>
            //{
            //    Console.WriteLine(r);
            //};

            //var allStatuses = await bw.GetDefinedOrderStatuses();

            var dtFrom = DateTime.Now.AddDays(-365);

            var orderlist = await bw.GetOrders(dtFrom);
        }
    }
}

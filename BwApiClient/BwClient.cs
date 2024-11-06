using BwApiClient.Model.Data;
using BwApiClient.Model.Enums;
using BwApiClient.Model.Inputs;
using MTecl.GraphQlClient;
using MTecl.GraphQlClient.Execution;
using MTecl.GraphQlClient.ObjectMapping;
using MTecl.GraphQlClient.ObjectMapping.GraphModel.Variables;
using MTecl.GraphQlClient.ObjectMapping.Rendering.JsonConvertors;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BwApiClient
{
    public class BwClient
    {
        private readonly Func<HttpClient> _httpClientFactory;
        private readonly GqlRequestOptions _options;

        private static readonly GraphQlQueryBuilder<IQueries> _builder;

        static BwClient()
        {
            _builder = new GraphQlQueryBuilder<IQueries>();
            _builder.DateTimeConverter.Mode = DateTimeConverter.StringConversionMode("yyyy-MM-dd HH:mm:ss");
        }
        

        private static readonly IQuery<List<OrderStatus>> _orderStatusesQuery = _builder.Build(q => q.ListOrderStatuses(null, QueryVariable.Pass<bool>("$onlyactive")));

        private static readonly IQuery<PaginatedList<Order>> _ordersChangedAfterQuery = _builder.Build(
            q => q.GetOrderList(
                /*lang_code:*/ null,
                /*status:*/ null,
                /*newer_from:*/ null,
                /*changed_from:*/ QueryVariable.Pass<DateTime>("$changedAfter", "DateTime"),
                /*@params:*/ QueryVariable.Pass<OrderParams>("$params"),
                /*filter:*/ null).With(d => d.data.With(o => o.customer
                                                      //, o => o.creator
                                                      , o => o.vat_summary
                                                      , o => o.invoice_address
                                                      , o => o.delivery_address
                                                      //, o => o.shipments
                                                      , o => o.price_elements
                                                      , o => o.salesrep                                                      
                                                      , o => o.items.With(i => i.product))
                                ));

        

        /// <summary>
        /// https://www.byznysweb.cz/a/1267/volani-api
        /// </summary>
        /// <param name="url">API naslouchá na adrese https://vase-stranka.flox.cz/api/graphql.</param>
        /// <param name="token">https://www.byznysweb.cz/a/1274/token-api</param>
        /// <param name="httpClientFactory"></param>
        public BwClient(string url, string token, Func<HttpClient> httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;

            _options = new GqlRequestOptions()
            {
                RequestUri = new Uri(url)
            };

            _options.CustomRequestHeaders["BW-API-Key"] = $"Token {token}";
        }

        public GqlRequestOptions Options => _options;

        /// <summary>
        /// Loads all existing order statuses
        /// </summary>
        /// <returns></returns>
        public async Task<List<OrderStatus>> GetDefinedOrderStatuses(bool onlyActive = true)
        {

            return await Execute(_orderStatusesQuery.WithVariable("$onlyactive", onlyActive));
        }

        public async Task<PaginatedList<Order>> GetOrders(DateTime changedAfter, int? listOffset = 0, OrderSorting orderSorting = OrderSorting.last_change, Direction sortDirection = Direction.DESC) 
        {
            var filter = new OrderParams 
            {
                sort = sortDirection,
                order_by = orderSorting,
            };

            return await Execute(_ordersChangedAfterQuery
                                    .WithVariable("$changedAfter", changedAfter)
                                    .WithVariable("$params", filter));
        }

        private async Task<T> Execute<T>(IQuery<T> query)
        {
            using (var client = _httpClientFactory()) 
            {
                return await query.WithOptions(_options).ExecuteAsync<T>(client);
            }
        }
    }
}

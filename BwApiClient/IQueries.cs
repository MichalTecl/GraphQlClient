using BwApiClient.Model.Data;
using BwApiClient.Model.Enums;
using BwApiClient.Model.Inputs;
using MTecl.GraphQlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace BwApiClient
{
    public interface IQueries
    {
        /// <summary>
        /// Simple greeting method to test request validity, syntax or response processing.
        /// </summary>
        /// <param name="name">Whom to greet.</param>
        [Gql("hello")]
        string Hello(string name);

        /// <summary>
        /// Lists orders. Please note the returned type is a pageable list.
        /// </summary>
        /// <param name="lang_code">Language.</param>
        /// <param name="status">Order status ID.</param>
        /// <param name="newer_from">New orders received from.</param>
        /// <param name="changed_from">Orders changed from.</param>
        /// <param name="params">Pagination and filtering parameters.</param>
        /// <param name="filter">Mass order filter available only with partner token.</param>
        [Gql("getOrderList")]
        PaginatedList<Order> GetOrderList(
            string lang_code,
            int? status,
            DateTime? newer_from,
            DateTime? changed_from,
            OrderParams @params,
            OrderFilter filter);

        /// <summary>
        /// Retrieve detailed information about a specific order. Use '_Order' fragment to retrieve default set of information.
        /// </summary>
        /// <param name="order_num">Order's evidence number.</param>
        [Gql("getOrder")]
        Order GetOrder(string order_num);

        /// <summary>
        /// Preinvoice list.
        /// </summary>
        /// <param name="company_id">Company ID.</param>
        /// <param name="params">Params.</param>
        /// <param name="filter">Usage of mass preinvoice filter requires partner token.</param>
        [Gql("getPreinvoiceList")]
        PaginatedList<Preinvoice> GetPreinvoiceList(
            string company_id,
            OrderParams @params,
            PreinvoiceFilter filter);

        /// <summary>
        /// Invoice list.
        /// </summary>
        /// <param name="company_id">Company ID.</param>
        /// <param name="params">Params.</param>
        /// <param name="filter">Usage of mass invoice filter requires partner token.</param>
        [Gql("getInvoiceList")]
        PaginatedList<Invoice> GetInvoiceList(
            string company_id,
            OrderParams @params,
            InvoiceFilter filter);

        /// <summary>
        /// Invoice detail.
        /// </summary>
        /// <param name="invoice_num">Invoice number.</param>
        [Gql("getInvoice")]
        Invoice GetInvoice(string invoice_num);

        /// <summary>
        /// Product list.
        /// </summary>
        /// <param name="lang_code">Products for language version.</param>
        /// <param name="params">Params.</param>
        /// <param name="filter">Mass product filter available only with partner token.</param>
        [Gql("getProductList")]
        PaginatedList<Product> GetProductList(
            string lang_code,
            ProductParams @params,
            ProductFilter filter);

        /// <summary>
        /// Product detail.
        /// </summary>
        /// <param name="product_id">Internal product ID.</param>
        /// <param name="lang_code">Language code - if omitted, product details of main system language version are provided.</param>
        /// <param name="import_code">Import code from supplier or data source.</param>
        /// <param name="ean">Gets product detail by EAN.</param>
        [Gql("getProduct")]
        Product GetProduct(
            string product_id,
            string lang_code,
            string import_code,
            string ean);

        /// <summary>
        /// Product XML feed URL. Contains products and their final prices.
        /// </summary>
        /// <param name="lang_code">Language code.</param>
        /// <param name="type">Feed type: product or availability.</param>
        [Gql("getFeedUrl")]
        string GetFeedUrl(string lang_code, FeedType type);

        /// <summary>
        /// Category detail.
        /// </summary>
        /// <param name="category_id">Internal product ID.</param>
        /// <param name="productListParams">Params filter and pagination for products in this category.</param>
        [Gql("getCategory")]
        Category GetCategory(string category_id, ProductParams productListParams);

        /// <summary>
        /// List of warehouse items with change of blocking and total quantity in stock.
        /// </summary>
        /// <param name="changed_from">Search for stock items changed since.</param>
        /// <param name="params">Params.</param>
        [Gql("getWarehouseItemsWithRecentStockUpdates")]
        PaginatedList<WarehouseItem> GetWarehouseItemsWithRecentStockUpdates(
            DateTime changed_from,
            WarehouseItemParams @params);

        /// <summary>
        /// Invoicing company.
        /// </summary>
        /// <param name="id">ID.</param>
        /// <param name="name">Name.</param>
        /// <param name="company_id">Company ID.</param>
        [Gql("listMyCompanies")]
        List<InvoicingCompany> ListMyCompanies(string id, string name, string company_id);

        /// <summary>
        /// Order statuses list.
        /// </summary>
        /// <param name="lang_code">Gets order statuses translated for specified language version.</param>
        /// <param name="only_active">Pass TRUE to ignore statuses managed by inactive paygates.</param>
        [Gql("listOrderStatuses")]
        List<OrderStatus> ListOrderStatuses(string lang_code, bool only_active);
                
        /// <summary>
        /// Warehouse status list.
        /// </summary>
        /// <param name="allow_order">Allow order flag.</param>
        /// <param name="pickup">Pickup flag.</param>
        /// <param name="lang_code">Language code.</param>
        [Gql("listWarehouseStatuses")]
        List<WarehouseStatus> ListWarehouseStatuses(bool allow_order, bool pickup, string lang_code);

        /// <summary>
        /// Producer list.
        /// </summary>
        /// <param name="search">Search string for filter.</param>
        [Gql("listProducers")]
        List<Producer> ListProducers(string search);

        /// <summary>
        /// Shipping list.
        /// </summary>
        /// <param name="lang_code">Shippings belonging to specified language version.</param>
        /// <param name="only_active">Only active flag.</param>
        [Gql("listShippings")]
        List<Shipping> ListShippings(string lang_code, bool only_active);

        /// <summary>
        /// Payment list.
        /// </summary>
        /// <param name="lang_code">Payment ways belonging to specified language version.</param>
        /// <param name="only_active">Only active flag.</param>
        [Gql("listPayments")]
        List<Payment> ListPayments(string lang_code, bool only_active);

        /// <summary>
        /// Currencies definition.
        /// </summary>
        [Gql("listCurrencies")]
        List<Currency> ListCurrencies();

        /// <summary>
        /// Language version list.
        /// </summary>
        [Gql("listLanguageVersions")]
        List<LanguageVersion> ListLanguageVersions();
    }


}

using MTecl.GraphQlClient;
using MTecl.GraphQlClient.ObjectMapping.Descriptors;
using System;
using System.Collections.Generic;
using System.Text;

namespace BwApiClient.Model.Data
{
    public class Order
    {
        /// <summary>
        /// System's internal ID.
        /// </summary>
        public string id { get; set; }

        /// <summary>
        /// Order's evidence number.
        /// </summary>
        public string order_num { get; set; }

        /// <summary>
        /// Order's optional external reference (e.g., reference to an order from a marketplace).
        /// </summary>
        public string external_ref { get; set; }

        /// <summary>
        /// Date and time of receiving the order.
        /// </summary>
        public DateTime pur_date { get; set; }

        /// <summary>
        /// Variable symbol (payment reference).
        /// </summary>
        public int? var_symb { get; set; }

        /// <summary>
        /// Source of the order (e.g., e-shop, email, phone). It may contain custom values.
        /// </summary>
        public string source { get; set; }

        /// <summary>
        /// Flag indicating that the order is awaiting administrator's intervention or review.
        /// Further processing should be avoided until manually reviewed by staff.
        /// </summary>
        /// BW returns error if this field is included in the query
        //public bool blocked { get; set; }

        /// <summary>
        /// Customer's note for the order.
        /// </summary>
        public string note { get; set; }

        /// <summary>
        /// Internal note for the order (not visible to the customer).
        /// </summary>
        public string internal_note { get; set; }

        /// <summary>
        /// Customer information.
        /// </summary>
        public Customer customer { get; set; }

        /// <summary>
        /// Reference to the person/account creating the order.
        /// </summary>
        /// TODO
        //public OrderCreator creator { get; set; } 

        /// <summary>
        /// Invoice address.
        /// </summary>
        public AddressData invoice_address { get; set; }

        /// <summary>
        /// Delivery address, if specified, or pickup point's address if applicable.
        /// </summary>
        public AddressData delivery_address { get; set; }

        /// <summary>
        /// Shipment reports.
        /// </summary>
        /// BW throws
        //public List<ShipmentInfo> shipments { get; set; }

        /// <summary>
        /// Ordered items (products).
        /// </summary>
        public List<OrderItem> items { get; set; }

        /// <summary>
        /// Non-product elements and fees.
        /// </summary>
        public List<OrderPriceElement> price_elements { get; set; }

        /// <summary>
        /// Total order price - may be net or inclusive of tax.
        /// </summary>
        [Gql(InclusionMode = FieldInclusionMode.Include)]
        public Price sum { get; set; }

        /// <summary>
        /// Current order status.
        /// </summary>
        [Gql(InclusionMode = FieldInclusionMode.Include)]
        public OrderStatusRef status { get; set; }

        /// <summary>
        /// Assigned sales representative.
        /// </summary>        
        public Person salesrep { get; set; }

        /// <summary>
        /// Set of preinvoices (payment requests) related to this order.
        /// </summary>
        public List<Preinvoice> preinvoices { get; set; }

        /// <summary>
        /// Set of invoices (final documents) related to this order.
        /// </summary>
        public List<Invoice> invoices { get; set; }

        /// <summary>
        /// VAT summary - contains multiple taxation objects corresponding to specific tax rates within the order.
        /// </summary>
        public List<Taxation> vat_summary { get; set; }

        /// <summary>
        /// Order language.
        /// </summary>
        public LanguageVersion language { get; set; }

        /// <summary>
        /// Date of the last change.
        /// </summary>
        public DateTime last_change { get; set; }

        /// <summary>
        /// Indicates if the order is in the One-Stop-Shop (OSS) EU VAT regime.
        /// </summary>
        public bool oss { get; set; }

        /// <summary>
        /// Country code for OSS regime - VAT taxed follows rates and is dedicated to this EU country.
        /// </summary>
        public string oss_country { get; set; }
    }

}

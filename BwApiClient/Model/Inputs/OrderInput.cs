using System;
using System.Collections.Generic;
using System.Text;

namespace BwApiClient.Model.Inputs
{
    public class OrderInput
    {
        /// <summary>
        /// External order number, used for matching records.
        /// </summary>
        public string ext_order_id { get; set; }

        /// <summary>
        /// Customer data.
        /// </summary>
        public CustomerInput customer { get; set; }

        /// <summary>
        /// Invoice address.
        /// </summary>
        public AddressDataInput invoice_address { get; set; }

        /// <summary>
        /// Delivery address.
        /// </summary>
        public AddressDataInput delivery_address { get; set; }

        /// <summary>
        /// Datetime when the order was created in the source environment.
        /// This is recorded to order's history for reference and does not impact the internal 'pur_date'.
        /// </summary>
        public DateTime? external_pur_date { get; set; }

        /// <summary>
        /// List of ordered items.
        /// </summary>
        public List<OrderItemInput> items { get; set; }

        /// <summary>
        /// Delivery, payment, and other order elements.
        /// </summary>
        public List<OrderPriceElementInput> price_elements { get; set; }

        /// <summary>
        /// Initial order status (internal ID).
        /// </summary>
        public int status { get; set; }

        /// <summary>
        /// Order note.
        /// </summary>
        public string note { get; set; }

        /// <summary>
        /// Order source, e.g., marketplace identifier. Defaults to 'api'.
        /// </summary>
        public string source { get; set; }

        /// <summary>
        /// Array of taxation inputs (base, rate, tax amount).
        /// </summary>
        public List<TaxationInput> vat_summary { get; set; }

        /// <summary>
        /// Language version internal ID.
        /// </summary>
        public int lang_id { get; set; }

        /// <summary>
        /// Currency internal ID (not currency code).
        /// </summary>
        public int currency_id { get; set; }

        /// <summary>
        /// Final price of the order, subject to verification. 
        /// If omitted, it is calculated based on 'vat_summary', ordered items, and price elements.
        /// </summary>
        public PriceInput sum { get; set; }

        /// <summary>
        /// Additional information for shipping, e.g., pickup point branch ID.
        /// </summary>
        public List<ShipmentInfoInput> shipments { get; set; }

        /// <summary>
        /// Order in One Stop Shop (OSS) mode. Defaults to FALSE.
        /// </summary>
        public bool? oss { get; set; }

        /// <summary>
        /// Code for country in OSS. Defaults to NULL.
        /// </summary>
        public string oss_country { get; set; }

        /// <summary>
        /// Indicates if the order is tax-excluded. Defaults to FALSE.
        /// </summary>
        public bool? tax_excl { get; set; }

        /// <summary>
        /// Reason for tax exemption. Can use predefined constants (e.g., "eu-deal", "export") or custom text.
        /// </summary>
        public string tax_excl_reason { get; set; }

        /// <summary>
        /// Unique entity identifier.
        /// </summary>
        public string order_submission_id { get; set; }
    }

}

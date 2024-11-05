using System;
using System.Collections.Generic;
using System.Text;

namespace BwApiClient.Model.Data
{
    public class Invoice
    {
        /// <summary>
        /// Invoice internal ID.
        /// </summary>
        public string id { get; set; }

        /// <summary>
        /// Invoicing company (supplier).
        /// </summary>
        public InvoicingCompany supplier { get; set; }

        /// <summary>
        /// Customer information.
        /// </summary>
        public Customer customer { get; set; }

        /// <summary>
        /// Invoice address.
        /// </summary>
        public AddressData invoice_address { get; set; } // Placeholder for AddressData

        /// <summary>
        /// Buy date.
        /// </summary>
        public DateTime buy_date { get; set; }

        /// <summary>
        /// Due date.
        /// </summary>
        public DateTime? due_date { get; set; }

        /// <summary>
        /// Pay date - the date of payment of the full amount or the last surcharge.
        /// </summary>
        public DateTime? pay_date { get; set; }

        /// <summary>
        /// List of payments.
        /// </summary>
        public List<Receipt> payments { get; set; }

        /// <summary>
        /// Invoice creation date.
        /// </summary>
        public DateTime created { get; set; }

        /// <summary>
        /// Indicates if the invoice is paid or if the sum of received relevant payments is higher than the invoiced amount.
        /// </summary>
        public bool paid { get; set; }

        /// <summary>
        /// Regular payment identification - omit starting zeroes (variable symbol).
        /// </summary>
        public int? var_symb { get; set; }

        /// <summary>
        /// Preinvoice number.
        /// </summary>
        public int? preinvoice_num { get; set; }

        /// <summary>
        /// Preinvoice reference.
        /// </summary>
        public Preinvoice preinvoice { get; set; }

        /// <summary>
        /// Invoice number.
        /// </summary>
        public string invoice_num { get; set; }

        /// <summary>
        /// Invoice items.
        /// </summary>
        public List<OrderItem> items { get; set; } // Placeholder for OrderItem

        /// <summary>
        /// Invoice price elements.
        /// </summary>
        public List<OrderPriceElement> price_elements { get; set; }

        /// <summary>
        /// Summary price.
        /// </summary>
        public Price sum { get; set; }

        /// <summary>
        /// Detailed taxing.
        /// </summary>
        public List<Taxation> vat_summary { get; set; }

        /// <summary>
        /// Original order.
        /// </summary>
        public Order order { get; set; } // Placeholder for Order

        /// <summary>
        /// URL link for PDF document. Downloading this document via API does not mark it as printed in the e-shop.
        /// </summary>
        public string download_pdf { get; set; }
    }

}

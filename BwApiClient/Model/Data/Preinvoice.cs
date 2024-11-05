using System;
using System.Collections.Generic;
using System.Text;

namespace BwApiClient.Model.Data
{
    public class Preinvoice
    {
        /// <summary>
        /// Preinvoice internal ID.
        /// </summary>
        public string id { get; set; }

        /// <summary>
        /// Preinvoice number.
        /// </summary>
        public int preinvoice_num { get; set; }

        /// <summary>
        /// Variable symbol.
        /// </summary>
        public int var_symb { get; set; }

        /// <summary>
        /// Supplier information.
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
        /// Creation date.
        /// </summary>
        public DateTime created { get; set; }

        /// <summary>
        /// Due date.
        /// </summary>
        public DateTime? due_date { get; set; }

        /// <summary>
        /// Order reference.
        /// </summary>
        public Order order { get; set; } // Placeholder for Order

        /// <summary>
        /// List of payments.
        /// </summary>
        public List<Receipt> payments { get; set; }

        /// <summary>
        /// Linked invoice.
        /// </summary>
        public Invoice invoice { get; set; } // Placeholder for Invoice

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
    }

}

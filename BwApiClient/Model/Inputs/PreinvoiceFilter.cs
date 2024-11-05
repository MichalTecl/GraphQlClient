using System;
using System.Collections.Generic;
using System.Text;

namespace BwApiClient.Model.Inputs
{
    public class PreinvoiceFilter
    {
        /// <summary>
        /// Purchased from date.
        /// </summary>
        public DateTime? buy_date_from { get; set; }

        /// <summary>
        /// Purchased until date.
        /// </summary>
        public DateTime? buy_date_to { get; set; }

        /// <summary>
        /// Due from date.
        /// </summary>
        public DateTime? due_date_from { get; set; }

        /// <summary>
        /// Due until date.
        /// </summary>
        public DateTime? due_date_to { get; set; }

        /// <summary>
        /// Paid from date.
        /// </summary>
        public DateTime? pay_date_from { get; set; }

        /// <summary>
        /// Paid until date.
        /// </summary>
        public DateTime? pay_date_to { get; set; }

        /// <summary>
        /// Total amount greater than or equal to.
        /// </summary>
        public float? sum_from { get; set; }

        /// <summary>
        /// Total amount less than or equal to.
        /// </summary>
        public float? sum_to { get; set; }

        /// <summary>
        /// Preinvoiced from date.
        /// </summary>
        public DateTime? pre_inv_date_from { get; set; }

        /// <summary>
        /// Preinvoiced until date.
        /// </summary>
        public DateTime? pre_inv_date_to { get; set; }

        /// <summary>
        /// Indicates if overdue.
        /// </summary>
        public bool? over_due { get; set; }

        /// <summary>
        /// Invoice number.
        /// </summary>
        public string inv_num { get; set; }

        /// <summary>
        /// Search for preinvoices with existing/missing final invoices.
        /// </summary>
        public bool? invoiced { get; set; }

        /// <summary>
        /// Preinvoice number.
        /// </summary>
        public string pre_inv_num { get; set; }

        /// <summary>
        /// Customer name, surname, or company name.
        /// </summary>
        public string customer { get; set; }

        /// <summary>
        /// Full-text search for address.
        /// </summary>
        public string address { get; set; }

        /// <summary>
        /// Currency code.
        /// </summary>
        public string currency { get; set; }

        /// <summary>
        /// Original order status internal ID.
        /// </summary>
        public int? status { get; set; }

        /// <summary>
        /// Shipping internal ID.
        /// </summary>
        public int? shipping { get; set; }

        /// <summary>
        /// Language version internal ID.
        /// </summary>
        public int? lang_id { get; set; }

        /// <summary>
        /// Internal ID of invoicing company.
        /// </summary>
        public string invoicer { get; set; }

        /// <summary>
        /// Original order number.
        /// </summary>
        public string order_num { get; set; }
    }

}

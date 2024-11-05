using System;
using System.Collections.Generic;
using System.Text;

namespace BwApiClient.Model.Inputs
{
    public class OrderFilter
    {
        /// <summary>
        /// Created from date.
        /// </summary>
        public DateTime? pur_date_from { get; set; }

        /// <summary>
        /// Created until date.
        /// </summary>
        public DateTime? pur_date_to { get; set; }

        /// <summary>
        /// Minimum total amount.
        /// </summary>
        public float? sum_from { get; set; }

        /// <summary>
        /// Maximum total amount.
        /// </summary>
        public float? sum_to { get; set; }

        /// <summary>
        /// Title of item contained in the order.
        /// </summary>
        public string item_title { get; set; }

        /// <summary>
        /// Minimum count of items in the order.
        /// </summary>
        public float? items_from { get; set; }

        /// <summary>
        /// Maximum count of items in the order.
        /// </summary>
        public float? items_to { get; set; }

        /// <summary>
        /// Order number.
        /// </summary>
        public string order_num { get; set; }

        /// <summary>
        /// Customer name, surname, or company name.
        /// </summary>
        public string customer { get; set; }

        /// <summary>
        /// Address.
        /// </summary>
        public string address { get; set; }

        /// <summary>
        /// Currency code.
        /// </summary>
        public string currency { get; set; }

        /// <summary>
        /// Order status ID.
        /// </summary>
        public int? status { get; set; }

        /// <summary>
        /// Shipping ID.
        /// </summary>
        public int? shipping { get; set; }

        /// <summary>
        /// Payment ID.
        /// </summary>
        public int? payment { get; set; }

        /// <summary>
        /// Language version ID.
        /// </summary>
        public int? lang_id { get; set; }

        /// <summary>
        /// Invoicing company ID.
        /// </summary>
        public int? invoicer_id { get; set; }

        /// <summary>
        /// Full-text search in EAN, Warehouse number, and Import code.
        /// </summary>
        public string product_identificator { get; set; }

        /// <summary>
        /// Order source.
        /// </summary>
        public string source { get; set; }

        /// <summary>
        /// Internal note.
        /// </summary>
        public string internal_note { get; set; }
    }

}

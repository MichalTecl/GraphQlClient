using System;
using System.Collections.Generic;
using System.Text;

namespace BwApiClient.Model.Data
{
    public class ReceiptItem
    {
        /// <summary>
        /// Item internal ID.
        /// </summary>
        public string id { get; set; }

        /// <summary>
        /// Text description of the item.
        /// </summary>
        public string item_label { get; set; }

        /// <summary>
        /// Item quantity.
        /// </summary>
        public int quantity { get; set; }

        /// <summary>
        /// Item price.
        /// </summary>
        public Price price { get; set; }

        /// <summary>
        /// Tax ratio for the given order item.
        /// </summary>
        public float tax_rate { get; set; }
    }

}

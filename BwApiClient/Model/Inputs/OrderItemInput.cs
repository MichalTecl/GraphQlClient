using System;
using System.Collections.Generic;
using System.Text;

namespace BwApiClient.Model.Inputs
{
    public class OrderItemInput
    {
        /// <summary>
        /// Warehouse number.
        /// </summary>
        public string warehouse_number { get; set; }

        /// <summary>
        /// Item title.
        /// </summary>
        public string title { get; set; }

        /// <summary>
        /// Quantity of items.
        /// </summary>
        public float quantity { get; set; }

        /// <summary>
        /// Tax rate for the item.
        /// </summary>
        public float tax_rate { get; set; }

        /// <summary>
        /// Weight of the item.
        /// </summary>
        public WeightInput weight { get; set; } // Placeholder for WeightInput

        /// <summary>
        /// Product's EAN.
        /// </summary>
        public string ean { get; set; }

        /// <summary>
        /// Price of the item.
        /// </summary>
        public PriceInput price { get; set; } // Placeholder for PriceInput
    }

}

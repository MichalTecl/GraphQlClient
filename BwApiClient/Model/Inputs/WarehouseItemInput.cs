using System;
using System.Collections.Generic;
using System.Text;

namespace BwApiClient.Model.Inputs
{
    public class WarehouseItemInput
    {
        /// <summary>
        /// Warehouse number.
        /// </summary>
        public string warehouse_number { get; set; }

        /// <summary>
        /// Product's EAN.
        /// </summary>
        public string ean { get; set; }

        /// <summary>
        /// Quantity of items in stock.
        /// </summary>
        public float? quantity { get; set; }

        /// <summary>
        /// Weight of the item.
        /// </summary>
        public WeightInput weight { get; set; } // Placeholder for WeightInput

        /// <summary>
        /// Warehouse status of this variant.
        /// </summary>
        public WarehouseStatusInput status { get; set; } // Placeholder for WarehouseStatusInput

        /// <summary>
        /// Price of the item.
        /// </summary>
        public PriceInput price { get; set; } // Placeholder for PriceInput
    }

}

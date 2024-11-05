using System;
using System.Collections.Generic;
using System.Text;

namespace BwApiClient.Model.Data
{
    public class WarehouseItem
    {
        /// <summary>
        /// Item internal ID.
        /// </summary>
        public string id { get; set; }

        /// <summary>
        /// Quantity in stock.
        /// </summary>
        public float quantity { get; set; }

        /// <summary>
        /// Non-blocked quantity in stock.
        /// </summary>
        public float available_quantity { get; set; }

        /// <summary>
        /// Warehouse identifier.
        /// </summary>
        public string warehouse_number { get; set; }

        /// <summary>
        /// Product.
        /// </summary>
        public Product product { get; set; }

        /// <summary>
        /// EAN.
        /// </summary>
        public string ean { get; set; }

        /// <summary>
        /// Weight.
        /// </summary>
        public string weight { get; set; }

        /// <summary>
        /// Warehouse status of this variant. May be null if the status is determined by the main product's status.
        /// </summary>
        public WarehouseStatus status { get; set; }

        /// <summary>
        /// Attributes making up the physical variant of the product.
        /// </summary>
        public List<Attribute> attributes { get; set; }

        /// <summary>
        /// Image path for specific warehouse item variant.
        /// </summary>
        public string image { get; set; }

        /// <summary>
        /// Product variant's net price (excluding tax). If not set, the product's main price is returned.
        /// </summary>
        public Price price { get; set; }

        /// <summary>
        /// Final price of the product, including tax, discounts, and all other price components.
        /// </summary>
        public Price final_price { get; set; }
    }

}

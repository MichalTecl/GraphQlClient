using System;
using System.Collections.Generic;
using System.Text;

namespace BwApiClient.Model.Data
{
    public class WarehouseStatus
    {
        /// <summary>
        /// Internal warehouse status ID.
        /// </summary>
        public string id { get; set; }

        /// <summary>
        /// Status name.
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// RGB color in hex notation (e.g., #A45594).
        /// </summary>
        public string color { get; set; }

        /// <summary>
        /// Status image.
        /// </summary>
        public string image { get; set; }

        /// <summary>
        /// Text note.
        /// </summary>
        public string note { get; set; }

        /// <summary>
        /// Allows ordering of items with this warehouse status.
        /// </summary>
        public bool allow_order { get; set; }

        /// <summary>
        /// Shows product on public e-shop.
        /// </summary>
        public bool show_product { get; set; }

        /// <summary>
        /// Default delivery time in days.
        /// </summary>
        public int delivery_in_days { get; set; }

        /// <summary>
        /// Daily deadline for propagated delivery.
        /// You must place an order before this time of day (specific hour) to be entitled to the 'delivery_in_days' expected delivery.
        /// </summary>
        public string order_deadline { get; set; }
    }

}

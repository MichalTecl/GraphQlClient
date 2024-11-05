using System;
using System.Collections.Generic;
using System.Text;

namespace BwApiClient.Model.Inputs
{
    public class ProductFilter
    {
        /// <summary>
        /// Product name.
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// Product EAN.
        /// </summary>
        public string ean { get; set; }

        /// <summary>
        /// Import code.
        /// </summary>
        public string import_code { get; set; }

        /// <summary>
        /// Warehouse number.
        /// </summary>
        public string warehouse_number { get; set; } // Assuming WarehouseNumber is represented as a string

        /// <summary>
        /// Producer.
        /// </summary>
        public string producer { get; set; }

        /// <summary>
        /// Indicates if the product is active.
        /// </summary>
        public bool? active { get; set; }

        /// <summary>
        /// Indicates whether the product originates from a feed or is native in the system.
        /// </summary>
        public bool? import { get; set; }

        /// <summary>
        /// Category internal ID to which the product is assigned.
        /// </summary>
        public int? category { get; set; }

        /// <summary>
        /// Warehouse status internal ID.
        /// </summary>
        public int? warehouse_status { get; set; }
    }

}

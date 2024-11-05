using BwApiClient.Model.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace BwApiClient.Model.Inputs
{
    public class OrderPriceElementInput
    {
        /// <summary>
        /// Type of the price element.
        /// </summary>
        public PriceElementType type { get; set; }

        /// <summary>
        /// Title of the price element.
        /// </summary>
        public string title { get; set; }

        /// <summary>
        /// Tax rate for the price element.
        /// </summary>
        public float? tax_rate { get; set; }

        /// <summary>
        /// Price details of the element.
        /// </summary>
        public PriceInput price { get; set; } // Placeholder for PriceInput

        /// <summary>
        /// Reference ID for the price element.
        /// </summary>
        public string reference_id { get; set; }
    }

}

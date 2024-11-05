using System;
using System.Collections.Generic;
using System.Text;

namespace BwApiClient.Model.Data
{
    public class CurrencyRef
    {
        /// <summary>
        /// Internal ID.
        /// </summary>
        public string id { get; set; }

        /// <summary>
        /// 3-letter ISO code.
        /// </summary>
        public string code { get; set; }
    }

    public class Currency
    {
        /// <summary>
        /// Internal ID.
        /// </summary>
        public string id { get; set; }

        /// <summary>
        /// 3-letter ISO code.
        /// </summary>
        public string code { get; set; } 

        /// <summary>
        /// Currency symbol, e.g., '$' or '€'.
        /// </summary>
        public string symbol { get; set; }

        /// <summary>
        /// Custom currency name.
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// Rounding precision.
        /// </summary>
        public int rounding_precision { get; set; }

        /// <summary>
        /// Number of decimal places.
        /// </summary>
        public int decimals { get; set; }
    }

}

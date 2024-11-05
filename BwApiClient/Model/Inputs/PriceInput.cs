using System;
using System.Collections.Generic;
using System.Text;

namespace BwApiClient.Model.Inputs
{
    public class PriceInput
    {
        /// <summary>
        /// Amount.
        /// </summary>
        public float value { get; set; }

        /// <summary>
        /// Currency ISO code.
        /// </summary>
        public string currency_code { get; set; }

        /// <summary>
        /// Indicates if the price includes tax. For VAT non-payers, this field may carry both values and should not be relied upon.
        /// </summary>
        public bool? is_net_price { get; set; }
    }

}

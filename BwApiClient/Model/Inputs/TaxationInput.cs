using System;
using System.Collections.Generic;
using System.Text;

namespace BwApiClient.Model.Inputs
{
    public class TaxationInput
    {
        /// <summary>
        /// Tax rate in percent points.
        /// </summary>
        public float tax_rate { get; set; }

        /// <summary>
        /// Tax amount.
        /// </summary>
        public float amount { get; set; }

        /// <summary>
        /// Taxed amount (base).
        /// </summary>
        public float tax_base { get; set; }

        /// <summary>
        /// Currency code.
        /// </summary>
        public string currency_code { get; set; }
    }
}

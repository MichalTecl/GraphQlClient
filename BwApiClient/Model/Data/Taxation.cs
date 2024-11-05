using System;
using System.Collections.Generic;
using System.Text;

namespace BwApiClient.Model.Data
{
    public class Taxation
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
    }

}

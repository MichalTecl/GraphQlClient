using MTecl.GraphQlClient.ObjectMapping.Descriptors;
using MTecl.GraphQlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace BwApiClient.Model.Data
{
    public class Price
    {
        /// <summary>
        /// Amount with rounding rules.
        /// </summary>
        public float value { get; set; }

        /// <summary>
        /// Raw amount (unrounded).
        /// </summary>
        /// BW throws error if this field is included in the query
        // public float raw_value { get; set; }

        /// <summary>
        /// Currency reference.
        /// </summary>    
        [Gql(InclusionMode = FieldInclusionMode.Include)]
        public CurrencyRef currency { get; set; }
                
        /// <summary>
        /// Indicates if the price includes tax. For VAT non-payers, this field may carry both values and should not be relied upon.
        /// </summary>
        public bool? is_net_price { get; set; }

        /// <summary>
        /// Amount including currency symbol and optional formatting/rounding rules.
        /// </summary>
        public string formatted { get; set; }
    }

}

using MTecl.GraphQlClient.ObjectMapping.Descriptors;
using MTecl.GraphQlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace BwApiClient.Model.Data
{
    public class OrderPriceElement
    {
        /// <summary>
        /// Internal element ID.
        /// </summary>
        public string id { get; set; }

        /// <summary>
        /// Back reference to order.
        /// </summary>
        public Order order { get; set; }

        /// <summary>
        /// Element type.
        /// </summary>
        public string type { get; set; }

        /// <summary>
        /// Element title.
        /// </summary>
        public string title { get; set; }

        /// <summary>
        /// Detailed information about the element (e.g., gift certificate's code, pickup point ID).
        /// </summary>
        public string value { get; set; }

        /// <summary>
        /// Always net price for the element.
        /// </summary>
        [Gql(InclusionMode = FieldInclusionMode.Include)]
        public Price price { get; set; }

        /// <summary>
        /// Tax rate for the element.
        /// </summary>
        /// BW - it should be declared as nullable
        public float? tax_rate { get; set; }

        /// <summary>
        /// Sum per row (including tax).
        /// </summary>
        [Gql(InclusionMode = FieldInclusionMode.Include)]
        public Price sum_with_tax { get; set; }
    }

}

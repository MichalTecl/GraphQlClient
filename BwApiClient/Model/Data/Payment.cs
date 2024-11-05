using System;
using System.Collections.Generic;
using System.Text;

namespace BwApiClient.Model.Data
{
    public class Payment
    {
        /// <summary>
        /// Payment internal ID.
        /// </summary>
        public string id { get; set; }

        /// <summary>
        /// Payment name.
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// Payment price.
        /// </summary>
        public Price price { get; set; }

        /// <summary>
        /// Payment type.
        /// </summary>
        public string type { get; set; }
    }

}

using System;
using System.Collections.Generic;
using System.Text;

namespace BwApiClient.Model.Data
{
    public class Shipping
    {
        /// <summary>
        /// Shipping internal ID.
        /// </summary>
        public string id { get; set; }

        /// <summary>
        /// Shipping name.
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// Shipping price.
        /// </summary>
        public Price price { get; set; }

        /// <summary>
        /// Indicates if the shipping method is store pickup.
        /// </summary>
        public bool store_pickup { get; set; }

        /// <summary>
        /// Carrier identifier.
        /// </summary>
        public string carrier { get; set; }
    }

}

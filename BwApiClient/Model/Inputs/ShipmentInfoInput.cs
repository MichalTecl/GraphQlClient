using System;
using System.Collections.Generic;
using System.Text;

namespace BwApiClient.Model.Inputs
{
    public class ShipmentInfoInput
    {
        /// <summary>
        /// Carrier identifier.
        /// </summary>
        public string carrier { get; set; }

        /// <summary>
        /// Destination logistic point.
        /// </summary>
        public LogisticPointInput destination_point { get; set; }
    }

}

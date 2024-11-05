using MTecl.GraphQlClient;
using MTecl.GraphQlClient.ObjectMapping.Descriptors;
using System;
using System.Collections.Generic;
using System.Text;

namespace BwApiClient.Model.Data
{
    public class ShipmentInfo
    {
        /// <summary>
        /// Carrier identifier.
        /// </summary>
        public string carrier { get; set; }

        /// <summary>
        /// Destination logistic point.
        /// </summary>
        [Gql(InclusionMode = FieldInclusionMode.Include)]
        public LogisticPoint destination_point { get; set; }
    }

}

using MTecl.GraphQlClient.ObjectMapping.Descriptors;
using MTecl.GraphQlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace BwApiClient.Model.Data
{
    public class Attribute
    {
        /// <summary>
        /// Internal attribute ID.
        /// </summary>
        public string id { get; set; }

        /// <summary>
        /// Attribute's name. The value of title may change corresponding to specific language for the same attribute.
        /// </summary>
        public string title { get; set; }

        /// <summary>
        /// Type.
        /// </summary>
        public string type { get; set; }

        /// <summary>
        /// Set of attribute values. For specific attribute types (e.g., variants or parameters defined as checkbox), a product may have multiple values.
        /// </summary>
        [Gql(InclusionMode = FieldInclusionMode.Include)]
        public List<AttributeValue> values { get; set; }
    }

}

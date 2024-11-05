using System;
using System.Collections.Generic;
using System.Text;

namespace BwApiClient.Model.Data
{
    public class AttributeValue
    {
        /// <summary>
        /// ID.
        /// </summary>
        public string id { get; set; }

        /// <summary>
        /// Text value.
        /// </summary>
        public string value { get; set; }

        /// <summary>
        /// Image URL.
        /// </summary>
        public string image { get; set; } // Assuming Url is represented as a string
    }

}

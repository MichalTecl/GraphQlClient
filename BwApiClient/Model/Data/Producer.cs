using System;
using System.Collections.Generic;
using System.Text;

namespace BwApiClient.Model.Data
{
    public class Producer
    {
        /// <summary>
        /// Producer internal ID.
        /// </summary>
        public string id { get; set; }

        /// <summary>
        /// Producer name.
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// Producer's website URL.
        /// </summary>
        public string url { get; set; }
    }

}

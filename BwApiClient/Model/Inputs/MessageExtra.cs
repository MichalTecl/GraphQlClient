using System;
using System.Collections.Generic;
using System.Text;

namespace BwApiClient.Model.Inputs
{
    public class MessageExtra
    {
        /// <summary>
        /// User ID.
        /// </summary>
        public string user_id { get; set; }

        /// <summary>
        /// Text message.
        /// </summary>
        public string text { get; set; }

        /// <summary>
        /// Email address.
        /// </summary>
        public string email { get; set; }

        /// <summary>
        /// URL.
        /// </summary>
        public string url { get; set; }

        /// <summary>
        /// Phone number.
        /// </summary>
        public string phone { get; set; }
    }

}

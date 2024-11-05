using System;
using System.Collections.Generic;
using System.Text;

namespace BwApiClient.Model.Data
{

    public class OrderStatusRef 
    {
        /// <summary>
        /// Internal status ID.
        /// </summary>
        public string id { get; set; }

        /// <summary>
        /// Status name.
        /// </summary>
        public string name { get; set; }
    }

    public class OrderStatus : OrderStatusRef
    {        
        /// <summary>
        /// RGB color in hex notation (e.g., #A45594).
        /// </summary>
        public string color { get; set; }

        /// <summary>
        /// Text description of the meaning of the status.
        /// </summary>
        public string note { get; set; }

        /// <summary>
        /// Indicates if the customer is allowed to change the order for this status.
        /// </summary>
        /// BW returns error if this field is included in the query
        //public bool order_change { get; set; }

        /// <summary>
        /// System preset to send a notification email when setting this status.
        /// </summary>
        /// BW returns error if this field is included in the query
        //public bool statusmail { get; set; }

        /// <summary>
        /// System preset to send a notification SMS when setting this status.
        /// </summary>
        /// BW returns error if this field is included in the query
        //public bool send_sms { get; set; }

        /// <summary>
        /// Action in warehouse when setting this status.
        /// </summary>
        public string warehouse_action { get; set; }
    }

}

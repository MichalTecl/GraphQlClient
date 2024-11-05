using System;
using System.Collections.Generic;
using System.Text;

namespace BwApiClient.Model.Enums
{
    public enum OrderSorting
    {
        /// <summary>
        /// Sort by order ID.
        /// </summary>
        order_id,

        /// <summary>
        /// Sort by order number.
        /// </summary>
        order_num,

        /// <summary>
        /// Sort by the last change of the record. Option available only with partner token.
        /// </summary>
        last_change,

        /// <summary>
        /// Sort by preinvoice ID.
        /// </summary>
        pre_inv_id,

        /// <summary>
        /// Sort by invoice ID.
        /// </summary>
        inv_id,

        /// <summary>
        /// Sort by purchase date.
        /// </summary>
        pur_date
    }

}

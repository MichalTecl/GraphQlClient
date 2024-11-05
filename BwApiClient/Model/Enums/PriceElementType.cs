using System;
using System.Collections.Generic;
using System.Text;

namespace BwApiClient.Model.Enums
{
    public enum PriceElementType
    {
        /// <summary>
        /// Shipping fee. The 'value' field holds Pick-up location ID.
        /// </summary>
        shipping,

        /// <summary>
        /// Payment fee.
        /// </summary>
        payment,

        /// <summary>
        /// Gift certificate.
        /// </summary>
        gift,

        /// <summary>
        /// Custom price element, may be positive (custom surcharge) or negative (ad-hoc discount).
        /// </summary>
        custom,

        /// <summary>
        /// Rounding quantification - system calculated.
        /// </summary>
        autoround,

        /// <summary>
        /// Customer partner card (e.g., ISIC, SPHERE). The 'value' field holds vendor-specific card number.
        /// </summary>
        partner_card,

        /// <summary>
        /// Applied percent discount. The 'price' field holds negative amount (discount sum).
        /// The 'value' field holds percentage of applied discount.
        /// </summary>
        percent_discount,

        /// <summary>
        /// Applied discount. The 'price' field holds negative amount (discount).
        /// The 'value' field holds positive amount (absolute value) of a discount.
        /// </summary>
        discount
    }

}

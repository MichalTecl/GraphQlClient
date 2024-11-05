using BwApiClient.Model.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace BwApiClient.Model.Inputs
{
    public class ProductParams
    {
        /// <summary>
        /// Count of returned results (max) [up to 30].
        /// </summary>
        public int? limit { get; set; }

        /// <summary>
        /// Ordinal position of the first contained result (list offset).
        /// </summary>
        public int? cursor { get; set; }

        /// <summary>
        /// Property name to order the results according to.
        /// </summary>
        public ProductSorting order_by { get; set; }

        /// <summary>
        /// Sort direction.
        /// </summary>
        public Direction sort { get; set; }

        /// <summary>
        /// Search string.
        /// </summary>
        public string search { get; set; }
    }

}

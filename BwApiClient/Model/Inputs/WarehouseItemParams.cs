using System;
using System.Collections.Generic;
using System.Text;

namespace BwApiClient.Model.Inputs
{
    public class WarehouseItemParams
    {
        /// <summary>
        /// Count of returned results (max) [up to 30].
        /// </summary>
        public int? limit { get; set; }

        /// <summary>
        /// Ordinal position of the first contained result (list offset).
        /// </summary>
        public int? cursor { get; set; }
    }
}

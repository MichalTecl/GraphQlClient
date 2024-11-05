using System;
using System.Collections.Generic;
using System.Text;

namespace BwApiClient.Model.Data
{
    public class Weight
    {
        /// <summary>
        /// Weight value.
        /// </summary>
        public float value { get; set; }

        /// <summary>
        /// Weight unit (currently only 'kg' unit is supported).
        /// </summary>
        public string unit { get; set; }
    }

}

using System;
using System.Collections.Generic;
using System.Text;

namespace BwApiClient.Model.Inputs
{
    public class WeightInput
    {
        /// <summary>
        /// Weight value.
        /// </summary>
        public float? value { get; set; }

        /// <summary>
        /// Weight unit.
        /// </summary>
        public string unit { get; set; } // Assuming WeightUnit is represented as a string
    }
}

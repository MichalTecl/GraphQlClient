using System;
using System.Collections.Generic;
using System.Text;

namespace BwApiClient.Model.Inputs
{
    public class AddressDataInput
    {
        /// <summary>
        /// Company's name, including legal form.
        /// </summary>
        public string company_name { get; set; }

        /// <summary>
        /// Person's name.
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// Person's surname.
        /// </summary>
        public string surname { get; set; }

        /// <summary>
        /// Street name.
        /// </summary>
        public string street { get; set; }

        /// <summary>
        /// Building ID (descriptive number).
        /// </summary>
        public string descriptive_number { get; set; }

        /// <summary>
        /// Street number (orientation number).
        /// </summary>
        public string orientation_number { get; set; }

        /// <summary>
        /// Name of city/town/municipality.
        /// </summary>
        public string city { get; set; }

        /// <summary>
        /// Zip code.
        /// </summary>
        public string zip { get; set; }

        /// <summary>
        /// State.
        /// </summary>
        public string state { get; set; }

        /// <summary>
        /// Country code (Alpha-2).
        /// </summary>
        public string country { get; set; }
    }
}

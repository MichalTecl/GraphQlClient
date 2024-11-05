using System;
using System.Collections.Generic;
using System.Text;

namespace BwApiClient.Model.Data
{
    public class Address
    {
        /// <summary>
        /// Internal address ID - if address is one of predefined customer's addresses.
        /// Allows reuse of one address multiple times. The same address may exist under 
        /// different IDs, either by different customers registering the same address as 
        /// 'their own' or by a single customer creating duplicates.
        /// </summary>
        public string id { get; set; }

        /// <summary>
        /// Company's name including legal form.
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
        /// Country.
        /// </summary>
        public string country { get; set; }

        /// <summary>
        /// Contact e-mail.
        /// </summary>
        public string email { get; set; }

        /// <summary>
        /// Contact phone.
        /// </summary>
        public string phone { get; set; }
    }


}

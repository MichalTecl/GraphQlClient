using System;
using System.Collections.Generic;
using System.Text;

namespace BwApiClient.Model.Data
{
    public class Person
    {
        /// <summary>
        /// Internal person ID.
        /// </summary>
        public string id { get; set; }

        /// <summary>
        /// Person's name.
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// Person's surname.
        /// </summary>
        public string surname { get; set; }

        /// <summary>
        /// Contact phone.
        /// </summary>
        public string phone { get; set; }

        /// <summary>
        /// E-mail address.
        /// </summary>
        public string email { get; set; }

        /// <summary>
        /// Set of predefined addresses (for repeated usage).
        /// </summary>
        public List<Address> registered_address { get; set; }
    }

}

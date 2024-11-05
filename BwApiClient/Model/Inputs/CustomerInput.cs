using System;
using System.Collections.Generic;
using System.Text;

namespace BwApiClient.Model.Inputs
{
    public class CustomerInput
    {
        /// <summary>
        /// Person's name.
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// Person's surname.
        /// </summary>
        public string surname { get; set; }

        /// <summary>
        /// E-mail address.
        /// </summary>
        public string email { get; set; }

        /// <summary>
        /// Contact phone.
        /// </summary>
        public string phone { get; set; }

        /// <summary>
        /// Legal registration ID.
        /// </summary>
        public string company_id { get; set; }

        /// <summary>
        /// Company's name, including legal form.
        /// </summary>
        public string company_name { get; set; }

        /// <summary>
        /// VAT ID or tax identification number (TIN).
        /// </summary>
        public string vat_id { get; set; }

        /// <summary>
        /// Secondary VAT ID or tax identification number (TIN).
        /// </summary>
        public string vat_id2 { get; set; }
    }
}
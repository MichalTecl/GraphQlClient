using System;
using System.Collections.Generic;
using System.Text;

namespace BwApiClient.Model.Data
{
    public class InvoicingCompany
    {
        /// <summary>
        /// Internal company ID.
        /// </summary>
        public string id { get; set; }

        /// <summary>
        /// Legal registration ID.
        /// </summary>
        public string company_id { get; set; }

        /// <summary>
        /// Company's name, including legal form.
        /// </summary>
        public string company_name { get; set; }

        /// <summary>
        /// Legal registration address.
        /// </summary>
        public Address address { get; set; } // Assuming Address is a predefined type

        /// <summary>
        /// Company e-mail.
        /// </summary>
        public string email { get; set; }

        /// <summary>
        /// Contact phone.
        /// </summary>
        public string phone { get; set; }

        /// <summary>
        /// VAT ID or tax identification number (TIN).
        /// Some countries use one TIN for both regular tax and VAT identification.
        /// </summary>
        public string vat_id { get; set; }

        /// <summary>
        /// VAT ID or tax identification number (TIN) #2.
        /// Some countries use a specific VAT ID number.
        /// </summary>
        public string vat_id2 { get; set; }
    }

}

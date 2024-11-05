using System;
using System.Collections.Generic;
using System.Text;

namespace BwApiClient.Model.Data
{
    public class LanguageVersion
    {
        /// <summary>
        /// Internal ID.
        /// </summary>
        public string id { get; set; }

        /// <summary>
        /// 2-letter ISO code (country code).
        /// </summary>
        public string code { get; set; }

        /// <summary>
        /// Country name.
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// Default currency used for this language version/country.
        /// </summary>
        public Currency currency { get; set; }

        /// <summary>
        /// Time zone.
        /// </summary>
        public string timezone { get; set; }

        /// <summary>
        /// Time format.
        /// </summary>
        public string time_format { get; set; }

        /// <summary>
        /// Date format.
        /// </summary>
        public string date_format { get; set; }

        /// <summary>
        /// Is visible on e-shop.
        /// </summary>
        public bool visible { get; set; }

        /// <summary>
        /// 2-letter ISO code of the language for system components, as supported by BizWebs platform.
        /// </summary>
        public string system_lang { get; set; }

        /// <summary>
        /// Company assigned to invoice orders from this language version.
        /// </summary>
        public InvoicingCompany company { get; set; }
    }

}

using System;
using System.Collections.Generic;
using System.Text;

namespace BwApiClient.Model.Data
{
    public class ProductList
    {
        /// <summary>
        /// List of products.
        /// </summary>
        public List<Product> data { get; set; }

        /// <summary>
        /// Pagination information.
        /// </summary>
        public PageInfo pageInfo { get; set; }
    }

}

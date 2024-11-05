using System;
using System.Collections.Generic;
using System.Text;

namespace BwApiClient.Model.Data
{
    public class Category
    {
        /// <summary>
        /// System's internal ID.
        /// </summary>
        public string id { get; set; }

        /// <summary>
        /// Category's main title.
        /// </summary>
        public string title { get; set; }

        /// <summary>
        /// Title for menu.
        /// </summary>
        public string menu_title { get; set; }

        /// <summary>
        /// Category's description.
        /// </summary>
        public string description { get; set; }

        /// <summary>
        /// Category language.
        /// </summary>
        public LanguageVersion language { get; set; }

        /// <summary>
        /// URL to category's webpage.
        /// </summary>
        public string link { get; set; }

        /// <summary>
        /// Indicates if the category contains used goods.
        /// </summary>
        public bool used_goods { get; set; }

        /// <summary>
        /// Indicates if the category may be part of full-text search results.
        /// </summary>
        public bool? search_indexed { get; set; }

        /// <summary>
        /// Parent category.
        /// </summary>
        public Category parent_category { get; set; }

        /// <summary>
        /// Children categories.
        /// </summary>
        public List<Category> children_categories { get; set; }

        /// <summary>
        /// Products in the category. Order of products is independent for each category.
        /// </summary>
        public ProductList products { get; set; } // Placeholder for ProductList
    }
}


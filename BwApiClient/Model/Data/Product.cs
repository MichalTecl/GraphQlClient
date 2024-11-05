using System;
using System.Collections.Generic;
using System.Text;

namespace BwApiClient.Model.Data
{
    public class ProductRef
    {
        /// <summary>
        /// Product internal ID.
        /// </summary>
        public string id { get; set; }

        /// <summary>
        /// Main product title.
        /// </summary>
        public string title { get; set; }

        /// <summary>
        /// Link to the e-shop product.
        /// </summary>
        public string link { get; set; }

        /// <summary>
        /// Tax rate for currency price.
        /// </summary>
        public float tax_rate { get; set; }
    }

    public class Product : ProductRef
    {        
        /// <summary>
        /// Producer.
        /// </summary>
        public Producer producer { get; set; }

        /// <summary>
        /// Main product image.
        /// </summary>
        public string image { get; set; }
               

        /// <summary>
        /// Short description (plain text).
        /// </summary>
        public string shortDescription { get; set; }

        /// <summary>
        /// Long description (HTML format allowed).
        /// </summary>
        public string longDescription { get; set; }

        /// <summary>
        /// Main product category.
        /// </summary>
        public Category main_category { get; set; }

        /// <summary>
        /// Attribute category.
        /// </summary>
        public Category attribute_category { get; set; }

        /// <summary>
        /// Assigned categories.
        /// </summary>
        public List<Category> assigned_categories { get; set; }

        /// <summary>
        /// Product EAN.
        /// </summary>
        public string ean { get; set; }

        /// <summary>
        /// Display the product in the store to customers.
        /// </summary>
        public bool active { get; set; }

        /// <summary>
        /// Product identifier from supplier or data source.
        /// </summary>
        public string import_code { get; set; }

        /// <summary>
        /// Product net price (excluding tax).
        /// </summary>
        public Price price { get; set; }

        /// <summary>
        /// Final price of the product, including tax, discounts, and other price components.
        /// </summary>
        public Price final_price { get; set; }

        /// <summary>
        /// Warehouse items.
        /// </summary>
        public List<WarehouseItem> warehouse_items { get; set; }

        /// <summary>
        /// Attributes.
        /// </summary>
        public List<Attribute> attributes { get; set; }
                
        /// <summary>
        /// Product image gallery.
        /// </summary>
        public List<string> alternative_images { get; set; }
    }

}

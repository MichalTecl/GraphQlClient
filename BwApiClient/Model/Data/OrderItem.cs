using MTecl.GraphQlClient.ObjectMapping.Descriptors;
using MTecl.GraphQlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace BwApiClient.Model.Data
{
    public class OrderItem
    {
        /// <summary>
        /// Internal order item ID.
        /// </summary>
        public string id { get; set; }

        /// <summary>
        /// Reference to product. Note that the source product may not exist anymore.
        /// </summary>        
        public ProductRef product { get; set; }

        /// <summary>
        /// Item's title. This label might have been changed by an admin, so it may not be identical to the source product's name.
        /// </summary>
        public string item_label { get; set; }

        /// <summary>
        /// Import code (identifier from supplier's feed or data source).
        /// </summary>
        public string import_code { get; set; }

        /// <summary>
        /// Product's EAN.
        /// </summary>
        public string ean { get; set; }

        /// <summary>
        /// Warehouse number.
        /// </summary>
        public string warehouse_number { get; set; }

        /// <summary>
        /// Count of ordered pieces.
        /// </summary>
        public int quantity { get; set; }

        /// <summary>
        /// Net price per unit (excluding tax).
        /// </summary>
        [Gql(InclusionMode = FieldInclusionMode.Include)]
        public Price price { get; set; }

        /// <summary>
        /// Net sum per row (excluding tax).
        /// </summary>
        [Gql(InclusionMode = FieldInclusionMode.Include)]
        public Price sum { get; set; }

        /// <summary>
        /// Recycle fee per unit (1 pc).
        /// </summary>
        [Gql(InclusionMode = FieldInclusionMode.Include)]
        public Price recycle_fee { get; set; }

        /// <summary>
        /// Weight per unit (1 pc).
        /// </summary>
        [Gql(InclusionMode = FieldInclusionMode.Include)]
        public Weight weight { get; set; }

        /// <summary>
        /// Tax ratio for the given order item.
        /// </summary>
        public float tax_rate { get; set; }

        /// <summary>
        /// Sum per row (including tax).
        /// </summary>
        [Gql(InclusionMode = FieldInclusionMode.Include)]
        public Price sum_with_tax { get; set; }
    }

}

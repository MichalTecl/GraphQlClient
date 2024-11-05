using MTecl.GraphQlClient.ObjectMapping.Descriptors;
using MTecl.GraphQlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace BwApiClient.Model.Data
{
    public class PaginatedList<T>
    {
        /// <summary>
        /// List of items
        /// </summary>
        [Gql(InclusionMode = FieldInclusionMode.Include)]
        public List<T> data { get; set; }

        /// <summary>
        /// Pagination information.
        /// </summary>
        [Gql(InclusionMode = FieldInclusionMode.Include)]
        public PageInfo pageInfo { get; set; }
    }
}

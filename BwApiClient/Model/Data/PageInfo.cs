using System;
using System.Collections.Generic;
using System.Text;

namespace BwApiClient.Model.Data
{
    public class PageInfo
    {
        /// <summary>
        /// Listing has a next page.
        /// </summary>
        public bool hasNextPage { get; set; }

        /// <summary>
        /// Listing has a previous page.
        /// </summary>
        public bool hasPreviousPage { get; set; }

        /// <summary>
        /// Index pointer (ordinal position/offset) of the first record on the next page of results.
        /// </summary>
        public int? nextCursor { get; set; }

        /// <summary>
        /// Index pointer (ordinal position/offset) of the first record on the previous page of results.
        /// </summary>
        public int? previousCursor { get; set; }

        /// <summary>
        /// Current page index.
        /// </summary>
        public int pageIndex { get; set; }

        /// <summary>
        /// Total number of pages.
        /// </summary>
        public int totalPages { get; set; }
    }

}

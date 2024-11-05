using System;
using System.Collections.Generic;
using System.Text;

namespace BwApiClient.Model.Data
{
    public class Receipt
    {
        /// <summary>
        /// Receipt internal ID.
        /// </summary>
        public string id { get; set; }

        /// <summary>
        /// Receipt number.
        /// </summary>
        public string receipt_num { get; set; }

        /// <summary>
        /// Payment type identifier.
        /// </summary>
        public string payment_type { get; set; }

        /// <summary>
        /// Date the receipt was created.
        /// </summary>
        public DateTime? created { get; set; }

        /// <summary>
        /// Date of payment.
        /// </summary>
        public DateTime? pay_date { get; set; } // Assuming Date is represented as DateTime

        /// <summary>
        /// Total amount of the receipt.
        /// </summary>
        public Price sum { get; set; }

        /// <summary>
        /// List of items.
        /// </summary>
        public List<ReceiptItem> items { get; set; }

        /// <summary>
        /// Order reference.
        /// </summary>
        public Order order { get; set; } // Placeholder for Order
    }

}

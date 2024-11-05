using BwApiClient.Model.Data;
using BwApiClient.Model.Enums;
using BwApiClient.Model.Inputs;
using MTecl.GraphQlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace BwApiClient
{
    public interface IMutation
    {
        /// <summary>
        /// Creates a preinvoice (payment request) for an order.
        /// </summary>
        /// <param name="order_num">Order's evidence number for which to create preinvoice.</param>
        /// <param name="send_notification">Possibility to tweak default system behavior or send multiple notifications.</param>
        [Gql("preinvoiceOrder")]
        Preinvoice PreinvoiceOrder(string order_num, List<NotificationRequest> send_notification);

        /// <summary>
        /// Creates final invoice for preinvoice.
        /// </summary>
        /// <param name="preinvoice_num">Preinvoice (payment request's) evidence number.</param>
        /// <param name="send_notification">Possibility to tweak default system behavior or send multiple notifications.</param>
        [Gql("finalizeInvoice")]
        Invoice FinalizeInvoice(string preinvoice_num, List<NotificationRequest> send_notification);

        /// <summary>
        /// Sets order status.
        /// </summary>
        /// <param name="order_num">Order's evidence number for which to change the status.</param>
        /// <param name="status_id">System's internal status ID.</param>
        /// <param name="send_notification">Possibility to tweak default system behavior or send multiple notifications.</param>
        [Gql("changeOrderStatus")]
        Order ChangeOrderStatus(string order_num, int status_id, List<NotificationRequest> send_notification);

        /// <summary>
        /// Change status and quantity of a warehouse item.
        /// </summary>
        /// <param name="warehouse_item">Warehouse item input.</param>
        /// <param name="send_notification">Possibility to tweak default system behavior or send multiple notifications.</param>
        [Gql("updateWarehouseItem")]
        WarehouseItem UpdateWarehouseItem(WarehouseItemInput warehouse_item, List<NotificationRequest> send_notification);

        /// <summary>
        /// Create a new order.
        /// </summary>
        /// <param name="data">Order input data.</param>
        [Gql("newOrder")]
        Order NewOrder(OrderInput data);

        /// <summary>
        /// Payment of the invoice/preinvoice and creation of a receipt.
        /// </summary>
        /// <param name="payment_reference">Payment reference identifier.</param>
        /// <param name="reference_type">Reference type e.g. preinvoice number, invoice number or variable symbol.</param>
        /// <param name="payment_type">Payment type identifier.</param>
        /// <param name="amount">Total amount of payment.</param>
        /// <param name="pay_date">Payment date.</param>
        [Gql("recordPayment")]
        Receipt RecordPayment(
            string payment_reference,
            PaymentReferenceType reference_type,
            PaymentType payment_type,
            PriceInput amount,
            DateTime pay_date);
    }

}

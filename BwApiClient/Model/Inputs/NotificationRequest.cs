using BwApiClient.Model.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace BwApiClient.Model.Inputs
{
    public class NotificationRequest
    {
        /// <summary>
        /// Conditions for sending the notification.
        /// </summary>
        public List<NotificationCondition> @if { get; set; }

        /// <summary>
        /// Notification type.
        /// </summary>
        public string type { get; set; }

        /// <summary>
        /// Extra message details.
        /// </summary>
        public MessageExtra extra { get; set; }
    }

}

using System;
using System.Collections.Generic;
using System.Text;

namespace BwApiClient.Model.Enums
{
    public enum NotificationCondition
    {
        /// <summary>
        /// Sends notification only if it is the first time ever the entity was created.
        /// </summary>
        and_FIRST_CREATE,

        /// <summary>
        /// Sends notification only if the entity was re-created.
        /// </summary>
        and_RE_CREATE,

        /// <summary>
        /// Sends notification only if the entity was just created or re-created.
        /// </summary>
        and_CREATE_OR_EDIT,

        /// <summary>
        /// Sends notification only if it has never been sent before.
        /// </summary>
        and_EMAIL_FIRST_SEND,

        /// <summary>
        /// Sends notification only if there has been some change to underlying data.
        /// </summary>
        and_CHANGED,

        /// <summary>
        /// Notification is sent or not based on current system settings.
        /// </summary>
        and_SYSTEM_DEFAULT,

        /// <summary>
        /// Force sending of notification.
        /// </summary>
        or_ALWAYS,

        /// <summary>
        /// Force silent operation - no notification is sent.
        /// </summary>
        NONE
    }

}

using System;

namespace Clock
{
    /// <inheritdoc />
    /// <summary>
    /// Information about the occurrence of a certain time interval.
    /// </summary>
    public class ClockEventArgs : EventArgs
    {
        /// <inheritdoc />
        /// <param name="notificationData">notification data</param>
        /// <param name="notificationInterval">time interval</param>
        public ClockEventArgs(DateTime notificationData, int notificationInterval)
        {
            this.NotificationData = notificationData;
            this.NotificationInterval = notificationInterval;
        }

        /// <summary>
        /// Notification data.
        /// </summary>
        public DateTime NotificationData { get; }

        /// <summary>
        /// Time interval.
        /// </summary>
        public int NotificationInterval { get; }
    }
}

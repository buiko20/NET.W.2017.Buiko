using System;
using System.Threading;

namespace Clock
{
    public class Clock
    {
        /// <summary>
        /// Event occurring after a certain period of time.
        /// </summary>
        public event EventHandler<ClockEventArgs> Notification = delegate { };

        /// <summary>
        /// Notifies all subscribers.
        /// </summary>
        /// <param name="milliseconds">period of time through which to send a notification</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="milliseconds"/> less than zero.</exception>
        public void Notify(int milliseconds)
        {
            if (milliseconds < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(milliseconds), $"{nameof(milliseconds)} must be greater than or equal to zero");
            }

            Thread.Sleep(milliseconds);
            OnNotification(this, new ClockEventArgs(DateTime.Now, milliseconds));
        }

        protected virtual void OnNotification(object sender, ClockEventArgs arg)
        {
            EventHandler<ClockEventArgs> temp = Notification;

            temp?.Invoke(sender, arg);
        }
    }
}

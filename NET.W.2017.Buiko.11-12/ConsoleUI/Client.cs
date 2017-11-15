using System;
using Clock;

namespace ConsoleUI
{
    internal class Client
    {
        public void Subscribe(Clock.Clock clock) =>
            clock.Notification += ShowNotification;

        public void Unsubscribe(Clock.Clock clock) =>
            clock.Notification -= ShowNotification;

        private static void ShowNotification(object sender, ClockEventArgs e) =>
            Console.WriteLine($"Notification from: {sender.GetType().FullName}. Args: {e.NotificationData} | {e.NotificationInterval}");
    }
}

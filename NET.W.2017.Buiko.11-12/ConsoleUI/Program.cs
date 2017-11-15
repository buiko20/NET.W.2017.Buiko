using System;

namespace ConsoleUI
{
    internal class Program
    {
        private static void Main()
        {
            var clock = new Clock.Clock();
            clock.Notify(1000);

            var client1 = new Client();
            var client2 = new Client();

            client1.Subscribe(clock);
            client2.Subscribe(clock);

            clock.Notify(2000);
            Console.WriteLine("--------------------------");

            client2.Unsubscribe(clock);

            clock.Notify(2000);


            Console.ReadLine();
        }

    }
}

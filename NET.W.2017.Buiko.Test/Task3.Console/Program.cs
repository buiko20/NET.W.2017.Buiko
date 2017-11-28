using Task3.Solution;

namespace Task3.Console
{
    internal class Program
    {
        private static void Main()
        {
            var bank = new Bank("My bank");
            var broker = new Broker("My broker");
            var stock = new Stock();

            stock.StocksInfo += bank.Update;
            stock.StocksInfo += broker.Update;

            stock.Market();

            System.Console.ReadLine();
        }
    }
}

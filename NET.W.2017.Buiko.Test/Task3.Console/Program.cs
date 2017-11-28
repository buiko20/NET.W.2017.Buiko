using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task3.Solution;

namespace Task3.Console
{
    class Program
    {
        static void Main(string[] args)
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

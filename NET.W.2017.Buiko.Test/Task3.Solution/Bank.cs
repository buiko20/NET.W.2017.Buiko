using System;

namespace Task3.Solution
{
    public class Bank
    {
        public Bank(string name)
        {
            this.Name = name;
        }

        public string Name { get; set; }

        public void Update(object sender, StockEventArgs info)
        {
            if (info.Euro > 40)
                Console.WriteLine("Банк {0} продает евро;  Курс евро: {1}", this.Name, info.Euro);
            else
                Console.WriteLine("Банк {0} покупает евро;  Курс евро: {1}", this.Name, info.Euro);
        }
    }
}

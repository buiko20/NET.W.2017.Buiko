using System;

namespace Task3.Solution
{
    public class Broker
    {
        public Broker(string name)
        {
            this.Name = name;
        }

        public string Name { get; set; }

        public void Update(object sender, StockEventArgs info)
        {
            if (info.Usd > 30)
                Console.WriteLine("Брокер {0} продает доллары;  Курс доллара: {1}", this.Name, info.Usd);
            else
                Console.WriteLine("Брокер {0} покупает доллары;  Курс доллара: {1}", this.Name, info.Usd);
        }
    }
}

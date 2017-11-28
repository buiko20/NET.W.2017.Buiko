using System;

namespace Task3.Solution
{
    public class Stock
    {
        public event EventHandler<StockEventArgs> StocksInfo = delegate { };

        public void Market()
        {
            var rnd = new Random();
            OnEvent(this, new StockEventArgs(rnd.Next(20, 40), rnd.Next(30, 50)));
        }

        protected virtual void OnEvent(Stock stock, StockEventArgs arg)
        {
            EventHandler<StockEventArgs> temp = StocksInfo;
            temp?.Invoke(this, arg);
        }
    }
}

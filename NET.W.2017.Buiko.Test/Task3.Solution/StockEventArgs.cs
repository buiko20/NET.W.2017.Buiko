using System;

namespace Task3.Solution
{
    public class StockEventArgs : EventArgs
    {
        public StockEventArgs(int usd, int euro)
        {
            this.Usd = usd;
            this.Euro = euro;
        }

        public int Usd { get; }

        public int Euro { get; }
    }
}

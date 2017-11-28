using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task3.Solution
{
    public class StockEventArgs : EventArgs
    {
        public StockEventArgs(int usd, int euro)
        {
            USD = usd;
            Euro = euro;
        }

        public int USD { get; }
        public int Euro { get; }
    }
}

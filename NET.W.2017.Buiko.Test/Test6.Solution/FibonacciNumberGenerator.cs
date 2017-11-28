using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Test6.Solution
{
    public class FibonacciNumberGenerator : IFormulaCalculator<int>
    {
        public int CalculateNthNumber(int current, int previous) 
        {
            return current + previous;
        }
    }
}

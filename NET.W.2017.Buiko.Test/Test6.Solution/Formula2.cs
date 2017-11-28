using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test6.Solution
{
    public class Formula2 : IFormulaCalculator<int>
    {
        public int CalculateNthNumber(int current, int previous)
        {
            return 6 * current - 8 * previous;
        }
    }
}

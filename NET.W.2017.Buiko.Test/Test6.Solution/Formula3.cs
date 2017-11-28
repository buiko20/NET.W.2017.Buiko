using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test6.Solution
{
    public class Formula3 : IFormulaCalculator<double>
    {
        public double CalculateNthNumber(double current, double previous)
        {
            return current + previous / current;
        }
    }
}

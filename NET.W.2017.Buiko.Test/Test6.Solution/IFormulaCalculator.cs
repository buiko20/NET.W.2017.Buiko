using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test6.Solution
{
    public interface IFormulaCalculator<T>
    {
        T CalculateNthNumber(T current, T previous);
    }
}

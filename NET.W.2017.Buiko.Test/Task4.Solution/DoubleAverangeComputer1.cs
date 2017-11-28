using System.Collections.Generic;
using System.Linq;

namespace Task4.Solution
{
    public class DoubleAverangeComputer1 : IDoubleAverangeComputer
    {
        public double ComputeAverange(IList<double> values) =>
            values.Sum() / values.Count;
    }
}

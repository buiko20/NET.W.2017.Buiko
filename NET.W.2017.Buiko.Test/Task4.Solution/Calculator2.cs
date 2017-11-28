using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task4.Solution
{
    public class Calculator2
    {
        public double CalculateAverage(List<double> values, Func<IList<double>, double> averagingMethod)
        {
            if (ReferenceEquals(values, null))
                throw new ArgumentNullException(nameof(values));

            if (ReferenceEquals(averagingMethod, null))
                throw new ArgumentNullException(nameof(averagingMethod));

            return averagingMethod(values);
        }
    }
}

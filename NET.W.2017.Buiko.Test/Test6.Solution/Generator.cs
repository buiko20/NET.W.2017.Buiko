using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test6.Solution
{
    public static class Generator
    {
        public static IEnumerable<T> GenerateSequence<T>(int count, T a, T b, Func<T, T, T> calculator)
        {
            if (count < 0)
                throw new ArgumentException($"{nameof(count)} must be greater than 0.", nameof(count));

            if (ReferenceEquals(a, null))
                throw new ArgumentNullException(nameof(a));

            if (ReferenceEquals(b, null))
                throw new ArgumentNullException(nameof(b));

            if (ReferenceEquals(calculator, null))
                throw new ArgumentNullException(nameof(calculator));

            return Generate(count, a, b, calculator);
        }

        public static IEnumerable<T> GenerateSequence<T>(int count, T a, T b, IFormulaCalculator<T> calculator)
        {
            if (ReferenceEquals(calculator, null))
                throw new ArgumentNullException(nameof(calculator));

            return Generate(count, a, b, calculator.CalculateNthNumber);
        }

        private static IEnumerable<T> Generate<T>(int count, T a, T b, Func<T, T, T> calculator)
        {
            if (count == 0) yield break;

            if (count >= 1) yield return a;

            if (count >= 2) yield return b;

            for (int i = 3; i < count; i++)
            {
                var temp = b;
                b = calculator(b, a);
                yield return b;
                a = temp;
            }
        }

        private static IEnumerable<T> Generate<T>(int count, T a, T b, IFormulaCalculator<T> calculator) =>
            Generate(count, a, b, calculator.CalculateNthNumber);
    }
}

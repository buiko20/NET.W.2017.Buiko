using System;
using System.Collections.Generic;

namespace Algorithm
{
    public static class NumberAlgorithm
    {
        #region private fields

        private const int FirstFibonacciNumber = 0;
        private const int SecondFibonacciNumber = 1;

        #endregion // !private fields.

        #region public

        /// <summary>
        /// Returns an array of Fibonacci numbers.
        /// </summary>
        /// <param name="length">count of Fibonacci numbers or 
        /// number of the last Fibonacci number which is the same.</param>
        /// <returns>Array of Fibonacci numbers.</returns>
        /// <exception cref="ArgumentException">Thrown when <paramref name="length"/> &lt; 0.</exception>
        public static IEnumerable<int> CalculateFibonacciNumbers(int length)
        {
            if (length < 0)
            {
                throw new ArgumentException(nameof(length) + " must be >= 0.", nameof(length));
            }

            switch (length)
            {
                case 0:
                    return new int[] { };
                case 1:
                    return new[] { FirstFibonacciNumber };
            }

            int[] result = new int[length];
            result[0] = FirstFibonacciNumber;
            result[1] = SecondFibonacciNumber;
            if (length > 2)
            {
                for (int i = 2; i < length; i++)
                {
                    result[i] = result[i - 1] + result[i - 2];
                }
            }

            return result;
        }

        /// <summary>
        /// Returns Fibonacci numbers.
        /// </summary>
        /// <param name="length">count of Fibonacci numbers or 
        /// number of the last Fibonacci number which is the same.</param>
        /// <returns>Fibonacci numbers.</returns>
        /// <exception cref="ArgumentException">Thrown when <paramref name="length"/> &lt; 0.</exception>
        public static IEnumerable<int> FibonacciNumbers(int length)
        {
            if (length < 0)
            {
                throw new ArgumentException(nameof(length) + " must be >= 0.", nameof(length));
            }

            switch (length)
            {
                case 0:
                    yield break;
                case 1:
                    yield return FirstFibonacciNumber;
                    yield break;
            }

            yield return FirstFibonacciNumber;
            yield return SecondFibonacciNumber;

            if (length == 2)
            {
                yield break;
            }

            int number1 = FirstFibonacciNumber, number2 = SecondFibonacciNumber;
            for (int i = 2; i < length; i++)
            {
                int result = number1 + number2;
                number1 = number2;
                number2 = result;
                yield return result;
            }
        }

        #endregion // !public.
    }
}

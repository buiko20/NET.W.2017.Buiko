using System;
using System.Collections.Generic;
using System.Numerics;

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
        public static IEnumerable<BigInteger> CalculateFibonacciNumbers(int length)
        {
            if (length < 0)
            {
                throw new ArgumentException(nameof(length) + " must be greater than or equal to", nameof(length));
            }

            var result = new BigInteger[length];

            int i = 0;
            foreach (var fibonacciNumber in FibonacciNumbers(length))
            {
                result[i++] = fibonacciNumber;
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
        public static IEnumerable<BigInteger> FibonacciNumbers(int length)
        {
            if (length < 0)
            {
                throw new ArgumentException(nameof(length) + " must be greater than or equal to 0", nameof(length));
            }

            return GetFibonacciNumbers(length);
        }

        #endregion // !public.

        #region private

        private static IEnumerable<BigInteger> GetFibonacciNumbers(int length)
        {
            BigInteger previous = FirstFibonacciNumber;
            BigInteger current = SecondFibonacciNumber;
            for (int i = 0; i < length; i++)
            {
                yield return previous;
                var temp = previous;
                previous = current;
                current += temp;
            }
        }

        #endregion // !private.
    }
}

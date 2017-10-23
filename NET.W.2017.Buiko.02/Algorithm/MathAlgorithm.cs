using System;
using System.Collections;
using System.Diagnostics;

namespace Algorithm
{
    public class MathAlgorithm
    {
        private const int RightBorder = 31;
        private const int LeftBorder = 0;

        /// <summary>
        /// Inserts bits of the 2nd number starting from position startPosition to endPosition
        /// to the 1st number.
        /// </summary>
        /// <param name="number1">Int32 nubmer1</param>
        /// <param name="number2">Int32 number2</param>
        /// <param name="startPosition">Position from which the bits are taken</param>
        /// <param name="endPosition">Position to which bits are taken</param>
        /// <exception cref="ArgumentException">
        /// Thrown when startPosition greater than endPosition or
        /// (startPosition &lt; 0) || (startPosition &gt; 32) || (endPosition &lt; 0) || (endPosition &gt; 32).
        /// </exception>
        /// <returns>Int32 nubmer</returns>
        public static int BitInsert(int number1, int number2, int startPosition, int endPosition)
        {
            if (startPosition > endPosition)
                throw new ArgumentException($"{nameof(startPosition)} must be less than {nameof(endPosition)}");

            if ((startPosition < LeftBorder) || (startPosition > RightBorder) || (endPosition < LeftBorder) || (endPosition > RightBorder))
                throw new ArgumentException($"{nameof(startPosition)} and {nameof(endPosition)} must be greater than 0 and less than 32");

            int mask = ((2 << (endPosition - startPosition)) - 1) << startPosition;
            return (number1 & ~mask) | ((number2 << startPosition) & mask);
        }

        /// <summary>
        /// Returns the nearest largest integer consisting of digits of the original number.
        /// </summary>
        /// <param name="number">Source number</param>
        /// <returns>
        /// Nearest largest integer consisting of digits of the original number.
        /// If such a number does not exist, -1 returns.
        /// </returns>
        /// <exception cref="ArgumentException">Thrown if number is less than 0</exception>
        public static int FindNextBiggerNumber(int number)
        {
            if (number < 0)
                throw new ArgumentException($"{nameof(number)} must be greater than 0", nameof(number));

            char[] numberCharArray = number.ToString().ToCharArray();

            if (IsDescendingOrder(numberCharArray)) return -1;

            for (int i = numberCharArray.Length - 1; i > 0; i--)
                if (numberCharArray[i] > numberCharArray[i - 1])
                {
                    for (int j = numberCharArray.Length - 1; j >= i; j--)
                        if (numberCharArray[j] > numberCharArray[i - 1])
                        {
                            char temp = numberCharArray[j];
                            numberCharArray[j] = numberCharArray[i - 1];
                            numberCharArray[i - 1] = temp;
                            break;
                        }

                    Array.Reverse(numberCharArray, i, numberCharArray.Length - i);
                    break;
                }

            return int.Parse(new string(numberCharArray));
        }

        /// <summary>
        /// Returns the nearest largest integer consisting of digits of the original number.
        /// </summary>
        /// <param name="number">Source number</param>
        /// <param name="operationTime">Time spent on the operation</param>
        /// <returns>
        /// Nearest largest integer consisting of digits of the original number.
        /// If such a number does not exist, -1 returns.
        /// </returns>
        /// <exception cref="ArgumentException">Thrown if number is less than 0</exception>
        public static int FindNextBiggerNumber(int number, out TimeSpan operationTime)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            int result = FindNextBiggerNumber(number);

            watch.Stop();
            operationTime = watch.Elapsed;

            return result;
        }

        /// <summary>
        /// Searches for numbers containing a given digit.
        /// </summary>
        /// <param name="digit">target digit</param>
        /// <param name="numbers">source numbers</param>
        /// <returns>Numbers containing a given digit or null if there are no such numbers.</returns>
        /// <exception cref="ArgumentException">Thrown when digit &lt; 0 || digit &gt; 9</exception>
        /// <exception cref="NullReferenceException">Thrown when numbers == null</exception>
        public static int[] FilterDigit(int digit, params int[] numbers)
        {
            if ((digit < 0) || (digit > 9))
                throw new ArgumentException($"{nameof(digit)} must be from 0 to 9", nameof(digit));

            if (numbers == null)
                throw new ArgumentException(nameof(numbers));

            if (numbers.Length == 0)
                return null;

            int[] result = new int[0];
            string digitStr = digit.ToString();
            for (int i = 0; i < numbers.Length; i++)
            {
                string number = numbers[i].ToString();
                if (number.Contains(digitStr))
                {
                    Array.Resize(ref result, result.Length + 1);
                    result[result.Length - 1] = numbers[i];
                }
            }

            return result;
        }

        /// <summary>
        /// Newton's algorithm for getting n-th root of number.
        /// </summary>
        /// <param name="number">source number</param>
        /// <param name="root">degree of root</param>
        /// <param name="eps">root computation accuracy</param>
        /// <returns>N'th root of number</returns>
        /// <exception cref="ArgumentException">Thrown when when one of the arguments is less than 0</exception>
        public static double FindNthRoot(double number, int root, double eps = 0.001)
        {
            if (root <= 0)
                throw new ArgumentException($"{nameof(root)} must be greater than 0", nameof(root));

            if ((number <= 0) && (root % 2 == 0))
                throw new ArgumentException($"{nameof(number)} must be greater than or equal to 0", nameof(number));

            if (eps <= 0)
                throw new ArgumentException($"{nameof(eps)} must be greater than 0", nameof(eps));

            double x0 = number / root;
            double x1 = ComputeNextNumber(number, x0, root);
            double delta = eps * 2;

            while (delta > eps)
            {
                x0 = x1;
                x1 = ComputeNextNumber(number, x0, root);
                delta = Math.Abs(x0 - x1);
            }

            return x1;
        }

        private static bool IsDescendingOrder(char[] array)
        {
            bool result = true;
            for (int i = array.Length - 1; i > 0; i--)
                if (array[i] > array[i - 1])
                {
                    result = false;
                    break;
                }

            return result;
        }

        private static double ComputeNextNumber(double number, double x0, int root)
        {
            return (1.0 / root) * ((root - 1) * x0 + number / Math.Pow(x0, root - 1));
        }
    }
}

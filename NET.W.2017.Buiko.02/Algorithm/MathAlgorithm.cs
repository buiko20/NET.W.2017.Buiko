using System;
using System.Diagnostics;
using System.Linq;

namespace Algorithm
{
    public class MathAlgorithm
    {
        #region private fields

        private const int RightBorder = 31;
        private const int LeftBorder = 0;

        #endregion // !private fields.

        #region public 

        /// <summary>
        /// Inserts bits of the <paramref name="number2"/> starting from <paramref name="startPosition"/> 
        /// to <paramref name="endPosition"/> to the <paramref name="number1"/>.
        /// </summary>
        /// <param name="number1">first number</param>
        /// <param name="number2">second number</param>
        /// <param name="startPosition">Position from which the bits are taken</param>
        /// <param name="endPosition">Position to which bits are taken</param>
        /// <exception cref="ArgumentException">
        /// Thrown when <paramref name="startPosition"/> greater than <paramref name="endPosition"/> or
        /// (<paramref name="startPosition"/> &lt; 0) || (<paramref name="startPosition"/> &gt; 32) 
        /// || (<paramref name="endPosition"/> &lt; 0) || (<paramref name="endPosition"/> &gt; 32).
        /// </exception>
        /// <returns>Number.</returns>
        public static int BitInsert(int number1, int number2, int startPosition, int endPosition)
        {
            if (startPosition > endPosition)
            {
                throw new ArgumentException($"{nameof(startPosition)} must be less than {nameof(endPosition)}");
            }

            if ((startPosition < LeftBorder) || (startPosition > RightBorder) || (endPosition < LeftBorder) ||
                (endPosition > RightBorder))
            {
                throw new ArgumentException($"{nameof(startPosition)} and {nameof(endPosition)} must be greater than 0 and less than 32");
            }

            int mask = ((2 << (endPosition - startPosition)) - 1) << startPosition;
            return (number1 & ~mask) | ((number2 << startPosition) & mask);
        }

        /// <summary>
        /// Returns the nearest largest integer consisting of digits of the <paramref name="number"/>.
        /// </summary>
        /// <param name="number">source number</param>
        /// <returns>
        /// Nearest largest integer consisting of digits of the <paramref name="number"/>.
        /// If such a number does not exist, -1 returns.
        /// </returns>
        /// <exception cref="ArgumentException">Thrown if <paramref name="number"/> is less than 0.</exception>
        public static int FindNextBiggerNumber(int number)
        {
            if (number < 0)
            {
                throw new ArgumentException($"{nameof(number)} must be greater than 0", nameof(number));
            }

            var numberCharArray = number.ToString().ToCharArray();

            if (IsDescendingOrder(numberCharArray))
            {
                return -1;
            }

            for (int i = numberCharArray.Length - 1; i > 0; i--)
            {
                if (numberCharArray[i] > numberCharArray[i - 1])
                {
                    for (int j = numberCharArray.Length - 1; j >= i; j--)
                    {
                        if (numberCharArray[j] > numberCharArray[i - 1])
                        {
                            var temp = numberCharArray[j];
                            numberCharArray[j] = numberCharArray[i - 1];
                            numberCharArray[i - 1] = temp;
                            break;
                        }
                    }

                    Array.Reverse(numberCharArray, i, numberCharArray.Length - i);
                    break;
                }
            }

            return int.Parse(new string(numberCharArray));
        }

        /// <summary>
        /// Returns the nearest largest integer consisting of digits of the <paramref name="number"/>.
        /// </summary>
        /// <param name="number">source number</param>
        /// <param name="operationTime">Time spent on the operation</param>
        /// <returns>
        /// Nearest largest integer consisting of digits of the <paramref name="number"/>.
        /// If such a number does not exist, -1 returns.
        /// </returns>
        /// <exception cref="ArgumentException">Thrown if <paramref name="number"/> is less than 0.</exception>
        public static int FindNextBiggerNumber(int number, out TimeSpan operationTime)
        {
            var watch = new Stopwatch();
            watch.Start();

            int result = FindNextBiggerNumber(number);

            watch.Stop();
            operationTime = watch.Elapsed;

            return result;
        }

        /// <summary>
        /// Searches for numbers corresponding to the given <paramref name="predicate"/>.
        /// </summary>
        /// <param name="numbers">source numbers</param>
        /// <param name="predicate">predicate determining the choice of an element</param>
        /// <returns>The numbers selected by the <paramref name="predicate"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="predicate"/> or <paramref name="numbers"/> is null.</exception>
        public static int[] FilterDigit(Predicate<int> predicate, params int[] numbers)
        {
            if (ReferenceEquals(predicate, null))
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            if (ReferenceEquals(numbers, null))
            {
                throw new ArgumentNullException(nameof(numbers));
            }

            if (numbers.Length == 0)
            {
                return new int[0];
            }

            return numbers.Where(predicate.Invoke).ToArray();
        }

        /// <summary>
        /// Newton's algorithm for getting Nth root of <paramref name="number"/>.
        /// </summary>
        /// <param name="number">source number</param>
        /// <param name="root">degree of root</param>
        /// <param name="eps">root computation accuracy</param>
        /// <returns>Nth root of <paramref name="number"/>.</returns>
        /// <exception cref="ArgumentException">Thrown when when one of the arguments is less than 0.</exception>
        public static double FindNthRoot(double number, int root, double eps = 0.001)
        {
            if (root <= 0)
            {
                throw new ArgumentException($"{nameof(root)} must be greater than 0", nameof(root));
            }

            if ((number <= 0) && (root % 2 == 0))
            {
                throw new ArgumentException($"{nameof(number)} must be greater than or equal to 0", nameof(number));
            }

            if (eps <= 0)
            {
                throw new ArgumentException($"{nameof(eps)} must be greater than 0", nameof(eps));
            }

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

        #endregion // !public.

        #region private

        private static bool IsDescendingOrder(char[] array)
        {
            bool result = true;
            for (int i = array.Length - 1; i > 0; i--)
            {
                if (array[i] > array[i - 1])
                {
                    result = false;
                    break;
                }
            }

            return result;
        }

        private static double ComputeNextNumber(double number, double x0, int root) =>
            (1.0 / root) * (((root - 1) * x0) + (number / Math.Pow(x0, root - 1)));

        #endregion // !private.
    }
}

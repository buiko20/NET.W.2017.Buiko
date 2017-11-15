using System;
using System.Diagnostics;

namespace Algorithm
{
    public class GcdAlrogithm
    {
        #region public

        #region EuclideanAlgorithm

        #region сalculation of the algorithm

        /// <summary>
        /// The Euclidean algorithm for finding the greatest common divisor.
        /// </summary>
        /// <param name="number1">first number</param>
        /// <param name="number2">second number</param>
        /// <returns>Greatest common divisor of <paramref name="number1"/> and <paramref name="number2"/>.</returns>
        public static int EuclideanAlgorithm(int number1, int number2)
        {
            number1 = Math.Abs(number1);
            number2 = Math.Abs(number2);

            if ((number1 == 0) && (number2 != 0))
            {
                return number2;
            }

            if ((number2 == 0) && (number1 != 0))
            {
                return number1;
            }

            while (number1 != number2)
            {
                if (number1 > number2)
                {
                    number1 -= number2;
                }
                else
                {
                    number2 -= number1;
                }
            }

            return number1;
        }

        /// <summary>
        /// The Euclidean algorithm for finding the greatest common divisor.
        /// </summary>
        /// <param name="number1">first number</param>
        /// <param name="number2">second number</param>
        /// <param name="number3">third number</param>
        /// <returns>Greatest common divisor of 3 numbers</returns>
        public static int EuclideanAlgorithm(int number1, int number2, int number3) =>
            ComputeGcd(EuclideanAlgorithm, number1, number2, number3);

        /// <summary>
        /// The Euclidean algorithm for finding the greatest common divisor.
        /// </summary>
        /// <param name="number1">first number</param>
        /// <param name="number2">second number</param>
        /// <param name="number3">third number</param>
        /// <param name="number4">fourth number</param>
        /// <returns>Greatest common divisor of 4 numbers</returns>
        public static int EuclideanAlgorithm(int number1, int number2, int number3, int number4) =>
            ComputeGcd(EuclideanAlgorithm, number1, number2, number3, number4);

        /// <summary>
        /// The Euclidean algorithm for finding the greatest common divisor.
        /// </summary>
        /// <param name="numbers">array of numbers</param>
        /// <returns>Greatest common divisor of all numbers</returns>
        public static int EuclideanAlgorithm(params int[] numbers) =>
            ComputeGcd(EuclideanAlgorithm, numbers);

        #endregion // !сalculation of the algorithm.

        #region calculating the execution time of the algorithm

        /// <summary>
        /// The Euclidean algorithm for finding the greatest common divisor.
        /// </summary>
        /// <param name="operationTime">method execution time</param>
        /// <param name="number1">first number</param>
        /// <param name="number2">second number</param>
        /// <returns>Greatest common divisor of 2 numbers</returns>
        public static int EuclideanAlgorithm(out TimeSpan operationTime, int number1, int number2) =>
            ComputeGcd(() => EuclideanAlgorithm(number1, number2), out operationTime);

        /// <summary>
        /// The Euclidean algorithm for finding the greatest common divisor.
        /// </summary>
        /// <param name="operationTime">method execution time</param>
        /// <param name="number1">first number</param>
        /// <param name="number2">second number</param>
        /// <param name="number3">third number</param>
        /// <returns>Greatest common divisor of 3 numbers</returns>
        public static int EuclideanAlgorithm(out TimeSpan operationTime, int number1, int number2, int number3) =>
            ComputeGcd(() => EuclideanAlgorithm(number1, number2, number3), out operationTime);

        /// <summary>
        /// The Euclidean algorithm for finding the greatest common divisor.
        /// </summary>
        /// <param name="operationTime">method execution time</param>
        /// <param name="number1">first number</param>
        /// <param name="number2">second number</param>
        /// <param name="number3">third number</param>
        /// <param name="number4">fourth number</param>
        /// <returns>Greatest common divisor of 4 numbers</returns>
        public static int EuclideanAlgorithm(out TimeSpan operationTime, int number1, int number2, int number3, int number4) =>
            ComputeGcd(() => EuclideanAlgorithm(number1, number2, number3, number4), out operationTime);

        /// <summary>
        /// The Euclidean algorithm for finding the greatest common divisor.
        /// </summary>
        /// <param name="operationTime">method execution time</param>
        /// <param name="numbers">array of numbers</param>
        /// <returns>Greatest common divisor of all numbers</returns>
        public static int EuclideanAlgorithm(out TimeSpan operationTime, params int[] numbers) =>
            ComputeGcd(() => EuclideanAlgorithm(numbers), out operationTime);

        #endregion // !calculating the execution time of the algorithm.

        #endregion // !EuclideanAlgorithm.

        #region SteinAlgorithm

        #region сalculation of the algorithm

        /// <summary>
        /// The Stein algorithm for finding the greatest common divisor.
        /// </summary>
        /// <param name="number1">first number</param>
        /// <param name="number2">second number</param>
        /// <returns>Greatest common divisor of <paramref name="number1"/> and <paramref name="number2"/>.</returns>
        public static int SteinAlgorithm(int number1, int number2)
        {
            number1 = Math.Abs(number1);
            number2 = Math.Abs(number2);

            if ((number1 == 0) && (number2 != 0))
            {
                return number2;
            }

            if ((number1 != 0) && (number2 == 0))
            {
                return number1;
            }

            if (number1 == number2)
            {
                return number1;
            }

            if ((number1 == 1) || (number2 == 1))
            {
                return 1;
            }

            if (IsEven(number1) && IsEven(number2))
            {
                return 2 * SteinAlgorithm(number1 / 2, number2 / 2);
            }

            if (IsEven(number1) && !IsEven(number2))
            {
                return SteinAlgorithm(number1 / 2, number2);
            }

            if (!IsEven(number1) && IsEven(number2))
            {
                return SteinAlgorithm(number1, number2 / 2);
            }

            if ((!IsEven(number1)) && (!IsEven(number2)) && (number2 > number1))
            {
                return SteinAlgorithm(number1, (number2 - number1) / 2);
            }

            if ((!IsEven(number1)) && (!IsEven(number2)) && (number2 < number1))
            {
                return SteinAlgorithm((number1 - number2) / 2, number2);
            }

            return 0;
        }

        /// <summary>
        /// The Stein algorithm for finding the greatest common divisor.
        /// </summary>
        /// <param name="number1">first number</param>
        /// <param name="number2">second number</param>
        /// <param name="number3">third number</param>
        /// <returns>Greatest common divisor of 3 numbers</returns>
        public static int SteinAlgorithm(int number1, int number2, int number3) =>
            ComputeGcd(SteinAlgorithm, number1, number2, number3);

        /// <summary>
        /// The Stein algorithm for finding the greatest common divisor.
        /// </summary>
        /// <param name="number1">first number</param>
        /// <param name="number2">second number</param>
        /// <param name="number3">third number</param>
        /// <param name="number4">fourth number</param>
        /// <returns>Greatest common divisor of 4 numbers</returns>
        public static int SteinAlgorithm(int number1, int number2, int number3, int number4) =>
            ComputeGcd(SteinAlgorithm, number1, number2, number3, number4);

        /// <summary>
        /// The Stein algorithm for finding the greatest common divisor.
        /// </summary>
        /// <param name="numbers">array of numbers</param>
        /// <returns>Greatest common divisor of all numbers</returns>
        public static int SteinAlgorithm(params int[] numbers) =>
            ComputeGcd(SteinAlgorithm, numbers);

        #endregion // !сalculation of the algorithm.

        #region calculating the execution time of the algorithm

        /// <summary>
        /// The Stein algorithm for finding the greatest common divisor.
        /// </summary>
        /// <param name="operationTime">method execution time</param>
        /// <param name="number1">first number</param>
        /// <param name="number2">second number</param>
        /// <returns>Greatest common divisor of 2 numbers</returns>
        public static int SteinAlgorithm(out TimeSpan operationTime, int number1, int number2) =>
            ComputeGcd(() => SteinAlgorithm(number1, number2), out operationTime);

        /// <summary>
        /// The Stein algorithm for finding the greatest common divisor.
        /// </summary>
        /// <param name="operationTime">method execution time</param>
        /// <param name="number1">first number</param>
        /// <param name="number2">second number</param>
        /// <param name="number3">third number</param>
        /// <returns>Greatest common divisor of 3 numbers</returns>
        public static int SteinAlgorithm(out TimeSpan operationTime, int number1, int number2, int number3) =>
            ComputeGcd(() => SteinAlgorithm(number1, number2, number3), out operationTime);

        /// <summary>
        /// The Stein algorithm for finding the greatest common divisor.
        /// </summary>
        /// <param name="operationTime">method execution time</param>
        /// <param name="number1">first number</param>
        /// <param name="number2">second number</param>
        /// <param name="number3">third number</param>
        /// <param name="number4">fourth number</param>
        /// <returns>Greatest common divisor of 4 numbers</returns>
        public static int SteinAlgorithm(out TimeSpan operationTime, int number1, int number2, int number3, int number4) =>
            ComputeGcd(() => SteinAlgorithm(number1, number2, number3, number4), out operationTime);

        /// <summary>
        /// The Stein algorithm for finding the greatest common divisor.
        /// </summary>
        /// <param name="operationTime">method execution time</param>
        /// <param name="numbers">array of numbers</param>
        /// <returns>Greatest common divisor of all numbers</returns>
        public static int SteinAlgorithm(out TimeSpan operationTime, params int[] numbers) =>
            ComputeGcd(() => SteinAlgorithm(numbers), out operationTime);

        #endregion // !calculating the execution time of the algorithm.

        #endregion // !SteinAlgorithm.

        #endregion // !public.

        #region private

        private static int ComputeGcd(Func<int, int, int> gcdOperation, int number1, int number2, int number3) =>
            gcdOperation(gcdOperation(number1, number2), number3);

        private static int ComputeGcd(Func<int, int, int> gcdOperation, int number1, int number2, int number3, int number4)
        {
            int gcd = gcdOperation(number1, number2);
            gcd = gcdOperation(gcd, number3);

            return gcdOperation(gcd, number4);
        }

        private static int ComputeGcd(Func<int, int, int> gcdOperation, params int[] numbers)
        {
            if (ReferenceEquals(numbers, null))
            {
                throw new ArgumentNullException(nameof(numbers));
            }

            if (numbers.Length < 2)
            {
                throw new ArgumentException("Too few arguments", nameof(numbers));
            }

            int result = gcdOperation(numbers[0], numbers[1]);

            for (int i = 2; i < numbers.Length; i++)
            {
                result = gcdOperation(result, numbers[i]);
            }

            return result;
        }

        private static int ComputeGcd(Func<int> gcdOperation, out TimeSpan operationTime)
        {
            var watch = new Stopwatch();
            watch.Start();

            int result = gcdOperation();

            watch.Stop();
            operationTime = watch.Elapsed;

            return result;
        }

        private static bool IsEven(int number) =>
            (number % 2) == 0;

        #endregion // !private.
    }
}

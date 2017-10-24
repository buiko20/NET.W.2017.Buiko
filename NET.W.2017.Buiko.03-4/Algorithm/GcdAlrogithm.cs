using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm
{
    public static class GcdAlrogithm
    {
        #region public methods

        #region public methods EuclideanAlgorithm
        /// <summary>
        /// The Euclidean algorithm for finding the greatest common divisor.
        /// </summary>
        /// <param name="number1">first number</param>
        /// <param name="number2">second number</param>
        /// <returns>Greatest common divisor of 2 numbers</returns>
        public static int EuclideanAlgorithm(int number1, int number2)
        {
            number1 = Math.Abs(number1);
            number2 = Math.Abs(number2);

            return GcdEuclidean(number1, number2);
        }

        /// <summary>
        /// The Euclidean algorithm for finding the greatest common divisor.
        /// </summary>
        /// <param name="number1">1st number</param>
        /// <param name="number2">2nd number</param>
        /// <param name="number3">3th number</param>
        /// <returns>Greatest common divisor of 3 numbers</returns>
        public static int EuclideanAlgorithm(int number1, int number2, int number3)
        {
            number1 = Math.Abs(number1);
            number2 = Math.Abs(number2);
            number3 = Math.Abs(number3);

            int gcd = GcdEuclidean(number1, number2);

            return GcdEuclidean(gcd, number3);
        }

        /// <summary>
        /// The Euclidean algorithm for finding the greatest common divisor.
        /// </summary>
        /// <param name="number1">1st number</param>
        /// <param name="number2">2nd number</param>
        /// <param name="number3">3th number</param>
        /// <param name="number4">4th number</param>
        /// <returns>Greatest common divisor of 4 numbers</returns>
        public static int EuclideanAlgorithm(int number1, int number2, int number3, int number4)
        {
            number1 = Math.Abs(number1);
            number2 = Math.Abs(number2);
            number3 = Math.Abs(number3);
            number4 = Math.Abs(number4);

            int gcd = GcdEuclidean(number1, number2);
            gcd = GcdEuclidean(gcd, number3);

            return GcdEuclidean(gcd, number4);
        }

        /// <summary>
        /// The Euclidean algorithm for finding the greatest common divisor.
        /// </summary>
        /// <param name="numbers">array of numbers</param>
        /// <returns>Greatest common divisor of all numbers</returns>
        public static int EuclideanAlgorithm(params int[] numbers)
        {
            if (numbers == null)
                throw new ArgumentNullException(nameof(numbers));

            if (numbers.Length < 2)
                throw new ArgumentException("Too few arguments", nameof(numbers));

            Abs(numbers);

            return GcdEuclidean(numbers);
        }

        /// <summary>
        /// The Euclidean algorithm for finding the greatest common divisor.
        /// </summary>
        /// <param name="operationTime">method execution time</param>
        /// <param name="number1">first number</param>
        /// <param name="number2">second number</param>
        /// <returns>Greatest common divisor of 2 numbers</returns>
        public static int EuclideanAlgorithm(out TimeSpan operationTime, int number1, int number2)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            int result = EuclideanAlgorithm(number1, number2);

            watch.Stop();
            operationTime = watch.Elapsed;

            return result;
        }

        /// <summary>
        /// The Euclidean algorithm for finding the greatest common divisor.
        /// </summary>
        /// <param name="operationTime">method execution time</param>
        /// <param name="number1">1st number</param>
        /// <param name="number2">2nd number</param>
        /// <param name="number3">3th number</param>
        /// <returns>Greatest common divisor of 3 numbers</returns>
        public static int EuclideanAlgorithm(out TimeSpan operationTime, int number1, int number2, int number3)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            int result = EuclideanAlgorithm(number1, number2, number3);

            watch.Stop();
            operationTime = watch.Elapsed;

            return result;
        }

        /// <summary>
        /// The Euclidean algorithm for finding the greatest common divisor.
        /// </summary>
        /// <param name="operationTime">method execution time</param>
        /// <param name="number1">1st number</param>
        /// <param name="number2">2nd number</param>
        /// <param name="number3">3th number</param>
        /// <param name="number4">4th number</param>
        /// <returns>Greatest common divisor of 4 numbers</returns>
        public static int EuclideanAlgorithm(out TimeSpan operationTime, int number1, int number2, int number3, int number4)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            int result = EuclideanAlgorithm(number1, number2, number3, number4);

            watch.Stop();
            operationTime = watch.Elapsed;

            return result;
        }

        /// <summary>
        /// The Euclidean algorithm for finding the greatest common divisor.
        /// </summary>
        /// <param name="operationTime">method execution time</param>
        /// <param name="numbers">array of numbers</param>
        /// <returns>Greatest common divisor of all numbers</returns>
        public static int EuclideanAlgorithm(out TimeSpan operationTime, params int[] numbers)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            int result = EuclideanAlgorithm(numbers);

            watch.Stop();
            operationTime = watch.Elapsed;

            return result;
        }
        #endregion

        #region public methods SteinAlgorithm
        /// <summary>
        /// The Stein algorithm for finding the greatest common divisor.
        /// </summary>
        /// <param name="number1">first number</param>
        /// <param name="number2">second number</param>
        /// <returns>Greatest common divisor of 2 numbers</returns>
        public static int SteinAlgorithm(int number1, int number2)
        {
            number1 = Math.Abs(number1);
            number2 = Math.Abs(number2);

            return GcdStein(number1, number2);
        }

        /// <summary>
        /// The Stein algorithm for finding the greatest common divisor.
        /// </summary>
        /// <param name="number1">1st number</param>
        /// <param name="number2">2nd number</param>
        /// <param name="number3">3th number</param>
        /// <returns>Greatest common divisor of 3 numbers</returns>
        public static int SteinAlgorithm(int number1, int number2, int number3)
        {
            number1 = Math.Abs(number1);
            number2 = Math.Abs(number2);
            number3 = Math.Abs(number3);

            int gcd = GcdStein(number1, number2);

            return GcdStein(gcd, number3);
        }

        /// <summary>
        /// The Stein algorithm for finding the greatest common divisor.
        /// </summary>
        /// <param name="number1">1st number</param>
        /// <param name="number2">2nd number</param>
        /// <param name="number3">3th number</param>
        /// <param name="number4">4th number</param>
        /// <returns>Greatest common divisor of 4 numbers</returns>
        public static int SteinAlgorithm(int number1, int number2, int number3, int number4)
        {
            number1 = Math.Abs(number1);
            number2 = Math.Abs(number2);
            number3 = Math.Abs(number3);
            number4 = Math.Abs(number4);

            int gcd = GcdStein(number1, number2);
            gcd = GcdStein(gcd, number3);

            return GcdStein(gcd, number4);
        }

        /// <summary>
        /// The Stein algorithm for finding the greatest common divisor.
        /// </summary>
        /// <param name="numbers">array of numbers</param>
        /// <returns>Greatest common divisor of all numbers</returns>
        public static int SteinAlgorithm(params int[] numbers)
        {
            if (numbers == null)
                throw new ArgumentNullException(nameof(numbers));

            if (numbers.Length < 2)
                throw new ArgumentException("Too few arguments", nameof(numbers));

            Abs(numbers);

            return GcdStein(numbers);
        }

        /// <summary>
        /// The Stein algorithm for finding the greatest common divisor.
        /// </summary>
        /// <param name="operationTime">method execution time</param>
        /// <param name="number1">first number</param>
        /// <param name="number2">second number</param>
        /// <returns>Greatest common divisor of 2 numbers</returns>
        public static int SteinAlgorithm(out TimeSpan operationTime, int number1, int number2)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            int result = SteinAlgorithm(number1, number2);

            watch.Stop();
            operationTime = watch.Elapsed;

            return result;
        }

        /// <summary>
        /// The Stein algorithm for finding the greatest common divisor.
        /// </summary>
        /// <param name="operationTime">method execution time</param>
        /// <param name="number1">1st number</param>
        /// <param name="number2">2nd number</param>
        /// <param name="number3">3th number</param>
        /// <returns>Greatest common divisor of 3 numbers</returns>
        public static int SteinAlgorithm(out TimeSpan operationTime, int number1, int number2, int number3)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            int result = SteinAlgorithm(number1, number2, number3);

            watch.Stop();
            operationTime = watch.Elapsed;

            return result;
        }

        /// <summary>
        /// The Stein algorithm for finding the greatest common divisor.
        /// </summary>
        /// <param name="operationTime">method execution time</param>
        /// <param name="number1">1st number</param>
        /// <param name="number2">2nd number</param>
        /// <param name="number3">3th number</param>
        /// <param name="number4">4th number</param>
        /// <returns>Greatest common divisor of 4 numbers</returns>
        public static int SteinAlgorithm(out TimeSpan operationTime, int number1, int number2, int number3, int number4)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            int result = SteinAlgorithm(number1, number2, number3, number4);

            watch.Stop();
            operationTime = watch.Elapsed;

            return result;
        }

        /// <summary>
        /// The Stein algorithm for finding the greatest common divisor.
        /// </summary>
        /// <param name="operationTime">method execution time</param>
        /// <param name="numbers">array of numbers</param>
        /// <returns>Greatest common divisor of all numbers</returns>
        public static int SteinAlgorithm(out TimeSpan operationTime, params int[] numbers)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            int result = SteinAlgorithm(numbers);

            watch.Stop();
            operationTime = watch.Elapsed;

            return result;
        }
        #endregion

        #endregion

        #region private methods
        private static int GcdEuclidean(int number1, int number2)
        {
            if ((number1 == 0) && (number2 != 0))
                return number2;

            if ((number2 == 0) && (number1 != 0))
                return number1;

            while (number1 != number2)
            {
                if (number1 > number2) number1 -= number2;
                else number2 -= number1;
            }

            return number1;
        }

        private static int GcdEuclidean(params int[] numbers)
        {
            int result = GcdEuclidean(numbers[0], numbers[1]);

            for (int i = 2; i < numbers.Length; i++)
                result = GcdEuclidean(result, numbers[i]);

            return result;
        }

        private static void Abs(params int[] numbers)
        {
            for (int i = 0; i < numbers.Length; i++)
                numbers[i] = Math.Abs(numbers[i]);
        }

        private static int GcdStein(int number1, int number2)
        {
            if ((number1 == 0) && (number2 != 0)) return number2;
            if ((number1 != 0) && (number2 == 0)) return number1;
            if (number1 == number2) return number1;

            if ((number1 == 1) || (number2 == 1)) return 1;

            if ((IsEven(number1)) && (IsEven(number2)))
                return 2 * GcdStein(number1 / 2, number2 / 2);

            if ((IsEven(number1)) && (!IsEven(number2)))
                return GcdStein(number1 / 2, number2);

            if ((!IsEven(number1)) && (IsEven(number2)))
                return GcdStein(number1, number2 / 2);

            if ((!IsEven(number1)) && (!IsEven(number2)) && (number2 > number1))
                return GcdStein(number1, (number2 - number1) / 2);

            if ((!IsEven(number1)) && (!IsEven(number2)) && (number2 < number1))
                return GcdStein((number1 - number2) / 2, number2);

            return 0;
        }

        private static int GcdStein(params int[] numbers)
        {
            int result = GcdStein(numbers[0], numbers[1]);

            for (int i = 2; i < numbers.Length; i++)
                result = GcdStein(result, numbers[i]);

            return result;
        }

        private static bool IsEven(int number)
        {
            return (number % 2) == 0;
        }
        #endregion
    }
}

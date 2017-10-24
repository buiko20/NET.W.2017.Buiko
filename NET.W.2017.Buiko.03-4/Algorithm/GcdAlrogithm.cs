using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm
{
    public static class GcdAlrogithm
    {
        #region EuclideanAlgorithm

        #region public methods EuclideanAlgorithm
        public static int EuclideanAlgorithm(int number1, int number2)
        {
            number1 = Math.Abs(number1);
            number2 = Math.Abs(number2);

            return GcdEuclidean(number1, number2);
        }

        public static int EuclideanAlgorithm(int number1, int number2, int number3)
        {
            number1 = Math.Abs(number1);
            number2 = Math.Abs(number2);
            number3 = Math.Abs(number3);

            int temp = GcdEuclidean(number1, number2);

            return GcdEuclidean(temp, number3);
        }

        public static int EuclideanAlgorithm(int number1, int number2, int number3, int number4)
        {
            number1 = Math.Abs(number1);
            number2 = Math.Abs(number2);
            number3 = Math.Abs(number3);
            number4 = Math.Abs(number4);

            int temp1 = GcdEuclidean(number1, number2);
            int temp2 = GcdEuclidean(temp1, number3);

            return GcdEuclidean(temp2, number4);
        }

        public static int EuclideanAlgorithm(params int[] numbers)
        {
            if (numbers == null)
                throw new ArgumentNullException(nameof(numbers));

            if (numbers.Length < 2)
                throw new ArgumentException("Too few arguments", nameof(numbers));

            Abs(numbers);

            return GcdEuclidean(numbers);
        }
        #endregion

        #region private methods EuclideanAlgorithm
        private static int GcdEuclidean(int number1, int number2)
        {
            if ((number1 == 0) && (number2 != 0))
                return number2;

            if ((number2 == 0) && (number1 != 0))
                return number1;

            while (number1 != number2)
            {
                if (number1 > number2) number1 = number1 - number2;
                else number2 = number2 - number1;
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
        #endregion

        #endregion
    }
}

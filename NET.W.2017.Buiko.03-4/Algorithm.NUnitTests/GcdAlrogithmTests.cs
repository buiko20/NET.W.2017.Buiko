using System;
using NUnit.Framework;

namespace Algorithm.NUnitTests
{
    [TestFixture]
    public class GcdAlrogithmTests
    {
        #region EuclideanAlgorithm tests

        [TestCase(1428, 420, ExpectedResult = 84)]
        [TestCase(0, 0, ExpectedResult = 0)]
        [TestCase(0, 5, ExpectedResult = 5)]
        [TestCase(5, 5, ExpectedResult = 5)]
        [TestCase(-5, 10, ExpectedResult = 5)]
        [TestCase(-5, -10, ExpectedResult = 5)]
        [TestCase(1, 17, ExpectedResult = 1)]
        [TestCase(1, 5, ExpectedResult = 1)]
        public int EuclideanAlgorithmTests(int number1, int number2)
        {
            return GcdAlrogithm.EuclideanAlgorithm(number1, number2);
        }

        [TestCase(1428, 420, 84, ExpectedResult = 84)]
        [TestCase(0, 0, 0, ExpectedResult = 0)]
        [TestCase(0, 5, -5, ExpectedResult = 5)]
        [TestCase(5, 10, 15, ExpectedResult = 5)]
        [TestCase(-5, 10, -20, ExpectedResult = 5)]
        [TestCase(-5, -10, 100, ExpectedResult = 5)]
        [TestCase(1, 17, 20, ExpectedResult = 1)]
        [TestCase(1, 5, 10, ExpectedResult = 1)]
        public int EuclideanAlgorithmTests(int number1, int number2, int number3)
        {
            return GcdAlrogithm.EuclideanAlgorithm(number1, number2, number3);
        }

        [TestCase(1428, 420, 84, 42, ExpectedResult = 42)]
        [TestCase(0, 0, 0, 0, ExpectedResult = 0)]
        [TestCase(0, 5, -5, 10, ExpectedResult = 5)]
        [TestCase(5, 10, 15, 20, ExpectedResult = 5)]
        [TestCase(-5, 10, -20, -40, ExpectedResult = 5)]
        [TestCase(-5, -10, 100, 15, ExpectedResult = 5)]
        [TestCase(1, 17, 20, 31, ExpectedResult = 1)]
        [TestCase(1, 5, 10, 20, ExpectedResult = 1)]
        public int EuclideanAlgorithmTests(int number1, int number2, int number3, int number4)
        {
            return GcdAlrogithm.EuclideanAlgorithm(number1, number2, number3, number4);
        }

        [TestCase(1428, 420, 84, 42, 420, 1428, ExpectedResult = 42)]
        [TestCase(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, ExpectedResult = 0)]
        [TestCase(0, 5, -5, 10, 25, -50, 100, 510, ExpectedResult = 5)]
        [TestCase(5, 10, 15, 20, -1000, 55, 40, -10, ExpectedResult = 5)]
        [TestCase(-5, 10, -20, -40, 20, 40, 5, 15, ExpectedResult = 5)]
        [TestCase(-5, -10, 100, 15, -45, 60, 80, ExpectedResult = 5)]
        [TestCase(1, 17, 20, 31, 13, 21, 15, 61, 77, 4, ExpectedResult = 1)]
        [TestCase(1, 5, 10, 20, 42, 11, 22, 19, 53, ExpectedResult = 1)]
        public int EuclideanAlgorithmTests(params int[] numbers)
        {
            return GcdAlrogithm.EuclideanAlgorithm(numbers);
        }

        [TestCase(1)]
        public void EuclideanAlgorithmTestsArgumentException(params int[] numbers)
        {
            Assert.Throws<ArgumentException>(() => GcdAlrogithm.EuclideanAlgorithm(numbers));
        }

        #endregion

        #region SteinAlgorithm tests

        [TestCase(1428, 420, ExpectedResult = 84)]
        [TestCase(0, 0, ExpectedResult = 0)]
        [TestCase(0, 5, ExpectedResult = 5)]
        [TestCase(5, 5, ExpectedResult = 5)]
        [TestCase(-5, 10, ExpectedResult = 5)]
        [TestCase(-5, -10, ExpectedResult = 5)]
        [TestCase(1, 17, ExpectedResult = 1)]
        [TestCase(1, 5, ExpectedResult = 1)]
        public int SteinAlgorithmTests(int number1, int number2)
        {
            return GcdAlrogithm.SteinAlgorithm(number1, number2);
        }

        [TestCase(1428, 420, 84, ExpectedResult = 84)]
        [TestCase(0, 0, 0, ExpectedResult = 0)]
        [TestCase(0, 5, -5, ExpectedResult = 5)]
        [TestCase(5, 10, 15, ExpectedResult = 5)]
        [TestCase(-5, 10, -20, ExpectedResult = 5)]
        [TestCase(-5, -10, 100, ExpectedResult = 5)]
        [TestCase(1, 17, 20, ExpectedResult = 1)]
        [TestCase(1, 5, 10, ExpectedResult = 1)]
        public int SteinAlgorithmTests(int number1, int number2, int number3)
        {
            return GcdAlrogithm.SteinAlgorithm(number1, number2, number3);
        }

        [TestCase(1428, 420, 84, 42, ExpectedResult = 42)]
        [TestCase(0, 0, 0, 0, ExpectedResult = 0)]
        [TestCase(0, 5, -5, 10, ExpectedResult = 5)]
        [TestCase(5, 10, 15, 20, ExpectedResult = 5)]
        [TestCase(-5, 10, -20, -40, ExpectedResult = 5)]
        [TestCase(-5, -10, 100, 15, ExpectedResult = 5)]
        [TestCase(1, 17, 20, 31, ExpectedResult = 1)]
        [TestCase(1, 5, 10, 20, ExpectedResult = 1)]
        public int SteinAlgorithmTests(int number1, int number2, int number3, int number4)
        {
            return GcdAlrogithm.SteinAlgorithm(number1, number2, number3, number4);
        }

        [TestCase(1428, 420, 84, 42, 420, 1428, ExpectedResult = 42)]
        [TestCase(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, ExpectedResult = 0)]
        [TestCase(0, 5, -5, 10, 25, -50, 100, 510, ExpectedResult = 5)]
        [TestCase(5, 10, 15, 20, -1000, 55, 40, -10, ExpectedResult = 5)]
        [TestCase(-5, 10, -20, -40, 20, 40, 5, 15, ExpectedResult = 5)]
        [TestCase(-5, -10, 100, 15, -45, 60, 80, ExpectedResult = 5)]
        [TestCase(1, 17, 20, 31, 13, 21, 15, 61, 77, 4, ExpectedResult = 1)]
        [TestCase(1, 5, 10, 20, 42, 11, 22, 19, 53, ExpectedResult = 1)]
        public int SteinAlgorithmTests(params int[] numbers)
        {
            return GcdAlrogithm.SteinAlgorithm(numbers);
        }

        [TestCase(1)]
        public void SteinAlgorithmTestsArgumentException(params int[] numbers)
        {
            Assert.Throws<ArgumentException>(() => GcdAlrogithm.SteinAlgorithm(numbers));
        }

        #endregion
    }
}

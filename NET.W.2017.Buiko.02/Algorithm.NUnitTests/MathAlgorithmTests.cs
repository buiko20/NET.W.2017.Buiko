using System;
using NUnit.Framework;

namespace Algorithm.NUnitTests
{
    [TestFixture]
    public class MathAlgorithmTests
    {
        [TestCase(15, 15, 0, 0, ExpectedResult = 15)]
        [TestCase(8, 15, 0, 0, ExpectedResult = 9)]
        [TestCase(8, 15, 3, 8, ExpectedResult = 120)]
        [TestCase(42, 42, 0, 33, ExpectedResult = -2)]
        public int BitInsertTest(int number1, int number2, int startPosition, int endPosition)
        {
            try
            {
                return MathAlgorithm.BitInsert(number1, number2, startPosition, endPosition);
            }
            catch (ArgumentException)
            {
                return -2;
            }
        }

        [TestCase(12, ExpectedResult = 21)]
        [TestCase(1234126, ExpectedResult = 1234162)]
        [TestCase(10, ExpectedResult = -1)]
        [TestCase(-56, ExpectedResult = -2)]
        public int FindNextBiggerNumberTest(int number)
        {
            try
            {
                return MathAlgorithm.FindNextBiggerNumber(number);
            }
            catch (ArgumentException)
            {
                return -2;
            }
        }

        [TestCase(7, 77, 17, 32, ExpectedResult = new[] { 77, 17 })]
        [TestCase(7, 8, 32, ExpectedResult = new int[0])]
        [TestCase(7, ExpectedResult = null)]
        public int[] FilterDigitTests(int digit, params int[] numbers)
        {
            return MathAlgorithm.FilterDigit(digit, numbers);
        }

        [TestCase(77, 77, 89)]
        public void FilterDigitTests_ArgumentException(int digit, params int[] numbers)
        {
            Assert.Throws<ArgumentException>(() =>
                MathAlgorithm.FilterDigit(digit, numbers));
        }

        [TestCase(4, 2, 0.00001)]
        [TestCase(27, 3, 0.00001)]
        [TestCase(0.001, 3, 0.00001)]
        public void FindNthRoot(double a, int n, double eps)
        {
            Assert.IsTrue(Math.Abs(MathAlgorithm.FindNthRoot(a, n, eps) - Math.Pow(a, 1.0 / n)) < eps);
        }

        [TestCase(5, 2, -0.0001)]
        [TestCase(5, -2, 0.0001)]
        [TestCase(-5, 2, 0.0001)]
        public void FindNthRoot_ThrowsArgumentException(double a, int n, double eps)
        {
            Assert.Throws<ArgumentException>(
                () => MathAlgorithm.FindNthRoot(a, n, eps));
        }
    }
}

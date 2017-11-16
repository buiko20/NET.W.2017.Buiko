using System;
using System.Collections;
using System.Linq;
using System.Numerics;
using NUnit.Framework;

namespace Algorithm.NUnitTests
{
    public class FibonacciNumbersAlgorithmTests
    {
        public static IEnumerable TestData
        {
            get
            {
                yield return new TestCaseData(0).Returns(new BigInteger[] { });
                yield return new TestCaseData(1).Returns(new BigInteger[] { 0 });
                yield return new TestCaseData(2).Returns(new BigInteger[] { 0, 1 });
                yield return new TestCaseData(10).Returns(new BigInteger[] { 0, 1, 1, 2, 3, 5, 8, 13, 21, 34 });
                yield return new TestCaseData(15).Returns(new BigInteger[] { 0, 1, 1, 2, 3, 5, 8, 13, 21, 34, 55, 89, 144, 233, new BigInteger(377) });
            }
        }

        [Test, TestCaseSource(nameof(TestData))]
        public BigInteger[] FibonacciNumbersTest1(int length) =>
            NumberAlgorithm.CalculateFibonacciNumbers(length).ToArray();

        [TestCase(-1)]
        public void FibonacciNumbersArgumentExceptionThrown(int length) =>
            Assert.Throws<ArgumentException>(() => NumberAlgorithm.CalculateFibonacciNumbers(length));

        [Test, TestCaseSource(nameof(TestData))]
        public BigInteger[] FibonacciNumbersTest2(int length)
        {
            var result = new BigInteger[length];

            int i = 0;
            foreach (var number in NumberAlgorithm.FibonacciNumbers(length))
            { 
                result[i++] = number;
            }

            return result;
        }
    }
}

using System;
using System.Collections.Generic;
using NUnit.Framework;
using Task4;
using Task4.Solution;

namespace Task4.Tests
{
    [TestFixture]
    public class TestCalculator
    {
        private readonly List<double> values = new List<double> { 10, 5, 7, 15, 13, 12, 8, 7, 4, 2, 9 };

        [Test]
        public void Test_AverageByMean1()
        {
            var calculator = new Calculator1();
            var computer = new DoubleAverangeComputer1();

            double expected = 8.3636363;

            double actual = calculator.CalculateAverage(values, computer);

            Assert.AreEqual(expected, actual, 0.000001);
        }

        [Test]
        public void Test_AverageByMedian1()
        {
            var calculator = new Calculator1();
            var computer = new DoubleAverangeComputer2();

            double expected = 8.0;

            double actual = calculator.CalculateAverage(values, computer);

            Assert.AreEqual(expected, actual, 0.000001);
        }

        [Test]
        public void Test_AverageByMean2()
        {
            var calculator = new Calculator2();
            var computer = new DoubleAverangeComputer1();

            double expected = 8.3636363;

            double actual = calculator.CalculateAverage(values, computer.ComputeAverange);

            Assert.AreEqual(expected, actual, 0.000001);
        }

        [Test]
        public void Test_AverageByMedian2()
        {
            var calculator = new Calculator2();
            var computer = new DoubleAverangeComputer2();

            double expected = 8.0;

            double actual = calculator.CalculateAverage(values, computer.ComputeAverange);

            Assert.AreEqual(expected, actual, 0.000001);
        }
    }
}
using System.Collections.Generic;
using System.Linq;
using Moq;
using NUnit.Framework;
using Task4.Solution;

namespace Task4.Tests
{
    [TestFixture]
    public class TestCalculator
    {
        private readonly List<double> _values = new List<double> { 10, 5, 7, 15, 13, 12, 8, 7, 4, 2, 9 };

        [Test]
        public void Test_AverageByMean1()
        {
            var calculator = new Calculator1();
            var computer = new DoubleAverangeComputer1();

            double expected = 8.3636363;

            double actual = calculator.CalculateAverage(_values, computer);

            Assert.AreEqual(expected, actual, 0.000001);
        }

        [Test]
        public void Test_AverageByMedian1()
        {
            var calculator = new Calculator1();
            var computer = new DoubleAverangeComputer2();

            double expected = 8.0;

            double actual = calculator.CalculateAverage(_values, computer);

            Assert.AreEqual(expected, actual, 0.000001);
        }

        [Test]
        public void Test_AverageByMean2()
        {
            var calculator = new Calculator2();
            var computer = new DoubleAverangeComputer1();

            double expected = 8.3636363;

            double actual = calculator.CalculateAverage(_values, computer.ComputeAverange);

            Assert.AreEqual(expected, actual, 0.000001);
        }

        [Test]
        public void Test_AverageByMedian2()
        {
            var calculator = new Calculator2();
            var computer = new DoubleAverangeComputer2();

            double expected = 8.0;

            double actual = calculator.CalculateAverage(_values, computer.ComputeAverange);

            Assert.AreEqual(expected, actual, 0.000001);
        }

        [Test]
        public void Calculator1CallIDoubleAverange_ComputeAverangeMethod()
        {
            var doubleAverangeComputerMock = new Mock<IDoubleAverangeComputer>();
            var calculator1 = new Calculator1();

            calculator1.CalculateAverage(_values, doubleAverangeComputerMock.Object);

            doubleAverangeComputerMock.Verify(computer => computer.ComputeAverange(It.Is<IList<double>>(list => list.SequenceEqual(_values))), Times.Once);
        }
    }
}
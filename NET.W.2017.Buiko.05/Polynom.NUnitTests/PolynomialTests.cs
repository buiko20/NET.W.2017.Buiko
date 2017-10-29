using System;
using NUnit.Framework;

namespace Polynom.NUnitTests
{
    [TestFixture]
    public class PolynomialTests
    {
        #region constructor tests

        [TestCase(null, null, 0.01)]
        [TestCase(null, new int[1] { 5 }, 0.01)]
        [TestCase(new double[1] { 5d }, null, 0.01)]
        [TestCase(new double[1] { 5d }, new int[1] { 5 }, 255d)]
        [TestCase(new double[1] { 5d }, new int[1] { 5 }, 0.0d)]
        [TestCase(new double[1] { 5d }, new int[1] { 5 }, -255d)]
        [TestCase(new double[2] { 5d, 13d }, new int[1] { 5 }, 0.0001)]
        [TestCase(new double[1] { 5d }, new int[2] { 5, 4 }, 0.0001)]
        [TestCase(new double[2] { 5d, -3d }, new int[2] { 5, 5 }, 0.0001)]
        [TestCase(new double[2] { 5d, -3d }, new int[2] { 5, 6 }, 0.0001)]
        [TestCase(new double[2] { 5d, -3d }, new int[2] { 5, -1 }, 0.0001)]
        [TestCase(new double[0] { }, new int[2] { 5, -1 }, 0.0001)]
        [TestCase(new double[2] { 5d, -3d }, new int[0] { }, 0.0001)]
        [TestCase(new double[0] { }, new int[0] { }, 0.0001)]
        public void PolynomialConstructorExceptionTests(double[] coefficients, int[] degrees, double accuracy)
        {
            if ((coefficients == null) || (degrees == null))
                Assert.Throws<ArgumentNullException>(() => new Polynomial(coefficients, degrees, accuracy));
            else
                Assert.Throws<ArgumentException>(() => new Polynomial(coefficients, degrees, accuracy));
        }

        #endregion // !constructor tests.

        #region indexator tests

        [TestCase(new double[3] { 5d, 177d, -3.5d }, new int[3] { 5, 2, 0 }, 0.0001)]
        public void PolynomialIndexerTests(double[] coefficients, int[] degrees, double accuracy)
        {
            var polynomial = new Polynomial(coefficients, degrees, accuracy);

            for (int i = 0; i < polynomial.Length; i++)
            {
                var temp = polynomial[i];
                if (Math.Abs(temp.Item1 - coefficients[i]) > accuracy) Assert.Fail();
                if (temp.Item2 != degrees[i]) Assert.Fail();
            }

            Assert.Throws<IndexOutOfRangeException>(() =>
            {
                var temp = polynomial[degrees.Length + 1];
            });
        }

        #endregion // !indexator tests.

        #region ToString tests

        [TestCase(new double[1] { 5d }, new int[1] { 5 }, ExpectedResult = "5x^5")]
        [TestCase(new double[2] { 5d, 3.5d }, new int[2] { 5, 4 }, ExpectedResult = "5x^5+3,5x^4")]
        [TestCase(new double[3] { 5d, 3.5d, 0d }, new int[3] { 5, 4, 0 }, ExpectedResult = "5x^5+3,5x^4+0x^0")]
        [TestCase(new double[2] { 5d, -3.555d }, new int[2] { 5, 4 }, ExpectedResult = "5x^5-3,555x^4")]
        [TestCase(new double[3] { -5d, -3.501d, -6.001d }, new int[3] { 5, 4, 0 }, ExpectedResult = "-5x^5-3,501x^4-6,001x^0")]
        public string PolynomialToStringTests(double[] coefficients, int[] degrees)
        {
            var polynomial = new Polynomial(coefficients, degrees);

            var actual = polynomial.ToString();

            return actual;
        }

        #endregion // !ToString tests.

        #region Equals tests

        [TestCase(new double[2] { 5d, 3.5d }, new int[2] { 5, 4 }, 0.001, new double[2] { 5d, 3.5d }, new int[2] { 5, 4 }, 0.001, ExpectedResult = true)]
        [TestCase(new double[1] { 5d }, new int[1] { 5 }, 0.001, new double[2] { 5d, 3.5d }, new int[2] { 5, 4 }, 0.001, ExpectedResult = false)]
        [TestCase(new double[1] { 5d }, new int[1] { 5 }, 0.1, new double[2] { 5d, 3.5d }, new int[2] { 5, 4 }, 0.001, ExpectedResult = false)]
        [TestCase(new double[1] { 5d }, new int[1] { 5 }, 0.1, new double[1] { 5d }, new int[1] { 5 }, 0.001, ExpectedResult = false)]
        public bool PolynomialEqualsTests1(double[] coefficients1, int[] degrees1, double accuracy1,
            double[] coefficients2, int[] degrees2, double accuracy2)
        {
            var polynomial1 = new Polynomial(coefficients1, degrees1, accuracy1);
            var polynomial2 = new Polynomial(coefficients2, degrees2, accuracy2);

            bool result = (polynomial1.Equals(polynomial2)) && (polynomial1 == polynomial2);

            return  result;
        }

        [Test]
        public void PolynomialEqualsTests2()
        {
            var polynomial = new Polynomial(new double[2] { 5d, 3.5 }, new int[2] { 5, 4 }, 0.001);

            Assert.IsTrue(polynomial.Equals(polynomial));

            Assert.IsFalse(polynomial.Equals(null));
            Assert.IsFalse(polynomial.Equals(new object()));
        }

        #endregion // !Equals tests.

        #region Add and operator "+" tests

        [TestCase(new double[2] { 5d, 3.5d }, new int[2] { 5, 4 }, 0.001, new double[2] { 5d, 3.5d }, new int[2] { 5, 4 }, 0.001, ExpectedResult = "10x^5+7x^4")]
        [TestCase(new double[1] { 5d }, new int[1] { 5 }, 0.001, new double[2] { 5d, 3.5d }, new int[2] { 5, 4 }, 0.001, ExpectedResult = "10x^5+3,5x^4")]
        [TestCase(new double[1] { 5d }, new int[1] { 5 }, 0.1, new double[2] { 5d, -3.5d }, new int[2] { 5, 4 }, 0.001, ExpectedResult = "10x^5-3,5x^4")]
        [TestCase(new double[1] { 5d }, new int[1] { 5 }, 0.1, new double[1] { 5d }, new int[1] { 5 }, 0.001, ExpectedResult = "10x^5")]
        [TestCase(new double[1] { 5d }, new int[1] { 5 }, 0.1, new double[1] { -5d }, new int[1] { 5 }, 0.001, ExpectedResult = "0x^5")]
        [TestCase(new double[1] { 5d }, new int[1] { 5 }, 0.1, new double[1] { -5d }, new int[1] { 7 }, 0.001, ExpectedResult = "-5x^7+5x^5")]
        [TestCase(new double[2] { 5d, -0.1d }, new int[2] { 5, 1 }, 0.1, new double[2] { -5d, 2d }, new int[2] { 7, 3 }, 0.001, ExpectedResult = "-5x^7+5x^5+2x^3-0,1x^1")]
        public string PolynomialAddTests1(double[] coefficients1, int[] degrees1, double accuracy1,
            double[] coefficients2, int[] degrees2, double accuracy2)
        {
            var polynomial1 = new Polynomial(coefficients1, degrees1, accuracy1);
            var polynomial2 = new Polynomial(coefficients2, degrees2, accuracy2);

            var result1 = polynomial1.Add(polynomial2).ToString();
            var result2 = (polynomial1 + polynomial2).ToString();

            if (result1 != result2) Assert.Fail();

            return result1;
        }

        [TestCase(new double[2] { 5d, 3.5d }, new int[2] { 5, 4 }, 0.001, 3.5d, 4, ExpectedResult = "5x^5+7x^4")]
        [TestCase(new double[2] { 5d, 3.5d }, new int[2] { 5, 4 }, 0.001, -3.5d, 4, ExpectedResult = "5x^5+0x^4")]
        [TestCase(new double[2] { 5d, 3.5d }, new int[2] { 5, 4 }, 0.001, 0d, 4, ExpectedResult = "5x^5+3,5x^4")]
        public string PolynomialAddTests2(double[] coefficients, int[] degrees, double accuracy, double coefficient, int degree)
        {
            var polynomial1 = new Polynomial(coefficients, degrees, accuracy);
            var tuple = new Tuple<double, int>(coefficient, degree);

            return polynomial1.Add(tuple).ToString();
        }

        #endregion // !Add and operator "+" tests.

        #region Subtract and operator "-" tests

        [TestCase(new double[2] { 5d, 3.5d }, new int[2] { 5, 4 }, 0.001, new double[2] { 5d, 3.5d }, new int[2] { 5, 4 }, 0.001, ExpectedResult = "0x^5+0x^4")]
        [TestCase(new double[1] { 5d }, new int[1] { 5 }, 0.001, new double[2] { 5d, 3.5d }, new int[2] { 5, 4 }, 0.001, ExpectedResult = "0x^5-3,5x^4")]
        [TestCase(new double[1] { 5d }, new int[1] { 5 }, 0.1, new double[2] { 5d, -3.5d }, new int[2] { 5, 4 }, 0.001, ExpectedResult = "0x^5+3,5x^4")]
        [TestCase(new double[1] { 5d }, new int[1] { 5 }, 0.1, new double[1] { 5d }, new int[1] { 5 }, 0.001, ExpectedResult = "0x^5")]
        [TestCase(new double[1] { 5d }, new int[1] { 5 }, 0.1, new double[1] { -5d }, new int[1] { 5 }, 0.001, ExpectedResult = "10x^5")]
        [TestCase(new double[1] { 5d }, new int[1] { 5 }, 0.1, new double[1] { -5d }, new int[1] { 7 }, 0.001, ExpectedResult = "5x^7+5x^5")]
        [TestCase(new double[2] { 5d, -0.1d }, new int[2] { 5, 1 }, 0.1, new double[2] { -5d, 2d }, new int[2] { 7, 3 }, 0.001, ExpectedResult = "5x^7+5x^5-2x^3-0,1x^1")]
        public string PolynomialSubtractTests1(double[] coefficients1, int[] degrees1, double accuracy1,
            double[] coefficients2, int[] degrees2, double accuracy2)
        {
            var polynomial1 = new Polynomial(coefficients1, degrees1, accuracy1);
            var polynomial2 = new Polynomial(coefficients2, degrees2, accuracy2);

            var result1 = polynomial1.Subtract(polynomial2).ToString();
            var result2 = (polynomial1 - polynomial2).ToString();

            if (result1 != result2) Assert.Fail();

            return result1;
        }

        [TestCase(new double[2] { 5d, 3.5d }, new int[2] { 5, 4 }, 0.001, 3.5d, 4, ExpectedResult = "5x^5+0x^4")]
        [TestCase(new double[2] { 5d, 3.5d }, new int[2] { 5, 4 }, 0.001, -3.5d, 4, ExpectedResult = "5x^5+7x^4")]
        [TestCase(new double[2] { 5d, 3.5d }, new int[2] { 5, 4 }, 0.001, 0d, 4, ExpectedResult = "5x^5+3,5x^4")]
        public string PolynomialSubtractTests2(double[] coefficients, int[] degrees, double accuracy, double coefficient, int degree)
        {
            var polynomial1 = new Polynomial(coefficients, degrees, accuracy);
            var tuple = new Tuple<double, int>(coefficient, degree);

            return polynomial1.Subtract(tuple).ToString();
        }

        #endregion // !Subtract and operator "-" tests.

        #region Multiply and operator "*" tests

        [TestCase(new double[2] { 5d, 3.5d }, new int[2] { 5, 4 }, 0.001, 1d, ExpectedResult = "5x^5+3,5x^4")]
        [TestCase(new double[2] { 5d, 3.5d }, new int[2] { 5, 4 }, 0.001, -1d, ExpectedResult = "-5x^5-3,5x^4")]
        [TestCase(new double[2] { 5d, 3.5d }, new int[2] { 5, 4 }, 0.001, 2d, ExpectedResult = "10x^5+7x^4")]
        [TestCase(new double[2] { 5d, 3.5d }, new int[2] { 5, 4 }, 0.001, -2d, ExpectedResult = "-10x^5-7x^4")]
        public string PolynomialMultiplyTests1(double[] coefficients, int[] degrees, double accuracy, double x)
        {
            var polynomial = new Polynomial(coefficients, degrees, accuracy);

            var result1 = polynomial.Multiply(x).ToString();
            var result2 = (polynomial * x).ToString();

            if (result1 != result2) Assert.Fail();

            return result1;
        }

        [TestCase(new double[2] { 5d, 3.5d }, new int[2] { 5, 4 }, 0.001, new double[2] { 5d, 3.5d }, new int[2] { 5, 4 }, 0.001, ExpectedResult = "25x^10+35x^9+12,25x^8")]
        [TestCase(new double[1] { 5d }, new int[1] { 5 }, 0.001, new double[2] { 5d, 3.5d }, new int[2] { 5, 4 }, 0.001, ExpectedResult = "25x^10+17,5x^9")]
        [TestCase(new double[1] { 5d }, new int[1] { 5 }, 0.1, new double[2] { 5d, -3.5d }, new int[2] { 5, 4 }, 0.001, ExpectedResult = "25x^10-17,5x^9")]
        [TestCase(new double[1] { 5d }, new int[1] { 5 }, 0.1, new double[1] { 5d }, new int[1] { 5 }, 0.001, ExpectedResult = "25x^10")]
        [TestCase(new double[1] { 5d }, new int[1] { 5 }, 0.1, new double[1] { -5d }, new int[1] { 5 }, 0.001, ExpectedResult = "-25x^10")]
        [TestCase(new double[1] { 5d }, new int[1] { 5 }, 0.1, new double[1] { -5d }, new int[1] { 7 }, 0.001, ExpectedResult = "-25x^12")]
        [TestCase(new double[3] { 5d, 3.5d, -17.089d }, new int[3] { 5, 4, 2 }, 0.001, new double[4] { 5d, 3.5d, 24.3d, -0.042d }, new int[4] { 5, 4, 2, 0 }, 0.001, ExpectedResult = "25x^10+35x^9+12,25x^8+36,055x^7+25,2385x^6-0,21x^5-415,4097x^4+0,717738x^2")]
        [TestCase(new double[2] { 5d, 3d }, new int[2] { 1, 0 }, 0.001, new double[3] { 8d, -2d, 5d }, new int[3] { 2, 1, 0}, 0.001, ExpectedResult = "40x^3+14x^2+19x^1+15x^0")]
        [TestCase(new double[2] { 5d, 3d }, new int[2] { 1, 0 }, 0.001, new double[3] { 8d, 2d, 5d }, new int[3] { 2, 1, 0}, 0.001, ExpectedResult = "40x^3+34x^2+31x^1+15x^0")]
        public string PolynomialMultiplyTests2(double[] coefficients1, int[] degrees1, double accuracy1,
            double[] coefficients2, int[] degrees2, double accuracy2)
        {
            var polynomial1 = new Polynomial(coefficients1, degrees1, accuracy1);
            var polynomial2 = new Polynomial(coefficients2, degrees2, accuracy2);

            var result1 = polynomial1.Multiply(polynomial2).ToString();
            var result2 = (polynomial1 * polynomial2).ToString();

            if (result1 != result2) Assert.Fail();

            return result1;
        }

        #endregion // !Multiply and operator "*" tests.

        #region Compute tests

        [TestCase(new double[1] { 5d }, new int[1] { 5 }, 0.0d, 0.00001, 0.0d)]
        [TestCase(new double[2] { 5d, 3.5d }, new int[2] { 5, 4 }, 1.0d, 0.00001, 8.5d)]
        [TestCase(new double[3] { 5d, 3.5d, 0.0d }, new int[3] { 5, 4, 0 }, -1.0d, 0.00001, -1.5d)]
        [TestCase(new double[2] { 5d, -3.555d }, new int[2] { 5, 4 }, 1.777d, 0.00001, 53.1467754915d)]
        public void PolynomialComputeTests(double[] coefficients, int[] degrees, double x, double accuracy, double result)
        {
            var polynomial = new Polynomial(coefficients, degrees, accuracy);

            var actual = polynomial.Compute(x);
            if (Math.Abs(actual - result) > polynomial.Accuracy) Assert.Fail();
        }

        #endregion // !Compute tests.
    }
}

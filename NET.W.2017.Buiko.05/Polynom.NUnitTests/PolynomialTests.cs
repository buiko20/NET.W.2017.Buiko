using System;
using NUnit.Framework;

namespace Polynom.NUnitTests
{
    [TestFixture]
    public class PolynomialTests
    {
        #region constructor tests

        [TestCase(null, null)]
        [TestCase(null, new int[1] { 5 })]
        [TestCase(new double[1] { 5d }, null)]
        [TestCase(new double[2] { 5d, 13d }, new int[1] { 5 })]
        [TestCase(new double[1] { 5d }, new int[2] { 5, 4 })]
        [TestCase(new double[2] { 5d, -3d }, new int[2] { 5, 5 })]
        [TestCase(new double[2] { 5d, -3d }, new int[2] { 5, 6 })]
        [TestCase(new double[2] { 5d, -3d }, new int[2] { 5, -1 })]
        [TestCase(new double[0] { }, new int[2] { 5, -1 })]
        [TestCase(new double[2] { 5d, -3d }, new int[0] { })]
        [TestCase(new double[0] { }, new int[0] { })]
        public void PolynomialConstructorExceptionTests(double[] coefficients, int[] degrees)
        {
            if ((coefficients == null) || (degrees == null))
                Assert.Throws<ArgumentNullException>(() => new Polynomial(coefficients, degrees));
            else
                Assert.Throws<ArgumentException>(() => new Polynomial(coefficients, degrees));
        }

        #endregion // !constructor tests.

        #region indexator tests

        [TestCase(new double[3] { 5d, 177d, -3.5d }, new int[3] { 5, 2, 0 })]
        public void PolynomialIndexerTests(double[] coefficients, int[] degrees)
        {
            var polynomial = new Polynomial(coefficients, degrees);

            for (int i = 0; i < polynomial.Length; i++)
            {
                var temp = polynomial[i];
                if (Math.Abs(temp.Item1 - coefficients[i]) > Polynomial.Accuracy) Assert.Fail();
                if (temp.Item2 != degrees[i]) Assert.Fail();
            }

            Assert.Throws<ArgumentOutOfRangeException>(() =>
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

        [TestCase(new double[2] { 5d, 3.5d }, new int[2] { 5, 4 }, new double[2] { 5d, 3.5d }, new int[2] { 5, 4 }, ExpectedResult = true)]
        [TestCase(new double[1] { 5d }, new int[1] { 5 }, new double[2] { 5d, 3.5d }, new int[2] { 5, 4 }, ExpectedResult = false)]
        [TestCase(new double[1] { 5d }, new int[1] { 5 }, new double[2] { 5d, 3.5d }, new int[2] { 5, 4 }, ExpectedResult = false)]
        [TestCase(new double[1] { 5d }, new int[1] { 5 }, new double[1] { 5d }, new int[1] { 5 }, ExpectedResult = true)]
        public bool PolynomialEqualsTests1(double[] coefficients1, int[] degrees1, double[] coefficients2, int[] degrees2)
        {
            var polynomial1 = new Polynomial(coefficients1, degrees1);
            var polynomial2 = new Polynomial(coefficients2, degrees2);

            bool result = (polynomial1.Equals(polynomial2)) && (polynomial1 == polynomial2);

            return  result;
        }

        [Test]
        public void PolynomialEqualsTests2()
        {
            var polynomial = new Polynomial(new double[2] { 5d, 3.5 }, new int[2] { 5, 4 });

            Assert.IsTrue(polynomial.Equals(polynomial));

            Assert.IsFalse(polynomial.Equals(null));
            Assert.IsFalse(polynomial.Equals(new object()));
        }

        #endregion // !Equals tests.

        #region Add and operator "+" tests

        [TestCase(new double[2] { 5d, 3.5d }, new int[2] { 5, 4 }, new double[2] { 5d, 3.5d }, new int[2] { 5, 4 }, ExpectedResult = "10x^5+7x^4")]
        [TestCase(new double[1] { 5d }, new int[1] { 5 }, new double[2] { 5d, 3.5d }, new int[2] { 5, 4 }, ExpectedResult = "10x^5+3,5x^4")]
        [TestCase(new double[1] { 5d }, new int[1] { 5 }, new double[2] { 5d, -3.5d }, new int[2] { 5, 4 }, ExpectedResult = "10x^5-3,5x^4")]
        [TestCase(new double[1] { 5d }, new int[1] { 5 }, new double[1] { 5d }, new int[1] { 5 }, ExpectedResult = "10x^5")]
        [TestCase(new double[1] { 5d }, new int[1] { 5 }, new double[1] { -5d }, new int[1] { 5 }, ExpectedResult = "0x^5")]
        [TestCase(new double[1] { 5d }, new int[1] { 5 }, new double[1] { -5d }, new int[1] { 7 }, ExpectedResult = "-5x^7+5x^5")]
        [TestCase(new double[2] { 5d, -0.1d }, new int[2] { 5, 1 }, new double[2] { -5d, 2d }, new int[2] { 7, 3 }, ExpectedResult = "-5x^7+5x^5+2x^3-0,1x^1")]
        public string PolynomialAddTests1(double[] coefficients1, int[] degrees1, double[] coefficients2, int[] degrees2)
        {
            var polynomial1 = new Polynomial(coefficients1, degrees1);
            var polynomial2 = new Polynomial(coefficients2, degrees2);

            var result1 = Polynomial.Add(polynomial1, polynomial2).ToString();
            var result2 = (polynomial1 + polynomial2).ToString();

            if (result1 != result2) Assert.Fail();

            return result1;
        }

        [TestCase(new double[2] { 5d, 3.5d }, new int[2] { 5, 4 }, 3.5d, 4, ExpectedResult = "5x^5+7x^4")]
        [TestCase(new double[2] { 5d, 3.5d }, new int[2] { 5, 4 }, -3.5d, 4, ExpectedResult = "5x^5+0x^4")]
        [TestCase(new double[2] { 5d, 3.5d }, new int[2] { 5, 4 }, 0d, 4, ExpectedResult = "5x^5+3,5x^4")]
        public string PolynomialAddTests2(double[] coefficients, int[] degrees, double coefficient, int degree)
        {
            var polynomial1 = new Polynomial(coefficients, degrees);
            var tuple = new Tuple<double, int>(coefficient, degree);

            return Polynomial.Add(polynomial1, tuple).ToString();
        }

        #endregion // !Add and operator "+" tests.

        #region Subtract and operator "-" tests

        [TestCase(new double[2] { 5d, 3.5d }, new int[2] { 5, 4 }, new double[2] { 5d, 3.5d }, new int[2] { 5, 4 }, ExpectedResult = "0x^5+0x^4")]
        [TestCase(new double[1] { 5d }, new int[1] { 5 }, new double[2] { 5d, 3.5d }, new int[2] { 5, 4 }, ExpectedResult = "0x^5-3,5x^4")]
        [TestCase(new double[1] { 5d }, new int[1] { 5 }, new double[2] { 5d, -3.5d }, new int[2] { 5, 4 }, ExpectedResult = "0x^5+3,5x^4")]
        [TestCase(new double[1] { 5d }, new int[1] { 5 }, new double[1] { 5d }, new int[1] { 5 }, ExpectedResult = "0x^5")]
        [TestCase(new double[1] { 5d }, new int[1] { 5 }, new double[1] { -5d }, new int[1] { 5 }, ExpectedResult = "10x^5")]
        [TestCase(new double[1] { 5d }, new int[1] { 5 }, new double[1] { -5d }, new int[1] { 7 }, ExpectedResult = "5x^7+5x^5")]
        [TestCase(new double[2] { 5d, -0.1d }, new int[2] { 5, 1 }, new double[2] { -5d, 2d }, new int[2] { 7, 3 }, ExpectedResult = "5x^7+5x^5-2x^3-0,1x^1")]
        public string PolynomialSubtractTests1(double[] coefficients1, int[] degrees1, double[] coefficients2, int[] degrees2)
        {
            var polynomial1 = new Polynomial(coefficients1, degrees1);
            var polynomial2 = new Polynomial(coefficients2, degrees2);

            var result1 = Polynomial.Subtract(polynomial1, polynomial2).ToString();
            var result2 = (polynomial1 - polynomial2).ToString();

            if (result1 != result2) Assert.Fail();

            return result1;
        }

        [TestCase(new double[2] { 5d, 3.5d }, new int[2] { 5, 4 }, 3.5d, 4, ExpectedResult = "5x^5+0x^4")]
        [TestCase(new double[2] { 5d, 3.5d }, new int[2] { 5, 4 }, -3.5d, 4, ExpectedResult = "5x^5+7x^4")]
        [TestCase(new double[2] { 5d, 3.5d }, new int[2] { 5, 4 }, 0d, 4, ExpectedResult = "5x^5+3,5x^4")]
        public string PolynomialSubtractTests2(double[] coefficients, int[] degrees, double coefficient, int degree)
        {
            var polynomial1 = new Polynomial(coefficients, degrees);
            var tuple = new Tuple<double, int>(coefficient, degree);

            return Polynomial.Subtract(polynomial1, tuple).ToString();
        }

        #endregion // !Subtract and operator "-" tests.

        #region Multiply and operator "*" tests

        [TestCase(new double[2] { 5d, 3.5d }, new int[2] { 5, 4 }, 1d, ExpectedResult = "5x^5+3,5x^4")]
        [TestCase(new double[2] { 5d, 3.5d }, new int[2] { 5, 4 }, -1d, ExpectedResult = "-5x^5-3,5x^4")]
        [TestCase(new double[2] { 5d, 3.5d }, new int[2] { 5, 4 }, 2d, ExpectedResult = "10x^5+7x^4")]
        [TestCase(new double[2] { 5d, 3.5d }, new int[2] { 5, 4 }, -2d, ExpectedResult = "-10x^5-7x^4")]
        public string PolynomialMultiplyTests1(double[] coefficients, int[] degrees, double x)
        {
            var polynomial = new Polynomial(coefficients, degrees);

            var result1 = Polynomial.Multiply(polynomial, x).ToString();
            var result2 = (polynomial * x).ToString();

            if (result1 != result2) Assert.Fail();

            return result1;
        }

        [TestCase(new double[2] { 5d, 3.5d }, new int[2] { 5, 4 }, new double[2] { 5d, 3.5d }, new int[2] { 5, 4 }, ExpectedResult = "25x^10+35x^9+12,25x^8")]
        [TestCase(new double[1] { 5d }, new int[1] { 5 }, new double[2] { 5d, 3.5d }, new int[2] { 5, 4 }, ExpectedResult = "25x^10+17,5x^9")]
        [TestCase(new double[1] { 5d }, new int[1] { 5 }, new double[2] { 5d, -3.5d }, new int[2] { 5, 4 }, ExpectedResult = "25x^10-17,5x^9")]
        [TestCase(new double[1] { 5d }, new int[1] { 5 }, new double[1] { 5d }, new int[1] { 5 }, ExpectedResult = "25x^10")]
        [TestCase(new double[1] { 5d }, new int[1] { 5 }, new double[1] { -5d }, new int[1] { 5 }, ExpectedResult = "-25x^10")]
        [TestCase(new double[1] { 5d }, new int[1] { 5 }, new double[1] { -5d }, new int[1] { 7 }, ExpectedResult = "-25x^12")]
        [TestCase(new double[3] { 5d, 3.5d, -17.089d }, new int[3] { 5, 4, 2 }, new double[4] { 5d, 3.5d, 24.3d, -0.042d }, new int[4] { 5, 4, 2, 0 }, ExpectedResult = "25x^10+35x^9+12,25x^8+36,055x^7+25,2385x^6-0,21x^5-415,4097x^4+0,717738x^2")]
        [TestCase(new double[2] { 5d, 3d }, new int[2] { 1, 0 }, new double[3] { 8d, -2d, 5d }, new int[3] { 2, 1, 0}, ExpectedResult = "40x^3+14x^2+19x^1+15x^0")]
        [TestCase(new double[2] { 5d, 3d }, new int[2] { 1, 0 }, new double[3] { 8d, 2d, 5d }, new int[3] { 2, 1, 0}, ExpectedResult = "40x^3+34x^2+31x^1+15x^0")]
        public string PolynomialMultiplyTests2(double[] coefficients1, int[] degrees1, double[] coefficients2, int[] degrees2)
        {
            var polynomial1 = new Polynomial(coefficients1, degrees1);
            var polynomial2 = new Polynomial(coefficients2, degrees2);

            var result1 = Polynomial.Multiply(polynomial1, polynomial2).ToString();
            var result2 = (polynomial1 * polynomial2).ToString();

            if (result1 != result2) Assert.Fail();

            return result1;
        }

        #endregion // !Multiply and operator "*" tests.

        #region Compute tests

        [TestCase(new double[1] { 5d }, new int[1] { 5 }, 0.0d, 0.0d)]
        [TestCase(new double[2] { 5d, 3.5d }, new int[2] { 5, 4 }, 1.0d, 8.5d)]
        [TestCase(new double[3] { 5d, 3.5d, 0.0d }, new int[3] { 5, 4, 0 }, -1.0d, -1.5d)]
        [TestCase(new double[2] { 5d, -3.555d }, new int[2] { 5, 4 }, 1.777d, 53.1467754915d)]
        public void PolynomialComputeTests(double[] coefficients, int[] degrees, double x, double result)
        {
            var polynomial = new Polynomial(coefficients, degrees);

            var actual = polynomial.Compute(x);
            if (Math.Abs(actual - result) > Polynomial.Accuracy) Assert.Fail();
        }

        #endregion // !Compute tests.
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Polynom
{
    public sealed class Polynomial : ICloneable, IEquatable<Polynomial>
    {
        #region private fields

        private static double _accuracy;
        private double[] _coefficients;
        private int[] _degrees;

        #endregion // !private fields.

        #region public

        #region constructors

        static Polynomial()
        {
            try
            {
                var appSettingsReader = new System.Configuration.AppSettingsReader();
                Accuracy = (double)appSettingsReader.GetValue("accurancy", typeof(double));
            }
            catch (Exception)
            {
                Accuracy = 0.00001;
            }
        }

        /// <summary>
        /// Copy Constructor.
        /// </summary>
        /// <param name="polynomial">source polynomial</param>
        public Polynomial(Polynomial polynomial)
        {
            if (ReferenceEquals(polynomial, null))
            {
                throw new ArgumentNullException(nameof(polynomial));
            }

            var data = polynomial.GetElements();

            ConstructInstance(GetCoefficients(data), GetDegrees(data));
        }

        /// <summary>
        /// Creates a polynomial using the values of elements from the pairs.
        /// </summary>
        /// <param name="polynomial">the pressure of pairs of values of elements of a polynomial</param>
        /// <exception cref="ArgumentException">
        /// Thrown when the degrees of a polynomial are not ordered 
        /// in descending order or there are negative degrees.
        /// </exception>
        /// <exception cref="ArgumentNullException">Thrown when polynomial is null</exception>
        public Polynomial(IEnumerable<Tuple<double, int>> polynomial)
        {
            if (polynomial == null)
            {
                throw new ArgumentNullException(nameof(polynomial));
            }

            if (!polynomial.Any())
            {
                throw new ArgumentException("An enumeration must have at least one pair", nameof(polynomial));
            }

            ConstructInstance(GetCoefficients(polynomial), GetDegrees(polynomial));
        }

        /// <summary>
        /// Creates a polynomial using the values of elements from the pairs.
        /// </summary>
        /// <param name="coefficients">the pressure of pairs of values of elements of a polynomial</param>
        /// <exception cref="ArgumentNullException">Thrown when coefficients or degrees is null</exception>
        public Polynomial(double[] coefficients)
        {
            if (ReferenceEquals(coefficients, null))
            {
                throw new ArgumentNullException(nameof(coefficients));
            }

            int[] degrees = new int[coefficients.Length];
            for (int i = degrees.Length - 1; i >= 0; i++)
            {
                degrees[i] = i;
            }

            ConstructInstance(coefficients, degrees);
        }

        /// <summary>
        /// Creates a polynomial using the values of elements from the pairs.
        /// </summary>
        /// <param name="coefficients">the pressure of pairs of values of elements of a polynomial</param>
        /// <param name="degrees"></param>
        /// <exception cref="ArgumentException">
        /// Thrown when the degrees of a polynomial are not ordered 
        /// in descending order or there are negative degrees or 
        /// the number of coefficients does not correspond to the number of degrees.
        /// </exception>
        /// <exception cref="ArgumentNullException">Thrown when coefficients or degrees is null</exception>
        public Polynomial(double[] coefficients, int[] degrees)
        {
            ConstructInstance(coefficients, degrees);
        }

        #endregion // !constructors.

        #region properties

        /// <summary>
        /// The accuracy of comparing the values of a polynomial.
        /// </summary>
        public static double Accuracy
        {
            get
            {
                return _accuracy;
            }

            set
            {
                if ((value - 0.1 > 0.0d) || (value <= 0.0d))
                {
                    throw new ArgumentException($"{nameof(value)} must be from 0 to 0.0..1", nameof(value));
                }

                _accuracy = value;
            }
        }

        /// <summary>
        /// Number of elements in a polynomial.
        /// </summary>
        public int Length => _degrees.Length;

        /// <summary>
        /// The maximum degree of a variable in a polynomial.
        /// </summary>
        public int Degree => _degrees[0];

        #endregion // !properties.

        #region other

        /// <summary>
        /// Indexer by polynomial.
        /// </summary>
        /// <param name="index">element number in a polynomial</param>
        /// <returns>A pair representing the coefficient and degree of an element.</returns>
        /// <exception cref="ArgumentOutOfRangeException">if ((index &lt; 0) || (index &gt; Length)) </exception>
        public Tuple<double, int> this[int index]
        {
            get
            {
                if ((index < 0) || (index > Length))
                {
                    throw new ArgumentOutOfRangeException(nameof(index));
                }

                return new Tuple<double, int>(_coefficients[index], _degrees[index]);
            }
        }

        /// <summary>
        /// Returns the degrees of a polynomial.
        /// </summary>
        /// <param name="data">polinomial</param>
        /// <returns>Degrees of a polynomial.</returns>
        public static int[] GetDegrees(IEnumerable<Tuple<double, int>> data)
        {
            int[] result = new int[data.Count()];

            int i = 0;
            foreach (var tuple in data)
            {
                result[i++] = tuple.Item2;
            }

            return result;
        }

        /// <summary>
        /// Returns the coefficients  of a polynomial.
        /// </summary>
        /// <param name="data">polinomial</param>
        /// <returns>Сoefficients  of a polynomial.</returns>
        public static double[] GetCoefficients(IEnumerable<Tuple<double, int>> data)
        {
            double[] result = new double[data.Count()];

            int i = 0;
            foreach (var tuple in data)
            {
                result[i++] = tuple.Item1;
            }

            return result;
        }

        #endregion // !other.

        #region add methods and operator +

        /// <summary>
        /// Adds two polynomials.
        /// </summary>
        /// <param name="polynomial1">addendum 1</param>
        /// <param name="polynomial2">addendum 2</param>
        /// <returns>Polynomial result of addition.</returns>
        public static Polynomial Add(Polynomial polynomial1, Polynomial polynomial2)
        {
            if (ReferenceEquals(polynomial1, null))
            {
                throw new ArgumentNullException(nameof(polynomial1));
            }

            if (ReferenceEquals(polynomial2, null))
            {
                throw new ArgumentNullException(nameof(polynomial2));
            }

            return Operate(polynomial1, polynomial2, (a, b) => a + b, a => a);
        }

        /// <summary>
        /// Adds two polynomials.
        /// </summary>
        /// <param name="polynomial1">addendum 1</param>
        /// <param name="polynomial2">addendum 2</param>
        /// <returns>Polynomial result of addition.</returns>
        public static Polynomial Add(Polynomial polynomial1, Tuple<double, int> polynomial2) =>
            Add(polynomial1, new Polynomial(new[] { polynomial2 }));

        /// <summary>
        /// Adds two polynomials.
        /// </summary>
        /// <param name="lhs">addendum 1</param>
        /// <param name="rhs">addendum 2</param>
        /// <returns>Polynomial result of addition.</returns>
        public static Polynomial operator +(Polynomial lhs, Polynomial rhs) =>
            OperatorX(lhs, rhs, true);

        /// <summary>
        /// Adds two polynomials.
        /// </summary>
        /// <param name="lhs">addendum 1</param>
        /// <param name="rhs">addendum 2</param>
        /// <returns>Polynomial result of addition.</returns>
        public static Polynomial operator +(Polynomial lhs, Tuple<double, int> rhs) =>
            OperatorX(lhs, rhs, true);

        /// <summary>
        /// Adds two polynomials.
        /// </summary>
        /// <param name="lhs">addendum 1</param>
        /// <param name="rhs">addendum 2</param>
        /// <returns>Polynomial result of addition.</returns>
        public static Polynomial operator +(Tuple<double, int> lhs, Polynomial rhs) =>
            OperatorX(rhs, lhs, true);

        #endregion // !add methods and operator +.

        #region subtract methods and operator -

        /// <summary>
        /// Subtract two polynomials.
        /// </summary>
        /// <param name="polynomial1">addendum 1</param>
        /// <param name="polynomial2">addendum 2</param>
        /// <returns>Polynomial result of subtraction.</returns>
        public static Polynomial Subtract(Polynomial polynomial1, Polynomial polynomial2)
        {
            if (ReferenceEquals(polynomial1, null))
            {
                throw new ArgumentNullException(nameof(polynomial1));
            }

            if (ReferenceEquals(polynomial2, null))
            {
                throw new ArgumentNullException(nameof(polynomial2));
            }

            return Operate(polynomial1, polynomial2, (a, b) => a - b, a => -a);
        }

        /// <summary>
        /// Subtract two polynomials.
        /// </summary>
        /// <param name="polynomial1">addendum 1</param>
        /// <param name="polynomial2">addendum 2</param>
        /// <returns>Polynomial result of subtraction.</returns>
        public static Polynomial Subtract(Polynomial polynomial1, Tuple<double, int> polynomial2) =>
            Subtract(polynomial1, new Polynomial(new[] { polynomial2 }));

        /// <summary>
        /// Subtract two polynomials.
        /// </summary>
        /// <param name="lhs">addendum 1</param>
        /// <param name="rhs">addendum 2</param>
        /// <returns>Polynomial result of subtraction.</returns>
        public static Polynomial operator -(Polynomial lhs, Polynomial rhs) =>
            OperatorX(lhs, rhs, false);

        /// <summary>
        /// Unary minus.
        /// </summary>
        /// <param name="lhs">polynomial</param>
        /// <returns>Polynomial result of subtraction.</returns>
        public static Polynomial operator -(Polynomial lhs) =>
            Multiply(lhs, -1d);

        /// <summary>
        /// Subtract two polynomials.
        /// </summary>
        /// <param name="lhs">addendum 1</param>
        /// <param name="rhs">addendum 2</param>
        /// <returns>Polynomial result of subtraction.</returns>
        public static Polynomial operator -(Polynomial lhs, Tuple<double, int> rhs) =>
            OperatorX(lhs, rhs, false);

        /// <summary>
        /// Subtract two polynomials.
        /// </summary>
        /// <param name="lhs">addendum 1</param>
        /// <param name="rhs">addendum 2</param>
        /// <returns>Polynomial result of subtraction.</returns>
        public static Polynomial operator -(Tuple<double, int> lhs, Polynomial rhs) =>
            OperatorX(new Polynomial(new[] { lhs }), rhs, false);

        #endregion // !subtract methods and operator -.

        #region multiply methods and operator *

        /// <summary>
        /// Multiplies a polynomial by a number.
        /// </summary>
        /// <param name="rhs">multiplier</param>
        /// <param name="lhs">polynomial</param>
        /// <returns>Polynomial result of multiplication.</returns>
        public static Polynomial Multiply(Polynomial lhs, double rhs)
        {
            if (ReferenceEquals(lhs, null))
            {
                throw new ArgumentNullException(nameof(lhs));
            }

            var polynomial = lhs.GetElements().ToArray();
            var degrees = GetDegrees(polynomial);
            var coefficients = GetCoefficients(polynomial);

            for (int i = 0; i < coefficients.Length; i++)
            {
                coefficients[i] *= rhs;
            }

            return new Polynomial(coefficients, degrees);
        }

        /// <summary>
        /// Multiplies a polynomial by a number.
        /// </summary>
        /// <param name="lhs">multiplier1</param>
        /// <param name="rhs">multiplier1</param>
        /// <returns>Polynomial result of multiplication.</returns>
        public static Polynomial Multiply(Polynomial lhs, Polynomial rhs)
        {
            if (ReferenceEquals(lhs, null))
            {
                throw new ArgumentNullException(nameof(lhs));
            }

            if (ReferenceEquals(rhs, null))
            {
                throw new ArgumentNullException(nameof(rhs));
            }

            var polynomial1 = lhs.GetElements().ToArray();
            var polynomial2 = rhs.GetElements().ToArray();

            var degrees1 = GetDegrees(polynomial1);
            var degrees2 = GetDegrees(polynomial2);

            var coefficients1 = GetCoefficients(polynomial1);
            var coefficients2 = GetCoefficients(polynomial2);

            var resultPolynomial = new List<Tuple<double, int>>(degrees1.Length);

            MultiplyOperation(resultPolynomial, coefficients1, degrees1, coefficients2, degrees2);

            SumDuplicateDegree(resultPolynomial);

            DescendingSortOrderByDegree(resultPolynomial);

            return new Polynomial(resultPolynomial);
        }

        /// <summary>
        /// Multiplies a polynomial by a number.
        /// </summary>
        /// <param name="lhs">multiplier 1</param>
        /// <param name="rhs">multiplier 2</param>
        /// <returns>Polynomial result of multiplication.</returns>
        public static Polynomial operator *(Polynomial lhs, Polynomial rhs)
        {
            if (ReferenceEquals(lhs, null))
            {
                throw new ArgumentNullException(nameof(lhs));
            }

            if (ReferenceEquals(rhs, null))
            {
                throw new ArgumentNullException(nameof(rhs));
            }

            return Multiply(lhs, rhs);
        }

        /// <summary>
        /// Multiplies a polynomial by a number.
        /// </summary>
        /// <param name="lhs">multiplier 1</param>
        /// <param name="rhs">multiplier 2</param>
        /// <returns>Polynomial result of multiplication.</returns>
        public static Polynomial operator *(Polynomial lhs, double rhs)
        {
            if (ReferenceEquals(lhs, null))
            {
                throw new ArgumentNullException(nameof(lhs));
            }

            return Multiply(lhs, rhs);
        }

        /// <summary>
        /// Multiplies a polynomial by a number.
        /// </summary>
        /// <param name="lhs">multiplier 1</param>
        /// <param name="rhs">multiplier 2</param>
        /// <returns>Polynomial result of multiplication.</returns>
        public static Polynomial operator *(double lhs, Polynomial rhs) =>
            rhs * lhs;

        /// <summary>
        /// Multiply two polynomials.
        /// </summary>
        /// <param name="lhs">multiplier 1</param>
        /// <param name="rhs">multiplier 2</param>
        /// <returns>Polynomial result of multiplication.</returns>
        public static Polynomial operator *(Polynomial lhs, Tuple<double, int> rhs)
        {
            if (ReferenceEquals(lhs, null))
            {
                throw new ArgumentNullException(nameof(lhs));
            }

            if (ReferenceEquals(rhs, null))
            {
                throw new ArgumentNullException(nameof(rhs));
            }

            return lhs * new Polynomial(new[] { rhs.Item1 }, new[] { rhs.Item2 });
        }

        /// <summary>
        /// Multiply two polynomials.
        /// </summary>
        /// <param name="lhs">multiplier 1</param>
        /// <param name="rhs">multiplier 2</param>
        /// <returns>Polynomial result of multiplication.</returns>
        public static Polynomial operator *(Tuple<double, int> lhs, Polynomial rhs) =>
            rhs * lhs;

        #endregion // !multiply methods and operator *.

        #region op_Equality and op_Inequality

        /// <summary>
        /// Compares 2 operands to each other for equivalence.
        /// </summary>
        /// <param name="lhs">operand 1</param>
        /// <param name="rhs">operand 2</param>
        /// <returns>True if equivalents, false otherwise</returns>
        public static bool operator ==(Polynomial lhs, Polynomial rhs)
        {
            if (ReferenceEquals(lhs, rhs))
            {
                return true;
            }

            if (ReferenceEquals(rhs, null))
            {
                return false;
            }

            if (lhs.Length != rhs.Length)
            {
                return false;
            }

            for (int i = 0; i < lhs.Length; i++)
            {
                if (Math.Abs(lhs[i].Item1 - rhs[i].Item1) > Accuracy)
                {
                    return false;
                }

                if (lhs[i].Item2 != rhs[i].Item2)
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Compares 2 operands to each other for equivalence.
        /// </summary>
        /// <param name="lhs">operand 1</param>
        /// <param name="rhs">operand 2</param>
        /// <returns>True if equivalents, false otherwise</returns>
        public static bool operator ==(Tuple<double, int> lhs, Polynomial rhs)
        {
            if (ReferenceEquals(lhs, null))
            {
                throw new ArgumentNullException(nameof(lhs));
            }

            if (ReferenceEquals(rhs, null))
            {
                throw new ArgumentNullException(nameof(rhs));
            }

            return new Polynomial(new[] { lhs.Item1 }, new[] { lhs.Item2 }) == rhs;
        }

        /// <summary>
        /// Compares 2 operands to each other for equivalence.
        /// </summary>
        /// <param name="lhs">operand 1</param>
        /// <param name="rhs">operand 2</param>
        /// <returns>True if equivalents, false otherwise</returns>
        public static bool operator ==(Polynomial lhs, Tuple<double, int> rhs) =>
            rhs == lhs;

        /// <summary>
        /// Compares 2 operands to each other for equivalence.
        /// </summary>
        /// <param name="lhs">operand 1</param>
        /// <param name="rhs">operand 2</param>
        /// <returns>False if equivalents, true otherwise</returns>
        public static bool operator !=(Polynomial lhs, Polynomial rhs) =>
            !(lhs == rhs);

        /// <summary>
        /// Compares 2 operands to each other for equivalence.
        /// </summary>
        /// <param name="lhs">operand 1</param>
        /// <param name="rhs">operand 2</param>
        /// <returns>False if equivalents, true otherwise</returns>
        public static bool operator !=(Tuple<double, int> lhs, Polynomial rhs) =>
            !(lhs == rhs);

        /// <summary>
        /// Compares 2 operands to each other for equivalence.
        /// </summary>
        /// <param name="lhs">operand 1</param>
        /// <param name="rhs">operand 2</param>
        /// <returns>False if equivalents, true otherwise</returns>
        public static bool operator !=(Polynomial lhs, Tuple<double, int> rhs) =>
            !(lhs == rhs);

        #endregion // !op_Equality and op_Inequality.

        #region object override methods

        /// <summary>
        /// Converts a polynomial to a string representation.
        /// </summary>
        /// <returns>string representation of a polynomial</returns>
        public override string ToString()
        {
            var result = new StringBuilder();

            for (int i = 0; i < _coefficients.Length; i++)
            {
                var sign = _coefficients[i] < 0 ? string.Empty : "+";
                var element = $"{_coefficients[i]}x^{_degrees[i]}";

                result.Append(sign);
                result.Append(element);
            }

            if (result[0] == '+')
            {
                result.Remove(0, 1);
            }

            return result.ToString();
        }

        /// <summary>
        /// Compares an object to a polynomial.
        /// </summary>
        /// <param name="obj">object to compare</param>
        /// <returns>true if equals, false otherwise</returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (ReferenceEquals(obj, null))
            {
                return false;
            }

            return obj.GetType() == this.GetType() && this.Equals((Polynomial)obj);
        }

        /// <summary>
        /// Generates a hash code for a polynomial.
        /// </summary>
        /// <returns>hash code of a polynomial</returns>
        public override int GetHashCode() => ToString().GetHashCode();

        #endregion // !object override methods.

        #region other

        /// <summary>
        /// Returns the set of pairs of a polynomial.
        /// </summary>
        /// <returns>set of pairs of a polynomial</returns>
        public IEnumerable<Tuple<double, int>> GetElements()
        {
            var result = new List<Tuple<double, int>>(_coefficients.Length);

            result.AddRange(_coefficients.Select((t, i) => new Tuple<double, int>(t, _degrees[i])));

            return result;
        }

        /// <summary>
        /// Computes the value of the polynomial at the point.
        /// </summary>
        /// <param name="x">point</param>
        /// <returns>Value of the polynomial at the point.</returns>
        public double Compute(double x)
        {
            // return _degrees.Select(t => Math.Pow(x, t)).Select((temp, i) => temp * _coefficients[i]).Sum();
            // I think that without LINQ, more clearly what is happening in this code in this case.
            double result = 0.0d;

            for (int i = 0; i < _degrees.Length; i++)
            {
                var temp = Math.Pow(x, _degrees[i]);
                temp *= _coefficients[i];
                result += temp;
            }

            return result;
        }

        /// <inheritdoc />
        /// <summary>
        /// Creates a copy of the polynomial.
        /// </summary>
        /// <returns>copy of the polynomial</returns>
        public object Clone() => new Polynomial(this);

        /// <inheritdoc />
        /// <summary>
        /// Tests for equality two polynomials.
        /// </summary>
        /// <param name="other">comparison polynomial</param>
        /// <returns>True if equivalents, false otherwise</returns>
        public bool Equals(Polynomial other) => this == other;

        #endregion // !other.

        #endregion // !public.

        #region private

        #region verify input methods

        private static void VerifyInput(double[] coefficients, int[] degrees)
        {
            VerifyCoefficients(coefficients);

            VerifyDegrees(degrees);

            if (coefficients.Length != degrees.Length)
            {
                throw new ArgumentException("The number of coefficients does not correspond to the number of degrees");
            }
        }

        private static void VerifyCoefficients(double[] coefficients)
        {
            if (ReferenceEquals(coefficients, null))
            {
                throw new ArgumentNullException(nameof(coefficients));
            }

            if (coefficients.Length == 0)
            {
                throw new ArgumentException($"{nameof(coefficients)} length must be more than 0", nameof(coefficients));
            }
        }

        private static void VerifyDegrees(int[] degrees)
        {
            if (ReferenceEquals(degrees, null))
            {
                throw new ArgumentNullException(nameof(degrees));
            }

            if (degrees.Length == 0)
            {
                throw new ArgumentException($"{nameof(degrees)} length must be more than 0", nameof(degrees));
            }

            if (degrees.Any(degree => degree < 0))
            {
                throw new ArgumentException("degrees should be non-negative", nameof(degrees));
            }

            if (HasDuplicateElements(degrees))
            {
                throw new ArgumentException($"{nameof(degrees)} must have no duplicate elements", nameof(degrees));
            }

            if (!IsSortedInDescendingOrder(degrees))
            {
                throw new ArgumentException("Degrees should be sorted in descending order", nameof(degrees));
            }
        }

        private static bool HasDuplicateElements(int[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                for (int j = i + 1; j < array.Length; j++)
                {
                    if (array[i] == array[j])
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private static bool IsSortedInDescendingOrder(int[] array)
        {
            for (int i = 0; i < array.Length - 1; i++)
            {
                if (array[i] < array[i + 1])
                {
                    return false;
                }
            }

            return true;
        }

        #endregion // !verify input methods.

        #region add, subtract and multiply helpers

        private static Polynomial Operate(Polynomial first, Polynomial second, Func<double, double, double> operation, Func<double, double> inversion)
        {
            var polynomial1 = first.GetElements().ToArray();
            var polynomial2 = second.GetElements().ToArray();

            var degrees1 = GetDegrees(polynomial1);
            var degrees2 = GetDegrees(polynomial2);

            var resultPolynomial = new List<Tuple<double, int>>(degrees1.Length);

            OperateIdenticalDegree(resultPolynomial, degrees1, degrees2, polynomial1, polynomial2, operation);

            AddUniqueDegrees(degrees1, resultPolynomial, -2, polynomial1, null);
            AddUniqueDegrees(degrees2, resultPolynomial, -1, polynomial2, inversion);

            DescendingSortOrderByDegree(resultPolynomial);

            return new Polynomial(resultPolynomial);
        }

        private static void OperateIdenticalDegree(IList<Tuple<double, int>> resultPolynomial, int[] degrees1, int[] degrees2, Tuple<double, int>[] polynomial1, Tuple<double, int>[] polynomial2, Func<double, double, double> operation)
        {
            for (int i = 0; i < degrees1.Length; i++)
            {
                for (int j = 0; j < degrees2.Length; j++)
                {
                    if (degrees1[i] == degrees2[j])
                    {
                        degrees2[j] = -1;
                        double coefficient = operation(polynomial1[i].Item1, polynomial2[j].Item1);
                        resultPolynomial.Add(new Tuple<double, int>(coefficient, degrees1[i]));
                        degrees1[i] = -2;
                        break;
                    }
                }
            }
        }

        private static void AddUniqueDegrees(int[] degrees, IList<Tuple<double, int>> resultPolynomial, int sign, Tuple<double, int>[] source, Func<double, double> operation)
        {
            for (int i = 0; i < degrees.Length; i++)
            {
                if (degrees[i] != sign)
                {
                    resultPolynomial.Add(operation != null
                        ? new Tuple<double, int>(operation(source[i].Item1), degrees[i])
                        : new Tuple<double, int>(source[i].Item1, degrees[i]));
                }
            }
        }

        private static void DescendingSortOrderByDegree(IList<Tuple<double, int>> polynomial)
        {
            for (int i = 0; i < polynomial.Count - 1; i++)
            {
                for (int j = 0; j < polynomial.Count - 1; j++)
                {
                    if (polynomial[j].Item2 < polynomial[j + 1].Item2)
                    {
                        var temp = polynomial[j];
                        polynomial[j] = polynomial[j + 1];
                        polynomial[j + 1] = temp;
                    }
                }
            }
        }

        private static Polynomial OperatorX(Polynomial lhs, Polynomial rhs, bool isAddition)
        {
            if (ReferenceEquals(lhs, null))
            {
                throw new ArgumentNullException(nameof(lhs));
            }

            if (ReferenceEquals(rhs, null))
            {
                throw new ArgumentNullException(nameof(rhs));
            }

            return isAddition ? Add(lhs, rhs) : Subtract(lhs, rhs);
        }

        private static Polynomial OperatorX(Polynomial lhs, Tuple<double, int> rhs, bool isAddition)
        {
            if (ReferenceEquals(lhs, null))
            {
                throw new ArgumentNullException(nameof(lhs));
            }

            if (ReferenceEquals(rhs, null))
            {
                throw new ArgumentNullException(nameof(rhs));
            }

            return isAddition ? Add(lhs, rhs) : Subtract(lhs, rhs);
        }

        private static void MultiplyOperation(IList<Tuple<double, int>> resultPolynomial, double[] coefficients1, int[] degrees1, double[] coefficients2, int[] degrees2)
        {
            for (int i = 0; i < coefficients1.Length; i++)
            {
                for (int j = 0; j < coefficients2.Length; j++)
                {
                    double coefficient = coefficients1[i] * coefficients2[j];
                    int degree = degrees1[i] + degrees2[j];
                    resultPolynomial.Add(new Tuple<double, int>(coefficient, degree));
                }
            }
        }

        private static void SumDuplicateDegree(IList<Tuple<double, int>> resultPolynomial)
        {
            for (int i = 0; i < resultPolynomial.Count; i++)
            {
                for (int j = i + 1; j < resultPolynomial.Count; j++)
                {
                    if (resultPolynomial[i].Item2 == resultPolynomial[j].Item2)
                    {
                        double coefficient = resultPolynomial[i].Item1 + resultPolynomial[j].Item1;
                        int degree = resultPolynomial[i].Item2;
                        resultPolynomial.RemoveAt(j);
                        resultPolynomial.RemoveAt(i);
                        resultPolynomial.Insert(i, new Tuple<double, int>(coefficient, degree));
                    }
                }
            }
        }

        #endregion // !add, subtract and multiply helpers.

        #region other

        private void ConstructInstance(double[] coefficients, int[] degrees)
        {
            VerifyInput(coefficients, degrees);

            SetInputData(coefficients, degrees);
        }

        private void SetInputData(double[] coefficients, int[] degrees)
        {
            _coefficients = new double[coefficients.Length];
            Array.Copy(coefficients, _coefficients, _coefficients.Length);

            _degrees = new int[degrees.Length];
            Array.Copy(degrees, _degrees, degrees.Length);
        }

        #endregion // !other.

        #endregion // !private.
    }
}
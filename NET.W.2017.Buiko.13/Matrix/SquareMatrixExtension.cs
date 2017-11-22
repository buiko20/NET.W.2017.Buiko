using System;

namespace Matrix
{
    /// <summary>
    /// The class extending the functionality of a square abstractSquareMatrix.
    /// </summary>
    public static class SquareMatrixExtension
    {
        /// <summary>
        /// Summarizes two matrices.
        /// </summary>
        /// <typeparam name="T">matrix element type.</typeparam>
        /// <param name="matrix">first summand</param>
        /// <param name="addMatrix">second term</param>
        /// <returns>Sum of two matrices.</returns>
        /// <exception cref="ArgumentNullException">Exception thrown when <paramref name="addMatrix"/> is null.</exception>
        /// <exception cref="InvalidOperationException">Exception thrown when the addition operation is impossible.</exception>
        public static AbstractSquareMatrix<T> Add<T>(this AbstractSquareMatrix<T> matrix, AbstractSquareMatrix<T> addMatrix)
        {
            if (ReferenceEquals(addMatrix, null))
            {
                return matrix;
            }

            if (matrix.Order != addMatrix.Order)
            {
                throw new InvalidOperationException("It is possible to sum only matrices of the same size.");
            }

            try
            {
                return Add((dynamic)matrix, (dynamic)addMatrix);
            }
            catch (Exception e)
            {
                throw new NotSupportedException(e.Message, e);
            }
        }

        private static SquareMatrix<T> Add<T>(SquareMatrix<T> lhs, SquareMatrix<T> rhs)
        {
            var resultArray = new T[lhs.Order, lhs.Order];

            for (int i = 0; i < lhs.Order; i++)
            {
                for (int j = 0; j < lhs.Order; j++)
                {
                    resultArray[i, j] = (dynamic)lhs[i, j] + (dynamic)rhs[i, j];
                }
            }

            return new SquareMatrix<T>(resultArray);
        }

        private static SquareMatrix<T> Add<T>(SquareMatrix<T> lhs, SymmetricMatrix<T> rhs)
        {
            var resultArray = new T[lhs.Order, lhs.Order];

            for (int i = 0; i < lhs.Order; i++)
            {
                for (int j = i; j < lhs.Order; j++)
                {
                    resultArray[i, j] = (dynamic)lhs[i, j] + (dynamic)rhs[i, j];
                    resultArray[j, i] = (dynamic)lhs[j, i] + (dynamic)rhs[i, j];
                }
            }

            return new SquareMatrix<T>(resultArray);
        }

        private static SquareMatrix<T> Add<T>(SquareMatrix<T> lhs, DiagonalMatrix<T> rhs)
        {
            var result = new SquareMatrix<T>(lhs);

            for (int i = 0; i < result.Order; i++)
            {
                result[i, i] = (dynamic)result[i, i] + (dynamic)rhs[i, i];
            }

            return result;
        }

        private static DiagonalMatrix<T> Add<T>(DiagonalMatrix<T> lhs, DiagonalMatrix<T> rhs)
        {
            var resultArray = new T[lhs.Order];

            for (int i = 0; i < lhs.Order; i++)
            {
                resultArray[i] = (dynamic)lhs[i, i] + (dynamic)rhs[i, i];
            }

            return new DiagonalMatrix<T>(resultArray);
        }

        private static SquareMatrix<T> Add<T>(DiagonalMatrix<T> lhs, SquareMatrix<T> rhs) => Add(rhs, lhs);

        private static SymmetricMatrix<T> Add<T>(DiagonalMatrix<T> lhs, SymmetricMatrix<T> rhs)
        {
            var result = new SymmetricMatrix<T>(rhs);

            for (int i = 0; i < lhs.Order; i++)
            {
                result[i, i] = (dynamic)lhs[i, i] + (dynamic)result[i, i];
            }

            return result;
        }

        private static SymmetricMatrix<T> Add<T>(SymmetricMatrix<T> lhs, SymmetricMatrix<T> rhs)
        {
            var resultArray = new T[lhs.Order, lhs.Order];

            for (int i = 0; i < lhs.Order; i++)
            {
                for (int j = i; j < lhs.Order; j++)
                {
                    resultArray[i, j] = (dynamic)lhs[i, j] + (dynamic)rhs[i, j];
                }
            }

            return new SymmetricMatrix<T>(resultArray);
        }

        private static SymmetricMatrix<T> Add<T>(SymmetricMatrix<T> lhs, DiagonalMatrix<T> rhs) => Add(rhs, lhs);

        private static SquareMatrix<T> Add<T>(SymmetricMatrix<T> lhs, SquareMatrix<T> rhs) => Add(rhs, lhs);
    }
}

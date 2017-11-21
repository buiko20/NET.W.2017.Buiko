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
                Type resultMatrixType = ComputeResultantMatrixType(matrix, addMatrix);
                return Add(matrix, addMatrix, resultMatrixType);
            }
            catch (Exception e)
            {
                throw new NotSupportedException(e.Message, e);
            }
        }

        private static AbstractSquareMatrix<T> Add<T>(AbstractSquareMatrix<T> matrix1, AbstractSquareMatrix<T> matrix2, Type resultMatrixType)
        {
            var resultArray = new T[matrix1.Order, matrix2.Order];

            for (int i = 0; i < matrix1.Order; i++)
            {
                for (int j = 0; j < matrix1.Order; j++)
                {
                    resultArray[i, j] = (dynamic)matrix1[i, j] + (dynamic)matrix2[i, j];
                }
            }

            return (AbstractSquareMatrix<T>)Activator.CreateInstance(resultMatrixType, resultArray);
        }

        private static Type ComputeResultantMatrixType<T>(AbstractSquareMatrix<T> matrix1, AbstractSquareMatrix<T> matrix2)
        {
            if ((matrix1.GetType() == typeof(SquareMatrix<T>)) || (matrix2.GetType() == typeof(SquareMatrix<T>)))
            {
                return typeof(SquareMatrix<T>);
            }

            if ((matrix1.GetType() == typeof(SymmetricMatrix<T>)) || (matrix2.GetType() == typeof(SymmetricMatrix<T>)))
            {
                return typeof(SymmetricMatrix<T>);
            }

            return typeof(DiagonalMatrix<T>);
        }
    }
}

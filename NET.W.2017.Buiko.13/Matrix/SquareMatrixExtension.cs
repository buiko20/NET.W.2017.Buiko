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
            try
            {
                return matrix.Add(addMatrix, (arg1, arg2) => (dynamic)arg1 + (dynamic)arg2);
            }
            catch (Exception e)
            {
                throw new InvalidOperationException(e.Message, e);
            }
        }

        /// <summary>
        /// Summarizes two matrices.
        /// </summary>
        /// <typeparam name="T">matrix element type.</typeparam>
        /// <param name="matrix">first summand</param>
        /// <param name="addMatrix">second term</param>
        /// <param name="sumOperation">element sum operation</param>
        /// <returns>Sum of two matrices.</returns>
        /// <exception cref="ArgumentNullException">Exception thrown when <paramref name="addMatrix"/> is null.</exception>
        /// <exception cref="InvalidOperationException">Exception thrown when the addition operation is impossible.</exception>
        public static AbstractSquareMatrix<T> Add<T>(this AbstractSquareMatrix<T> matrix, AbstractSquareMatrix<T> addMatrix, Func<T, T, T> sumOperation)
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
                var resultArray = new T[matrix.Order, matrix.Order];

                for (int i = 0; i < matrix.Order; i++)
                {
                    for (int j = 0; j < matrix.Order; j++)
                    {
                        resultArray[i, j] = sumOperation(matrix[i, j], addMatrix[i, j]);
                    }
                }

                return (AbstractSquareMatrix<T>)Activator.CreateInstance(matrix.GetType(), resultArray);
            }
            catch (Exception e)
            {
                throw new InvalidOperationException(e.Message, e);
            }
        }
    }
}

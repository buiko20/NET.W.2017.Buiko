using System;

namespace Matrix
{
    /// <inheritdoc />
    /// <summary>
    /// A class representing a square matrix.
    /// </summary>
    public class SquareMatrix<T> : Matrix<T>
    {
        #region constructors

        /// <summary>
        /// Reserved matrix of <paramref name="order"/>.
        /// </summary>
        /// <param name="order">matrix order</param>
        /// <exception cref="ArgumentException">Exception thrown when <paramref name="order"/> is less than
        /// or equal to 0.</exception>
        public SquareMatrix(int order)
        {
            if (order <= 0)
            {
                throw new ArgumentException($"{nameof(order)} must be greater than 0", nameof(order));
            }

            this.matrix = new T[order, order];
            this.RowCount = order;
            this.ColumnCount = order;
        }

        /// <summary>
        /// Creates a matrix based on an <paramref name="sorceMatrix"/>.
        /// </summary>
        /// <param name="sorceMatrix">array of matrix elements</param>
        /// <exception cref="ArgumentNullException">Exception thrown when <paramref name="sorceMatrix"/> is null.</exception>
        /// <exception cref="ArgumentException">Exception thrown when matrix size is not compatible.</exception>
        public SquareMatrix(T[,] sorceMatrix)
        {
            if (object.ReferenceEquals(sorceMatrix, null))
            {
                throw new ArgumentNullException(nameof(sorceMatrix));
            }

            if (sorceMatrix.GetLength(0) != sorceMatrix.GetLength(1))
            {
                throw new ArgumentException($"{nameof(sorceMatrix)} is not square.", nameof(sorceMatrix));
            }

            this.RowCount = sorceMatrix.GetLength(0);
            this.ColumnCount = sorceMatrix.GetLength(1);

            this.matrix = new T[this.RowCount, this.RowCount];
            Array.Copy(sorceMatrix, this.matrix, this.RowCount * this.RowCount);
        }

        /// <summary>
        /// Creates a matrix based on an <paramref name="sorceMatrix"/>.
        /// </summary>
        /// <param name="sorceMatrix">array of matrix elements</param>
        /// <exception cref="ArgumentNullException">Exception thrown when <paramref name="sorceMatrix"/> is null.</exception>
        /// <exception cref="ArgumentException">Exception thrown when matrix size is not compatible.</exception>
        public SquareMatrix(Matrix<T> sorceMatrix)
        {
            if (object.ReferenceEquals(sorceMatrix, null))
            {
                throw new ArgumentNullException(nameof(sorceMatrix));
            }

            if (sorceMatrix.RowCount != sorceMatrix.ColumnCount)
            {
                throw new ArgumentException($"{nameof(sorceMatrix)} is not square.", nameof(sorceMatrix));
            }

            this.RowCount = sorceMatrix.RowCount;
            this.ColumnCount = sorceMatrix.ColumnCount;

            this.matrix = new T[this.RowCount, this.RowCount];
            this.CopyMatrix(sorceMatrix);
        }

        #endregion // !constructors.

        #region protected

        /// <inheritdoc />
        protected override void SetValue(T value, int i, int j) =>
            this.matrix[i, j] = value;

        #endregion // !protected.

        #region private

        private void CopyMatrix(Matrix<T> sourceMatrix)
        {
            for (int i = 0; i < this.RowCount; i++)
            {
                for (int j = 0; j < this.ColumnCount; j++)
                {
                    this.matrix[i, j] = sourceMatrix[i, j];
                }
            }
        }

        #endregion // !private.
    }
}

using System;

namespace Matrix
{
    /// <inheritdoc />
    /// <summary>
    /// A class representing a square matrix.
    /// </summary>
    public class SquareMatrix<T> : AbstractSquareMatrix<T>
    {
        #region private fileds

        private readonly T[,] matrix;

        #endregion

        #region constructors

        /// <inheritdoc />
        public SquareMatrix(int order) : base(order)
        {
            this.matrix = new T[order, order];
        }

        /// <inheritdoc />
        public SquareMatrix(T[,] sorceMatrix) : base(sorceMatrix)
        {
            this.matrix = new T[this.Order, this.Order];
            Array.Copy(sorceMatrix, this.matrix, this.Order * this.Order);
        }

        /// <inheritdoc />
        public SquareMatrix(AbstractSquareMatrix<T> sorceMatrix) : base(sorceMatrix)
        {
            this.matrix = sorceMatrix.ToArray();
        }

        #endregion // !constructors.

        #region protected

        /// <inheritdoc />
        protected override void SetValue(T value, int i, int j) =>
            this.matrix[i, j] = value;

        /// <inheritdoc />
        protected override T GetValue(int i, int j) =>
            this.matrix[i, j];

        #endregion // !protected.
    }
}

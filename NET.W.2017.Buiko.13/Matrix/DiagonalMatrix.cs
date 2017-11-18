namespace Matrix
{
    public class DiagonalMatrix<T> : SquareMatrix<T>
    {
        #region constructors

        /// <inheritdoc />
        public DiagonalMatrix(int order) : base(order)
        {
        }

        /// <inheritdoc />
        /// <summary>
        /// Creates a matrix by transforming the <paramref name="sorceMatrix"/> into a diagonal matrix.
        /// </summary>
        public DiagonalMatrix(T[,] sorceMatrix) : base(sorceMatrix)
        {
            this.MakeMatrixDiagonal();
        }

        /// <inheritdoc />
        /// <summary>
        /// Creates a matrix by transforming the <paramref name="sorceMatrix"/> into a diagonal matrix.
        /// </summary>
        public DiagonalMatrix(Matrix<T> sorceMatrix) : base(sorceMatrix)
        {
            this.MakeMatrixDiagonal();
        }

        #endregion // !constructors.

        #region protected

        /// <inheritdoc />
        protected override void SetValue(T value, int i, int j)
        {
            if (i == j)
            {
                this.matrix[i, j] = value;
            }
        }

        #endregion // !protected.

        #region private

        private void MakeMatrixDiagonal()
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    if (i != j)
                    {
                        this.matrix[i, j] = default(T);
                    }
                }
            }
        }

        #endregion
    }
}

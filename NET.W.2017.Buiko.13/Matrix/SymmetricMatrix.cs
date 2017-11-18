namespace Matrix
{
    /// <inheritdoc />
    /// <summary>
    /// Class representing a symmetric matrix.
    /// </summary>
    public class SymmetricMatrix<T> : SquareMatrix<T>
    {
        #region constructors

        /// <inheritdoc />
        public SymmetricMatrix(int order) : base(order)
        {
        }

        /// <inheritdoc />
        /// <summary>
        /// Creates a matrix by transforming the <paramref name="sorceMatrix"/> into a symmetric matrix.
        /// </summary>
        public SymmetricMatrix(T[,] sorceMatrix) : base(sorceMatrix)
        {
            this.MakeMatrixSymmetric(true);
        }

        /// <inheritdoc />
        /// <summary>
        /// Creates a matrix by transforming the <paramref name="sorceMatrix"/> into a symmetric matrix.
        /// </summary>
        public SymmetricMatrix(Matrix<T> sorceMatrix) : base(sorceMatrix)
        {
            this.MakeMatrixSymmetric(true);
        }

        #endregion // !constructors.

        #region private

        private void MakeMatrixSymmetric(bool isMakeLeftSymmetric)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    if (isMakeLeftSymmetric)
                    {
                        this.matrix[i, j] = this.matrix[j, i];
                    }
                    else
                    {
                        this.matrix[j, i] = this.matrix[i, j];
                    }
                }
            }
        }

        #endregion // !private.
    }
}

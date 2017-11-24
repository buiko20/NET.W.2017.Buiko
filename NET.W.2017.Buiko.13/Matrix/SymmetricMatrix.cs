namespace Matrix
{
    /// <summary>
    /// Class representing symmetric matrix.
    /// </summary>
    public class SymmetricMatrix<T> : AbstractSquareMatrix<T>
    {
        #region private fields

        private readonly T[] matrix;

        #endregion // !private fields.

        #region constructors

        /// <inheritdoc />
        public SymmetricMatrix(int order) : base(order)
        {
            int matrixSize = ComputeMatrixArraySize(order);
            this.matrix = new T[matrixSize];
        }

        /// <inheritdoc />
        /// <summary>
        /// Creates a matrix by transforming the <paramref name="sorceMatrix"/> into a symmetric matrix.
        /// </summary>
        public SymmetricMatrix(T[,] sorceMatrix) : base(sorceMatrix)
        {
            int k = ComputeMatrixArraySize(this.Order);
            this.matrix = new T[k];

            k = 0;
            for (int i = 0; i < this.Order; i++)
            {
                for (int j = i; j < this.Order; j++)
                {
                    this.matrix[k++] = sorceMatrix[i, j];
                }
            }
        }

        /// <inheritdoc />
        /// <summary>
        /// Creates a matrix by transforming the <paramref name="sorceMatrix"/> into a symmetric matrix.
        /// </summary>
        public SymmetricMatrix(AbstractSquareMatrix<T> sorceMatrix) : base(sorceMatrix)
        {
            int k = ComputeMatrixArraySize(this.Order);
            this.matrix = new T[k];

            k = 0;
            for (int i = 0; i < this.Order; i++)
            {
                for (int j = i; j < this.Order; j++)
                {
                    this.matrix[k++] = sorceMatrix[i, j];
                }
            }
        }

        #endregion // !constructors.

        #region protected

        /// <inheritdoc />
        protected override void SetValue(T value, int i, int j)
        {
            if (j < i)
            {
                i ^= j;
                j ^= i;
                i ^= j;
            }

            int index = ComputeIndex(i, j, this.Order);
            this.matrix[index] = value;
        }

        /// <inheritdoc />
        protected override T GetValue(int i, int j)
        {
            int index = j >= i ? ComputeIndex(i, j, this.Order) 
                               : ComputeIndex(j, i, this.Order);
            return this.matrix[index];
        }

        #endregion // !protected.

        #region private

        private static int ComputeMatrixArraySize(int matrixOrder)
        {
            int result = 0;

            while (matrixOrder > 0)
            {
                result += matrixOrder;
                matrixOrder--;
            }

            return result;
        }

        private static int ComputeIndex(int i, int j, int order)
        {
            int result = 0;
            for (int k = 0; k < order; k++)
            {
                for (int z = k; z < order; z++)
                {
                    if ((i == k) && (j == z))
                    {
                        return result;
                    }

                    result++;
                }
            }

            return result;
        }

        #endregion // !private.
    }
}

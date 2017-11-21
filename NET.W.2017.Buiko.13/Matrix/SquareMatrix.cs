namespace Matrix
{
    /// <inheritdoc />
    /// <summary>
    /// A class representing a square matrix.
    /// </summary>
    public class SquareMatrix<T> : AbstractSquareMatrix<T>
    {
        #region private fileds

        private readonly T[] matrix;

        #endregion

        #region constructors

        /// <inheritdoc />
        public SquareMatrix(int order) : base(order)
        {
            this.matrix = new T[order * order];
        }

        /// <inheritdoc />
        public SquareMatrix(T[,] sorceMatrix) : base(sorceMatrix)
        {
            this.matrix = new T[this.Order * this.Order];
            for (int i = 0; i < this.Order; i++)
            {
                for (int j = 0; j < this.Order; j++)
                {
                    int index = ComputeIndex(i, j, this.Order);
                    this.matrix[index] = sorceMatrix[i, j];
                }
            }
        }

        /// <inheritdoc />
        public SquareMatrix(AbstractSquareMatrix<T> sorceMatrix) : base(sorceMatrix)
        {
            this.matrix = new T[this.Order * this.Order];
            for (int i = 0; i < this.Order; i++)
            {
                for (int j = 0; j < this.Order; j++)
                {
                    int index = ComputeIndex(i, j, this.Order);
                    this.matrix[index] = sorceMatrix[i, j];
                }
            }
        }

        #endregion // !constructors.

        #region protected

        /// <inheritdoc />
        protected override void SetValue(T value, int i, int j) =>
            this.matrix[ComputeIndex(i, j, this.Order)] = value;

        /// <inheritdoc />
        protected override T GetValue(int i, int j) =>
            this.matrix[ComputeIndex(i, j, this.Order)];

        #endregion // !protected.

        #region private

        private static int ComputeIndex(int i, int j, int columnCount) =>
            (i * columnCount) + j;

        #endregion // !private.
    }
}

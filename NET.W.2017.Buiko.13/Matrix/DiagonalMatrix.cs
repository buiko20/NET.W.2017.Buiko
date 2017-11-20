using System;

namespace Matrix
{
    public class DiagonalMatrix<T> : AbstractSquareMatrix<T>
    {
        #region private fields

        /*
         
        |1 2 3|
        |4 5 6| --> matrix = { 1, 5, 9 };
        |7 8 9|

        */

        private readonly T[] diagonalElements;

        #endregion // !private fields.

        #region constructors

        /// <inheritdoc />
        public DiagonalMatrix(int order) : base(order)
        {
            this.diagonalElements = new T[order];
        }

        /// <inheritdoc />
        /// <summary>
        /// Creates a matrix by transforming the <paramref name="sorceMatrix"/> into a diagonal matrix.
        /// </summary>
        public DiagonalMatrix(T[,] sorceMatrix) : base(sorceMatrix)
        {
            this.diagonalElements = new T[this.Order];
            for (int i = 0; i < this.Order; i++)
            {
                this.diagonalElements[i] = sorceMatrix[i, i];
            }
        }

        /// <inheritdoc />
        /// <summary>
        /// Creates a matrix by transforming the <paramref name="sorceMatrix"/> into a diagonal matrix.
        /// </summary>
        public DiagonalMatrix(AbstractSquareMatrix<T> sorceMatrix) : base(sorceMatrix)
        {
            this.diagonalElements = new T[this.Order];
            for (int i = 0; i < this.Order; i++)
            {
                this.diagonalElements[i] = sorceMatrix[i, i];
            }
        }

        /// <summary>
        /// Creates a diagonal matrix based on <paramref name="matrixDiagonalElements"/>.
        /// </summary>
        /// <param name="matrixDiagonalElements">matrix diagonal elements</param>
        /// <exception cref="ArgumentNullException">Exception thrown when <paramref name="matrixDiagonalElements"/> is null.</exception>
        /// <exception cref="ArgumentException">Exception thrown when <paramref name="matrixDiagonalElements"/>
        /// length equals to 0.</exception>
        public DiagonalMatrix(T[] matrixDiagonalElements) : base()
        {
            if (object.ReferenceEquals(matrixDiagonalElements, null))
            {
                throw new ArgumentNullException(nameof(matrixDiagonalElements));
            }

            if (matrixDiagonalElements.Length == 0)
            {
                throw new ArgumentException($"{nameof(matrixDiagonalElements)} is empty", nameof(matrixDiagonalElements));
            }

            this.Order = matrixDiagonalElements.Length;
            this.diagonalElements = new T[this.Order];
            Array.Copy(matrixDiagonalElements, this.diagonalElements, this.Order);
        }

        #endregion // !constructors.

        #region protected

        /// <inheritdoc />
        protected override void SetValue(T value, int i, int j)
        {
            if (i == j)
            {
                this.diagonalElements[i] = value;
            }
        }

        /// <inheritdoc />
        protected override T GetValue(int i, int j) =>
            i == j ? this.diagonalElements[i] : default(T);

        #endregion // !protected.
    }
}

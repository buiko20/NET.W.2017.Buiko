using System;
using System.Collections;
using System.Collections.Generic;

namespace Matrix
{
    /// <summary>
    /// Abstract class representing the square matrix.
    /// </summary>
    /// <typeparam name="T">Matrix element type.</typeparam>
    public abstract class AbstractSquareMatrix<T> : IEnumerable<T>, IEnumerable
    {
        #region public 

        #region constructors

        protected AbstractSquareMatrix()
        {
        }

        /// <summary>
        /// Initialize matrix of <paramref name="order"/>.
        /// </summary>
        /// <param name="order">matrix order</param>
        /// <exception cref="ArgumentException">Exception thrown when <paramref name="order"/> 
        /// is less than or equal to 0.</exception>
        protected AbstractSquareMatrix(int order)
        {
            if (order <= 0)
            {
                throw new ArgumentException($"{nameof(order)} must be greater than 0", nameof(order));
            }

            this.Order = order;
        }

        /// <summary>
        /// Creates a matrix based on an <paramref name="sorceMatrix"/>.
        /// </summary>
        /// <param name="sorceMatrix">array of matrix elements</param>
        /// <exception cref="ArgumentNullException">Exception thrown when <paramref name="sorceMatrix"/> is null.</exception>
        /// <exception cref="ArgumentException">Exception thrown when matrix size is not compatible.</exception>
        protected AbstractSquareMatrix(T[,] sorceMatrix)
        {
            if (ReferenceEquals(sorceMatrix, null))
            {
                throw new ArgumentNullException(nameof(sorceMatrix));
            }

            if (sorceMatrix.GetLength(0) != sorceMatrix.GetLength(1))
            {
                throw new ArgumentException($"{nameof(sorceMatrix)} is not square.", nameof(sorceMatrix));
            }

            this.Order = sorceMatrix.GetLength(0);
        }

        /// <summary>
        /// Creates a matrix based on an <paramref name="sorceMatrix"/>.
        /// </summary>
        /// <param name="sorceMatrix">array of matrix elements</param>
        /// <exception cref="ArgumentNullException">Exception thrown when <paramref name="sorceMatrix"/> is null.</exception>
        /// <exception cref="ArgumentException">Exception thrown when matrix size is not compatible.</exception>
        protected AbstractSquareMatrix(AbstractSquareMatrix<T> sorceMatrix)
        {
            if (ReferenceEquals(sorceMatrix, null))
            {
                throw new ArgumentNullException(nameof(sorceMatrix));
            }

            this.Order = sorceMatrix.Order;
        }

        #endregion // !constructors.

        #region other

        /// <summary>
        /// The event that occurs by changing the elements of the matrix.
        /// </summary>
        public event EventHandler<MatrixEventArgs<T>> ElementChanged = delegate { };

        /// <summary>
        /// Order of the matrix.
        /// </summary>
        public int Order { get; protected set; }

        /// <summary>
        /// Indexer for the matrix.
        /// </summary>
        /// <param name="i">row element index</param>
        /// <param name="j">column element index</param>
        /// <returns>Element of a matrix defined by indices.</returns>
        /// <exception cref="ArgumentNullException">Exception thrown when <paramref name="value"/> is null.</exception>
        /// <exception cref="AggregateException">Exception thrown when <paramref name="i"/> or 
        /// <paramref name="j"/> is invalid.</exception>
        public T this[int i, int j]
        {
            get
            {
                this.VerifyIndexes(i, j);
                return this.GetValue(i, j);
            }

            set
            {
                this.VerifyIndexes(i, j);

                var oldValue = this.GetValue(i, j);
                this.SetValue(value, i, j);
                this.OnElementChanged(this, new MatrixEventArgs<T>(i, j, oldValue, value));
            }
        }

        /// <summary>
        /// Returns an array of matrix elements.
        /// </summary>
        /// <returns>Array of matrix elements.</returns>
        public T[,] ToArray()
        {
            var result = new T[this.Order, this.Order];

            for (int i = 0; i < this.Order; i++)
            {
                for (int j = 0; j < this.Order; j++)
                {
                    result[i, j] = this.GetValue(i, j);
                }
            }

            return result;
        }

        #endregion // !other.

        #region interface implementation

        /// <inheritdoc />
        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < this.Order; i++)
            {
                for (int j = 0; j < this.Order; j++)
                {
                    yield return this.GetValue(i, j);
                }
            }
        }

        /// <inheritdoc />
        IEnumerator IEnumerable.GetEnumerator() =>
            this.GetEnumerator();

        #endregion // !interface implementation.

        #endregion // !public.

        #region protected

        protected abstract void SetValue(T value, int i, int j);

        protected abstract T GetValue(int i, int j);

        protected virtual void OnElementChanged(object sender, MatrixEventArgs<T> eventArgs)
        {
            EventHandler<MatrixEventArgs<T>> temp = this.ElementChanged;
            temp?.Invoke(sender, eventArgs);
        }

        #endregion // !protected.

        #region private

        private void VerifyIndexes(int i, int j)
        {
            if (i < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(i), $"{nameof(i)} must be greater than or equal to 0");
            }

            if (i > this.Order)
            {
                throw new ArgumentOutOfRangeException(nameof(i), $"{nameof(i)} must be less than abstractSquareMatrix order");
            }

            if (j < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(j), $"{nameof(j)} must be greater than or equal to 0");
            }

            if (j > this.Order)
            {
                throw new ArgumentOutOfRangeException(nameof(j), $"{nameof(j)} must be less than abstractSquareMatrix order");
            }
        }

        #endregion // !private.
    }
}

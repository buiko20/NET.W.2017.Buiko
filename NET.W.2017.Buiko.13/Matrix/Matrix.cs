using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Matrix
{
    /// <summary>
    /// Abstract class representing the matrix.
    /// </summary>
    /// <typeparam name="T">Matrix element type.</typeparam>
    public abstract class Matrix<T> : IEnumerable<T>, IEquatable<Matrix<T>>, IEnumerable
    {
        #region fields

        /// <summary>
        /// An array containing matrix elements.
        /// </summary>
        protected T[,] matrix;

        /// <summary>
        /// The event that occurs by changing the elements of the matrix.
        /// </summary>
        public event EventHandler<MatrixEventArgs<T>> ElementChanged = delegate { };

        #endregion // !fields.

        #region properties

        /// <summary>
        /// Number of rows in the matrix.
        /// </summary>
        public int RowCount { get; protected set; }

        /// <summary>
        /// Number of columns in the matrix.
        /// </summary>
        public int ColumnCount { get; protected set; }

        /// <summary>
        /// Total number of elements in the matrix.
        /// </summary>
        public int Count => this.RowCount * this.ColumnCount;

        #endregion // !properties.

        #region indexer

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
                VerifyIndexes(i, j);
                return matrix[i, j];
            }

            set
            {
                VerifyIndexes(i, j);

                if (ReferenceEquals(value, null))
                {
                    throw new ArgumentNullException(nameof(value));
                }

                var oldValue = matrix[i, j];
                SetValue(value, i, j);
                OnElementChanged(this, new MatrixEventArgs<T>(i, j, oldValue, value));
            }
        }

        #endregion // !indexer.

        #region interface implementation

        /// <inheritdoc />
        public bool Equals(Matrix<T> other)
        {
            if (ReferenceEquals(this, other))
            {
                return true;
            }

            if (ReferenceEquals(other, null))
            {
                return false;
            }

            if ((other.RowCount != this.RowCount) || (other.ColumnCount != this.ColumnCount))
            {
                return false;
            }

            var equalityComparer = EqualityComparer<T>.Default;
            for (int i = 0; i < this.RowCount; i++)
            {
                for (int j = 0; j < this.ColumnCount; j++)
                {
                    if (!equalityComparer.Equals(this[i, j], other[i, j]))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        /// <inheritdoc />
        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    yield return matrix[i, j];
                }
            }
        }

        /// <inheritdoc />
        IEnumerator IEnumerable.GetEnumerator() =>
            this.GetEnumerator();

        #endregion // !interface implementation.

        #region object override

        /// <inheritdoc />
        public override string ToString()
        {
            var result = new StringBuilder(this.RowCount * this.ColumnCount);

            for (int i = 0; i < this.RowCount; i++)
            {
                for (int j = 0; j < this.ColumnCount; j++)
                {
                    result.Append(this.matrix[i, j]);
                }

                result.Append(Environment.NewLine);
            }

            return result.ToString();
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (ReferenceEquals(obj, null))
            {
                return false;
            }

            if (this.GetType() != obj.GetType())
            {
                return false;
            }

            return this.Equals((Matrix<T>)obj);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = this.matrix != null ? this.matrix.GetHashCode() : 0;
                hashCode = (hashCode * 397) ^ this.RowCount;
                hashCode = (hashCode * 397) ^ this.ColumnCount;
                return hashCode;
            }
        }

        #endregion // !object override.

        #region other

        /// <summary>
        /// Returns an array of matrix elements.
        /// </summary>
        /// <returns>Array of matrix elements.</returns>
        public T[,] ToArray()
        {
            var result = new T[this.RowCount, this.ColumnCount];

            Array.Copy(this.matrix, result, this.RowCount * this.ColumnCount);

            return result;
        }

        #endregion // !other.

        #region protected

        /// <summary>
        /// The method that establishes the value of the matrix cell.
        /// </summary>
        /// <param name="value">element value</param>
        /// <param name="i">row element index</param>
        /// <param name="j">column element index</param>
        protected abstract void SetValue(T value, int i, int j);

        protected virtual void OnElementChanged(object sender, MatrixEventArgs<T> eventArgs)
        {
            EventHandler<MatrixEventArgs<T>> temp = ElementChanged;
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

            if (i > matrix.GetLength(0))
            {
                throw new ArgumentOutOfRangeException(nameof(i), $"{nameof(i)} must be less than matrix size");
            }

            if (j < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(j), $"{nameof(j)} must be greater than or equal to 0");
            }

            if (j > matrix.GetLength(1))
            {
                throw new ArgumentOutOfRangeException(nameof(j), $"{nameof(j)} must be less than matrix size");
            }
        }

        #endregion // !private.
    }
}

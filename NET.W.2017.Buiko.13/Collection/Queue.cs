using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Collection
{
    /// <summary>
    /// Class representing the basic functionality of the queue.
    /// </summary>
    /// <remarks>
    /// Implemented <see cref="ICollection"/> instead of <see cref="ICollection{T}"/> because 
    /// <see cref="ICollection{T}"/> contains the <see cref="ICollection{T}.Remove"/> method, 
    /// which removes an element from anywhere in the collection, which contradicts the prince of the queue.
    /// </remarks>
    /// <typeparam name="T">Queue element type.</typeparam>
    public class Queue<T> : IEnumerable<T>, IReadOnlyCollection<T>, IEnumerable, ICollection
    {
        #region private fields

        // Array size increase factor.
        private const int Coefficient = 2;

        private const int DefaultCapacity = 10;

        private T[] _queue;
        private int _head;       
        private int _tail;       
        private int _size;       
        private int _version;

        #endregion // !private fields.

        #region public

        #region constructors

        /// <summary>
        /// Initializes a queue with a default capacity of 10.
        /// </summary>
        public Queue()
        {
            _queue = new T[DefaultCapacity];
        }

        /// <summary>
        ///  Initializes a queue with a specified <paramref name="capacity"/>.
        /// </summary>
        /// <param name="capacity">queue capacity</param>
        /// <exception cref="ArgumentException">Exception thrown when 
        /// <paramref name="capacity"/> is less than 0.</exception>
        public Queue(int capacity)
        {
            if (capacity < 0)
            {
                throw new ArgumentException($"{nameof(capacity)} must be greater than or equal to zero", nameof(capacity));
            }

            _queue = new T[capacity];
        }

        /// <summary>
        /// Initializes the queue with the specified <paramref name="collection"/>.
        /// </summary>
        /// <param name="collection">queue element enumeration</param>
        /// <exception cref="ArgumentNullException">Exception thrown when <paramref name="collection"/> is null.</exception>
        public Queue(IEnumerable<T> collection)
        {
            if (ReferenceEquals(collection, null))
            {
                throw new ArgumentNullException(nameof(collection));
            }

            var enumerable = collection as T[] ?? collection.ToArray();
            _queue = new T[enumerable.Length];
            _size = _queue.Length;
            _tail = _size;
            Array.Copy(enumerable, _queue, _queue.Length);
        }

        #endregion // !constructors.

        #region interface implementation

        #region ICollection

        /// <inheritdoc />
        public int Count
        {
            get
            {
                return _size;
            }
        }

        /// <inheritdoc />
        public bool IsSynchronized => false;

        /// <inheritdoc />
        public object SyncRoot { get; } = new object();

        /// <inheritdoc />
        void ICollection.CopyTo(Array array, int index)
        {
            VerifyInput(array, index);

            try
            {
                // Array.Copy will verify the validity of the input parameters.
                Array.Copy(_queue, array, _queue.Length);
            }
            catch (Exception e)
            {
                throw new ArgumentException(e.Message);
            }
        }

        #endregion // !ICollection.
        
        #region IEnumerable

        public Enumerator GetEnumerator() =>
            new Enumerator(this);

        /// <inheritdoc />
        IEnumerator<T> IEnumerable<T>.GetEnumerator() =>
            this.GetEnumerator();

        /// <inheritdoc />
        IEnumerator IEnumerable.GetEnumerator() =>
            this.GetEnumerator();

        #endregion // !IEnumerable.

        #endregion // !interface implementation.

        #region other queue methods

        /// <summary>
        /// Copies the elements of the queue into an <paramref name="array"/> starting from <paramref name="index"/>.
        /// </summary>
        /// <param name="array">array where the elements of the queue will be copied</param>
        /// <param name="index">index in the array from which the elements of the queue will be written</param>
        /// <exception cref="ArgumentNullException">Exception thrown when <paramref name="array"/> is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Exception thrown when <paramref name="index"/> less than 0 or
        /// <paramref name="index"/> greater than <paramref name="array"/> length.</exception>
        /// <exception cref="ArgumentException">Exception thrown when <paramref name="array"/> 
        /// and <paramref name="index"/> are not valid for the copy operation.</exception>
        public void CopyTo(T[] array, int index)
        {
            if (ReferenceEquals(array, null))
            {
                throw new ArgumentNullException(nameof(array));
            }

            if ((index < 0) || (index > array.Length))
            {
                throw new ArgumentOutOfRangeException(nameof(index), $"{nameof(index)} must be between 0 and {array.Length}.");
            }

            try
            {
                // Array.Copy will verify the validity of the input parameters.
                Array.Copy(_queue, array, _queue.Length);
            }
            catch (Exception e)
            {
                throw new ArgumentException(e.Message);
            }
        }

        /// <summary>
        /// Clears the queue.
        /// </summary>
        public void Clear()
        {
            _head = 0;
            _tail = 0;
            _size = 0;
            _version++;
            Array.Clear(_queue, 0, _queue.Length);
        }

        /// <summary>
        /// Inserts an <paramref name="item"/> at the end of the queue.
        /// </summary>
        /// <param name="item">element for insertion.</param>
        /// <exception cref="ArgumentNullException">Exception thrown when <paramref name="item"/> is null.</exception>
        public void Enqueue(T item)
        {
            if (ReferenceEquals(item, null))
            {
                throw new ArgumentNullException(nameof(item));
            }

            if (_size == _queue.Length)
            {
                Array.Resize(ref _queue, _queue.Length * Coefficient);
            }

            _queue[_tail++] = item;
            _size++;
            _version++;
        }

        /// <summary>
        /// Returns an element from the beginning of the queue.
        /// </summary>
        /// <returns>Element from the beginning of the queue.</returns>
        /// <exception cref="InvalidOperationException">Exception thrown when queue is empty.</exception>
        public T Dequeue()
        {
            if (_queue.Length == 0)
            {
                throw new InvalidOperationException("Queue is empty.");
            }

            var result = _queue[_head];
            _queue[_head++] = default(T);
            _size--;
            _version++;
            return result;
        }

        /// <summary>
        /// Returns an element from the beginning of the queue but does not delete it.
        /// </summary>
        /// <returns>Element from the beginning of the queue.</returns>
        /// <exception cref="InvalidOperationException">Exception thrown when queue is empty.</exception>
        public T Peek()
        {
            if (_queue.Length == 0)
            {
                throw new InvalidOperationException("Queue is empty.");
            }

            return _queue[_head];
        }

        /// <summary>
        /// Checks if an <paramref name="item"/> exists in the queue.
        /// </summary>
        /// <param name="item">search item</param>
        /// <returns>True if such an <paramref name="item"/> is in the queue, false otherwise.</returns>
        /// <exception cref="ArgumentNullException">Exception thrown when <paramref name="item"/> is null.</exception>
        public bool Contains(T item)
        {
            if (ReferenceEquals(item, null))
            {
                throw new ArgumentNullException(nameof(item));
            }

            // Contains use EqualityComparer<T>.Default.
            return _queue.Contains(item);
        }

        /// <summary>
        /// Copies the elements of the queue into an array.
        /// </summary>
        /// <returns>Array of queue elements</returns>
        public T[] ToArray() => _queue.ToArray();

        /// <summary>
        /// Cuts the extra capacity of the queue.
        /// </summary>
        public void TrimExcess()
        {
            var newQueue = new T[_size];
            Array.Copy(_queue, _head, newQueue, 0, _size);
            _queue = newQueue;
        }

        #endregion // !other queue methods.

        #endregion // !public.

        #region private

        private void VerifyInput(Array array, int index)
        {
            if (ReferenceEquals(array, null))
            {
                throw new ArgumentNullException(nameof(array));
            }

            if ((index < 0) || (index > array.Length))
            {
                throw new ArgumentOutOfRangeException(nameof(index), $"{nameof(index)} must be between 0 and {array.Length}.");
            }

            if (array.Rank != 1)
            {
                throw new ArgumentException($"{nameof(array)} must be one-dimensional.", nameof(array));
            }

            if (array.GetLowerBound(0) != 0)
            {
                throw new ArgumentException($"The indexing of an {nameof(array)} must start from 0", nameof(array));
            }
        }

        private T GetElement(int index) => _queue[index];

        #endregion // !private.

        /// <inheritdoc cref="IEnumerable{T}" cref="IEnumerable" />
        /// <summary>
        /// Queue enumerator.
        /// </summary>
        /// <remarks>
        /// The emulator is nested in the <see cref="Queue{T}"/>, as this allows access to 
        /// private queue fields and the type of <see cref="Queue{T}"/> elements
        /// </remarks>
        public struct Enumerator : IEnumerator<T>, IEnumerator
        {
            #region private fields

            private const string CollectionWasModifiedMessage =
                "Collection was modified; it is impossible to perform the operation.";

            private const string EnumerationNotStartedMessage =
                "Enumeration is not started. Call MoveNext.";

            private const string EnumerationCompletedMessage =
                "The enumeration has already been completed.";

            private readonly Queue<T> _queue;
            private readonly int _enumeratorVersion; // The version of the queue for which the Enumerator was created.
            private int _index;
            private T _current;

            #endregion // !private fields.

            #region public

            #region constructors

            /// <summary>
            /// Initializes an instance of the enumerator for a particular version of the <paramref name="queue"/>.
            /// </summary>
            /// <param name="queue">The queue for which the enumerator is created.</param>
            /// <exception cref="ArgumentNullException">Exception thrown when <paramref name="queue"/> is null.</exception>
            internal Enumerator(Queue<T> queue)
            {
                if (ReferenceEquals(queue, null))
                {
                    throw new ArgumentNullException(nameof(queue));
                }

                _queue = queue;
                _enumeratorVersion = queue._version;
                _index = -1;
                _current = default(T);
            }

            #endregion // !constructors.

            #region interface implementation

            /// <inheritdoc />
            public T Current
            {
                get
                {
                    if (_enumeratorVersion != _queue._version)
                    {
                        throw new InvalidOperationException(CollectionWasModifiedMessage);
                    }

                    if (_index == -1)
                    {
                        throw new InvalidOperationException(EnumerationNotStartedMessage);
                    }

                    if (_index == -2)
                    {
                        throw new InvalidOperationException(EnumerationCompletedMessage);
                    }

                    return _current;
                }
            }

            /// <inheritdoc />
            object IEnumerator.Current => this.Current;

            /// <inheritdoc />
            public void Dispose()
            {
                _index = -2;
                _current = default(T);
            }

            /// <inheritdoc />
            public bool MoveNext()
            {
                if (_enumeratorVersion != _queue._version)
                {
                    throw new InvalidOperationException(CollectionWasModifiedMessage);
                }

                if (_index == -2)
                {
                    return false;
                }

                if (++_index >= _queue._size)
                {
                    return false;
                }

                _current = _queue.GetElement(_index);
                return true;
            }

            /// <inheritdoc />
            public void Reset()
            {
                if (_enumeratorVersion != _queue._version)
                {
                    throw new InvalidOperationException(CollectionWasModifiedMessage);
                }

                _index = -1;
                _current = default(T);
            }

            #endregion // !interface implementation.

            #endregion // !public.
        }
    }
}

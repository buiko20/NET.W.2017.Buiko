using System;
using System.Collections;
using System.Collections.Generic;

namespace Collection
{
    /// <summary>
    /// The class represents a simple binary search tree.
    /// </summary>
    /// <typeparam name="T">Type of tree elements.</typeparam>
    public class BinarySearchTree<T> : ICollection<T>, IEnumerable<T>, IReadOnlyCollection<T>, IEnumerable, ICollection
    {
        #region private fields

        private TreeNode<T> _root;
        private Comparison<T> _orderComparer;
        private int _count;

        #endregion // !private fields.

        #region public 

        #region constructors

        /// <summary>
        /// Initializes an empty tree with a default comparator.
        /// </summary>
        public BinarySearchTree()
        {
            this.SetDefaultOrderComparer();
        }

        /// <summary>
        /// Initializes an empty tree with a default comparator and adds enumeration elements to it.
        /// </summary>
        /// <param name="enumerable">tree elements</param>
        /// <exception cref="ArgumentNullException">Exception thrown when <paramref name="enumerable"/> is null.</exception>
        public BinarySearchTree(IEnumerable<T> enumerable)
        {
            this.SetDefaultOrderComparer();
            this.AddEnumerable(enumerable);
        }

        /// <inheritdoc />
        /// <summary>
        /// Initializes an empty tree with comparator.
        /// </summary>
        /// <param name="orderComparer">tree elements comparer</param>
        /// <exception cref="T:System.ArgumentNullException">Exception thrown when <paramref name="orderComparer" /> is null.</exception>
        public BinarySearchTree(IComparer<T> orderComparer) : this(orderComparer.Compare)
        {
        }

        /// <summary>
        /// Initializes an empty tree with comparator.
        /// </summary>
        /// <param name="orderComparer">tree elements comparer</param>
        /// <exception cref="ArgumentNullException">Exception thrown when <paramref name="orderComparer"/> is null.</exception>
        public BinarySearchTree(Comparison<T> orderComparer)
        {
            this.OrderComparer = orderComparer;
        }

        /// <inheritdoc />
        /// <summary>
        /// Initializes an empty tree with comparator and adds enumeration elements to it.
        /// </summary>
        /// <param name="orderComparer">tree elements comparer</param>
        /// <param name="enumerable">tree elements</param>
        /// <exception cref="T:System.ArgumentNullException">Exception thrown when <paramref name="orderComparer" />
        /// or <paramref name="enumerable" /> is null.</exception>
        public BinarySearchTree(IComparer<T> orderComparer, IEnumerable<T> enumerable) 
            : this(orderComparer.Compare, enumerable)
        {
        }

        /// <summary>
        /// Initializes an empty tree with comparator and adds enumeration elements to it.
        /// </summary>
        /// <param name="orderComparer">tree elements comparer</param>
        /// <param name="enumerable">tree elements</param>
        /// <exception cref="ArgumentNullException">Exception thrown when <paramref name="orderComparer"/>
        /// or <paramref name="enumerable"/> is null.</exception>
        public BinarySearchTree(Comparison<T> orderComparer, IEnumerable<T> enumerable)
        {
            this.OrderComparer = orderComparer;
            this.AddEnumerable(enumerable);
        }

        #endregion // !constructors.

        #region interface implementation

        #region ICollection

        /// <inheritdoc cref="ICollection.Count"/>
        public int Count
        {
            get
            {
                return _count;
            }
        }

        public bool IsReadOnly { get; } = false;

        /// <inheritdoc />
        public object SyncRoot { get; } = new object();

        /// <inheritdoc />
        public bool IsSynchronized => false;

        /// <inheritdoc />
        void ICollection.CopyTo(Array array, int index)
        {
            BinarySearchTreeHelper.VerifyInput(array, index);

            try
            {
                var temp = this.ToArray();

                // Array.Copy will verify the validity of the input parameters.
                Array.Copy(temp, array, temp.Length);
            }
            catch (Exception e)
            {
                throw new ArgumentException(e.Message);
            }
        }

        /// <inheritdoc />
        /// <summary>
        /// Copies the elements of the tree into an <paramref name="array" /> starting from <paramref name="index" />.
        /// </summary>
        /// <param name="array">array where the elements of the tree will be copied</param>
        /// <param name="index">index in the array from which the elements of the tree will be written</param>
        /// <exception cref="T:System.ArgumentNullException">Exception thrown when <paramref name="array" /> is null.</exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException">Exception thrown when <paramref name="index" /> less than 0 or
        /// <paramref name="index" /> greater than <paramref name="array" /> length.</exception>
        /// <exception cref="T:System.ArgumentException">Exception thrown when <paramref name="array" /> 
        /// and <paramref name="index" /> are not valid for the copy operation.</exception>
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
                var temp = this.ToArray();

                // Array.Copy will verify the validity of the input parameters.
                Array.Copy(temp, array, temp.Length);
            }
            catch (Exception e)
            {
                throw new ArgumentException(e.Message);
            }
        }

        public bool Remove(T item)
        {
            throw new NotImplementedException();
        }

        #endregion // !ICollection.

        #region IEnumerable

        /// <inheritdoc />
        public IEnumerator<T> GetEnumerator() => 
            GetPreorderEnumerator().GetEnumerator();

        /// <inheritdoc />
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        #endregion // !IEnumerable.

        #endregion // !interface implementation.

        #region other

        /// <summary>
        /// Searches for an element defined by a selector.
        /// </summary>
        /// <param name="selectorAndGuider"> delegate who checks whether the element is suitable, 
        /// and if the element does not fit, then it indicates in which direction to move along the tree.
        /// </param>
        /// <returns>Element or default(T) if the item is not found.</returns>
        /// <remarks> 
        /// <paramref name="selectorAndGuider"/> return 0 if the element is the one you are looking for. 
        /// Otherwise return the value greater than 0 if you need to move left along the tree or 
        /// less than 0 if you need to move to the right along the tree.
        /// </remarks>
        /// <exception cref="ArgumentNullException">Exception thrown when <paramref name="selectorAndGuider"/> is null.</exception>
        public T Find(Func<T, int> selectorAndGuider)
        {
            // Do we need such an opportunity for a tree? An example of use is in the tests.
            if (ReferenceEquals(selectorAndGuider, null))
            {
                throw new ArgumentNullException(nameof(selectorAndGuider));
            }

            return BinarySearchTreeHelper.Find(_root, selectorAndGuider);
        }

        /// <summary>
        /// Adds an <paramref name="item"/> to the tree. 
        /// If such an <paramref name="item"/> already exists, then it updates its data.
        /// </summary>
        /// <param name="item">element to add to the tree</param>
        /// <exception cref="ArgumentNullException">Exception thrown when <paramref name="item"/> is null.</exception>
        public void Add(T item)
        {
            BinarySearchTreeHelper.Add(ref _root, item, OrderComparer);
            _count++;
        }           

        /// <summary>
        /// Checks if an <paramref name="item"/> is in the tree.
        /// </summary>
        /// <param name="item">search item</param>
        /// <returns>True is if such an element exists in the tree, false otherwise.</returns>
        /// <exception cref="ArgumentNullException">Exception thrown when <paramref name="item"/> is null.</exception>
        public bool Contains(T item) => this.Contains(item, OrderComparer);

        /// <summary>
        /// Checks if an <paramref name="item"/> is in the tree.
        /// </summary>
        /// <remarks>
        /// The method allows you to search for items in a binary 
        /// tree in an order different from the order in which the 
        /// tree was built. Allows you to walk on a binary tree 
        /// in any direction to search for items.
        /// </remarks>
        /// <param name="item">search item</param>
        /// <param name="orderComparer">element order comparer</param>
        /// <returns>True is if such an element exists in the tree, false otherwise.</returns>
        /// <exception cref="ArgumentNullException">Exception thrown when <paramref name="item"/> is null.</exception>
        public bool Contains(T item, IComparer<T> orderComparer)
        {
            if (ReferenceEquals(orderComparer, null))
            {
                throw new ArgumentNullException(nameof(orderComparer));
            }

            return this.Contains(item, orderComparer.Compare);
        }

        /// <summary>
        /// Checks if an <paramref name="item"/> is in the tree.
        /// </summary>
        /// <remarks>
        /// The method allows you to search for items in a binary 
        /// tree in an order different from the order in which the 
        /// tree was built. Allows you to walk on a binary tree 
        /// in any direction to search for items.
        /// </remarks>
        /// <param name="item">search item</param>
        /// <param name="orderComparer">element order comparer</param>
        /// <returns>True is if such an element exists in the tree, false otherwise.</returns>
        /// <exception cref="ArgumentNullException">Exception thrown when <paramref name="item"/> is null.</exception>
        public bool Contains(T item, Comparison<T> orderComparer)
        {
            if (ReferenceEquals(item, null))
            {
                throw new ArgumentNullException(nameof(item));
            }

            if (ReferenceEquals(orderComparer, null))
            {
                throw new ArgumentNullException(nameof(orderComparer));
            }

            return BinarySearchTreeHelper.Contains(_root, item, orderComparer);
        }

        /// <summary>
        /// Directly traversing a tree.
        /// </summary>
        /// <returns>The next element in the order of traversing the tree.</returns>
        public IEnumerable<T> GetPreorderEnumerator()
        {
            if (ReferenceEquals(_root, null))
            {
                yield break;
            }
            
            foreach (var element in BinarySearchTreeHelper.GetPreorderEnumerator(_root))
            {
                yield return element;
            }
        }

        /// <summary>
        /// Symmetrical tree traversing.
        /// </summary>
        /// <returns>The next element in the order of traversing the tree.</returns>
        public IEnumerable<T> GetInorderEnumerator()
        {
            if (ReferenceEquals(_root, null))
            {
                yield break;
            }

            foreach (var element in BinarySearchTreeHelper.GetInorderEnumerator(_root))
            {
                yield return element;
            }
        }

        /// <summary>
        /// Reverse tree traversing.
        /// </summary>
        /// <returns>The next element in the order of traversing the tree.</returns>
        public IEnumerable<T> GetPostorderEnumerator()
        {
            if (ReferenceEquals(_root, null))
            {
                yield break;
            }

            foreach (var element in BinarySearchTreeHelper.GetPostorderEnumerator(_root))
            {
                yield return element;
            }
        }

        /// <summary>
        /// Copies the elements of the tree into an array.
        /// </summary>
        /// <returns>Array of tree elements</returns>
        public T[] ToArray()
        {
            var result = new T[_count];
            int i = 0;
            foreach (var item in this)
            {
                result[i++] = item;
            }

            return result;
        }

        /// <summary>
        /// Clears the tree.
        /// </summary>
        public void Clear()
        {
            _root = null;
            _count = 0;
        }

        #endregion // !other.

        #endregion // !public.

        #region private

        private Comparison<T> OrderComparer
        {
            get
            {
                return _orderComparer;
            }

            set
            {
                if (ReferenceEquals(value, null))
                {
                    throw new ArgumentNullException(nameof(OrderComparer));
                }

                _orderComparer = value;
            }
        }

        private void SetDefaultOrderComparer()
        {
            var temp = BinarySearchTreeHelper.GetDefaultOrderComparer<T>();
            _orderComparer = (item1, item2) => temp.Compare(item1, item2);
        }

        private void AddEnumerable(IEnumerable<T> enumerable)
        {
            if (ReferenceEquals(enumerable, null))
            {
                throw new ArgumentNullException(nameof(enumerable));
            }

            foreach (var item in enumerable)
            {
                this.Add(item);
            }
        }

        #endregion // !private.
    }
}

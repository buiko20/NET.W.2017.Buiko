using System;
using System.Collections;
using System.Collections.Generic;

namespace Collection
{
    /// <summary>
    /// The class represents a simple binary search tree.
    /// </summary>
    /// <typeparam name="T">Type of tree elements.</typeparam>
    public class BinarySearchTree<T> : ICollection<T>, IReadOnlyCollection<T>, IEnumerable<T>, IEnumerable
    {
        #region private fields

        private TreeNode<T> _root;
        private Comparison<T> _orderComparer;

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

        /// <inheritdoc cref="ICollection{T}.Count" />
        public int Count { get; private set; }

        /// <inheritdoc />
        public bool IsReadOnly { get; } = false;

        /// <inheritdoc />
        public void CopyTo(T[] array, int index)
        {
            if (ReferenceEquals(array, null))
            {
                throw new ArgumentNullException(nameof(array));
            }

            if (index < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(index), $"{nameof(index)} must be greater than or equal to 0.");
            }

            try
            {
                var temp = this.ToArray();
                Array.Copy(temp, 0, array, index, temp.Length);
            }
            catch (Exception e)
            {
                throw new ArgumentException(e.Message);
            }
        }

        /// <inheritdoc />
        public void Clear()
        {
            _root = null;
            Count = 0;
        }

        /// <inheritdoc />
        public void Add(T item)
        {
            if (ReferenceEquals(item, null))
            {
                throw new ArgumentNullException(nameof(item));
            }

            this.Add(ref _root, item);
            Count++;
        }

        /// <inheritdoc />
        public bool Contains(T item) => this.Contains(_root, item);

        /// <inheritdoc />
        public bool Remove(T item)
        {
            if (ReferenceEquals(item, null))
            {
                throw new ArgumentNullException(nameof(item));
            }

            var parent = _root;
            var isRemoved = this.Remove(ref _root, ref parent, item);
            if (isRemoved)
            {
                Count--;
            }

            return isRemoved;
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
        /// Directly traversing a tree.
        /// </summary>
        /// <returns>The next element in the order of traversing the tree.</returns>
        public IEnumerable<T> GetPreorderEnumerator()
        {
            if (ReferenceEquals(_root, null))
            {
                yield break;
            }
            
            foreach (var element in GetPreorderEnumerator(_root))
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

            foreach (var element in GetInorderEnumerator(_root))
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

            foreach (var element in GetPostorderEnumerator(_root))
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
            var result = new T[Count];
            int i = 0;
            foreach (var item in this)
            {
                result[i++] = item;
            }

            return result;
        }

        #endregion // !other.

        #endregion // !public.

        #region private

        private static IComparer<T> TryGetDefaultOrderComparer()
        {
            var type = typeof(T);

            if (type == typeof(string))
            {
                return StringComparer.CurrentCulture as IComparer<T>;
            }

            if ((!ReferenceEquals(type.GetInterface("IComparable`1"), null)) ||
                (!ReferenceEquals(type.GetInterface("IComparable"), null)))
            {
                return Comparer<T>.Default;
            }

            throw new ArgumentException("At least one object must implement IComparable interface.");
        }

        private static IEnumerable<T> GetPreorderEnumerator(TreeNode<T> root)
        {
            while (true)
            {
                yield return root.Data;

                if (!ReferenceEquals(root.Left, null))
                {
                    foreach (var item in GetPreorderEnumerator(root.Left))
                    {
                        yield return item;
                    }
                }

                if (!ReferenceEquals(root.Rigth, null))
                {
                    root = root.Rigth;
                    continue;
                }

                break;
            }
        }

        private static IEnumerable<T> GetInorderEnumerator(TreeNode<T> root)
        {
            while (true)
            {
                if (!ReferenceEquals(root.Left, null))
                {
                    foreach (var item in GetInorderEnumerator(root.Left))
                    {
                        yield return item;
                    }
                }

                yield return root.Data;

                if (!ReferenceEquals(root.Rigth, null))
                {
                    root = root.Rigth;
                    continue;
                }

                break;
            }
        }

        private static IEnumerable<T> GetPostorderEnumerator(TreeNode<T> root)
        {
            if (!ReferenceEquals(root.Left, null))
            {
                foreach (var item in GetPostorderEnumerator(root.Left))
                {
                    yield return item;
                }
            }

            if (!ReferenceEquals(root.Rigth, null))
            {
                foreach (var item in GetPostorderEnumerator(root.Rigth))
                {
                    yield return item;
                }
            }

            yield return root.Data;
        }

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
            var temp = TryGetDefaultOrderComparer();
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

        private void Add(ref TreeNode<T> root, T item)
        {
            if (ReferenceEquals(root, null))
            {
                root = new TreeNode<T>(null, null, item);
                return;
            }

            var comparisonResult = OrderComparer(root.Data, item);
            if (comparisonResult == 0)
            {
                root.Data = item;
                return;
            }

            if (comparisonResult > 0)
            {
                Add(ref root.Left, item);
                return;
            }

            Add(ref root.Rigth, item);
        }

        private bool Contains(TreeNode<T> root, T item)
        {
            while (true)
            {
                if (ReferenceEquals(root, null))
                {
                    return false;
                }

                var comparisonResult = OrderComparer(root.Data, item);
                if (comparisonResult == 0)
                {
                    return true;
                }

                if (comparisonResult > 0)
                {
                    root = root.Left;
                    continue;
                }

                root = root.Rigth;
            }
        }

        private bool Remove(ref TreeNode<T> root, ref TreeNode<T> parent, T item)
        {
            if (ReferenceEquals(root, null))
            {
                return false;
            }

            var comparisonResult = OrderComparer(item, root.Data);
            if (comparisonResult < 0)
            {
                parent = root;
                return Remove(ref root.Left, ref parent, item);
            }

            if (comparisonResult > 0)
            {
                parent = root;
                return Remove(ref root.Rigth, ref parent, item);
            }

            if (ReferenceEquals(root.Left, null) && ReferenceEquals(root.Rigth, null))
            {
                root = null;
                return true;
            }

            if (!ReferenceEquals(root.Left, null) && ReferenceEquals(root.Rigth, null))
            {
                root = root.Left;
                return true;
            }

            if (ReferenceEquals(root.Left, null))
            {
                root = root.Rigth;
                return true;
            }

            if (ReferenceEquals(root.Rigth.Left, null))
            {
                root.Data = root.Rigth.Data;
                root.Rigth = root.Rigth.Rigth;
                return true;
            }

            var temp = root.Rigth;
            while (!ReferenceEquals(temp.Left, null))
            {
                parent = temp;
                temp = temp.Left;
            }

            var data = temp.Data;
            Remove(ref root, ref parent, data);
            root.Data = data;
            return true;
        }

        #endregion // !private.
    }
}

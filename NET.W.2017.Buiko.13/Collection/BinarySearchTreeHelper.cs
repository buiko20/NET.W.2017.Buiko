using System;
using System.Collections.Generic;

namespace Collection
{
    internal static class BinarySearchTreeHelper
    {
        internal static IComparer<T> GetDefaultOrderComparer<T>()
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

        internal static void Add<T>(ref TreeNode<T> root, T item, Comparison<T> orderComparer)
        {
            if (ReferenceEquals(root, null))
            {
                root = new TreeNode<T>(null, null, item);
                return;
            }

            var comparisonResult = orderComparer(root.Data, item);
            if (comparisonResult == 0)
            {
                root.Data = item;
                return;
            }

            if (comparisonResult > 0)
            {
                Add(ref root.Left, item, orderComparer);
                return;
            }

            Add(ref root.Rigth, item, orderComparer);          
        }

        internal static T Find<T>(TreeNode<T> root, Func<T, int> selectorAndGuider)
        {
            while (true)
            {
                if (ReferenceEquals(root, null))
                {
                    return default(T);
                }

                var comparisonResult = selectorAndGuider(root.Data);
                if (comparisonResult == 0)
                {
                    return root.Data;
                }

                if (comparisonResult > 0)
                {
                    root = root.Left;
                    continue;
                }

                root = root.Rigth;
            }
        }

        internal static bool Contains<T>(TreeNode<T> root, T item, Comparison<T> orderComparer)
        {
            while (true)
            {
                if (ReferenceEquals(root, null))
                {
                    return false;
                }

                var comparisonResult = orderComparer(root.Data, item);
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

        internal static bool Remove<T>(ref TreeNode<T> root, ref TreeNode<T> parent, T item, Comparison<T> orderComparer)
        {
            if (ReferenceEquals(root, null))
            {
                return false;
            }

            var comparisonResult = orderComparer(item, root.Data);
            if (comparisonResult < 0)
            {
                parent = root;
                return Remove(ref root.Left, ref parent, item, orderComparer);
            }

            if (comparisonResult > 0)
            {
                parent = root;
                return Remove(ref root.Rigth, ref parent, item, orderComparer);
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
            Remove(ref root, ref parent, data, orderComparer);
            root.Data = data;
            return true;
        }

        internal static IEnumerable<T> GetPreorderEnumerator<T>(TreeNode<T> root)
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

        internal static IEnumerable<T> GetInorderEnumerator<T>(TreeNode<T> root)
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

        internal static IEnumerable<T> GetPostorderEnumerator<T>(TreeNode<T> root)
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

        internal static void VerifyInput(Array array, int index)
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
    }
}

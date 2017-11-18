using System;
using System.Collections.Generic;
using System.Linq;

namespace Collection
{
    /// <summary>
    /// Class extending the functionality of a binary tree.
    /// </summary>
    public static class BinarySearchTreeExtensions
    {
        /// <summary>
        /// Adds <paramref name="items"/> to a <paramref name="tree"/>.
        /// </summary>
        /// <typeparam name="T">Type of tree elements.</typeparam>
        /// <param name="tree">binary search tree</param>
        /// <param name="items">elements to add to the <paramref name="tree"/></param>
        /// <exception cref="ArgumentNullException">Exception thrown when <paramref name="items"/> is null.</exception>
        public static void Add<T>(this BinarySearchTree<T> tree, params T[] items)
        {
            if (ReferenceEquals(items, null))
            {
                throw new ArgumentNullException(nameof(items));
            }

            tree.Add((IEnumerable<T>)items);
        }

        /// <summary>
        /// Adds <paramref name="items"/> to a <paramref name="tree"/>.
        /// </summary>
        /// <typeparam name="T">Type of tree elements.</typeparam>
        /// <param name="tree">binary search tree</param>
        /// <param name="items">elements to add to the <paramref name="tree"/></param>
        /// <exception cref="ArgumentNullException">Exception thrown when <paramref name="items"/> is null.</exception>
        public static void Add<T>(this BinarySearchTree<T> tree, IEnumerable<T> items)
        {
            if (ReferenceEquals(items, null))
            {
                throw new ArgumentNullException(nameof(items));
            }

            foreach (var item in items)
            {
                tree.Add(item);
            }
        }

        /// <summary>
        /// Removes elements selected by the <paramref name="equalityComparer"/> from a <paramref name="tree"/>.
        /// </summary>
        /// <typeparam name="T">Type of tree elements.</typeparam>
        /// <param name="tree">binary search tree</param>
        /// <param name="equalityComparer">comparer selects an item from the <paramref name="tree"/></param>
        /// <exception cref="ArgumentNullException">Exception thrown when <paramref name="equalityComparer"/> is null.</exception>
        public static void Remove<T>(this BinarySearchTree<T> tree, Predicate<T> equalityComparer)
        {
            if (ReferenceEquals(equalityComparer, null))
            {
                throw new ArgumentNullException(nameof(equalityComparer));
            }

            var removedItems = tree.Where(item => equalityComparer(item)).ToArray();

            foreach (var item in removedItems)
            {
                tree.Remove(item);
            }
        }

        /// <summary>
        /// Returns element selected by the <paramref name="equalityComparer"/> from a <paramref name="tree"/>.
        /// </summary>
        /// <typeparam name="T">Type of tree elements.</typeparam>
        /// <param name="tree">binary search tree</param>
        /// <param name="equalityComparer">comparer selects an item from the <paramref name="tree"/></param>
        /// <returns>Element or default(T) if the item is not found.</returns>
        /// <exception cref="ArgumentNullException">Exception thrown when <paramref name="equalityComparer"/> is null.</exception>
        public static T Find<T>(this BinarySearchTree<T> tree, Predicate<T> equalityComparer)
        {
            if (ReferenceEquals(equalityComparer, null))
            {
                throw new ArgumentNullException(nameof(equalityComparer));
            }

            foreach (var item in tree)
            {
                if (equalityComparer(item))
                {
                    return item;
                }
            }

            return default(T);
        }

        /// <summary>
        /// Returns elements selected by the <paramref name="equalityComparer"/> from a <paramref name="tree"/>.
        /// </summary>
        /// <typeparam name="T">Type of tree elements.</typeparam>
        /// <param name="tree">binary search tree</param>
        /// <param name="equalityComparer">comparer selects an item from the <paramref name="tree"/></param>
        /// <returns>Array of found elements.</returns>
        /// <exception cref="ArgumentNullException">Exception thrown when <paramref name="equalityComparer"/> is null.</exception>
        public static T[] FindAll<T>(this BinarySearchTree<T> tree, Predicate<T> equalityComparer)
        {
            if (ReferenceEquals(equalityComparer, null))
            {
                throw new ArgumentNullException(nameof(equalityComparer));
            }

            return tree.Where(item => equalityComparer(item)).ToArray();
        }

        /// <summary>
        /// Checks if an <paramref name="item"/> contains a <paramref name="tree"/>.
        /// </summary>
        /// <typeparam name="T">Type of tree elements.</typeparam>
        /// <param name="tree">binary search tree</param>
        /// <param name="item">search element</param>
        /// <param name="equalityComparer">items equality comparer</param>
        /// <returns>Array of found elements.</returns>                                   
        /// <exception cref="ArgumentNullException">Exception thrown when <paramref name="equalityComparer"/> is null.</exception>
        public static bool Contains<T>(this BinarySearchTree<T> tree, T item, IEqualityComparer<T> equalityComparer)
        {
            if (ReferenceEquals(equalityComparer, null))
            {
                throw new ArgumentNullException(nameof(equalityComparer));
            }

            return tree.Contains(item, (x, y) => equalityComparer.Equals(x, y));
        }

        /// <summary>
        /// Checks if an <paramref name="item"/> contains a <paramref name="tree"/>.
        /// </summary>
        /// <typeparam name="T">Type of tree elements.</typeparam>
        /// <param name="tree">binary search tree</param>
        /// <param name="item">search element</param>
        /// <param name="equalityComparer">items equality comparer</param>
        /// <returns>Array of found elements.</returns>                                   
        /// <exception cref="ArgumentNullException">Exception thrown when <paramref name="equalityComparer"/> is null.</exception>
        public static bool Contains<T>(this BinarySearchTree<T> tree, T item, Func<T, T, bool> equalityComparer)
        {
            if (ReferenceEquals(item, null))
            {
                throw new ArgumentNullException(nameof(item));
            }

            if (ReferenceEquals(equalityComparer, null))
            {
                throw new ArgumentNullException(nameof(equalityComparer));
            }

            return tree.Any(element => equalityComparer(item, element));
        }
    }
}

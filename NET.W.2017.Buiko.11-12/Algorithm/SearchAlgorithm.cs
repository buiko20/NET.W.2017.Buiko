using System;
using System.Collections.Generic;

namespace Algorithm
{
    public static class SearchAlgorithm
    {
        #region public

        /// <summary>
        /// Searches for an <paramref name="value"/> using the binary search method.
        /// </summary>
        /// <typeparam name="T">object type</typeparam>
        /// <param name="array">an array in which to search for the <paramref name="value"/></param>
        /// <param name="value">search object</param>
        /// <returns>The index in the array or -1 if no such.</returns>
        public static int BinarySearch<T>(T[] array, T value) =>
            BinarySearch(array, 0, array?.Length - 1 ?? -1, value, GetDefaultComparer<T>());

        /// <summary>
        /// Searches for an <paramref name="value"/> using the binary search method.
        /// </summary>
        /// <typeparam name="T">object type</typeparam>
        /// <param name="array">an array in which to search for the <paramref name="value"/></param>
        /// <param name="startIndex">position from which to start the search</param>
        /// <param name="endIndex">position on which to finish</param>
        /// <param name="value">search object</param>
        /// <returns>The index in the array or -1 if no such.</returns>
        /// <exception cref="ArgumentException">Thrown when <paramref name="startIndex"/> less than 0 or
        /// greater than <paramref name="endIndex"/> or <paramref name="endIndex"/> less than 0
        /// or greater than or equal to <paramref name="array"/> length.</exception>
        public static int BinarySearch<T>(T[] array, int startIndex, int endIndex, T value) =>
            BinarySearch(array, startIndex, endIndex, value, GetDefaultComparer<T>());

        /// <summary>
        /// Searches for an <paramref name="value"/> using the binary search method.
        /// </summary>
        /// <typeparam name="T">object type</typeparam>
        /// <param name="array">an array in which to search for the <paramref name="value"/></param>
        /// <param name="value">search object</param>
        /// <param name="comparer">comparer for comparing objects</param>
        /// <returns>The index in the array or -1 if no such.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="array"/> or
        /// <paramref name="comparer"/> is null.</exception>
        public static int BinarySearch<T>(T[] array, T value, IComparer<T> comparer) =>
            BinarySearch(array, 0, array?.Length - 1 ?? -1, value, comparer.Compare);

        /// <summary>
        /// Searches for an <paramref name="value"/> using the binary search method.
        /// </summary>
        /// <typeparam name="T">object type</typeparam>
        /// <param name="array">an array in which to search for the <paramref name="value"/></param>
        /// <param name="value">search object</param>
        /// <param name="comparer">comparer for comparing objects</param>
        /// <returns>The index in the array or -1 if no such.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="array"/> or
        /// <paramref name="comparer"/> is null.</exception>
        public static int BinarySearch<T>(T[] array, T value, Comparison<T> comparer) =>
            BinarySearch(array, 0, array?.Length - 1 ?? -1, value, comparer);

        /// <summary>
        /// Searches for an <paramref name="value"/> using the binary search method.
        /// </summary>
        /// <typeparam name="T">object type</typeparam>
        /// <param name="array">an array in which to search for the <paramref name="value"/></param>
        /// <param name="startIndex">position from which to start the search</param>
        /// <param name="endIndex">position on which to finish</param>
        /// <param name="value">search object</param>
        /// <param name="comparer">comparer for comparing objects</param>
        /// <returns>The index in the array or -1 if no such.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="array"/> or
        /// <paramref name="comparer"/> is null.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="startIndex"/> less than 0 or
        /// greater than <paramref name="endIndex"/> or <paramref name="endIndex"/> less than 0
        /// or greater than or equal to <paramref name="array"/> length.</exception>
        public static int BinarySearch<T>(T[] array, int startIndex, int endIndex, T value, IComparer<T> comparer) =>
            BinarySearch(array, startIndex, endIndex, value, comparer.Compare);

        /// <summary>
        /// Searches for an <paramref name="value"/> using the binary search method.
        /// </summary>
        /// <typeparam name="T">object type</typeparam>
        /// <param name="array">an array in which to search for the <paramref name="value"/></param>
        /// <param name="startIndex">position from which to start the search</param>
        /// <param name="endIndex">position on which to finish</param>
        /// <param name="value">search object</param>
        /// <param name="comparer">comparer for comparing objects</param>
        /// <returns>The index in the array or -1 if no such.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="array"/> or
        /// <paramref name="comparer"/> is null.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="startIndex"/> less than 0 or
        /// greater than <paramref name="endIndex"/> or <paramref name="endIndex"/> less than 0
        /// or greater than or equal to <paramref name="array"/> length.</exception>
        public static int BinarySearch<T>(T[] array, int startIndex, int endIndex, T value, Comparison<T> comparer)
        {
            VerifyInput(array, startIndex, endIndex, comparer);

            while (startIndex <= endIndex)
            {
                int mid = startIndex + ((endIndex - startIndex) >> 1);

                int comparisonResult = comparer(array[mid], value);
                if (comparisonResult == 0)
                {
                    return mid;
                }

                if (comparisonResult < 0)
                {
                    startIndex = mid + 1;
                }
                else
                {
                    endIndex = mid - 1;
                }
            }

            return -1;
        }

        #endregion // !public.

        #region private

        private static void VerifyInput<T>(T[] array, int startIndex, int endIndex, Comparison<T> comparer)
        {
            if (ReferenceEquals(array, null))
            {
                throw new ArgumentNullException(nameof(array));
            }

            if (array.Length == 0)
            {
                return;
            }

            VerifyIndexes(array.Length, startIndex, endIndex);

            if (ReferenceEquals(comparer, null))
            {
                throw new ArgumentNullException(nameof(comparer));
            }
        }

        private static void VerifyIndexes(int arrayLength, int startIndex, int endIndex)
        {
            if (startIndex < 0)
            {
                throw new ArgumentException($"{nameof(startIndex)} must be >= 0", nameof(startIndex));
            }

            if (endIndex < 0)
            {
                throw new ArgumentException($"{nameof(endIndex)} must be >= 0", nameof(endIndex));
            }

            if (startIndex > endIndex)
            {
                throw new ArgumentException($"{nameof(startIndex)} must be <= {nameof(endIndex)}", nameof(startIndex));
            }

            if (endIndex >= arrayLength)
            {
                throw new ArgumentException($"{nameof(endIndex)} must be < array.Length", nameof(endIndex));
            }
        }

        private static IComparer<T> GetDefaultComparer<T>()
        {
            if (typeof(T) == typeof(string))
            {
                return StringComparer.CurrentCulture as IComparer<T>;
            }

            return Comparer<T>.Default;
        }

        #endregion // !private.
    }
}

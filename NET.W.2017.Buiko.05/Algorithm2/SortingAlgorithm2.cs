using System;
using System.Collections.Generic;

namespace Algorithm2
{
    public class SortingAlgorithm2
    {
        // The implementation of the sorting method is closed on the interface.
        #region public methods

        /// <summary>
        /// Sorts the jagged array using the passed comparator.
        /// </summary>
        /// <param name="jaggedArray">source jagged array</param>
        /// <param name="comparator">sorting criterion</param>
        public static void Sort(int[][] jaggedArray, IComparer<int[]> comparator) =>
            BubbleSort(jaggedArray, comparator);

        /// <summary>
        /// Sorts the jagged array using the passed comparator.
        /// </summary>
        /// <param name="jaggedArray">source jagged array</param>
        /// <param name="comparator">sorting criterion</param>
        public static void Sort(int[][] jaggedArray, Comparison<int[]> comparator) =>
            BubbleSort(jaggedArray, new InterfaceAdapter(comparator));

        /// <summary>
        /// Sorts the jagged array using the passed comparator.
        /// </summary>
        /// <param name="jaggedArray">source jagged array</param>
        /// <param name="comparator">sorting criterion</param>
       // public static void Sort(int[][] jaggedArray, Func<int[], int[], int> comparator) =>
       //     BubbleSort(jaggedArray, new InterfaceAdapter(new Comparison<int[]>(comparator)));

        #endregion // !public methods.

        #region private methods

        private static void BubbleSort(int[][] jaggedArray, IComparer<int[]> comparator)
        {
            VerifyInput(jaggedArray, comparator);

            for (int i = 0; i < jaggedArray.Length - 1; i++)
            {
                for (int j = 0; j < jaggedArray.Length - 1; j++)
                {
                    if (comparator.Compare(jaggedArray[j], jaggedArray[j + 1]) > 0)
                    {
                        Swap(ref jaggedArray[j], ref jaggedArray[j + 1]);
                    }
                }
            }
        }

        private static void Swap<T>(ref T x, ref T y)
        {
            T temp = x;
            x = y;
            y = temp;
        }

        private static void VerifyInput(int[][] jaggedArray, IComparer<int[]> comparator)
        {
            if (jaggedArray == null)
            {
                throw new ArgumentNullException(nameof(jaggedArray));
            }

            if (comparator == null)
            {
                throw new ArgumentNullException(nameof(comparator));
            }

            for (int i = 0; i < jaggedArray.Length; i++)
            {
                if (jaggedArray[i] == null)
                {
                    throw new ArgumentNullException(nameof(jaggedArray), $"{i}th nested array in {nameof(jaggedArray)} is null");
                }
            }
        }

        #endregion // !private methods.
    }
}

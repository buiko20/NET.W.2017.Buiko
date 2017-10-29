using System;
using System.Collections.Generic;
using System.Linq;

namespace Algorithm
{
    public class SortAlgotithm
    {
        #region public methods

        /// <summary>
        /// Sort in ascending order of the sums of elements of the rows of the jagged array.
        /// </summary>
        /// <param name="jaggedArray">source jagged array</param>
        public static void Sort(int[][] jaggedArray) =>
            BubbleSort(jaggedArray, DefaultComparator);

        /// <summary>
        /// Sorts the jagged array using the passed comparator.
        /// </summary>
        /// <param name="jaggedArray">source jagged array</param>
        /// <param name="comparator">sorting criterion</param>
        public static void Sort(int[][] jaggedArray, IComparer<int[]> comparator) =>
            BubbleSort(jaggedArray, comparator.Compare);

        /// <summary>
        /// Sorts the jagged array using the passed comparator.
        /// </summary>
        /// <param name="jaggedArray">source jagged array</param>
        /// <param name="comparator">sorting criterion</param>
        public static void Sort(int[][] jaggedArray, Func<int[], int[], int> comparator) =>
            BubbleSort(jaggedArray, comparator);

        #endregion // !public methods.

        #region private methods

        private static void BubbleSort(int[][] jaggedArray, Func<int[], int[], int> comparator)
        {
            VerifyJaggedArray(jaggedArray);
            if (comparator == null) throw new ArgumentNullException(nameof(comparator));

            for (int i = 0; i < jaggedArray.Length - 1; i++)
            for (int j = 0; j < jaggedArray.Length - 1; j++)
                if (comparator.Invoke(jaggedArray[j], jaggedArray[j + 1]) > 0)
                    Swap(ref jaggedArray[j], ref jaggedArray[j + 1]);
        }

        private static int DefaultComparator(int[] array1, int[] array2)
        {
            if (array1.Length == 0) return -1;
            if (array2.Length == 0) return 1;

            int sum1 = array1.Sum();
            int sum2 = array2.Sum();

            if (sum1 == sum2) return 0;

            if (sum1 > sum2) return 1;

            return -1;
        }

        public static void Swap<T>(ref T x, ref T y)
        {
            T temp = x;
            x = y;
            y = temp;
        }

        private static void VerifyJaggedArray(int[][] jaggedArray)
        {
            if (jaggedArray == null) throw new ArgumentNullException(nameof(jaggedArray));

            for (int i = 0; i < jaggedArray.Length; i++)
                if (jaggedArray[i] == null)
                    throw new ArgumentNullException(nameof(jaggedArray), $"{i}th nested array in {nameof(VerifyJaggedArray)} is null");
        }

        #endregion // !private methods.
    }
}

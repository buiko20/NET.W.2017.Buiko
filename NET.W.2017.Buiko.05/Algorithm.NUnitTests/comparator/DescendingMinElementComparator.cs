﻿using System.Collections.Generic;
using System.Linq;

namespace Algorithm.NUnitTests.Comparator
{
    internal class DescendingMinElementComparator : IComparer<int[]>
    {
        /// <summary>
        /// Sorts in descending order of the min row items.
        /// </summary>
        /// <param name="array1">first array</param>
        /// <param name="array2">second array</param>
        /// <returns>
        /// 0 if sums of arrays element are equal, 
        /// -1 if sum of element of the array1 more than sum of element of the array2,
        /// 1 otherwise
        /// </returns>
        public int Compare(int[] array1, int[] array2)
        {
            if (array1 == null)
            {
                return 1;
            }

            if (array2 == null)
            {
                return -1;
            }

            if (array1.Length == 0)
            {
                return -1;
            }

            if (array2.Length == 0)
            {
                return 1;
            }

            int sum1 = array1.Min();
            int sum2 = array2.Min();

            if (sum1 == sum2)
            {
                return 0;
            }

            if (sum1 > sum2)
            {
                return -1;
            }

            return 1;
        }
    }
}

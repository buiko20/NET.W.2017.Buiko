using System.Collections.Generic;
using System.Linq;

namespace Algorithm.NUnitTests.Comparator
{
    internal class AscendingMinElementComparator : IComparer<int[]>
    {
        /// <summary>
        /// Sorts in ascending order of the min row items.
        /// </summary>
        /// <param name="array1">first array</param>
        /// <param name="array2">second array</param>
        /// <returns>
        /// 0 if min elements of arrays are equal, 
        /// 1 if min element of the array1 more than min element of the array2,
        /// -1 otherwise
        /// </returns>
        public int Compare(int[] array1, int[] array2)
        {
            if (array1 == null)
            {
                return -1;
            }

            if (array2 == null)
            {
                return 1;
            }

            if (array1.Length == 0)
            {
                return -1;
            }

            if (array2.Length == 0)
            {
                return 1;
            }

            int maxElement1 = array1.Min();
            int maxElement2 = array2.Min();

            if (maxElement1 == maxElement2)
            {
                return 0;
            }

            if (maxElement1 > maxElement2)
            {
                return 1;
            }

            return -1;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortAlgorithm
{
    public class Sort
    {
        public static void QuickSort(int[] array, int start, int end)
        {
            if (start > end)
                throw new ArgumentException(nameof(start) + " must be less than " + nameof(end));

            if (array == null)
                throw new ArgumentNullException(nameof(array));

            if (array.Length <= 1)
                return;

            if (array.Length <= end)
                throw new ArgumentException(nameof(end) + " must less than array length", nameof(end));

            int index = start + ((end - start) / 2);
            int basicElement = array[index];

            int i = start;
            int j = end;
            while (i <= j)
            {
                while ( (array[i] < basicElement) && (i <= end) ) ++i;
                while ( (array[j] > basicElement) && (j >= start) ) --j;

                if (i <= j)
                {
                    int temp = array[i];
                    array[i] = array[j];
                    array[j] = temp;
                    i++;
                    j--;
                }
            }

            if (i < end) QuickSort(array, i, end);
            if (j > start) QuickSort(array, start, j);
        }

        public static int[] MergeSort(int[] array)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array));

            if (array.Length == 0)
                throw new ArgumentException("length of " + nameof(array) + " must be greater than 0 ", nameof(array));

            if (array.Length == 1)
                return array;

            int center = array.Length / 2;
            int[] array1 = MergeSort(array.Take(center).ToArray());
            int[] array2 = MergeSort(array.Skip(center).ToArray());

            return Merge(array1, array2);
        }

        private static int[] Merge(int[] array1, int[] array2)
        {
            int array1Length = array1.Length;
            int array2Length = array2.Length;
            int resultLength = array1Length + array2Length;

            int[] result = new int[resultLength];
            int i = 0, j = 0;
            for (int iResult = 0; iResult < resultLength; iResult++)
            {
                if ( (i < array1Length) && (j < array2Length) )
                {
                    if (array1[i] > array2[j])
                        result[iResult] = array2[j++];
                    else
                        result[iResult] = array1[i++];
                }
                else
                {
                    if (j < array2Length)
                        result[iResult] = array2[j++];
                    else
                        result[iResult] = array1[i++];
                }
            }

            return result;
        }
    }
}

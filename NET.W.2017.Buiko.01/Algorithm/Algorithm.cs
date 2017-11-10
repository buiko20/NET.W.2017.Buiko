using System;
using System.Linq;

namespace Algorithm
{
    public class Sort
    {
        #region public methods

        public static void QuickSort(int[] array) =>
            QuickSort(array, 0, array?.Length - 1 ?? 0);

        public static void QuickSort(int[] array, int start, int end)
        {
            VerifyInput(array, start, end);

            if (array.Length <= 1)
            {
                return;
            }

            int index = start + ((end - start) / 2);
            int basicElement = array[index];

            int i = start, j = end;
            while (i <= j)
            {
                while ((array[i] < basicElement) && (i <= end))
                {
                    ++i;
                }

                while ((array[j] > basicElement) && (j >= start))
                {
                    --j;
                }

                if (i <= j)
                {
                    int temp = array[i];
                    array[i] = array[j];
                    array[j] = temp;
                    i++;
                    j--;
                }
            }

            if (i < end)
            {
                QuickSort(array, i, end);
            }

            if (j > start)
            {
                QuickSort(array, start, j);
            }
        }

        public static void MergeSort(int[] array)
        {
            var sortedArray = PMergeSort(array);
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = sortedArray[i];
            }
        }

        public static void BubbleSort(int[] array)
        {
            if (ReferenceEquals(array, null))
            {
                throw new ArgumentNullException(nameof(array));
            }

            for (int i = 0; i < array.Length - 1; i++)
            {
                for (int j = 0; j < array.Length - 1; j++)
                {
                    if (array[j] > array[j + 1])
                    {
                        Swap(ref array[j], ref array[j + 1]);
                    }
                }
            }
        }

        #endregion

        #region private methods

        private static void VerifyInput(int[] array, int start, int end)
        {
            if (ReferenceEquals(array, null))
            {
                throw new ArgumentNullException(nameof(array));
            }

            if (start < 0)
            {
                throw new ArgumentException($"{nameof(start)} must be greater than zero", nameof(start));
            }

            if (start > end)
            {
                throw new ArgumentException(nameof(start) + " must be less than " + nameof(end));
            }

            if (array.Length <= 1)
            {
                return;
            }

            if (array.Length <= end)
            {
                throw new ArgumentException(nameof(end) + " must less than array length", nameof(end));
            }
        }

        private static int[] PMergeSort(int[] array)
        {
            if (array == null)
            {
                throw new ArgumentNullException(nameof(array));
            }

            if (array.Length <= 1)
            {
                return array;
            }

            int center = array.Length / 2;
            int[] array1 = PMergeSort(array.Take(center).ToArray());
            int[] array2 = PMergeSort(array.Skip(center).ToArray());

            return Merge(array1, array2);
        }

        private static int[] Merge(int[] array1, int[] array2)
        {
            int array1Length = array1.Length;
            int array2Length = array2.Length;
            int resultLength = array1Length + array2Length;

            int[] result = new int[resultLength];
            int i = 0, j = 0;
            for (int k = 0; k < resultLength; k++)
            {
                if ((i < array1Length) && (j < array2Length))
                {
                    if (array1[i] > array2[j])
                    {
                        result[k] = array2[j++];
                    }
                    else
                    {
                        result[k] = array1[i++];
                    }
                }
                else
                {
                    if (j < array2Length)
                    {
                        result[k] = array2[j++];
                    }
                    else
                    {
                        result[k] = array1[i++];
                    }
                }
            }

            return result;
        }

        private static void Swap<T>(ref T x, ref T y)
        {
            var temp = x;
            x = y;
            y = temp;
        }

        #endregion
    }
}

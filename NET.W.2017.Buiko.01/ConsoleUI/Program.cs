using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SortAlgorithm;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] array;

            #region QuickSort tests
            Console.WriteLine("QuickSort tests:");

            array = new int[] { 5, 1, 9, 6, 7, 5, 8 };
            Sort.QuickSort(array, 0, array.Length - 1);
            PrintArray(array);

            array = new int[] { 5, 5, 1, 1, 7, 3, 8 };
            Sort.QuickSort(array, 0, array.Length - 1);
            PrintArray(array);

            array = new int[] { 0, 1, 2, 6, 7, 3, 8, 15, -1 };
            Sort.QuickSort(array, 0, array.Length - 1);
            PrintArray(array);

            try
            {
                array = new int[] { 0, 1, 2, 6, 7, 3, 8, 15, -1 };
                Sort.QuickSort(array, array.Length - 1, 0);
                PrintArray(array);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            try
            {
                array = new int[] { 0, 1, 2, 6, 7, 3, 8, 15, -1 };
                Sort.QuickSort(array, 0, array.Length + 10);
                PrintArray(array);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            try
            {
                array = new int[0];
                Sort.QuickSort(null, 0, 5);
                PrintArray(array);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            #endregion

            Console.WriteLine();

            #region MergeSort tests
            Console.WriteLine("MergeSort tests:");

            array = new int[] { 5, 1, 9, 6, 7, 5, 8 };
            Sort.MergeSort(array);
            PrintArray(array);

            array = new int[] { 5, 5, 1, 1, 7, 3, 8 };
            Sort.MergeSort(array);
            PrintArray(array);

            array = new int[] { 0, 1, 2, 6, 7, 3, 8, 15, -1 };
            Sort.MergeSort(array);
            PrintArray(array);

            try
            {
                Sort.MergeSort(null);
                PrintArray(array);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            try
            {
                array = new int[0];
                Sort.MergeSort(array);
                PrintArray(array);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            #endregion

            Console.ReadLine();
        }

        static void PrintArray(int[] array)
        {
            foreach (int element in array)
                Console.Write(element + " ");
            Console.WriteLine();
        }
    }
}

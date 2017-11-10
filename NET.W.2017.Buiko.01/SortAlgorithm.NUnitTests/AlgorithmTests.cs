using System;
using System.Linq;
using Algorithm;
using NUnit.Framework;

namespace SortAlgorithm.NUnitTests
{
    [TestFixture]
    public class AlgorithmTests
    {
        #region Test data

        private static readonly int[] TestArray1 = Enumerable.Range(0, 1000).Reverse().ToArray();
        private static readonly int[] TestArray2 = Enumerable.Range(0, 10000).Reverse().ToArray();
        private static readonly int[] TestArray3 = Enumerable.Range(100, 1000).ToArray();
        private static readonly int[] TestArray4 = Enumerable.Range(100, 10000).ToArray();
        private static readonly int[] TestArray5 = Enumerable.Range(100, 10000).Reverse().ToArray();
        private static readonly int[] TestArray6 = Enumerable.Repeat(0, 1000).ToArray();
        private static readonly int[] TestArray7 = Enumerable.Repeat(1, 10000).ToArray();

        private static readonly int[][] TestArrays = 
        {
            TestArray1, TestArray2, TestArray3,
            TestArray4, TestArray5, TestArray6,
            TestArray7
        };

        #endregion

        #region QuickSort tests

        [TestCase(new[] { 1, 2, 4, 7, 0, -3, 15 })]
        [TestCase(new[] { 1, 1, 1, 1, 2, 3, -9 })]
        [TestCase(new[] { 123, 524, -56, 123 })]
        [TestCase(new[] { 5, 5, 5, 5, 5, 5 })]
        [TestCase(new[] { 5 })]
        public void QuickSortTests_SortingArray(int[] array)
        {
            // Arrange.
            int[] temp = new int[array.Length];
            Array.Copy(array, temp, array.Length);

            // Act.
            Sort.QuickSort(array);
            Sort.QuickSort(temp, 0, temp.Length - 1);

            // Assert.
            for (int i = 0; i < array.Length - 1; i++)
            {
                if ((array[i] > array[i + 1]) && (temp[i] > temp[i + 1]))
                {
                    Assert.Fail();
                }
            }
        }

        [Test]
        public void QuickSortTests_SortingBigArrays()
        {
            // Arrange.
            int[][] arrays;
            ArrayInitialization(TestArrays, out arrays);

            // Act.
            foreach (var array in arrays)
            {
                Sort.QuickSort(array);
            }

            // Assert.
            foreach (var array in arrays)
            {
                for (int i = 0; i < array.Length - 1; i++)
                {
                    if (array[i] > array[i + 1])
                    {
                        Assert.Fail();
                    }
                }
            }
        }

        [TestCase(0, 6, new[] { 1, 2, 4, 7, 0, -3, 15 })]
        [TestCase(3, 5, new[] { 1, 1, 1, 1, 2, 3, -9 })]
        [TestCase(1, 5, new[] { 1, 89, 1, 1, 2, 3, -9 })]
        [TestCase(0, 3, new[] { 123, 524, -56, 123 })]
        [TestCase(0, 0, new[] { 5, 5, 5, 5, 5, 5 })]
        [TestCase(0, 0, new[] { 5 })]
        public void QuickSortTests_ArraySortingWithinBorders(int start, int end, int[] array)
        {
            // Arrange.

            // Act.
            Sort.QuickSort(array, start, end);

            // Assert.
            for (int i = start; i < end; i++)
            {
                if (array[i] > array[i + 1])
                {
                    Assert.Fail();
                }
            }
        }

        [TestCase(-9, 5, new[] { 1, 2, 3, 4, 5 })]
        [TestCase(10, 5, new[] { 1, 2, 3, 4 })]
        [TestCase(0, 10, new[] { 1, 2, 3, 4 })]
        public void QuickSortArgumentExceptionTests(int start, int end, int[] array)
        {
            Assert.Throws<ArgumentException>(() => Sort.QuickSort(array, start, end));
        }

        [Test]
        public void QuickSortArgumentNullExceptionTests()
        {
            Assert.Throws<ArgumentNullException>(() => Sort.QuickSort(null));
        }

        #endregion

        #region MergeSort tests

        [TestCase(new[] { 1, 2, 4, 7, 0, -3, 15 })]
        [TestCase(new[] { 1, 1, 1, 1, 2, 3, -9 })]
        [TestCase(new[] { 123, 524, -56, 123 })]
        [TestCase(new[] { 5, 5, 5, 5, 5, 5 })]
        [TestCase(new[] { 5 })]
        [TestCase(new int[0])]
        public void MergeSortTests_SortingArray(int[] array)
        {
            // Arrange.

            // Act.
            Sort.MergeSort(array);

            // Assert.
            for (int i = 0; i < array.Length - 1; i++)
            {
                if (array[i] > array[i + 1])
                {
                    Assert.Fail();
                }
            }
        }

        [Test]
        public void MergeSortTests_SortBigArrays()
        {
            // Arrange.
            int[][] arrays;
            ArrayInitialization(TestArrays, out arrays);

            // Act.
            foreach (var array in arrays)
            {
                Sort.MergeSort(array);
            }

            // Assert.
            foreach (var array in arrays)
            {
                for (int i = 0; i < array.Length - 1; i++)
                {
                    if (array[i] > array[i + 1])
                    {
                        Assert.Fail();
                    }
                }
            }
        }

        [Test]
        public void MergeSortArgumentNullExceptionTests()
        {
            Assert.Throws<ArgumentNullException>(() => Sort.MergeSort(null));
        }

        #endregion

        #region BubbleSort tests

        [TestCase(new[] { 1, 2, 4, 7, 0, -3, 15 })]
        [TestCase(new[] { 1, 1, 1, 1, 2, 3, -9 })]
        [TestCase(new[] { 123, 524, -56, 123 })]
        [TestCase(new[] { 5, 5, 5, 5, 5, 5 })]
        [TestCase(new[] { 5 })]
        [TestCase(new int[0])]
        public void BubbleSortTests_SortingArray(int[] array)
        {
            // Arrange.

            // Act.
            Sort.BubbleSort(array);

            // Assert.
            for (int i = 0; i < array.Length - 1; i++)
            {
                if (array[i] > array[i + 1])
                {
                    Assert.Fail();
                }
            }
        }

        [Test]
        public void BubbleSortTests_SortBigArrays()
        {
            // Arrange.
            int[][] arrays;
            ArrayInitialization(TestArrays, out arrays);

            // Act.
            foreach (var array in arrays)
            {
                Sort.BubbleSort(array);
            }

            // Assert.
            foreach (var array in arrays)
            {
                for (int i = 0; i < array.Length - 1; i++)
                {
                    if (array[i] > array[i + 1])
                    {
                        Assert.Fail();
                    }
                }
            }
        }

        [Test]
        public void BubbleSortArgumentNullExceptionTests()
        {
            Assert.Throws<ArgumentNullException>(() => Sort.BubbleSort(null));
        }

        #endregion

        #region test helpers

        private static void ArrayInitialization(int[][] source, out int[][] dest)
        {
            dest = new int[source.Length][];
            for (int i = 0; i < source.Length; i++)
            {
                dest[i] = new int[source[i].Length];
                Array.Copy(source[i], dest[i], source[i].Length);
            }
        }

        #endregion
    }
}

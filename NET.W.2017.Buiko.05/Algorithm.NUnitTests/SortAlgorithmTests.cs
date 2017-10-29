using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Algorithm.NUnitTests.comparator;
using NUnit.Framework;

namespace Algorithm.NUnitTests
{
    [TestFixture]
    public class SortAlgorithmTests
    {
        #region Sum sort tests

        [Test]
        public void AscendingSumSortTests()
        {
            for (int j = 0; j < 10; j++)
            {
                var array = TestHelper.GenerateJaggedArray(Guid.NewGuid().GetHashCode());
              //  TestHelper.Track(array, $"---------------------------\nUnsorted array {j + 1}:");

                SortAlgotithm.Sort(array, new AscendingSumComparator());
            //    TestHelper.Track(array, $"Sorted array {j + 1}:");

                for (int i = 0; i < array.Length - 1; i++)
                {
                    int sum1 = array[i].Sum();
                    int sum2 = array[i + 1].Sum();
                    if (sum1 > sum2) Assert.Fail();
                }
            }
        }

        [Test]
        public void DescendingSumSortTests()
        {
            for (int j = 0; j < 10; j++)
            {
                var array = TestHelper.GenerateJaggedArray(Guid.NewGuid().GetHashCode());
             //   TestHelper.Track(array, $"---------------------------\nUnsorted array {j + 1}:");

                SortAlgotithm.Sort(array, new DescendingSumComparator());
            //    TestHelper.Track(array, $"Sorted array {j + 1}:");

                for (int i = 0; i < array.Length - 1; i++)
                {
                    int sum1 = array[i].Sum();
                    int sum2 = array[i + 1].Sum();
                    if (sum1 < sum2) Assert.Fail();
                }
            }
        }

        #endregion // !Sum sort tests.

        #region Max element tests

        [Test]
        public void AscendingMaxElementSortTests()
        {
            for (int j = 0; j < 10; j++)
            {
                var array = TestHelper.GenerateJaggedArray(Guid.NewGuid().GetHashCode());
                TestHelper.Track(array, $"---------------------------\nUnsorted array {j + 1}:");

                SortAlgotithm.Sort(array, new AscendingMaxElementComparator());
                TestHelper.Track(array, $"Sorted array {j + 1}:");

                for (int i = 0; i < array.Length - 1; i++)
                {
                    int maxElement1 = array[i].Max();
                    int maxElement2 = array[i + 1].Max();
                    if (maxElement1 > maxElement2) Assert.Fail();
                }
            }
        }

        [Test]
        public void DescendingMaxElementSortTests()
        {
            for (int j = 0; j < 10; j++)
            {
                var array = TestHelper.GenerateJaggedArray(Guid.NewGuid().GetHashCode());
             //   TestHelper.Track(array, $"---------------------------\nUnsorted array {j + 1}:");

                SortAlgotithm.Sort(array, new DescendingMaxElementComparator());
             //   TestHelper.Track(array, $"Sorted array {j + 1}:");

                for (int i = 0; i < array.Length - 1; i++)
                {
                    int maxElement1 = array[i].Max();
                    int maxElement2 = array[i + 1].Max();
                    if (maxElement1 < maxElement2) Assert.Fail();
                }
            }
        }

        #endregion // !Max element tests.

        #region Min element tests

        [Test]
        public void AscendingMinElementSortTests()
        {
            for (int j = 0; j < 10; j++)
            {
                var array = TestHelper.GenerateJaggedArray(Guid.NewGuid().GetHashCode());
              //  TestHelper.Track(array, $"---------------------------\nUnsorted array {j + 1}:");

                SortAlgotithm.Sort(array, new AscendingMinElementComparator());
             //   TestHelper.Track(array, $"Sorted array {j + 1}:");

                for (int i = 0; i < array.Length - 1; i++)
                {
                    int minElement1 = array[i].Min();
                    int minElement2 = array[i + 1].Min();
                    if (minElement1 > minElement2) Assert.Fail();
                }
            }
        }

        [Test]
        public void DescendingMinElementSortTests()
        {
            for (int j = 0; j < 10; j++)
            {
                var array = TestHelper.GenerateJaggedArray(Guid.NewGuid().GetHashCode());
             //   TestHelper.Track(array, $"---------------------------\nUnsorted array {j + 1}:");

                SortAlgotithm.Sort(array, new DescendingMinElementComparator());
             //   TestHelper.Track(array, $"Sorted array {j + 1}:");

                for (int i = 0; i < array.Length - 1; i++)
                {
                    int minElement1 = array[i].Min();
                    int minElement2 = array[i + 1].Min();
                    if (minElement1 < minElement2) Assert.Fail();
                }
            }
        }

        #endregion // !Min element tests.
    }
}

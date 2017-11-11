using System;
using System.Linq;
using Algorithm.NUnitTests.Comparator;
using Algorithm2;
using NUnit.Framework;

namespace Algorithm.NUnitTests
{
    [TestFixture]
    public class SortingAlgorithm2Tests
    {
        #region Sum sort tests

        [Test]
        public void AscendingSumSortTests()
        {
            for (int j = 0; j < 10; j++)
            {
                var array = TestHelper.GenerateJaggedArray(Guid.NewGuid().GetHashCode());
                //// TestHelper.Track(array, $"---------------------------\nUnsorted array {j + 1}:");

                SortingAlgorithm2.Sort(array, new AscendingSumComparator());
                //// TestHelper.Track(array, $"Sorted array {j + 1}:");

                Assert.IsTrue(TestHelper.IsXscendingOrder(array, arr => arr.Sum(), (a, b) => a > b));
            }
        }

        [Test]
        public void DescendingSumSortTests()
        {
            for (int j = 0; j < 10; j++)
            {
                var array = TestHelper.GenerateJaggedArray(Guid.NewGuid().GetHashCode());
                //// TestHelper.Track(array, $"---------------------------\nUnsorted array {j + 1}:");

                SortingAlgorithm2.Sort(array, new DescendingSumComparator().Compare);
                //// TestHelper.Track(array, $"Sorted array {j + 1}:");

                Assert.IsTrue(TestHelper.IsXscendingOrder(array, arr => arr.Sum(), (a, b) => a < b));
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

                SortingAlgorithm2.Sort(array, new AscendingMaxElementComparator());
                TestHelper.Track(array, $"Sorted array {j + 1}:");

                Assert.IsTrue(TestHelper.IsXscendingOrder(array, arr => arr.Max(), (a, b) => a > b));
            }
        }

        [Test]
        public void DescendingMaxElementSortTests()
        {
            for (int j = 0; j < 10; j++)
            {
                var array = TestHelper.GenerateJaggedArray(Guid.NewGuid().GetHashCode());
                //// TestHelper.Track(array, $"---------------------------\nUnsorted array {j + 1}:");

                SortingAlgorithm2.Sort(array, new DescendingMaxElementComparator().Compare);
                //// TestHelper.Track(array, $"Sorted array {j + 1}:");

                Assert.IsTrue(TestHelper.IsXscendingOrder(array, arr => arr.Max(), (a, b) => a < b));
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
                //// TestHelper.Track(array, $"---------------------------\nUnsorted array {j + 1}:");

                SortingAlgorithm2.Sort(array, new AscendingMinElementComparator());
                //// TestHelper.Track(array, $"Sorted array {j + 1}:");

                Assert.IsTrue(TestHelper.IsXscendingOrder(array, arr => arr.Min(), (a, b) => a > b));
            }
        }

        [Test]
        public void DescendingMinElementSortTests()
        {
            for (int j = 0; j < 10; j++)
            {
                var array = TestHelper.GenerateJaggedArray(Guid.NewGuid().GetHashCode());
                //// TestHelper.Track(array, $"---------------------------\nUnsorted array {j + 1}:");

                SortingAlgorithm2.Sort(array, new DescendingMinElementComparator().Compare);
                //// TestHelper.Track(array, $"Sorted array {j + 1}:");

                Assert.IsTrue(TestHelper.IsXscendingOrder(array, arr => arr.Min(), (a, b) => a < b));
            }
        }

        #endregion // !Min element tests.
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Algorithm.NUnitTests
{
    [TestFixture]
    public class SearchAlgorithmTests
    {
        #region test data

        public static IEnumerable<TestCaseData> IntTestData
        {
            get
            {
                yield return new TestCaseData(new[] {1, 2, 3, 4, 5, 6, 7}, 4).Returns(3);
                yield return new TestCaseData(new[] {1, 1, 1, 1, 1, 1}, 1).Returns(2);
                yield return new TestCaseData(new[] {1}, 1).Returns(0);

                yield return new TestCaseData(new[] { 1, 2, 3, 4, 5, 6, 7 }, 77).Returns(-1);
                yield return new TestCaseData(new[] { 1, 1, 1, 1, 1, 1 }, 77).Returns(-1);
                yield return new TestCaseData(new[] { 1 }, 77).Returns(-1);
                yield return new TestCaseData(new int[0], 1).Returns(-1);

                yield return new TestCaseData(Enumerable.Range(0, 100).ToArray(), 1).Returns(1);
                yield return new TestCaseData(Enumerable.Range(0, 1000).ToArray(), 500).Returns(500);
                yield return new TestCaseData(Enumerable.Range(0, 10000).ToArray(), 9999).Returns(9999);
                yield return new TestCaseData(Enumerable.Range(0, 100000).ToArray(), 200).Returns(200);
                yield return new TestCaseData(Enumerable.Range(0, 1000000).ToArray(), 0).Returns(0);
                yield return new TestCaseData(Enumerable.Range(0, 10000000).ToArray(), 789).Returns(789);

                yield return new TestCaseData(Enumerable.Range(0, 100).ToArray(), -5).Returns(-1);
                yield return new TestCaseData(Enumerable.Range(0, 1000).ToArray(), -1).Returns(-1);
                yield return new TestCaseData(Enumerable.Range(0, 10000).ToArray(), -78).Returns(-1);
                yield return new TestCaseData(Enumerable.Range(0, 100000).ToArray(), 987654321).Returns(-1);
                yield return new TestCaseData(Enumerable.Range(0, 1000000).ToArray(), 123456789).Returns(-1);
                yield return new TestCaseData(Enumerable.Range(0, 10000000).ToArray(), 11111111).Returns(-1);
            }
        }

        public static IEnumerable<TestCaseData> DoubleTestData
        {
            get
            {
                yield return new TestCaseData(new[] { 1d, 2d, 3.32d, 4.01d, 5d, 6d, 7d }, 4.01d).Returns(3);
                yield return new TestCaseData(new[] { 1d, 1d, 1d, 1d, 1d, 1d }, 1d).Returns(2);
                yield return new TestCaseData(new[] { 1d }, 1d).Returns(0);

                yield return new TestCaseData(new[] { 1d, 2d, 3d, 4d, 5d, 6d, 7d }, 77d).Returns(-1);
                yield return new TestCaseData(new[] { 1d, 1d, 1d, 1d, 1d, 1d }, 77d).Returns(-1);
                yield return new TestCaseData(new[] { 1d }, 77d).Returns(-1);
                yield return new TestCaseData(new double[0], 1d).Returns(-1);
            }
        }

        public static IEnumerable<TestCaseData> CharTestData
        {
            get
            {
                yield return new TestCaseData(new[] { '1', '2', '3', '4', '5', '6', '7' }, '7').Returns(6);
                yield return new TestCaseData(new[] { '1', '1', '1', '1', '1', '1' }, '1').Returns(2);
                yield return new TestCaseData(new[] { '\'' }, '\'').Returns(0);

                yield return new TestCaseData(new[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G' }, '7').Returns(-1);
                yield return new TestCaseData(new[] { '1', '1', '1', '1', '1', '1' }, ';').Returns(-1);
                yield return new TestCaseData(new[] { '\'' }, '9').Returns(-1);
                yield return new TestCaseData(new char[0], '1').Returns(-1);
            }
        }

        public static IEnumerable<TestCaseData> StringTestData
        {
            get
            {
                yield return new TestCaseData(new[] { "1", "2", "3", "4", "5", "6", "7" }, "7").Returns(6);
                yield return new TestCaseData(new[] { "1", "1", "1", "1", "1", "1" }, "1").Returns(2);
                yield return new TestCaseData(new[] { "1" }, "1").Returns(0);

                yield return new TestCaseData(new[] { "1", "2", "3", "4", "5", "6", "7" }, "712").Returns(-1);
                yield return new TestCaseData(new[] { "1", "1", "1", "1", "1", "1" }, "111").Returns(-1);
                yield return new TestCaseData(new[] { "1" }, "12").Returns(-1);
                yield return new TestCaseData(new string[0], "1").Returns(-1);
            }
        }

        public static IEnumerable<TestCaseData> CustomClassTestData
        {
            get
            {
                yield return new TestCaseData(new[]
                {
                    new CustomClass(0, string.Empty),
                    new CustomClass(-1, "1"),
                    new CustomClass(100, "15"),
                    new CustomClass(89, "15"),
                    new CustomClass(4, "4"),
                }, 
                new CustomClass(0, string.Empty)).Returns(0);
                yield return new TestCaseData(new[]
                    {
                        new CustomClass(0, string.Empty),
                        new CustomClass(0, string.Empty),
                        new CustomClass(0, string.Empty),
                        new CustomClass(0, string.Empty)
                    },
                    new CustomClass(0, string.Empty)).Returns(1);
                yield return new TestCaseData(new[] { new CustomClass(0, string.Empty) }, new CustomClass(0, string.Empty)).Returns(0);

                yield return new TestCaseData(new[]
                    {
                        new CustomClass(0, string.Empty),
                        new CustomClass(-1, "1"),
                        new CustomClass(100, "15"),
                        new CustomClass(89, "15"),
                        new CustomClass(4, "4"),
                    },
                    new CustomClass(0, "1111")).Returns(-1);
                yield return new TestCaseData(new[]
                    {
                        new CustomClass(0, string.Empty),
                        new CustomClass(0, string.Empty),
                        new CustomClass(0, string.Empty),
                        new CustomClass(0, string.Empty)
                    },
                    new CustomClass(0, "1")).Returns(-1);
                yield return new TestCaseData(new[] { new CustomClass(0, string.Empty) }, new CustomClass(0, "1")).Returns(-1);
            }
        }

        public static IEnumerable<TestCaseData> CustomStructureTestData
        {
            get
            {
                yield return new TestCaseData(new[]
                    {
                        new CustomStructure(0, string.Empty),
                        new CustomStructure(-1, "1"),
                        new CustomStructure(100, "15"),
                        new CustomStructure(89, "15"),
                        new CustomStructure(4, "4"),
                    },
                    new CustomStructure(0, string.Empty)).Returns(0);
                yield return new TestCaseData(new[]
                    {
                        new CustomStructure(0, string.Empty),
                        new CustomStructure(0, string.Empty),
                        new CustomStructure(0, string.Empty),
                        new CustomStructure(0, string.Empty)
                    },
                    new CustomStructure(0, string.Empty)).Returns(1);
                yield return new TestCaseData(new[] { new CustomStructure(0, string.Empty) }, new CustomStructure(0, string.Empty)).Returns(0);

                yield return new TestCaseData(new[]
                    {
                        new CustomStructure(0, string.Empty),
                        new CustomStructure(-1, "1"),
                        new CustomStructure(100, "15"),
                        new CustomStructure(89, "15"),
                        new CustomStructure(4, "4"),
                    },
                    new CustomStructure(0, "1111")).Returns(-1);
                yield return new TestCaseData(new[]
                    {
                        new CustomStructure(0, string.Empty),
                        new CustomStructure(0, string.Empty),
                        new CustomStructure(0, string.Empty),
                        new CustomStructure(0, string.Empty)
                    },
                    new CustomStructure(0, "1")).Returns(-1);
                yield return new TestCaseData(new[] { new CustomStructure(0, string.Empty) }, new CustomStructure(0, "1")).Returns(-1);
            }
        }

        #endregion // !test data.

        #region tests

        [Test, TestCaseSource(nameof(IntTestData))]
        public int BinarySearchTests(int[] array, int value) =>
            SearchAlgorithm.BinarySearch(array, value);

        [Test, TestCaseSource(nameof(DoubleTestData))]
        public int BinarySearchTests(double[] array, double value) =>
            SearchAlgorithm.BinarySearch(array, value);

        [Test, TestCaseSource(nameof(CharTestData))]
        public int BinarySearchTests(char[] array, char value) =>
            SearchAlgorithm.BinarySearch(array, value);

        [Test, TestCaseSource(nameof(StringTestData))]
        public int BinarySearchTests(string[] array, string value) =>
            SearchAlgorithm.BinarySearch(array, value);

        [Test, TestCaseSource(nameof(CustomClassTestData))]
        public int BinarySearchTests(CustomClass[] array, CustomClass value) =>
            SearchAlgorithm.BinarySearch(array, value);

        [Test, TestCaseSource(nameof(CustomStructureTestData))]
        public int BinarySearchTests(CustomStructure[] array, CustomStructure value) =>
            SearchAlgorithm.BinarySearch(array, value);

        [Test, TestCaseSource(nameof(CustomClassTestData))]
        public int BinarySearchTestsWithComparer(CustomClass[] array, CustomClass value) =>
            SearchAlgorithm.BinarySearch(array, value, new CustomClassComparer());

        [Test]
        public void BinarySearchArgumentNullExceptionTests() =>
            Assert.Throws<ArgumentNullException>(() => SearchAlgorithm.BinarySearch(null, 1));

        [TestCase(-1, 14)]
        [TestCase(0, -1)]
        [TestCase(1, 0)]
        [TestCase(0, 4)]
        [TestCase(0, 5)]
        public void BinarySearchArgumentExceptionTests(int start, int end) =>
            Assert.Throws<ArgumentException>(() => SearchAlgorithm.BinarySearch(new[] { 10, 15, 20 }, start, end, 1));

        #endregion // !tests.
    }
}

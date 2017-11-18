using NUnit.Framework;

namespace Matrix.NUnitTests
{
    [TestFixture]
    public class MatrixTests
    {
        [Test]
        public void ConstructorTest()
        {
            ConstructorTest(14, new SquareMatrix<int>(14));
            ConstructorTest(14, new SymmetricMatrix<int>(14));
            ConstructorTest(14, new DiagonalMatrix<int>(14));
        }

        [Test]
        public void EnumeratorTest()
        {
            var array = new[,] { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 } };
            EnumeratorTest(array, new SquareMatrix<int>(array));
        }

        [Test]
        public void IndexerTest()
        {
            var array = new[,] { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 } };
            IndexerTest(array, new SquareMatrix<int>(array));
        }

        [Test]
        public void EqalityTest()
        {
            var array = new[,] { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 } };

            Assert.IsTrue(new SquareMatrix<int>(array).Equals(new SquareMatrix<int>(array)));
            Assert.IsTrue(new SymmetricMatrix<int>(array).Equals(new SymmetricMatrix<int>(array)));
            Assert.IsTrue(new DiagonalMatrix<int>(array).Equals(new DiagonalMatrix<int>(array)));

            Assert.IsFalse(new SquareMatrix<int>(array).Equals(new SymmetricMatrix<int>(array)));
            Assert.IsFalse(new SymmetricMatrix<int>(array).Equals(new DiagonalMatrix<int>(array)));
            Assert.IsFalse(new DiagonalMatrix<int>(array).Equals(new SquareMatrix<int>(array)));
        }

        [Test]
        public void ToArrayTest()
        {
            var array = new[,] { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 } };
            ToArrayTest(array, new SquareMatrix<int>(array));
        }

        [Test]
        public void AddTest()
        {
            var array = new[,] { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 } };
            var matrix1 = new SquareMatrix<int>(array);
            var matrix2 = new SquareMatrix<int>(array);

            var result = matrix1.Add(matrix2);

            for (int i = 0; i < matrix1.RowCount; i++)
            {
                for (int j = 0; j < matrix1.ColumnCount; j++)
                {
                    Assert.AreEqual(result[i, j], array[i, j] * 2);
                }
            }
        }

        private static void ConstructorTest(int order, Matrix<int> matrix)
        {
            Assert.IsTrue(matrix.RowCount == matrix.ColumnCount);
            Assert.IsTrue(14 == matrix.RowCount);
            Assert.IsTrue(14 * 14 == matrix.Count);
        }

        private static void EnumeratorTest(int[,] array, Matrix<int> matrix)
        {
            int i = 0, j = 0;
            foreach (var element in matrix)
            {
                Assert.AreEqual(array[i, j++], element);
                if (j == array.GetLength(1))
                {
                    j = 0;
                    i++;
                }
            }
        }

        private static void IndexerTest(int[,] array, Matrix<int> matrix)
        {
            for (int i = 0; i < matrix.RowCount; i++)
            {
                for (int j = 0; j < matrix.ColumnCount; j++)
                {
                    Assert.AreEqual(array[i, j], matrix[i, j]);
                }
            }
        }

        private static void ToArrayTest(int[,] array, Matrix<int> matrix)
        {
            var temp = matrix.ToArray();
            for (int i = 0; i < matrix.RowCount; i++)
            {
                for (int j = 0; j < matrix.ColumnCount; j++)
                {
                    Assert.AreEqual(array[i, j], temp[i, j]);
                }
            }
        }
    }
}

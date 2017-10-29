using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.NUnitTests
{
    internal static class TestHelper
    {
        private const int LeftBorder = 1;
        private const int RightBorder = 10;

        #region public methods

        public static int[][] GenerateJaggedArray(int seed)
        {
            var random = new Random(seed);
            int rowCount = random.Next(LeftBorder, RightBorder);

            var array = new int[rowCount][];

            for (int i = 0; i < array.Length; i++)
                array[i] = GetRandomArray(seed + Guid.NewGuid().GetHashCode());

            return array;
        }

        public static void Track(int[][] array, string message)
        {
            Console.WriteLine(message);
            for (int i = 0; i < array.Length; i++)
            {
                for (int j = 0; j < array[i].Length; j++)
                    Console.Write(array[i][j] + " ");
                Console.WriteLine();
            }
            Console.WriteLine("");
        }

        #endregion

        #region private methods

        private static int[] GetRandomArray(int seed)
        {
            var random = new Random(seed);
            int length = random.Next(LeftBorder, RightBorder);

            var array = new int[length];
            FillArrayRandom(array, seed + Guid.NewGuid().GetHashCode());

            return array;
        }

        private static void FillArrayRandom(int[] array, int seed)
        {
            var random = new Random(seed);
            for (int i = 0; i < array.Length; i++)
                array[i] = random.Next(LeftBorder, RightBorder);
        }

        #endregion
    }
}

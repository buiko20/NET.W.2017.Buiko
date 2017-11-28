using Task2.Solution;

namespace Task2.Console
{
    internal class Program
    {
        private static void Main()
        {
            var randomBytesFileGenerator = new RandomBytesFileGenerator(@".\", ".bin");
            var randomCharsFileGenerator = new RandomCharsFileGenerator(@".\", ".txt");

            randomBytesFileGenerator.GenerateFiles(1, 100);
            randomCharsFileGenerator.GenerateFiles(1, 100);
        }
    }
}

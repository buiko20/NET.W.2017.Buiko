using System;
using System.Linq;
using System.Text;

namespace Task2.Solution
{
    public class RandomCharsFileGenerator : RandomFileContetGenerator
    {
        public RandomCharsFileGenerator(string workingDirectory, string fileExtension) : base(workingDirectory, fileExtension)
        {
        }

        protected override byte[] GenerateFileContent(int contentLength)
        {
            var generatedString = RandomString(contentLength);

            var bytes = Encoding.Unicode.GetBytes(generatedString);

            return bytes;
        }

        private static string RandomString(int size)
        {
            var random = new Random();

            const string input = "abcdefghijklmnopqrstuvwxyz0123456789";

            var chars = Enumerable.Range(0, size).Select(x => input[random.Next(0, input.Length)]);

            return new string(chars.ToArray());
        }
    }
}

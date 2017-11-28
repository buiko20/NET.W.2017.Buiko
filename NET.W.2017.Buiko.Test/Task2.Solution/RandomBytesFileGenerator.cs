using System;

namespace Task2.Solution
{
    public class RandomBytesFileGenerator : RandomFileContetGenerator
    {
        public RandomBytesFileGenerator(string workingDirectory, string fileExtension) 
            : base(workingDirectory, fileExtension)
        {
        }

        protected override byte[] GenerateFileContent(int contentLength)
        {
            var random = new Random();

            var fileContent = new byte[contentLength];

            random.NextBytes(fileContent);

            return fileContent;
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2.Solution
{
    public abstract class RandomFileContetGenerator
    {
        protected RandomFileContetGenerator(string workingDirectory, string fileExtension)
        {
            if (string.IsNullOrWhiteSpace(workingDirectory))
                throw new ArgumentException($"{nameof(workingDirectory)} is invalid", nameof(workingDirectory));

            if (string.IsNullOrWhiteSpace(fileExtension))
                throw new ArgumentException($"{nameof(fileExtension)} is invalid", nameof(fileExtension));

            WorkingDirectory = workingDirectory;
            FileExtension = fileExtension;
        }

        public string WorkingDirectory { get; }

        public string FileExtension { get; }

        public void GenerateFiles(int filesCount, int contentLength)
        {
            for (var i = 0; i < filesCount; ++i)
            {
                var generatedFileContent = this.GenerateFileContent(contentLength);

                var generatedFileName = $"{Guid.NewGuid()}{this.FileExtension}";

                this.WriteBytesToFile(generatedFileName, generatedFileContent);
            }
        }

        protected abstract byte[] GenerateFileContent(int contentLength);

        private void WriteBytesToFile(string fileName, byte[] content)
        {
            if (!Directory.Exists(WorkingDirectory))
            {
                Directory.CreateDirectory(WorkingDirectory);
            }

            File.WriteAllBytes($"{WorkingDirectory}//{fileName}", content);
        }
    }
}

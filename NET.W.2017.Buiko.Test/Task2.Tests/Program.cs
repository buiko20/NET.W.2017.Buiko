using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task2.Solution;

namespace Task2.Console
{
    internal class Program
    {
        private static void Main()
        {
            var randomBytesFileGenerator = new RandomBytesFileGenerator(@"D:\epam\homework_git\NET.W.2017.Buiko.Test\Task2.Tests\bin\Debug", ".bin");
            var randomCharsFileGenerator = new RandomCharsFileGenerator(@"D:\epam\homework_git\NET.W.2017.Buiko.Test\Task2.Tests\bin\Debug", ".txt");

            randomBytesFileGenerator.GenerateFiles(1, 100);
            randomCharsFileGenerator.GenerateFiles(1, 100);
        }
    }
}

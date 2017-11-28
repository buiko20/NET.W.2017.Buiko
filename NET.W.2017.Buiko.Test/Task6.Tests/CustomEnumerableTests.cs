using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Test6.Solution;

namespace Task6.Tests
{
    [TestFixture]
    public class CustomEnumerableTests
    {
        [Test]
        public void Generator_ForSequence1()
        {
            int[] expected = {1, 1, 2, 3, 5, 8, 13, 21, 34, 55};

            var computer = new FibonacciNumberGenerator();

            int i = 0;
            foreach (var number in Generator.GenerateSequence(expected.Length, 1, 1, computer))
            {
                Assert.AreEqual(expected[i++], number);
            }
        }

        [Test]
        public void Generator_ForSequence2()
        {
            int[] expected = { 1, 2, 4, 8, 16, 32, 64, 128, 256, 512 };

            var computer = new Formula2();

            int i = 0;
            foreach (var number in Generator.GenerateSequence(expected.Length, 1, 2, computer))
            {
                Assert.AreEqual(expected[i++], number);
            }
        }

        [Test]
        public void Generator_ForSequence3()
        {
            double[] expected = { 1, 2, 2.5, 3.3, 4.05757575757576, 4.87086926018965, 5.70389834408211, 6.55785277425587, 7.42763417076325, 8.31053343902137 };

            var computer = new Formula3();
            
            int i = 0;
            foreach (var number in Generator.GenerateSequence(expected.Length, 1, 2, computer))
            {
                Assert.IsTrue(expected[i++] - number < 0.0001);
            }
        }
    }
}

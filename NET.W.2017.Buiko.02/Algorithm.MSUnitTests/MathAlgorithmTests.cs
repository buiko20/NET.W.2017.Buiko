﻿using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Algorithm.MSUnitTests
{
    [TestClass]
    public class MathAlgorithmTests
    {
        [TestMethod]
        public void BitInsert_15insert15from0to0_15returned()
        {
            // Arrange.
            int number1 = 15, number2 = 15;
            int i = 0, j = 0;
            int expected = 15;

            // Act.
            int actual = MathAlgorithm.BitInsert(number1, number2, i, j);

            // Assert.
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void BitInsert_8insert15from0to0_9returned()
        {
            // Arrange.
            int number1 = 8, number2 = 15;
            int i = 0, j = 0;
            int expected = 9;

            // Act.
            int actual = MathAlgorithm.BitInsert(number1, number2, i, j);

            // Assert.
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void BitInsert_8insert15from3to8_120returned()
        {
            // Arrange.
            int number1 = 8, number2 = 15;
            int i = 3, j = 8;
            int expected = 120;

            // Act.
            int actual = MathAlgorithm.BitInsert(number1, number2, i, j);

            // Assert.
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void BitInsert_15insertnegative56from20to5_exceptionreturned()
        {
            // Arrange.
            int number1 = 15, number2 = 56;
            int i = 20, j = 5;

            // Act.
            MathAlgorithm.BitInsert(number1, number2, i, j);

            // Assert.
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void BitInsert_15insertnegative56from20to33_exceptionreturned()
        {
            // Arrange.
            int number1 = 15, number2 = 56;
            int i = 20, j = 33;

            // Act.
            MathAlgorithm.BitInsert(number1, number2, i, j);

            // Assert.
        }




        [TestMethod]
        public void FindNextBiggerNumber_12_21returned()
        {
            // Arrange.
            int number = 12;
            int expected = 21;

            // Act.
            int actual = MathAlgorithm.FindNextBiggerNumber(number);

            // Assert.
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void FindNextBiggerNumber_1234126_1234162returned()
        {
            // Arrange.
            int number = 1234126;
            int expected = 1234162;

            // Act.
            int actual = MathAlgorithm.FindNextBiggerNumber(number);

            // Assert.
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void FindNextBiggerNumber_10_negative1returned()
        {
            // Arrange.
            int number = 10;
            int expected = -1;

            // Act.
            int actual = MathAlgorithm.FindNextBiggerNumber(number);

            // Assert.
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void FindNextBiggerNumber_negativenumber_exceptionreturned()
        {
            // Arrange.
            int number = -1;

            // Act.
            MathAlgorithm.FindNextBiggerNumber(number);

            // Assert.
        }



        private static readonly int[] array1 = { 1, 2, 3, 4, 5, 6, 7, 68, 69, 70, 15, 17 };
        private static readonly int[] expected1 = { 7, 70, 17 };
        [TestMethod]
        public void FilterDigit_digit7array1_expected1returned()
        {
            // Arrange.
            int digit = 7;

            // Act.
            int[] actual = MathAlgorithm.FilterDigit(digit, array1);

            // Assert.
            Assert.IsTrue(actual.SequenceEqual(expected1));
        }

        [TestMethod]
        public void FilterDigit_digit7from13or72_72returned()
        {
            // Arrange.
            int digit = 7;
            int number1 = 13, number2 = 72;
            int expected = 72;

            // Act.
            int[] actual = MathAlgorithm.FilterDigit(digit, number1, number2);

            // Assert.
            Assert.AreEqual(expected, actual[0]);
        }

        [TestMethod]
        public void FilterDigit_digit7from13or31_emptyarrayreturned()
        {
            // Arrange.
            int digit = 7;
            int number1 = 13, number2 = 31;
            int expectedArrayLength = 0;

            // Act.
            int[] actual = MathAlgorithm.FilterDigit(digit, number1, number2);

            // Assert.
            Assert.AreEqual(expectedArrayLength, actual.Length);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void FilterDigit_digit7nullarray_exceptionreturned()
        {
            // Arrange.
            int digit = 7;

            // Act.
            MathAlgorithm.FilterDigit(digit, null);

            // Assert.
        }

        [TestMethod]
        public void FilterDigit_digit7emptyarray_exceptionreturned()
        {
            // Arrange.
            int digit = 7;

            // Act.
            int[] actual = MathAlgorithm.FilterDigit(digit);

            // Assert.
            Assert.IsNull(actual);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void FilterDigit_digit7array_exceptionreturned()
        {
            // Arrange.
            int digit = 123;

            // Act.
            MathAlgorithm.FilterDigit(digit, 123);

            // Assert.
        }



        [TestMethod]
        public void FindNthRoot_number1root5eps00001_1returned()
        {
            // Arrange.
            double number = 1;
            int root = 5;
            double eps = 0.0001;
            double expected = 1;

            // Act.
            double actual = MathAlgorithm.FindNthRoot(number, root, eps);
            actual = Math.Round(actual, 4);

            // Assert.
            Assert.IsTrue(Math.Abs(actual - expected) < eps);
        }

        [TestMethod]
        public void FindNthRoot_number8root3eps00001_2returned()
        {
            // Arrange.
            double number = 8;
            int root = 3;
            double eps = 0.0001;
            double expected = 2.0000;

            // Act.
            double actual = MathAlgorithm.FindNthRoot(number, root, eps);
            actual = Math.Round(actual, 4);

            // Assert.
            Assert.IsTrue(Math.Abs(expected - actual) < eps);
        }

        [TestMethod]
        public void FindNthRoot_number001root3eps00001_01returned()
        {
            // Arrange.
            double number = 0.001;
            int root = 3;
            double eps = 0.0001;
            double expected = 0.1000;

            // Act.
            double actual = MathAlgorithm.FindNthRoot(number, root, eps);
            actual = Math.Round(actual, 4);

            // Assert.
            Assert.IsTrue(Math.Abs(expected - actual) < eps);
        }

        [TestMethod]
        public void FindNthRoot_numbernegative0008root3eps00001_negative02returned()
        {
            // Arrange.
            double number = -0.008;
            int root = 3;
            double eps = 0.0001;
            double expected = -0.2000;

            // Act.
            double actual = MathAlgorithm.FindNthRoot(number, root, eps);
            actual = Math.Round(actual, 4);

            // Assert.
            Assert.IsTrue(Math.Abs(expected - actual) < eps);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void FindNthRoot_numbernegative0008rootnegative3eps0001_negative02returned()
        {
            // Arrange.
            double number = -0.008;
            int root = -3;
            double eps = 0.0001;

            // Act.
            MathAlgorithm.FindNthRoot(number, root, eps);
        }
    }
}
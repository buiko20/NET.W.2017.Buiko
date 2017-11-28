using System;
using System.Linq;
using Moq;
using NUnit.Framework;

namespace Task1.Solution.Tests
{
    [TestFixture]
    public class PasswordCheckerServiceTests
    {
        [TestCase("123", ExpectedResult = false)]
        [TestCase("", ExpectedResult = false)]
        [TestCase("11111111111111111", ExpectedResult = false)]
        [TestCase("111111afa", ExpectedResult = true)]
        [TestCase("aaaaaaaaaaaaaaa", ExpectedResult = false)]
        [TestCase("5aaaaaa", ExpectedResult = false)]
        [TestCase("aaaaaa123aaaaaaaaa", ExpectedResult = false)]
        public bool VerifyPasswordTests(string password)
        {
            var repository = new SqlRepository();
            var passwordCheckerService = new PasswordCheckerService();

            var result = passwordCheckerService.VerifyPassword(password, repository, Verify);

            return result.Item1;
        }

        [TestCase("aaaa234a")]
        [TestCase("5aaa23aaa")]
        [TestCase("aa234234aa")]
        public void PasswordCheckerServiceCallRepository_CreateMethod(string password)
        {
            var repositoryMock = new Mock<IRepository>();
            var passwordCheckerService = new PasswordCheckerService();

            passwordCheckerService.VerifyPassword(password, repositoryMock.Object, Verify);

            repositoryMock.Verify(repository => repository.Create(It.Is<string>(s => string.Equals(s, password, StringComparison.Ordinal))), Times.Once);
        }

        private static Tuple<bool, string> Verify(string password)
        {
            // check if length more than 7 chars 
            if (password.Length <= 7)
                return Tuple.Create(false, $"{nameof(password)} length too short");

            // check if length more than 10 chars for admins
            if (password.Length >= 15)
                return Tuple.Create(false, $"{nameof(password)} length too long");

            // check if password conatins at least one alphabetical character 
            if (!password.Any(char.IsLetter))
                return Tuple.Create(false, $"{nameof(password)} hasn't alphanumerical chars");

            // check if password conatins at least one digit character 
            if (!password.Any(char.IsNumber))
                return Tuple.Create(false, $"{nameof(password)} hasn't digits");

            return new Tuple<bool, string>(true, "OK");
        }
    }
}

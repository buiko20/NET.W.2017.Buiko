using System;
using BLL.Interface.AccountService;
using BLL.Services;
using DAL.Interface;
using DAL.Interface.DTO;
using Moq;
using NUnit.Framework;
using Services.Interface.AccountIdService;

namespace BLL.Tests
{
    [TestFixture]
    public class AccountServiceTests
    {
        private static string _firstName;
        private static string _secondName;
        private static string _accountId;

        private static Mock<IAccountRepository> _repositoryMock;
        private static Mock<IAccountIdService> _accountIdServiceMock;
        private static IAccountService _accountService;

        [OneTimeSetUp]
        public void Init()
        {
            // Arrange.
            _firstName = "Jon";
            _secondName = "Skeet";
            _accountId = "1";

            _repositoryMock = new Mock<IAccountRepository>();

            _accountIdServiceMock = new Mock<IAccountIdService>(MockBehavior.Strict);
            _accountIdServiceMock.Setup(service => service.GenerateAccountId(_firstName, _secondName))
                .Returns(_accountId);

            _accountService = new AccountService(_repositoryMock.Object, _accountIdServiceMock.Object);

            // Act. Because all tests use this method.
            _accountService.OpenAccount(_firstName, _secondName, 100m);
        }

        [Test]
        public void TestAllAtOnce()
        {
            // Arrange.
            string firstName = "Jon";
            string secondName = "Skeet";
            string accountId = "123";

            var repositoryMock = new Mock<IAccountRepository>();

            var accountIdServiceMock = new Mock<IAccountIdService>(MockBehavior.Strict);
            accountIdServiceMock.Setup(service => service.GenerateAccountId(firstName, secondName))
                .Returns(accountId);

            var accountService = new AccountService(repositoryMock.Object, accountIdServiceMock.Object);

            Predicate<DalAccount> predicate = account =>
                string.Equals(account.Id, accountId, StringComparison.Ordinal);

            // Act.
            accountService.OpenAccount(firstName, secondName, 100m);

            accountService.DepositMoney(accountId, 100m);
            accountService.DepositMoney(accountId, 100m);
            accountService.WithdrawMoney(accountId, 100m);

            accountService.CloseAccount(accountId);

            // Assert.
            accountIdServiceMock.Verify(service => service.GenerateAccountId(firstName, secondName), Times.Once);

            repositoryMock.Verify(repository => repository.GetAccounts(), Times.Once);

            repositoryMock.Verify(
                repository => repository.AddAccount(It.Is<DalAccount>(account => predicate(account))), Times.Once);

            repositoryMock.Verify(
                repository => repository.UpdateAccount(It.Is<DalAccount>(account => predicate(account))), Times.Exactly(3));

            repositoryMock.Verify(
                repository => repository.RemoveAccount(It.Is<DalAccount>(account => predicate(account))), Times.Once);
        }

        [Test]
        public void CallAccountIdService_GenerateAccountIdMethod()
        {
            // Assert.
            _accountIdServiceMock.Verify(
                service => service.GenerateAccountId(_firstName, _secondName), Times.Once);
        }

        [Test]
        public void CallAccountRepository_GetAccountsMethod()
        {
            // Assert.
            _repositoryMock.Verify(repository => repository.GetAccounts(), Times.Once);
        }

        [Test]
        public void CallAccountRepository_AddAccountMethod()
        {
            // Assert.
            _repositoryMock.Verify(
                repository => repository.AddAccount(It.Is<DalAccount>(
                    account => string.Equals(account.Id, _accountId, StringComparison.Ordinal))), Times.Once);
        }

        [Test]
        public void CallAccountRepository_UpdateAccountMethod()
        {
            // Act.
            _accountService.DepositMoney(_accountId, 100m);
            _accountService.DepositMoney(_accountId, 100m);
            _accountService.WithdrawMoney(_accountId, 100m);

            // Assert.
            _repositoryMock.Verify(
                repository => repository.UpdateAccount(It.Is<DalAccount>(
                    account => string.Equals(account.Id, _accountId, StringComparison.Ordinal))), Times.Exactly(3));
        }

        [Test]
        public void CallAccountRepository_RemoveAccountMethod()
        {
            // Arrange.
            string localAccountId = "2";

            _accountIdServiceMock.Setup(service => service.GenerateAccountId(_firstName, _secondName))
                .Returns(localAccountId);

            _accountService.OpenAccount(_firstName, _secondName, 100m);

            // Act.
            _accountService.CloseAccount(localAccountId);

            // Assert.
            _repositoryMock.Verify(
                repository => repository.RemoveAccount(It.Is<DalAccount>(
                    account => string.Equals(account.Id, localAccountId, StringComparison.Ordinal))), Times.Once);
        }
    }
}

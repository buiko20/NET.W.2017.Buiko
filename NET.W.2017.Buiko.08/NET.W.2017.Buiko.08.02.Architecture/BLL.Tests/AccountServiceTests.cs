using System;
using System.Linq.Expressions;
using BLL.Interface.Account;
using BLL.Interface.AccountIdService;
using BLL.Services;
using DAL.Interface;
using DAL.Interface.DTO;
using Moq;
using NUnit.Framework;

namespace BLL.Tests
{
    [TestFixture]
    public class AccountServiceTests
    {
        [TestCase("Anders", "Hejlsberg", "i++")]
        [TestCase("Bjarne", "Stroustrup", "++i")]
        [TestCase("Dennis", "Ritchie", "i += 1")]
        [TestCase("Niklaus", "Wirth", "i := i + 1")]
        public void TestAllAtOnce(string firstName, string secondName, string accountId)
        {
            // Arrange.
            var repositoryMock = new Mock<IAccountRepository>();

            var accountIdServiceMock = new Mock<IAccountIdService>(MockBehavior.Strict);
            accountIdServiceMock.Setup(service => service.GenerateAccountId(firstName, secondName))
                .Returns(accountId);

            var accountService = new AccountService(repositoryMock.Object);

            // Act.
            accountService.OpenAccount(firstName, secondName, 100m, "email1@mail.ru", accountIdServiceMock.Object);

            var dalAccount = new DalAccount
            {
                AccountType = "Base",
                BonusPoints = 0,
                CurrentSum = 100m,
                Id = accountId,
                OwnerFirstName = firstName,
                OwnerSecondName = secondName,
                OwnerEmail = "email1@mail.ru"
            };

            repositoryMock.Setup(repository => repository.GetAccounts())
                .Returns(new[] { dalAccount });

            accountService.DepositMoney(accountId, 100m);
            accountService.DepositMoney(accountId, 100m);
            accountService.WithdrawMoney(accountId, 100m);

            accountService.CloseAccount(accountId);

            // Assert.
            Predicate<DalAccount> predicate = account =>
                string.Equals(account.Id, accountId, StringComparison.Ordinal);

            accountIdServiceMock.Verify(
                service => service.GenerateAccountId(firstName, secondName), Times.AtLeastOnce);

            repositoryMock.Verify(
                repository => repository.GetAccounts(), Times.AtLeastOnce);

            repositoryMock.Verify(
                repository => repository.AddAccount(It.Is<DalAccount>(account => predicate(account))), Times.Once);

            repositoryMock.Verify(
                repository => repository.UpdateAccount(It.Is<DalAccount>(account => predicate(account))), Times.Exactly(3));

            repositoryMock.Verify(
                repository => repository.RemoveAccount(It.Is<DalAccount>(account => predicate(account))), Times.Once);
        }

        [TestCase("Jon", "Skeet", "123")]
        [TestCase("Jeffrey", "Richter", "ABC")]
        public void CallAccountIdService_GenerateAccountIdMethod(string firstName, string secondName, string accountId)
        {
            // Arrange.
            var repositoryMock = new Mock<IAccountRepository>();

            var accountIdServiceMock = new Mock<IAccountIdService>(MockBehavior.Strict);
            accountIdServiceMock.Setup(service => service.GenerateAccountId(firstName, secondName))
                .Returns(accountId);

            var accountService = new AccountService(repositoryMock.Object);

            // Act.
            accountService.OpenAccount(firstName, secondName, 100m, "email1@mail.ru", accountIdServiceMock.Object);

            // Assert.
            accountIdServiceMock.Verify(
                service => service.GenerateAccountId(firstName, secondName), Times.AtLeastOnce);
        }

        [TestCase("Jon", "Skeet", "123")]
        [TestCase("Jeffrey", "Richter", "ABC")]
        public void CallAccountRepository_GetAccountsMethod(string firstName, string secondName, string accountId)
        {
            // Arrange.
            var repositoryMock = new Mock<IAccountRepository>();

            var accountIdServiceMock = new Mock<IAccountIdService>(MockBehavior.Strict);
            accountIdServiceMock.Setup(service => service.GenerateAccountId(firstName, secondName))
                .Returns(accountId);

            var accountService = new AccountService(repositoryMock.Object);

            // Act.
            accountService.OpenAccount(firstName, secondName, 100m, "email1@mail.ru", accountIdServiceMock.Object);

            // Assert.
            repositoryMock.Verify(repository => repository.GetAccounts(), Times.Once);
        }

        [TestCase("Jon", "Skeet", "123")]
        [TestCase("Jeffrey", "Richter", "ABC")]
        public void CallAccountRepository_AddAccountMethod(string firstName, string secondName, string accountId)
        {
            // Arrange.
            var repositoryMock = new Mock<IAccountRepository>();

            var accountIdServiceMock = new Mock<IAccountIdService>(MockBehavior.Strict);
            accountIdServiceMock.Setup(service => service.GenerateAccountId(firstName, secondName))
                .Returns(accountId);

            var accountService = new AccountService(repositoryMock.Object);

            // Act.
            accountService.OpenAccount(firstName, secondName, 100m, "email1@mail.ru", accountIdServiceMock.Object);

            // Assert.
            Predicate<DalAccount> predicate = account =>
                string.Equals(account.Id, accountId, StringComparison.Ordinal);

            Expression<Action<IAccountRepository>> addMethodInvoke = repository =>
                repository.AddAccount(It.Is<DalAccount>(account => predicate(account)));

            repositoryMock.Verify(addMethodInvoke, Times.Once);
        }

        [TestCase("Jon", "Skeet", "123", "jon123Skeet@mail.ru")]
        [TestCase("Jeffrey", "Richter", "ABC", "richter98@mail.ru")]
        public void CallAccountRepository_UpdateAccountMethod(string firstName, string secondName, string accountId, string email)
        {
            // Arrange.
            var repositoryMock = new Mock<IAccountRepository>();
            repositoryMock.Setup(repository => repository.GetAccounts())
                .Returns(new DalAccount[0]);

            var accountIdServiceMock = new Mock<IAccountIdService>(MockBehavior.Strict);
            accountIdServiceMock.Setup(service => service.GenerateAccountId(firstName, secondName))
                .Returns(accountId);

            var accountService = new AccountService(repositoryMock.Object);

            // Act.
            accountService.OpenAccount(firstName, secondName, 100m, email, accountIdServiceMock.Object);

            var dalAccount = new DalAccount
            {
                AccountType = "Base",
                BonusPoints = 0,
                CurrentSum = 100m,
                Id = accountId,
                OwnerFirstName = firstName,
                OwnerSecondName = secondName,
                OwnerEmail = email
            };

            repositoryMock.Setup(repository => repository.GetAccounts())
                .Returns(new[] { dalAccount });

            accountService.DepositMoney(accountId, 100m);
            accountService.DepositMoney(accountId, 100m);
            accountService.WithdrawMoney(accountId, 100m);

            // Assert.
            Predicate<DalAccount> predicate = account =>
                string.Equals(account.Id, accountId, StringComparison.Ordinal);

            Expression<Action<IAccountRepository>> updateMethodInvoke = repository =>
                repository.UpdateAccount(It.Is<DalAccount>(account => predicate(account)));

            repositoryMock.Verify(updateMethodInvoke, Times.Exactly(3));
        }

        [TestCase("Jon", "Skeet", "123", "jon123Skeet@mail.ru")]
        [TestCase("Jeffrey", "Richter", "ABC", "richter98@mail.ru")]
        public void CallAccountRepository_RemoveAccountMethod(string firstName, string secondName, string accountId, string email)
        {
            // Arrange.
            var repositoryMock = new Mock<IAccountRepository>();

            var accountIdServiceMock = new Mock<IAccountIdService>(MockBehavior.Strict);
            accountIdServiceMock.Setup(service => service.GenerateAccountId(firstName, secondName))
                .Returns(accountId);

            var accountService = new AccountService(repositoryMock.Object);

            // Act.
            accountService.OpenAccount(firstName, secondName, 100m, email, accountIdServiceMock.Object);

            var dalAccount = new DalAccount
            {
                AccountType = "Base",
                BonusPoints = 0,
                CurrentSum = 100m,
                Id = accountId,
                OwnerFirstName = firstName,
                OwnerSecondName = secondName,
                OwnerEmail = email
            };

            repositoryMock.Setup(repository => repository.GetAccounts())
                .Returns(new[] { dalAccount });

            accountService.CloseAccount(accountId);

            // Assert.
            Predicate<DalAccount> predicate = account =>
                string.Equals(account.Id, accountId, StringComparison.Ordinal);

            Expression<Action<IAccountRepository>> removeMethodInvoke = repository =>
                repository.RemoveAccount(It.Is<DalAccount>(account => predicate(account)));

            repositoryMock.Verify(removeMethodInvoke, Times.Once);
        }
    }
}

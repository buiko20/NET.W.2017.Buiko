using System;
using System.Collections.Generic;
using System.Linq;
using BLL.Interface.Account;
using BLL.Interface.AccountService;
using BLL.Mappers;
using DAL.Interface;
using Services.Interface.AccountIdService;

namespace BLL.Services
{
    /// <inheritdoc />
    public class AccountService : IAccountService
    {
        #region private fields

        private const int BaseAccountInitialBonuses = 0;
        private const int GoldAccountInitialBonuses = 100;
        private const int PlatinumAccountInitialBonuses = 500;

        private readonly IAccountRepository _accountRepository;
        private readonly IAccountIdService _accountIdService;

        private readonly List<Account> _accounts = new List<Account>();

        #endregion // !private fields.

        #region public

        #region constructors

        /// <summary>
        /// Initializes the instance of service.
        /// </summary>
        /// <param name="accountRepository">account repository service</param>
        /// <param name="accountIdService">account id service</param>
        /// <param name="accountCreater">account creation service</param>
        public AccountService(IAccountRepository accountRepository, IAccountIdService accountIdService)
        {
            if (ReferenceEquals(accountRepository, null))
            {
                throw new ArgumentNullException(nameof(accountRepository));
            }

            if (ReferenceEquals(accountIdService, null))
            {
                throw new ArgumentNullException(nameof(accountIdService));
            }

            _accountRepository = accountRepository;
            _accountIdService = accountIdService;

            try
            {
                var dalAccounts = _accountRepository.GetAccounts();
                _accounts.AddRange(dalAccounts.Select(account => account.ToBllAccount()));
            }
            catch (Exception)
            {
                _accounts.Clear();
            }
        }

        #endregion // !constructors.

        #region interface implementation

        /// <inheritdoc />
        public string OpenAccount(string ownerFirstName, string ownerSecondName, decimal sum) =>
            OpenAccount(AccountType.Base, ownerFirstName, ownerSecondName, sum);

        /// <inheritdoc />
        public string OpenAccount(AccountType accountType, string ownerFirstName, string ownerSecondName, decimal sum)
        {
            VerifyInput(ownerFirstName, ownerSecondName, sum);

            try
            {
                string accountId = this.GetUniqueAccoutId(ownerFirstName, ownerSecondName);

                int initialBonuses = GetInitialBonuses(accountType);

                Type typeOfAccount = GetTypeOfAccount(accountType);

                var account = CreateAccount(typeOfAccount, accountId, ownerFirstName, ownerSecondName, sum, initialBonuses);

                _accounts.Add(account);
                _accountRepository.AddAccount(account.ToDalAccount());

                return accountId;
            }
            catch (Exception e)
            {
                throw new AccountServiceException("Open account error", e);
            }
        }

        /// <inheritdoc />
        public void DepositMoney(string accountId, decimal sum) =>
            Operation(accountId, sum, DepositOperation);

        /// <inheritdoc />
        public void WithdrawMoney(string accountId, decimal sum) =>
            Operation(accountId, sum, WithdrawOperation);

        /// <inheritdoc />
        public void CloseAccount(string accountId) =>
            Operation(accountId, 1m, CloseOperation);

        /// <inheritdoc />
        public string GetAccountStatus(string accountId)
        {
            if (string.IsNullOrWhiteSpace(accountId))
            {
                throw new ArgumentException(nameof(accountId));
            }

            var account = _accounts.FirstOrDefault(acc => acc.Id == accountId);
            if (ReferenceEquals(account, null))
            {
                throw new AccountServiceException($"Account with id {accountId} not found");
            }

            return account.ToString();
        }

        #endregion // !interface implementation.

        #endregion // !public.

        #region private

        private static void VerifyInput(string ownerFirstName, string ownerSecondName, decimal sum)
        {
            if (string.IsNullOrWhiteSpace(ownerFirstName))
            {
                throw new ArgumentException($"{nameof(ownerFirstName)} IsNullOrWhiteSpace", nameof(ownerFirstName));
            }

            if (string.IsNullOrWhiteSpace(ownerSecondName))
            {
                throw new ArgumentException($"{nameof(ownerSecondName)} IsNullOrWhiteSpace", nameof(ownerSecondName));
            }

            if (sum < 0)
            {
                throw new ArgumentException("sum must be greater than 0", nameof(sum));
            }
        }

        private static int GetInitialBonuses(AccountType accountType)
        {
            switch (accountType)
            {
                case AccountType.Gold:
                    return GoldAccountInitialBonuses;
                case AccountType.Platinum:
                    return PlatinumAccountInitialBonuses;
                case AccountType.Base:
                default:
                    return BaseAccountInitialBonuses;
            }
        }

        private static Type GetTypeOfAccount(AccountType accountType)
        {
            switch (accountType)
            {
                case AccountType.Gold:
                    return typeof(GoldAccount);
                case AccountType.Platinum:
                    return typeof(PlatinumAccount);
                case AccountType.Base:
                default:
                    return typeof(BaseAccount);
            }           
        }

        private static Account CreateAccount(
            Type accountType, string id, string onwerFirstName, string onwerSecondName, decimal sum, int bonusPoints)
        {
            return (Account)Activator.CreateInstance(accountType, id, onwerFirstName, onwerSecondName, sum, bonusPoints);
        }

        private string GetUniqueAccoutId(string ownerFirstName, string ownerSecondName, int spinCount = 4000)
        {
            string id;
            int i = 0;
            do
            {
                id = _accountIdService.GenerateAccountId(ownerFirstName, ownerSecondName);
                if (++i >= spinCount)
                {
                    throw new AccountServiceException("Failed to get the unique account ID.");
                }
            }
            while (_accounts.Any(account => string.Compare(account.Id, id, StringComparison.Ordinal) == 0));

            return id;
        }

        private void Operation(string accountId, decimal sum, Action<Account, decimal> operation)
        {
            if (sum <= 0)
            {
                throw new ArgumentException($"{nameof(sum)} must be greater than 0", nameof(sum));
            }

            var account = _accounts.FirstOrDefault(acc => acc.Id == accountId);
            if (ReferenceEquals(account, null))
            {
                throw new AccountServiceException($"Account with id {accountId} not found");
            }

            try
            {
                operation(account, sum);
            }
            catch (Exception e)
            {
                throw new AccountServiceException("Operation error", e);
            }
        }

        private void DepositOperation(Account account, decimal sum)
        {
            account.DepositMoney(sum);
            _accountRepository.UpdateAccount(account.ToDalAccount());
        }

        private void WithdrawOperation(Account account, decimal sum)
        {
            account.WithdrawMoney(sum);
            _accountRepository.UpdateAccount(account.ToDalAccount());
        }

        private void CloseOperation(Account account, decimal sum)
        {
            _accountRepository.RemoveAccount(account.ToDalAccount());
            _accounts.Remove(account);
        }

        #endregion // !private.
    }
}

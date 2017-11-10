using System;
using System.Collections.Generic;
using System.Linq;
using Logic.AccountCreationService;
using Logic.AccountIdGeneratorService;
using Logic.AccountRepository;
using Logic.AccountService.Exceptions;
using Logic.Domain.Account;
using Logic.Domain.Account.Implementation;

namespace Logic.AccountService.Implementation
{
    /// <inheritdoc />
    public class AccountService : IAccountService
    {
        #region private fields

        private const int BaseAccountInitialBonuses = 0;
        private const int GoldAccountInitialBonuses = 100;
        private const int PlatinumAccountInitialBonuses = 500;

        private readonly IAccountRepository _accountRepository;
        private readonly AccountIdService _accountIdService;
        private readonly IAccountCreater _accountCreater;
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
        public AccountService(IAccountRepository accountRepository, AccountIdService accountIdService, IAccountCreater accountCreater)
        {
            VerifyInput(accountRepository, accountIdService, accountCreater);

            _accountRepository = accountRepository;
            _accountIdService = accountIdService;
            _accountCreater = accountCreater;

            try
            {
                _accounts.AddRange(_accountRepository.GetAccounts());
            }
            catch (Exception)
            {
                _accounts.Clear();
            }
        }

        #endregion // !constructors.

        #region interface implementation

        /// <inheritdoc />
        public string OpenAccount(string onwerFirstName, string onwerSecondName, decimal sum) =>
            OpenAccount(typeof(BaseAccount), onwerFirstName, onwerSecondName, sum);

        /// <inheritdoc />
        public string OpenAccount(Type accountType, string onwerFirstName, string onwerSecondName, decimal sum)
        {
            VerifyInput(accountType, onwerFirstName, onwerSecondName, sum);

            try
            {
                string accountId = _accountIdService.GenerateAccountId(onwerFirstName, onwerSecondName);

                int initialBonuses = GetInitialBonuses(accountType);

                var account =
                    _accountCreater.CreateAccount(accountType, accountId, onwerFirstName, onwerSecondName, sum, initialBonuses);

                _accounts.Add(account);
                _accountRepository.AddAccount(account);

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

        private static void VerifyInput(Type accountType, string onwerFirstName, string onwerSecondName, decimal sum)
        {
            if (accountType != typeof(BaseAccount) && 
                accountType != typeof(GoldAccount) &&
                accountType != typeof(PlatinumAccount))
            {
                throw new ArgumentException("Invalid account type", nameof(accountType));
            }

            if (string.IsNullOrWhiteSpace(onwerFirstName))
            {
                throw new ArgumentException($"{nameof(onwerFirstName)} IsNullOrWhiteSpace", nameof(onwerFirstName));
            }

            if (string.IsNullOrWhiteSpace(onwerSecondName))
            {
                throw new ArgumentException($"{nameof(onwerSecondName)} IsNullOrWhiteSpace", nameof(onwerSecondName));
            }

            if (sum < 0)
            {
                throw new ArgumentException("sum must be greater than 0", nameof(sum));
            }
        }

        private static void VerifyInput(IAccountRepository accountRepository, AccountIdService accountIdService, IAccountCreater accountCreater)
        {
            if (ReferenceEquals(accountRepository, null))
            {
                throw new ArgumentNullException(nameof(accountRepository));
            }

            if (ReferenceEquals(accountIdService, null))
            {
                throw new ArgumentNullException(nameof(accountIdService));
            }

            if (ReferenceEquals(accountCreater, null))
            {
                throw new ArgumentNullException(nameof(accountCreater));
            }
        }

        private static int GetInitialBonuses(Type accountType)
        {
            if (accountType == typeof(GoldAccount))
            {
                return GoldAccountInitialBonuses;
            }

            if (accountType == typeof(PlatinumAccount))
            {
                return PlatinumAccountInitialBonuses;
            }

            return BaseAccountInitialBonuses;
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
            _accountRepository.RemoveAccount(account);
            _accountRepository.AddAccount(account);
        }

        private void WithdrawOperation(Account account, decimal sum)
        {
            account.WithdrawMoney(sum);
            _accountRepository.RemoveAccount(account);
            _accountRepository.AddAccount(account);
        }

        private void CloseOperation(Account account, decimal sum)
        {
            _accountRepository.RemoveAccount(account);
            _accounts.Remove(account);
        }

        #endregion // !private.
    }
}

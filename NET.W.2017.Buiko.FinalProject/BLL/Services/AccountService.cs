using System;
using System.Linq;
using System.Net.Mail;
using BLL.Interface.Entities;
using BLL.Mappers;
using BLL.Services.Account_Service;
using BLL.Services.AccountIdService;
using BLL.Services.Accounts;
using DAL.Interface;
using DAL.Interface.DTO;

namespace BLL.Services
{
    public class AccountService : IAccountService
    {
        #region private fields

        private const int BaseAccountInitialBonuses = 0;
        private const int GoldAccountInitialBonuses = 100;
        private const int PlatinumAccountInitialBonuses = 500;

        private readonly IAccountRepository _accountRepository;
        private readonly IAccountIdService _accountIdService;

        #endregion // !private fields.

        #region public

        #region constructors

        /// <summary>
        /// Initializes account service with the passed parameters.
        /// </summary>
        /// <param name="accountRepository">account repository</param>
        /// <param name="accountIdService">account number generation service</param>
        /// <exception cref="ArgumentNullException">Exception thrown when
        /// <paramref name="accountRepository"/> or <paramref name="accountIdService"/> is null.</exception>
        public AccountService(IAccountRepository accountRepository, IAccountIdService accountIdService)
        {
            VerifyNull(accountRepository, nameof(accountRepository));
            VerifyNull(accountIdService, nameof(accountIdService));

            _accountRepository = accountRepository;
            _accountIdService = accountIdService;
        }

        #endregion // !constructors.

        #region implementation of IAccountService

        /// <inheritdoc />
        public string OpenAccount(BankUser bankUser, decimal sum, AccountType accountType = AccountType.Base)
        {
            VerifyNull(bankUser, nameof(bankUser));
            if (sum < 0)
            {
                throw new ArgumentException($"{nameof(sum)} must be not less than 0.", nameof(sum));
            }

            try
            {
                string accountId = this.GetUniqueAccoutId(bankUser.Email, bankUser.SecondName);
                string cryptedAccountId = CryptographyHelper.Encrypt(accountId, bankUser.Email);

                int initialBonuses = GetInitialBonuses(accountType);

                var account = CreateAccount(accountType, cryptedAccountId, sum, initialBonuses, bankUser);
                
                _accountRepository.AddAccount(account.ToDalAccount());

                return accountId;
            }
            catch (Exception e)
            {
                throw new AccountServiceException("Open account error.", e);
            }
        }

        /// <inheritdoc />
        public void DepositMoney(string email, string accountId, decimal sum) =>
            this.AccountOperation(email, accountId, sum, (a, arg) => a.DepositMoney(arg));

        /// <inheritdoc />
        public void WithdrawMoney(string email, string accountId, decimal sum) =>
            this.AccountOperation(email, accountId, sum, (a, arg) => a.WithdrawMoney(arg));

        /// <inheritdoc />
        public Account GetAccountInfo(string email, string accountId)
        {
            VerifyEmail(email, nameof(email));
            VerifyString(accountId, nameof(accountId));

            try
            {
                var bllAccount = this.GetAccount(email, accountId);
                return bllAccount.ToInterfaceAccount(bllAccount.BankUser);
            }
            catch (Exception e)
            {
                throw new AccountServiceException("Account operation error.", e);
            }
        }

        /// <inheritdoc />
        public void CloseAccount(string email, string accountId)
        {          
            VerifyEmail(email, nameof(email));
            VerifyString(accountId, nameof(accountId));

            try
            {
                var bllAccount = this.GetAccount(email, accountId);
                _accountRepository.RemoveAccount(bllAccount.ToDalAccount());
            }
            catch (Exception e)
            {
                throw new AccountServiceException("Account operation error.", e);
            }
        }

        #endregion // !implementation of IAccountService.

        #endregion // !public!

        #region private

        private static void VerifyNull(object value, string paramName)
        {
            if (ReferenceEquals(value, null))
            {
                throw new ArgumentNullException(paramName);
            }
        }

        private static void VerifyString(string value, string paramName)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException($"{paramName} is null or white space.", paramName);
            }
        }

        private static void VerifyEmail(string email, string paramName)
        {
            try
            {
                var mailAddress = new MailAddress(email);
            }
            catch (Exception)
            {
                throw new ArgumentException($"{email} is invalid email.", paramName);
            }
        }

        private static BllAccount CreateAccount(
            AccountType accountType,
            string id,
            decimal sum,
            int bonusPoints,
            BankUser bankUser)
        {
            switch (accountType)
            {
                case AccountType.Gold:
                    return new BllGoldAccount(id, sum, bonusPoints, bankUser);
                case AccountType.Platinum:
                    return new BllPlatinumAccount(id, sum, bonusPoints, bankUser);
                case AccountType.Base:
                default:
                    return new BllBaseAccount(id, sum, bonusPoints, bankUser);
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

        private string GetUniqueAccoutId(string email, string ownerSecondName, int spinCount = 4000)
        {
            string id;
            int i = 0;
            var temp = _accountRepository.GetAccounts();
            var dalAccounts = temp as DalAccount[] ?? temp.ToArray();
            do
            {
                id = _accountIdService.GenerateAccountId(email, ownerSecondName);
                if (++i >= spinCount)
                {
                    throw new AccountServiceException("Failed to get the unique account ID.");
                }
            }
            while (dalAccounts.Any(dalAccount => string.Compare(dalAccount.Id, id, StringComparison.Ordinal) == 0));

            return id;
        }

        private void AccountOperation(string email, string accountId, decimal sum, Action<BllAccount, decimal> operation)
        {
            VerifyEmail(email, nameof(email));
            VerifyString(accountId, nameof(accountId));
            if (sum < 0)
            {
                throw new ArgumentException($"{nameof(sum)} must be greater than 0.", nameof(sum));
            }

            try
            {
                var bllAccount = this.GetAccount(email, accountId);
                operation(bllAccount, sum);
                _accountRepository.UpdateAccount(bllAccount.ToDalAccount());
            }
            catch (Exception e)
            {
                throw new AccountServiceException("Account operation error.", e);
            }
        }

        private BllAccount GetAccount(string email, string accountId)
        {
            string cryptedAccountId = CryptographyHelper.Encrypt(accountId, email);
            var bllAccount = _accountRepository.GetUserAccounts(email)
                .FirstOrDefault(account => string.Equals(account.Id, cryptedAccountId, StringComparison.Ordinal))
                .ToBllAccount();
            return bllAccount;
        }

        #endregion // !private.
    }
}

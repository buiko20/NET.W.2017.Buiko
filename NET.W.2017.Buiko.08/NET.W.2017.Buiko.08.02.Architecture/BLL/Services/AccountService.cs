using System;
using System.Linq;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using BLL.Interface.Account;
using BLL.Interface.AccountIdService;
using BLL.Interface.AccountService;
using BLL.Interface.MailService;
using BLL.Mappers;
using DAL.Interface;
using DAL.Interface.DTO;

namespace BLL.Services
{
    /// <inheritdoc />
    public class AccountService : IAccountService
    {
        #region private fields

        private const string HostEmail = "tempmail4222@gmail.com";
        private const string HostEmailPassword = "05191997a";

        private const int BaseAccountInitialBonuses = 0;
        private const int GoldAccountInitialBonuses = 100;
        private const int PlatinumAccountInitialBonuses = 500;

        private readonly IUnitOfWork _unitOfWork;
        private readonly IAccountRepository _accountRepository;
        private readonly IMailService _mailService;

        #endregion // !private fields.

        #region public

        #region constructors

        /// <summary>
        /// Initializes the instance of service.
        /// </summary>
        /// <param name="unitOfWork">unit of work pattern</param>
        /// <param name="accountRepository">account repository service</param>
        /// <param name="mailService">mail service</param>
        /// <exception cref="ArgumentNullException">Exception thrown when <paramref name="accountRepository"/> is null.</exception>
        /// <exception cref="ArgumentNullException">Exception thrown when <paramref name="mailService"/> is null.</exception>
        public AccountService(IUnitOfWork unitOfWork, IAccountRepository accountRepository, IMailService mailService)
        {
            if (ReferenceEquals(unitOfWork, null))
            {
                throw new ArgumentNullException(nameof(unitOfWork));
            }

            if (ReferenceEquals(accountRepository, null))
            {
                throw new ArgumentNullException(nameof(accountRepository));
            }

            if (ReferenceEquals(mailService, null))
            {
                throw new ArgumentNullException(nameof(mailService));
            }

            _unitOfWork = unitOfWork;
            _accountRepository = accountRepository;
            _mailService = mailService;
        }

        #endregion // !constructors.

        #region interface implementation

        /// <inheritdoc />
        public string OpenAccount(
            string ownerFirstName,
            string ownerSecondName,
            decimal sum,
            string ownerEmail,
            IAccountIdService accountIdService)
        {
            return OpenAccount(
                AccountType.Base, 
                ownerFirstName, 
                ownerSecondName, 
                sum,
                ownerEmail, 
                accountIdService);
        }

        /// <inheritdoc />
        public string OpenAccount(
            AccountType accountType, 
            string ownerFirstName, 
            string ownerSecondName, 
            decimal sum,
            string ownerEmail,
            IAccountIdService accountIdService)
        {
            VerifyInput(ownerFirstName, ownerSecondName, sum, ownerEmail, accountIdService);

            try
            {
                string accountId = this.GetUniqueAccoutId(ownerFirstName, ownerSecondName, accountIdService);

                string accountIdHash = GetSha256Hash(accountId);

                int initialBonuses = GetInitialBonuses(accountType);

                var account = CreateAccount(accountType, accountIdHash, ownerFirstName, ownerSecondName, sum, initialBonuses, ownerEmail);

                _accountRepository.AddAccount(account.ToDalAccount());

                string subject = "Opened account on localhost!";
                string message = $"Thank you for choosing our service!<br/>Your account ID: {accountId}";
                this.SendMail(ownerEmail, subject, message);

                _unitOfWork.Commit();

                return accountId;
            }
            catch (Exception e)
            {
                throw new AccountServiceException("Open account error", e);
            }
        }

        /// <inheritdoc />
        public void DepositMoney(string accountId, decimal sum)
        {
            string accountIdHash = GetSha256Hash(accountId);
            this.Operation(accountIdHash, sum, DepositOperation);

            try
            {
                string ownerEmail = this.GetAccount(accountId).OwnerEmail;
                string subject = "Account operation";
                string message = $"Sum={sum} was paid to account id={accountId}";
                this.SendMail(ownerEmail, subject, message);

                _unitOfWork.Commit();
            }
            catch (Exception e)
            {
                throw new AccountServiceException("Deposit money operation error", e);
            }
        }

        /// <inheritdoc />
        public void WithdrawMoney(string accountId, decimal sum)
        {
            string accountIdHash = GetSha256Hash(accountId);
            this.Operation(accountIdHash, sum, WithdrawOperation);

            try
            {
                string ownerEmail = this.GetAccount(accountId).OwnerEmail;
                string subject = "Account operation";
                string message = $"From account with id={accountId} sum={sum} were withdrawn.";
                this.SendMail(ownerEmail, subject, message);

                _unitOfWork.Commit();
            }
            catch (Exception e)
            {
                throw new AccountServiceException("Withdraw money operation error", e);
            }
        }

        /// <inheritdoc />
        public void CloseAccount(string accountId)
        {
            string accountIdHash = GetSha256Hash(accountId);
            string ownerEmail = this.GetAccount(accountId).OwnerEmail;
            this.Operation(accountIdHash, 1m, CloseOperation);

            try
            {
                string subject = "Account closed";
                string message = $"account with id={accountId} is closed.";
                this.SendMail(ownerEmail, subject, message);

                _unitOfWork.Commit();
            }
            catch (Exception e)
            {
                throw new AccountServiceException("Close account error", e);
            }
        }     

        /// <inheritdoc />
        public string GetAccountStatus(string accountId) =>
            this.GetAccount(accountId).ToString();

        /// <inheritdoc />
        public void TransferFunds(string sourceAccountId, string destinationAccountId, decimal transferSum)
        {
            if (string.Equals(sourceAccountId, destinationAccountId, StringComparison.Ordinal))
            {
                return;
            }

            var sourceAccount = this.GetAccount(sourceAccountId);
            var destinationAccount = this.GetAccount(destinationAccountId);

            if (sourceAccount.CurrentSum < transferSum)
            {
                throw new AccountIdServiceException("There are not enough funds on account");
            }

            try
            {
                this.WithdrawOperation(sourceAccount, transferSum);
                this.DepositOperation(destinationAccount, transferSum);

                string email = sourceAccount.OwnerEmail;
                string subject = "Transfer funds operation";
                string message = $"From account with id={sourceAccountId} sum={transferSum} were withdrawn.";
                this.SendMail(email, subject, message);

                email = destinationAccount.OwnerEmail;
                message = $"Sum={transferSum} was paid to account id={destinationAccountId}";
                this.SendMail(email, subject, message);

                _unitOfWork.Commit();
            }
            catch (Exception e)
            {
                throw new AccountServiceException("Transfer funds error", e);
            }
        }

        #endregion // !interface implementation.

        #endregion // !public.

        #region private

        private static string GetSha256Hash(string input)
        {
            var sha256 = SHA256.Create();
            byte[] hashInBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));

            var result = new StringBuilder();
            foreach (var hashByte in hashInBytes)
            {
                result.Append(hashByte.ToString("x2"));
            }

            return result.ToString();
        }

        private static void VerifyInput(
            string ownerFirstName, 
            string ownerSecondName, 
            decimal sum, 
            string ownerEmail,
            IAccountIdService accountIdService)
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

            try
            {
                var mailAddress = new MailAddress(ownerEmail);
            }
            catch (Exception)
            {
                throw new ArgumentException($"{nameof(ownerEmail)} is invalid.", nameof(ownerEmail));
            }

            if (ReferenceEquals(accountIdService, null))
            {
                throw new ArgumentNullException(nameof(accountIdService));
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

        private static Account CreateAccount(
            AccountType accountType, 
            string id, 
            string onwerFirstName, 
            string onwerSecondName, 
            decimal sum,
            int bonusPoints,
            string ownerEmail)
        {
            switch (accountType)
            {
                case AccountType.Gold:
                    return new GoldAccount(id, onwerFirstName, onwerSecondName, sum, bonusPoints, ownerEmail);
                case AccountType.Platinum:
                    return new PlatinumAccount(id, onwerFirstName, onwerSecondName, sum, bonusPoints, ownerEmail);
                case AccountType.Base:
                default:
                    return new BaseAccount(id, onwerFirstName, onwerSecondName, sum, bonusPoints, ownerEmail);
            }
        }

        private string GetUniqueAccoutId(
            string ownerFirstName, 
            string ownerSecondName, 
            IAccountIdService accountIdService, 
            int spinCount = 4000)
        {
            string id;
            int i = 0;
            var temp = _accountRepository.GetAccounts();
            var dalAccounts = temp as DalAccount[] ?? temp.ToArray();
            do
            {
                id = accountIdService.GenerateAccountId(ownerFirstName, ownerSecondName);
                if (++i >= spinCount)
                {
                    throw new AccountServiceException("Failed to get the unique account ID.");
                }
            }
            while (dalAccounts.Any(dalAccount => string.Compare(dalAccount.Id, id, StringComparison.Ordinal) == 0));

            return id;
        }

        private void Operation(
            string accountId, decimal sum, Action<Account, decimal> operation)
        {
            if (sum <= 0)
            {
                throw new ArgumentException($"{nameof(sum)} must be greater than 0", nameof(sum));
            }

            var dalAccounts = _accountRepository.GetAccounts();
            var dalAccount = dalAccounts.FirstOrDefault(acc => acc.Id == accountId);
            if (ReferenceEquals(dalAccount, null))
            {
                throw new AccountServiceException($"Account with id {accountId} not found");
            }

            try
            {
                operation(dalAccount.ToBllAccount(), sum);
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

        private void CloseOperation(Account account, decimal sum) =>
            _accountRepository.RemoveAccount(account.ToDalAccount());

        private Account GetAccount(string accountId)
        {
            if (string.IsNullOrWhiteSpace(accountId))
            {
                throw new ArgumentException(nameof(accountId));
            }

            string accountIdHash = GetSha256Hash(accountId);

            var dalAccounts = _accountRepository.GetAccounts();
            var dalAccount = dalAccounts.FirstOrDefault(acc => acc.Id == accountIdHash);
            if (ReferenceEquals(dalAccount, null))
            {
                throw new AccountServiceException($"Account with id {accountId} not found");
            }

            return dalAccount.ToBllAccount();
        }

        private void SendMail(string to, string subject, string message)
        {
            var mailData = new MailData
            {
                To = to,
                From = HostEmail,
                FromPassword = HostEmailPassword,
                Subject = subject,
                Message = message
            };

            _mailService.SendMail(mailData);
        }

        #endregion // !private.
    }
}

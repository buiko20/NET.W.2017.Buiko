using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web.Configuration;
using BLL.Interface.Entities;
using BLL.Interface.Services;
using BLL.Interface.Services.Exceptions;
using BLL.Mappers;
using BLL.Services.Account_Service;
using BLL.Services.MailService;
using DAL.Interface;
using DAL.Interface.DTO;

namespace BLL.Services
{
    public class BankService : IBankService
    {
        #region private fields

        private readonly IMailService _mailService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBankUserRepository _userRepository;
        private readonly IAccountService _accountService;

        private readonly string _hostEmail;
        private readonly string _hostEmailPassword;

        #endregion // !private fields.

        #region public 

        #region constructors

        /// <summary>
        /// Initializes the bank service with the passed parameters.
        /// </summary>
        /// <param name="mailService">mail service</param>
        /// <param name="unitOfWork">unit of work</param>
        /// <param name="userRepository">user repository</param>
        /// <param name="accountService">account service</param>
        /// <exception cref="ArgumentNullException">Exception thrown when
        /// one of the arguments is null.</exception>
        public BankService(
            IMailService mailService,
            IUnitOfWork unitOfWork, 
            IBankUserRepository userRepository,
            IAccountService accountService)
        {
            VerifyNull(mailService, nameof(mailService));
            VerifyNull(unitOfWork, nameof(unitOfWork));
            VerifyNull(userRepository, nameof(userRepository));
            VerifyNull(accountService, nameof(accountService));

            _mailService = mailService;
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
            _accountService = accountService;

            _hostEmail = WebConfigurationManager.AppSettings["HostEmail"];
            _hostEmailPassword = WebConfigurationManager.AppSettings["HostEmailPassword"];
        }

        #endregion // !constructors.

        #region implementation of interface

        /// <inheritdoc />
        public void RegisterUser(string email, string password, string userFirstName, string userSecondName)
        {
            var bankUser = new BankUser(email, userFirstName, userSecondName);
            this.RegisterUser(bankUser, password);
        }

        /// <inheritdoc />
        public void RegisterUser(BankUser bankUser, string password)
        {
            VerifyNull(bankUser, nameof(bankUser));
            VerifyString(password, nameof(password));

            try
            {
                var user = _userRepository.GetByEmail(bankUser.Email);
                if (!ReferenceEquals(user, null))
                {
                    throw new UserAlreadyRegisteredException(bankUser.Email);
                }

                string passwordHash = CryptographyHelper.GetSha256Hash(password);
                _userRepository.AddBankUser(bankUser.ToDalBankUser(passwordHash));

                string subject = "Account registration";
                string message = "You have successfully registered an account.";
                this.SendMail(bankUser.Email, subject, message);

                _unitOfWork.Commit();
            }
            catch (Exception e)
            {
                if (e.GetType() == typeof(UserAlreadyRegisteredException))
                {
                    throw;
                }

                throw new BankServiceException("Registration error.", e);
            }
        }

        /// <inheritdoc />
        public BankUser GetUserInfo(string email)
        {
            try
            {
                var user = _userRepository.GetByEmail(email);
                return ReferenceEquals(user, null) ? null : user.ToInterfaceBankUser();
            }
            catch (Exception e)
            {
                throw new BankServiceException("Get user info error.", e);
            }
        }

        /// <inheritdoc />
        public void VerifyUserCredentials(string email, string password) =>
            this.VerifyUserCredentials(email, password, isAdmin: false);

        /// <inheritdoc />
        public string OpenAccount(string email, string password, decimal sum, AccountType accountType = AccountType.Base)
        {
            if (sum < 0)
            {
                throw new ArgumentException($"{nameof(sum)} greater than 0.", nameof(sum));
            }

            this.VerifyUserCredentials(email, password);

            try
            {
                var user = _userRepository.GetByEmail(email).ToInterfaceBankUser();
                string accountId = _accountService.OpenAccount(user, sum, accountType);

                string subject = "Opened account on localhost!";
                string message = $"Thank you for choosing our service!<br/>Your account ID: {accountId}";
                this.SendMail(user.Email, subject, message);

                _unitOfWork.Commit();                
                return accountId;
            }
            catch (Exception e)
            {
                throw new BankServiceException("Open account error.", e);
            }
        }

        /// <inheritdoc />
        public void DepositMoney(string email, string accountId, decimal sum)
        {
            this.AccountOperation(email, accountId, sum, _accountService.DepositMoney);

            try
            {
                string subject = "Account operation";
                string message = $"Sum={sum} was paid to account id={accountId}";
                this.SendMail(email, subject, message);

                _unitOfWork.Commit();
            }
            catch (Exception e)
            {
                throw new BankServiceException("Account operation error.", e);
            }
        }

        /// <inheritdoc />
        public void WithdrawMoney(string email, string accountId, decimal sum)
        {
            this.AccountOperation(email, accountId, sum, _accountService.WithdrawMoney);

            try
            {
                string subject = "Account operation";
                string message = $"From account with id={accountId} sum={sum} were withdrawn.";
                this.SendMail(email, subject, message);

                _unitOfWork.Commit();
            }
            catch (Exception e)
            {
                throw new BankServiceException("Account operation error.", e);
            }
        }

        /// <inheritdoc />
        public void TransferFunds(string fromEmail, string fromAccountId, string toEmail, string toAccountId, decimal sum)
        {
            this.VerifyUserExistence(fromEmail);
            this.VerifyUserExistence(toEmail);

            try
            {
                this.AccountOperation(fromEmail, fromAccountId, sum, _accountService.WithdrawMoney);
                this.AccountOperation(toEmail, toAccountId, sum, _accountService.DepositMoney);

                string subject = "Transfer funds operation";
                string message = $"From account with id={fromEmail} sum={sum} were withdrawn.";
                this.SendMail(fromEmail, subject, message);

                message = $"Sum={sum} was paid to account id={toEmail}";
                this.SendMail(toEmail, subject, message);

                _unitOfWork.Commit();
            }
            catch (Exception e)
            {
                throw new BankServiceException("Transfer funds operation error.", e);
            }
        }

        /// <inheritdoc />
        public Account GetAccountInfo(string email, string accountId)
        {
            VerifyString(accountId, nameof(accountId));
            this.VerifyUserExistence(email);

            try
            {           
                return _accountService.GetAccountInfo(email, accountId);
            }
            catch (Exception e)
            {
                throw new BankServiceException("Get account info operation error.", e);
            }
        }

        /// <inheritdoc />
        public void CloseAccount(string email, string password, string accountId)
        {
            VerifyString(accountId, nameof(accountId));
            this.VerifyUserCredentials(email, password);

            try
            {
                _accountService.CloseAccount(email, accountId);

                string subject = "Account closed";
                string message = $"account with id={accountId} is closed.";
                this.SendMail(email, subject, message);

                _unitOfWork.Commit();
            }
            catch (Exception e)
            {
                throw new BankServiceException("Account close operation error.", e);
            }
        }

        /// <inheritdoc />
        public IEnumerable<BankUser> GetUsers(string adminEmail, string adminPassword)
        {
            this.VerifyUserCredentials(adminEmail, adminPassword, isAdmin: true);
            return _userRepository.GetBankUsers()
                .Select(user => user.ToInterfaceBankUser())
                .ToArray();
        }

        #endregion // !implementation of interface.

        #endregion // !public.

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

        private void VerifyUserExistence(string email)
        {
            VerifyEmail(email, nameof(email));

            DalBankUser user;
            try
            {
                user = _userRepository.GetByEmail(email);
            }
            catch (Exception e)
            {
                throw new BankServiceException("Operation error.", e);
            }

            if (ReferenceEquals(user, null))
            {
                throw new UserUnregisteredException(email);
            }
        }

        private void SendMail(string to, string subject, string message)
        {
            var mailData = new MailData
            {
                To = to,
                From = _hostEmail,
                FromPassword = _hostEmailPassword,
                Subject = subject,
                Message = message
            };

            _mailService.SendMail(mailData);
        }

        private void AccountOperation(string email, string accountId, decimal sum, Action<string, string, decimal> operation)
        {
            if (sum <= 0)
            {
                throw new ArgumentException($"{nameof(sum)} must be greater than 0.", nameof(sum));
            }

            VerifyString(accountId, nameof(accountId));
            this.VerifyUserExistence(email);

            try
            {
                operation(email, accountId, sum);
            }
            catch (Exception e)
            {
                throw new BankServiceException("Account operation error.", e);
            }
        }

        private void VerifyUserCredentials(string email, string password, bool isAdmin)
        {
            VerifyEmail(email, nameof(email));
            VerifyString(password, nameof(password));

            DalBankUser user;
            try
            {
                user = _userRepository.GetByEmail(email);
            }
            catch (Exception e)
            {
                throw new BankServiceException("Operation error.", e);
            }

            if (ReferenceEquals(user, null))
            {
                throw new UserUnregisteredException(email);
            }

            string passwordHash = CryptographyHelper.GetSha256Hash(password);
            if (!string.Equals(passwordHash, user.PasswordHash, StringComparison.Ordinal))
            {
                throw new WrongCredentialsException(email);
            }

            if (!isAdmin)
            {
                return;
            }

            if (!string.Equals(user.Role, "admin", StringComparison.OrdinalIgnoreCase))
            {
                throw new BankServiceException("User is not an administrator.");
            }
        }

        #endregion // !private.
    }
}

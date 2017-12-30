using System;
using System.Collections.Generic;
using BLL.Interface.Entities;
using BLL.Interface.Services.Exceptions;

namespace BLL.Interface.Services
{
    /// <summary>
    /// Bank contract.
    /// </summary>
    public interface IBankService
    {
        /// <summary>
        /// Register user in bank. <see cref="RegisterUser(BankUser,string)"/>
        /// </summary>
        /// <param name="email">user email</param>
        /// <param name="password">user password</param>
        /// <param name="userFirstName">user first name</param>
        /// <param name="userSecondName">user second name</param>
        /// <exception cref="ArgumentException">Exception thrown when
        /// one of the parameters is invalid.</exception>
        /// <exception cref="UserAlreadyRegisteredException">Exception thrown when
        /// user with <paramref name="email"/> already registered in bank.</exception>
        /// <exception cref="BankServiceException">Exception thrown when
        /// error occurs in the bank service.</exception>
        void RegisterUser(
            string email,
            string password,
            string userFirstName,
            string userSecondName);

        /// <summary>
        /// Register <paramref name="bankUser"/> in bank.
        /// </summary>
        /// <param name="bankUser">user to be registered</param>
        /// <param name="password">user password</param>
        /// <exception cref="ArgumentException">Exception thrown when
        /// <paramref name="password"/> is invalid.</exception>
        /// <exception cref="ArgumentNullException">Exception thrown when
        /// <paramref name="bankUser"/> is null.</exception>
        /// <exception cref="UserAlreadyRegisteredException">Exception thrown when
        /// <paramref name="bankUser"/> already registered in bank.</exception>
        /// <exception cref="BankServiceException">Exception thrown when
        /// error occurs in the bank service.</exception>
        void RegisterUser(BankUser bankUser, string password);

        /// <summary>
        /// Return information about user with <paramref name="email"/>.
        /// </summary>
        /// <param name="email">user email</param>
        /// <returns>User information.</returns>
        /// <exception cref="BankServiceException">Exception thrown when
        /// error occurs in the bank service.</exception>
        BankUser GetUserInfo(string email);

        /// <summary>
        /// Authenticate the user with <paramref name="email"/>.
        /// </summary>
        /// <param name="email">user email</param>
        /// <param name="password">user password</param>
        /// <exception cref="ArgumentException">Exception thrown when
        /// <paramref name="email"/> or <paramref name="password"/> is invalid.</exception>
        /// <exception cref="BankServiceException">Exception thrown when
        /// error occurs in the bank service.</exception>
        /// <exception cref="UserUnregisteredException">Exception thrown when
        /// user with <paramref name="email"/> not registered in bank.</exception>
        /// <exception cref="WrongCredentialsException">Exception thrown when
        /// <paramref name="email"/> or <paramref name="password"/> is invalid.</exception>
        void VerifyUserCredentials(string email, string password);

        /// <summary>
        /// Opens a bank account for a user with <paramref name="email"/>.
        /// </summary>
        /// <param name="email">user email</param>
        /// <param name="password">user password</param>
        /// <param name="sum">account initial sum</param>
        /// <param name="accountType">account type</param>
        /// <returns>Bank account number.</returns>
        /// <exception cref="ArgumentException">Exception thrown when
        /// <paramref name="sum"/> is less than 0.</exception>
        /// <exception cref="BankServiceException">Exception thrown when
        /// error occurs in the bank service.</exception>
        /// <exception cref="UserUnregisteredException">Exception thrown when
        /// user with <paramref name="email"/> not registered in bank.</exception>
        /// <exception cref="WrongCredentialsException">Exception thrown when
        /// <paramref name="email"/> or <paramref name="password"/> is invalid.</exception>
        string OpenAccount(
            string email, 
            string password, 
            decimal sum, 
            AccountType accountType = AccountType.Base);

        /// <summary>
        /// Deposit funds from user account.
        /// </summary>
        /// <param name="email">user email</param>
        /// <param name="accountId">user account number</param>
        /// <param name="sum">funds</param>
        /// <exception cref="ArgumentException">Exception thrown when
        /// <paramref name="sum"/> is less than or equal to 0 or
        /// when <paramref name="email"/> or <paramref name="accountId"/>
        /// is invalid.</exception>
        /// <exception cref="BankServiceException">Exception thrown when
        /// error occurs in the bank service.</exception>
        /// <exception cref="UserUnregisteredException">Exception thrown when
        /// user with <paramref name="email"/> not registered in bank.</exception>
        void DepositMoney(string email, string accountId, decimal sum);

        /// <summary>
        /// Withdraw funds from user account.
        /// </summary>
        /// <param name="email">user email</param>
        /// <param name="accountId">user account number</param>
        /// <param name="sum">funds</param>
        /// <exception cref="ArgumentException">Exception thrown when
        /// <paramref name="sum"/> is less than or equal to 0 or
        /// when <paramref name="email"/> or <paramref name="accountId"/>
        /// is invalid.</exception>
        /// <exception cref="BankServiceException">Exception thrown when
        /// error occurs in the bank service.</exception>
        /// <exception cref="UserUnregisteredException">Exception thrown when
        /// user with <paramref name="email"/> not registered in bank.</exception>
        void WithdrawMoney(string email, string accountId, decimal sum);

        /// <summary>
        /// Transfer funds from user's account with <paramref name="fromAccountId"/> 
        /// to user's account with <paramref name="toAccountId"/>.
        /// </summary>
        /// <param name="fromEmail">source user</param>
        /// <param name="fromAccountId">source user account number</param>
        /// <param name="toEmail">destination user</param>
        /// <param name="toAccountId">destination user account number</param>
        /// <param name="sum">transfer sum</param>
        /// <exception cref="ArgumentException">Exception thrown when
        /// <paramref name="sum"/> is less than or equal to 0 or
        /// other arguments are invalid.</exception>
        /// <exception cref="BankServiceException">Exception thrown when
        /// error occurs in the bank service.</exception>
        /// <exception cref="UserUnregisteredException">Exception thrown when
        /// user with <paramref name="fromEmail"/>
        /// or <paramref name="toEmail"/> not registered in bank.</exception>
        void TransferFunds(string fromEmail, string fromAccountId, string toEmail, string toAccountId, decimal sum);

        /// <summary>
        /// Return account information.
        /// </summary>
        /// <param name="email">account owner email</param>
        /// <param name="accountId">account number</param>
        /// <returns>Account information.</returns>
        /// <exception cref="ArgumentException">Exception thrown when
        /// <paramref name="email"/> or <paramref name="accountId"/> is invalid.</exception>
        /// <exception cref="BankServiceException">Exception thrown when
        /// error occurs in the bank service.</exception>
        /// <exception cref="UserUnregisteredException">Exception thrown when
        /// user with <paramref name="email"/> not registered in bank.</exception>
        Account GetAccountInfo(string email, string accountId);

        /// <summary>
        /// Close bank account with <paramref name="accountId"/>.
        /// </summary>
        /// <param name="email">user email</param>
        /// <param name="password">user password</param>
        /// <param name="accountId">account number</param>
        /// <exception cref="ArgumentException">Exception thrown when
        /// <paramref name="email"/> or <paramref name="password"/> is invalid.</exception>
        /// <exception cref="BankServiceException">Exception thrown when
        /// error occurs in the bank service.</exception>
        /// <exception cref="UserUnregisteredException">Exception thrown when
        /// user with <paramref name="email"/> not registered in bank.</exception>
        /// <exception cref="WrongCredentialsException">Exception thrown when
        /// <paramref name="email"/> or <paramref name="password"/> is invalid.</exception>
        void CloseAccount(string email, string password, string accountId);

        /// <summary>
        /// Return all bank users.
        /// </summary>
        /// <param name="adminEmail">admin email</param>
        /// <param name="adminPassword">admin password</param>
        /// <returns>All bank users.</returns>
        /// <exception cref="ArgumentException">Exception thrown when
        /// <paramref name="adminEmail"/> or <paramref name="adminPassword"/> is invalid.</exception>
        /// <exception cref="BankServiceException">Exception thrown when
        /// error occurs in the bank service or when wrong
        /// <paramref name="adminEmail"/> or <paramref name="adminPassword"/>.</exception>
        /// <exception cref="UserUnregisteredException">Exception thrown when
        /// user with <paramref name="adminEmail"/> not registered in bank.</exception>
        /// <exception cref="WrongCredentialsException">Exception thrown when
        /// <paramref name="adminEmail"/> or <paramref name="adminPassword"/> is invalid.</exception>
        IEnumerable<BankUser> GetUsers(string adminEmail, string adminPassword);
    }
}

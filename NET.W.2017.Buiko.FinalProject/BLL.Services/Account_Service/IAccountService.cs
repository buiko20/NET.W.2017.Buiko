using System;
using BLL.Interface.Entities;

namespace BLL.Services.Account_Service
{
    /// <summary>
    /// Account service contract.
    /// </summary>
    public interface IAccountService
    {
        /// <summary>
        /// Opens a bank account.
        /// </summary>
        /// <param name="bankUser">account owner</param>
        /// <param name="sum">account initial sum</param>
        /// <param name="accountType">account type</param>
        /// <returns>Account number.</returns>
        /// <exception cref="ArgumentNullException">Exception thrown when
        /// <paramref name="bankUser"/> is null.</exception>
        /// <exception cref="ArgumentException">Exception thrown when
        /// <paramref name="sum"/> is less than 0.</exception>
        /// <exception cref="AccountServiceException">Exception thrown when
        /// error occurs in account service.</exception>
        string OpenAccount(BankUser bankUser, decimal sum, AccountType accountType = AccountType.Base);

        /// <summary>
        /// Deposit money from account with <paramref name="accountId"/>.
        /// </summary>
        /// <param name="email">account owner email</param>
        /// <param name="accountId">account number</param>
        /// <param name="sum">operation sum</param>
        /// <exception cref="ArgumentException">Exception thrown when
        /// <paramref name="sum"/> is less than 0 or when
        /// <paramref name="email"/> or <paramref name="accountId"/> is invalid.</exception>
        /// <exception cref="AccountServiceException">Exception thrown when
        /// error occurs in account service.</exception>
        void DepositMoney(string email, string accountId, decimal sum);

        /// <summary>
        ///  Withdraw money from account with <paramref name="accountId"/>.
        /// </summary>
        /// <param name="email">account owner email</param>
        /// <param name="accountId">account number</param>
        /// <param name="sum">operation sum</param>
        /// <exception cref="ArgumentException">Exception thrown when
        /// <paramref name="sum"/> is less than 0 or when
        /// <paramref name="email"/> or <paramref name="accountId"/> is invalid.</exception>
        /// <exception cref="AccountServiceException">Exception thrown when
        /// error occurs in account service.</exception>
        void WithdrawMoney(string email, string accountId, decimal sum);

        /// <summary>
        /// Returns account information.
        /// </summary>
        /// <param name="email">account owner email</param>
        /// <param name="accountId">account number</param>
        /// <returns>Account information.</returns>
        /// <exception cref="ArgumentException">Exception thrown  when
        /// <paramref name="email"/> or <paramref name="accountId"/> is invalid.</exception>
        /// <exception cref="AccountServiceException">Exception thrown when
        /// error occurs in account service.</exception>
        Account GetAccountInfo(string email, string accountId);

        /// <summary>
        /// Close account in bank.
        /// </summary>
        /// <param name="email">account owner email</param>
        /// <param name="accountId">account number</param>
        /// <exception cref="ArgumentException">Exception thrown  when
        /// <paramref name="email"/> or <paramref name="accountId"/> is invalid.</exception>
        /// <exception cref="AccountServiceException">Exception thrown when
        /// error occurs in account service.</exception>
        void CloseAccount(string email, string accountId);
    }
}

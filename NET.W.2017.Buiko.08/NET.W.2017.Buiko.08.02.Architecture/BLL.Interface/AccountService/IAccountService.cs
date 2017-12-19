using System;
using BLL.Interface.AccountIdService;

namespace BLL.Interface.AccountService
{
    /// <summary>
    /// Interface describing service contract.
    /// </summary>
    public interface IAccountService
    {
        /// <summary>
        /// Opens base account and returns its ID.
        /// </summary>
        /// <param name="onwerFirstName">account holder name</param>
        /// <param name="onwerSecondName">surname of account holder</param>
        /// <param name="sum">the initial amount of money on your account</param>
        /// <param name="ownerEmail">owner email</param>
        /// <param name="accountIdService">service that generates an account identifier</param>
        /// <returns>Account id.</returns>
        /// <exception cref="AccountServiceException">
        /// Thrown when an exception occurred in service.
        /// </exception>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="accountIdService"/> is null.</exception>
        string OpenAccount(
            string onwerFirstName, 
            string onwerSecondName, 
            decimal sum,
            string ownerEmail,
            IAccountIdService accountIdService);

        /// <summary>
        /// Opens the account and returns its ID.
        /// </summary>
        /// <param name="accountType">account type</param>
        /// <param name="onwerFirstName">account holder name</param>
        /// <param name="onwerSecondName">surname of account holder</param>
        /// <param name="sum">the initial amount of money on your account</param>
        /// <param name="ownerEmail">owner email</param>
        /// <param name="accountIdService">service that generates an account identifier</param>
        /// <returns>Account id.</returns>
        /// <exception cref="AccountServiceException">
        /// Thrown when an exception occurred in service.
        /// </exception>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="accountIdService"/> is null.</exception>
        string OpenAccount(
            AccountType accountType, 
            string onwerFirstName, 
            string onwerSecondName, 
            decimal sum,
            string ownerEmail,
            IAccountIdService accountIdService);

        /// <summary>
        /// Deposit money to your account with a specific <paramref name="accountId"/>.
        /// </summary>
        /// <param name="accountId">account id</param>
        /// <param name="sum">amount to be credited to your account</param>
        /// <exception cref="AccountServiceException">
        /// Thrown when an exception occurred in service or account with
        /// <paramref name="accountId"/> does not exists.
        /// </exception>
        void DepositMoney(string accountId, decimal sum);

        /// <summary>
        /// Withdraw money to your account with a specific <paramref name="accountId"/>.
        /// </summary>
        /// <param name="accountId">account id</param>
        /// <param name="sum">amount to be credited to your account</param>
        /// <exception cref="AccountServiceException">
        /// Thrown when an exception occurred in service or account with
        /// <paramref name="accountId"/> does not exists.
        /// </exception>
        void WithdrawMoney(string accountId, decimal sum);

        /// <summary>
        /// Closes an account with a specific <paramref name="accountId"/>.
        /// </summary>
        /// <param name="accountId">account id</param>
        /// <exception cref="AccountServiceException">
        /// Thrown when an exception occurred in service or account with
        /// <paramref name="accountId"/> does not exists.
        /// </exception>
        void CloseAccount(string accountId);

        /// <summary>
        /// Returns information about an account with a specific <paramref name="accountId"/>.
        /// </summary>
        /// <param name="accountId">account id</param>
        /// <returns>Information about an account</returns>
        /// <exception cref="AccountServiceException">
        /// Thrown when an exception occurred in service or account with
        /// <paramref name="accountId"/> does not exists.
        /// </exception>
        string GetAccountStatus(string accountId);

        /// <summary>
        /// Transfers funds from one <paramref name="sourceAccountId"/> to <paramref name="destinationAccountId"/>.
        /// </summary>
        /// <param name="sourceAccountId">account from which the transfer will be made</param>
        /// <param name="destinationAccountId">account for which will be transferred</param>
        /// <param name="transferSum">amount of remittance</param>
        /// <exception cref="AccountServiceException">
        /// Thrown when an exception occurred in service or accounts does not exists.
        /// </exception>
        void TransferFunds(string sourceAccountId, string destinationAccountId, decimal transferSum);
    }
}

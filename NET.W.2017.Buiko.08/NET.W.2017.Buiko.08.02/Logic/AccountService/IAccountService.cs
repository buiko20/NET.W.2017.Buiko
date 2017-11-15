using Logic.AccountService.Exceptions;

namespace Logic.AccountService
{
    /// <summary>
    /// Interface describing repository contract.
    /// </summary>
    public interface IAccountService
    {
        /// <summary>
        /// Opens base account and returns its ID.
        /// </summary>
        /// <param name="onwerFirstName">account holder name</param>
        /// <param name="onwerSecondName">surname of account holder</param>
        /// <param name="sum">the initial amount of money on your account</param>
        /// <returns>Account id.</returns>
        /// <exception cref="AccountServiceException">
        /// Thrown when an exception occurred in service.
        /// </exception>
        string OpenAccount(string onwerFirstName, string onwerSecondName, decimal sum);

        /// <summary>
        /// Opens the account and returns its ID.
        /// </summary>
        /// <param name="accountType">account type</param>
        /// <param name="onwerFirstName">account holder name</param>
        /// <param name="onwerSecondName">surname of account holder</param>
        /// <param name="sum">the initial amount of money on your account</param>
        /// <returns>Account id.</returns>
        /// <exception cref="AccountServiceException">
        /// Thrown when an exception occurred in service.
        /// </exception>
        string OpenAccount(AccountType accountType, string onwerFirstName, string onwerSecondName, decimal sum);

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
    }
}

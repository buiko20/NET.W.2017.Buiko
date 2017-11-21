using System.Collections.Generic;
using DAL.Interface.DTO;

namespace DAL.Interface
{
    /// <summary>
    /// Interface describing repository contract.
    /// </summary>
    public interface IAccountRepository
    {
        /// <summary>
        /// Adds <paramref name="account"/> to the repository.
        /// </summary>
        /// <param name="account"></param>
        /// <exception cref="RepositoryException">
        /// Thrown when an exception occurred in repository or
        /// <paramref name="account"/> already exists.
        /// </exception>
        void AddAccount(DalAccount account);

        /// <summary>
        /// Returns the account with the specified <paramref name="id"/>.
        /// </summary>
        /// <param name="id">account id</param>
        /// <returns>DalAccount or null if there is no such account in the repository.</returns>
        DalAccount GetAccount(string id);

        /// <summary>
        /// Updates account information.
        /// </summary>
        /// <param name="account">account</param>
        /// <exception cref="RepositoryException">
        /// Thrown when an exception occurred in repository or
        /// <paramref name="account"/> does not exists.
        /// </exception>
        void UpdateAccount(DalAccount account);

        /// <summary>
        /// Removes <param name="account"></param>.
        /// </summary>
        /// <param name="account">account</param>
        /// <exception cref="RepositoryException">
        /// Thrown when an exception occurred in repository or
        /// <paramref name="account"/> does not exists.
        /// </exception>
        void RemoveAccount(DalAccount account);

        /// <summary>
        /// Returns all accounts stored in the repository.
        /// </summary>
        /// <returns>All accounts contained in the repository.</returns>
        /// <exception cref="RepositoryException">
        /// Thrown when an exception occurred in repository.
        /// </exception>
        IEnumerable<DalAccount> GetAccounts();
    }
}

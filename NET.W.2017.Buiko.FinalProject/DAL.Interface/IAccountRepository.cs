using System;
using System.Collections.Generic;
using DAL.Interface.DTO;
using DAL.Interface.Exceptions;

namespace DAL.Interface
{
    /// <summary>
    /// Repository that stores data for bank accounts.
    /// </summary>
    public interface IAccountRepository
    {
        /// <summary>
        /// Add account to the repository.
        /// </summary>
        /// <param name="account">account to be added</param>
        /// <exception cref="ArgumentNullException">Exception thrown when
        /// <paramref name="account"/> is null.</exception>
        /// <exception cref="RepositoryException">Exception thrown when
        /// error occurs in repository.</exception>
        void AddAccount(DalAccount account);

        /// <summary>
        /// Update <paramref name="account"/> information.
        /// </summary>
        /// <param name="account">bank account</param>
        /// <exception cref="ArgumentNullException">Exception thrown when
        /// <paramref name="account"/> is null.</exception>
        /// <exception cref="RepositoryException">Exception thrown when
        /// error occurs in repository or user not found.</exception>
        void UpdateAccount(DalAccount account);

        /// <summary>
        /// Delete <paramref name="account"/> from repository.
        /// </summary>
        /// <param name="account">account to be deleted</param>
        /// <exception cref="ArgumentNullException">Exception thrown when
        /// <paramref name="account"/> is null.</exception>
        /// <exception cref="RepositoryException">Exception thrown when
        /// error occurs in repository or user not found.</exception>
        void RemoveAccount(DalAccount account);

        /// <summary>
        /// Get all bank accounts.
        /// </summary>
        /// <returns>All bank accounts.</returns>
        /// <exception cref="RepositoryException">Exception thrown when
        /// error occurs in repository.</exception>
        IEnumerable<DalAccount> GetAccounts();

        /// <summary>
        /// Get all accounts with user's <paramref name="email"/>.
        /// </summary>
        /// <param name="email">user's email</param>
        /// <returns>Accounts with user's <paramref name="email"/>.</returns>
        /// <exception cref="RepositoryException">Exception thrown when
        /// error occurs in repository.</exception>
        IEnumerable<DalAccount> GetUserAccounts(string email);
    }
}

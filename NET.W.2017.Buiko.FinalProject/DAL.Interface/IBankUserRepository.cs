using System;
using System.Collections.Generic;
using DAL.Interface.DTO;
using DAL.Interface.Exceptions;

namespace DAL.Interface
{
    /// <summary>
    /// Repository that stores data for bank users.
    /// </summary>
    public interface IBankUserRepository
    {
        /// <summary>
        /// Add user to the repository.
        /// </summary>
        /// <param name="bankUser">user to be added</param>
        /// <exception cref="ArgumentNullException">Exception thrown when
        /// <paramref name="bankUser"/> is null.</exception>
        /// <exception cref="RepositoryException">Exception thrown when
        /// error occurs in repository.</exception>
        void AddBankUser(DalBankUser bankUser);

        /// <summary>
        /// Return user with <paramref name="email"/>.
        /// </summary>
        /// <param name="email">user email</param>
        /// <returns>User with <paramref name="email"/>.</returns>
        /// <exception cref="ArgumentException">Exception thrown when
        /// <paramref name="email"/> is invalid.</exception>
        /// <exception cref="RepositoryException">Exception thrown when
        /// error occurs in repository.</exception>
        DalBankUser GetByEmail(string email);

        /// <summary>
        /// Update <paramref name="bankUser"/> information.
        /// </summary>
        /// <param name="bankUser">bank user</param>
        /// <exception cref="ArgumentNullException">Exception thrown when
        /// <paramref name="bankUser"/> is null.</exception>
        /// <exception cref="RepositoryException">Exception thrown when
        /// error occurs in repository or user not found.</exception>
        void UpdateBankUser(DalBankUser bankUser);

        /// <summary>
        /// Return all bank users.
        /// </summary>
        /// <returns>All bank users.</returns>
        /// <exception cref="RepositoryException">Exception thrown when
        /// error occurs in repository.</exception>
        IEnumerable<DalBankUser> GetBankUsers();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using Logic.Domain.Account;
using Logic.AccountRepository.Exceptions;

namespace Logic.AccountRepository.Implementation
{
    public class FakeAccountRepository : IAccountRepository
    {
        #region private fields

        private readonly List<Account> _accounts = new List<Account>();

        #endregion // !private fields.

        #region interfaces implementation

        /// <inheritdoc />
        public void AddAccount(Account account)
        {
            if (ReferenceEquals(account, null))
            {
                throw new ArgumentNullException(nameof(account));
            }

            if (_accounts.Any(account.Equals))
            {
                throw new RepositoryException("This account already exists");
            }

            try
            {
                _accounts.Add(account);
            }
            catch (Exception e)
            {
                throw new RepositoryException("Add account error", e);
            }
        }

        /// <inheritdoc />
        public Account GetAccount(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentException(nameof(id));
            }

            return _accounts.FirstOrDefault(account => account.Id == id);
        }

        /// <inheritdoc />
        public void UpdateAccount(Account account)
        {
            if (ReferenceEquals(account, null))
            {
                throw new ArgumentNullException(nameof(account));
            }

            if (!_accounts.Any(account.Equals))
            {
                throw new RepositoryException("Account does not exists");
            }

            try
            {
                _accounts.Remove(account);
                _accounts.Add(account);
            }
            catch (Exception e)
            {
                throw new RepositoryException("Update account error", e);
            }
        }

        /// <inheritdoc />
        public void RemoveAccount(Account account)
        {
            if (ReferenceEquals(account, null))
            {
                throw new ArgumentNullException(nameof(account));
            }

            if (!_accounts.Any(account.Equals))
            {
                throw new RepositoryException("Aaccount does not exists");
            }

            try
            {
                _accounts.Remove(account);
            }
            catch (Exception e)
            {
                throw new RepositoryException("Remove account error", e);
            }
        }

        /// <inheritdoc />
        public IEnumerable<Account> GetAccounts() =>
            _accounts.ToArray();

        #endregion // !interfaces implementation.
    }
}

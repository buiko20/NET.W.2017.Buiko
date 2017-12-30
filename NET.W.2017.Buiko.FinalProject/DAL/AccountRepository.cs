using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DAL.Interface;
using DAL.Interface.DTO;
using DAL.Interface.Exceptions;
using DAL.Mappers;
using ORM.Model;

namespace DAL
{
    public class AccountRepository : IAccountRepository
    {
        private readonly DbContext _dbContext;

        #region public

        /// <summary>
        /// Initializes the class with the passed parameters.
        /// </summary>
        /// <param name="dbContext">database context</param>
        /// <exception cref="ArgumentNullException">Exception thrown when
        /// <paramref name="dbContext"/> is null.</exception>
        public AccountRepository(DbContext dbContext)
        {
            if (ReferenceEquals(dbContext, null))
            {
                throw new ArgumentNullException(nameof(dbContext));
            }

            _dbContext = dbContext;
        }

        #region implementation of interface

        /// <inheritdoc />
        public void AddAccount(DalAccount account)
        {
            if (ReferenceEquals(account, null))
            {
                throw new ArgumentNullException(nameof(account));
            }

            try
            {
                var ormBankUser = this.FindBankUserByEmail(account.BankUser.Email);
                var ormAccount = account.ToOrmAccount(ormBankUser);
                _dbContext.Set<Account>().Add(ormAccount);
            }
            catch (Exception e)
            {
                throw new RepositoryException("Add account error.", e);
            }
        }

        /// <inheritdoc />
        public void UpdateAccount(DalAccount account)
        {
            if (ReferenceEquals(account, null))
            {
                throw new ArgumentNullException(nameof(account));
            }

            try
            {
                var ormBankUser = this.FindBankUserByEmail(account.BankUser.Email);
                var ormAccount = FindAccoundById(ormBankUser, account.Id);

                ormAccount.BonusPoints = account.BonusPoints;
                ormAccount.Sum = account.Sum;
                _dbContext.Entry(ormAccount).State = EntityState.Modified;
            }
            catch (Exception e)
            {
                throw new RepositoryException("Update account error.", e);
            }
        }

        /// <inheritdoc />
        public void RemoveAccount(DalAccount account)
        {
            if (ReferenceEquals(account, null))
            {
                throw new ArgumentNullException(nameof(account));
            }

            try
            {
                var ormBankUser = this.FindBankUserByEmail(account.BankUser.Email);
                var ormAccount = FindAccoundById(ormBankUser, account.Id);
                _dbContext.Entry(ormAccount).State = EntityState.Deleted;
            }
            catch (Exception e)
            {
                throw new RepositoryException("Delete account error.", e);
            }
        }

        /// <inheritdoc />
        public IEnumerable<DalAccount> GetAccounts()
        {
            try
            {
                return this.GetAccounts(account => true);
            }
            catch (Exception e)
            {
                throw new RepositoryException("Get accounts error.", e);
            }
        }

        /// <inheritdoc />
        public IEnumerable<DalAccount> GetUserAccounts(string email)
        {
            try
            {
                return this.GetAccounts(account =>
                    string.Equals(account.BankUser.Email, email, StringComparison.Ordinal));
            }
            catch (Exception e)
            {
                throw new RepositoryException("Get accounts error.", e);
            }
        }

        #endregion // !implementation of interface.

        #endregion // !public.

        #region private

        private static Account FindAccoundById(BankUser ormBankUser, string accoundId)
        {
            var ormAccount = ormBankUser.Accounts.FirstOrDefault(
                a => string.Equals(a.AccountId, accoundId, StringComparison.Ordinal));

            if (ReferenceEquals(ormAccount, null))
            {
                throw new RepositoryException($"Account with id {accoundId} not found.");
            }

            return ormAccount;
        }

        private BankUser FindBankUserByEmail(string email)
        {   
            var result = _dbContext.Set<BankUser>().Include(user => user.Accounts)
                .Include(user => user.Accounts.Select(account => account.AccountType))
                .Include(user => user.Role).ToList()
                .FirstOrDefault(user => string.Equals(user.Email, email, StringComparison.Ordinal));

            if (ReferenceEquals(result, null))
            {
                throw new RepositoryException($"User with email {email} not found.");
            }

            return result;
        }

        private IEnumerable<DalAccount> GetAccounts(Func<Account, bool> predicate) =>
            _dbContext.Set<Account>().Include(account => account.BankUser)
                .Include(account => account.AccountType).Include(account => account.BankUser.Role)
                .ToList().Where(predicate)
                .Select(account => account.ToDalAccount(account.BankUser.ToDalBankUser()));

        #endregion // !private.
    }
}

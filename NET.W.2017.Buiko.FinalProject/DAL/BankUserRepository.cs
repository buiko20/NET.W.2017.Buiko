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
    public class BankUserRepository : IBankUserRepository
    {
        private readonly DbContext _dbContext;

        #region public

        /// <summary>
        /// Initializes the class with the passed parameters.
        /// </summary>
        /// <param name="dbContext">database context</param>
        /// <exception cref="ArgumentNullException">Exception thrown when
        /// <paramref name="dbContext"/> is null.</exception>
        public BankUserRepository(DbContext dbContext)
        {
            if (ReferenceEquals(dbContext, null))
            {
                throw new ArgumentNullException(nameof(dbContext));
            }

            _dbContext = dbContext;
        }

        #region implementation of interface

        /// <inheritdoc />
        public void AddBankUser(DalBankUser bankUser)
        {
            if (ReferenceEquals(bankUser, null))
            {
                throw new ArgumentNullException(nameof(bankUser));
            }

            try
            {
                var ormBankUser = bankUser.ToOrmBankUser();
                _dbContext.Set<BankUser>().Add(ormBankUser);
            }
            catch (Exception e)
            {
                throw new RepositoryException("Add user error.", e);
            }
        }

        /// <inheritdoc />
        public DalBankUser GetByEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentException($"{nameof(email)} is invalid.", nameof(email));
            }

            try
            {
                var ormBankUser = this.FindBankUserByEmail(email);
                return ReferenceEquals(ormBankUser, null) ? null : ormBankUser.ToDalBankUser();
            }
            catch (Exception e)
            {
                throw new RepositoryException("Get user error.", e);
            }
        }

        /// <inheritdoc />
        public void UpdateBankUser(DalBankUser bankUser)
        {
            if (ReferenceEquals(bankUser, null))
            {
                throw new ArgumentNullException(nameof(bankUser));
            }

            try
            {
                var ormBankUser = this.FindBankUserByEmail(bankUser.Email);
                if (ReferenceEquals(ormBankUser, null))
                {
                    throw new RepositoryException($"User with email {bankUser.Email} not found.");
                }

                UpdateBankUser(ormBankUser, bankUser);
                _dbContext.Entry(ormBankUser).State = EntityState.Modified;
            }
            catch (Exception e)
            {
                throw new RepositoryException("Update user error.", e);
            }
        }

        /// <inheritdoc />
        public IEnumerable<DalBankUser> GetBankUsers()
        {
            try
            {
                return _dbContext.Set<BankUser>().Include(user => user.Accounts)
                    .Include(user => user.Accounts.Select(account => account.AccountType))
                    .Include(user => user.Role).ToList().Select(user => user.ToDalBankUser());
            }
            catch (Exception e)
            {
                throw new RepositoryException("Get users error.", e);
            }
        }

        #endregion // !implementation of interface.

        #endregion // !public.

        #region private

        private static void UpdateBankUser(BankUser ormBankUser, DalBankUser dalBankUser)
        {
            ormBankUser.FirstName = dalBankUser.FirstName;
            ormBankUser.SecondName = dalBankUser.SecondName;
            ormBankUser.PasswordHash = dalBankUser.PasswordHash;
            ormBankUser.Role.RoleInfo = dalBankUser.Role;
        }

        private BankUser FindBankUserByEmail(string email) =>
            _dbContext.Set<BankUser>().Include(user => user.Accounts)
                .Include(user => user.Accounts.Select(account => account.AccountType))
                .Include(user => user.Role).ToList()
                .FirstOrDefault(user => string.Equals(user.Email, email, StringComparison.Ordinal));

        #endregion // !private.
    }
}
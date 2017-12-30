using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DAL.Interface;
using DAL.Interface.DTO;
using ORM.Model;

namespace DAL.EF
{
    public class AccountRepository : IAccountRepository
    {
        private readonly DbContext _dbContext;

        public AccountRepository(DbContext dbContext)
        {
            if (ReferenceEquals(dbContext, null))
            {
                throw new ArgumentNullException(nameof(dbContext));
            }

            _dbContext = dbContext;
        }

        #region interface implementation

        public void AddAccount(DalAccount account)
        {
            if (ReferenceEquals(account, null))
            {
                throw new ArgumentNullException(nameof(account));
            }

            var accountOwner = GetAccountOwner(account);
            _dbContext.Set<AccountOwner>().Add(accountOwner);

            var efAccount = GetAccount(account, accountOwner);
            _dbContext.Set<Account>().Add(efAccount);
        }

        public DalAccount GetAccount(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentException($"{nameof(id)} is invalid.", nameof(id));
            }

            var account = _dbContext.Set<Account>()
                .Include(acc => acc.AccountType)
                .Include(acc => acc.AccountOwner)
                .FirstOrDefault(acc => acc.AccountId == id);

            return ReferenceEquals(account, null) ? null : ToDalAccount(account);
        }

        public void UpdateAccount(DalAccount account)
        {
            if (ReferenceEquals(account, null))
            {
                throw new ArgumentNullException(nameof(account));
            }

            var efAccount = _dbContext.Set<Account>()
                .Include(acc => acc.AccountType)
                .Include(acc => acc.AccountOwner)
                .FirstOrDefault(acc => acc.AccountId == account.Id);

            if (ReferenceEquals(efAccount, null))
            {
                throw new RepositoryException($"{nameof(account)} does not exist");
            }

            efAccount.BonusPoints = account.BonusPoints;
            efAccount.CurrentSum = account.CurrentSum;
            _dbContext.Entry(efAccount).State = EntityState.Modified;
        }

        public void RemoveAccount(DalAccount account)
        {
            if (ReferenceEquals(account, null))
            {
                throw new ArgumentNullException(nameof(account));
            }

            var efAccount = _dbContext.Set<Account>()
                .Include(acc => acc.AccountType)
                .Include(acc => acc.AccountOwner)
                .FirstOrDefault(acc => acc.AccountId == account.Id);

            if (ReferenceEquals(efAccount, null))
            {
                throw new RepositoryException($"{nameof(account)} does not exist");
            }

            var accountType = efAccount.AccountType;
            var accountOwner = efAccount.AccountOwner;

            _dbContext.Set<AccountType>().Remove(accountType);
            _dbContext.Set<AccountOwner>().Remove(accountOwner);
            _dbContext.Set<Account>().Remove(efAccount);
        }

        public IEnumerable<DalAccount> GetAccounts()
        {
            var accounts = new List<DalAccount>();
            foreach (var acc in _dbContext.Set<Account>()
                .Include(account => account.AccountType)
                .Include(account => account.AccountOwner))
            {
                accounts.Add(ToDalAccount(acc));
            }

            return accounts;
        }

        #endregion // !interface implementation.

        #region private

        private static AccountType GetAccountType(DalAccount account)
        {
            if (account.AccountType.Contains("Gold"))
            {
                return new AccountType { Type = "Gold" };
            }

            if (account.AccountType.Contains("Platinum"))
            {
                return new AccountType { Type = "Platinum" };
            }

            return new AccountType { Type = "Base" };
        }

        private static AccountOwner GetAccountOwner(DalAccount account) =>
            new AccountOwner
            {
                OwnerEmail = account.OwnerEmail,
                OwnerFirstName = account.OwnerFirstName,
                OwnerSecondName = account.OwnerSecondName
            };

        private static Account GetAccount(DalAccount account, AccountOwner accountOwner) =>
            new Account
            {
                AccountType = GetAccountType(account),
                CurrentSum = account.CurrentSum,
                BonusPoints = account.BonusPoints,
                AccountId = account.Id,
                AccountOwner = accountOwner
            };

        private static DalAccount ToDalAccount(Account account) =>
            new DalAccount
            {
                Id = account.AccountId,
                AccountType = account.AccountType.Type,
                OwnerEmail = account.AccountOwner.OwnerEmail,
                OwnerSecondName = account.AccountOwner.OwnerSecondName,
                OwnerFirstName = account.AccountOwner.OwnerFirstName,
                BonusPoints = account.BonusPoints,
                CurrentSum = account.CurrentSum
            };

        #endregion // !private.
    }
}

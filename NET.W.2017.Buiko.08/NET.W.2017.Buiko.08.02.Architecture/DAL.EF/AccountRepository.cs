using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DAL.EF.Model;
using DAL.Interface;
using DAL.Interface.DTO;

namespace DAL.EF
{
    public class AccountRepository : IAccountRepository
    {
        #region interface implementation

        public void AddAccount(DalAccount account)
        {
            if (ReferenceEquals(account, null))
            {
                throw new ArgumentNullException(nameof(account));
            }

            using (var db = new AccountContext())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        var accountOwner = GetAccountOwner(account);
                        db.Owners.Add(accountOwner);
                        db.SaveChanges();

                        var efAccount = GetAccount(account, accountOwner);
                        db.Accounts.Add(efAccount);
                        db.SaveChanges();

                        transaction.Commit();
                    }
                    catch (Exception e)
                    {
                        transaction.Rollback();
                        throw new RepositoryException("Add account error.", e);
                    }
                }
            }
        }

        public DalAccount GetAccount(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentException($"{nameof(id)} is invalid.", nameof(id));
            }

            using (var db = new AccountContext())
            {
                var account = db.Accounts
                    .Include(acc => acc.AccountType)
                    .Include(acc => acc.AccountOwner)
                    .FirstOrDefault(acc => acc.AccountId == id);
                if (ReferenceEquals(account, null))
                {
                    return null;
                }

                return ToDalAccount(account);
            }
        }

        public void UpdateAccount(DalAccount account)
        {
            if (ReferenceEquals(account, null))
            {
                throw new ArgumentNullException(nameof(account));
            }

            using (var db = new AccountContext())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        var efAccount = db.Accounts
                            .Include(acc => acc.AccountType)
                            .Include(acc => acc.AccountOwner)
                            .FirstOrDefault(acc => acc.AccountId == account.Id);
                        if (ReferenceEquals(efAccount, null))
                        {
                            throw new RepositoryException($"{nameof(account)} does not exist");
                        }

                        UpdateAccount(efAccount, account);
                        db.SaveChanges();
                        transaction.Commit();
                    }
                    catch (Exception e)
                    {
                        transaction.Rollback();
                        throw new RepositoryException("Update account error.", e);
                    }
                }
            }
        }

        public void RemoveAccount(DalAccount account)
        {
            if (ReferenceEquals(account, null))
            {
                throw new ArgumentNullException(nameof(account));
            }

            using (var db = new AccountContext())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        var efAccount = db.Accounts
                            .Include(acc => acc.AccountType)
                            .Include(acc => acc.AccountOwner)
                            .FirstOrDefault(acc => acc.AccountId == account.Id);
                        if (ReferenceEquals(efAccount, null))
                        {
                            throw new RepositoryException($"{nameof(account)} does not exist");
                        }

                        var accountType = efAccount.AccountType;
                        var accountOwner = efAccount.AccountOwner;

                        db.AccountTypes.Remove(accountType);
                        db.Owners.Remove(accountOwner);
                        db.Accounts.Remove(efAccount);
                        db.SaveChanges();

                        transaction.Commit();
                    }
                    catch (Exception e)
                    {
                        transaction.Rollback();
                        throw new RepositoryException("Update account error.", e);
                    }
                }
            }
        }

        public IEnumerable<DalAccount> GetAccounts()
        {
            var accounts = new List<DalAccount>();
            using (var db = new AccountContext())
            {
                foreach (var acc in db.Accounts
                    .Include(account => account.AccountType)
                    .Include(account => account.AccountOwner))
                {
                    accounts.Add(ToDalAccount(acc));
                }
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

        private static void UpdateAccount(Account dest, DalAccount source)
        {
            dest.BonusPoints = source.BonusPoints;
            dest.CurrentSum = source.CurrentSum;
        }

        #endregion // !private.
    }
}

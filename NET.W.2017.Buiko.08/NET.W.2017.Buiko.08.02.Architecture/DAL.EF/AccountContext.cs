using System.Data.Entity;
using DAL.EF.Model;

namespace DAL.EF
{
    internal class AccountContext : DbContext
    {
        public AccountContext() : base("Accounts")
        {
        }

        public DbSet<Account> Accounts { get; set; }

        public DbSet<AccountOwner> Owners { get; set; }

        public DbSet<AccountType> AccountTypes { get; set; }
    }
}

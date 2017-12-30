using System.Data.Entity;
using ORM.Model;

namespace ORM
{
    public class AccountContext : DbContext
    {
        public AccountContext() : base("Accounts")
        {
        }

        public DbSet<Account> Accounts { get; set; }

        public DbSet<AccountOwner> Owners { get; set; }

        public DbSet<AccountType> AccountTypes { get; set; }
    }
}

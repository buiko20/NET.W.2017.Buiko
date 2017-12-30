using System.Data.Entity;
using ORM.Model;

namespace ORM
{
    public class BankContext : DbContext
    {
        public BankContext() : base("BankDB")
        {
        }

        public DbSet<Account> Accounts { get; set; }

        public DbSet<BankUser> BankUsers { get; set; }

        public DbSet<AccountType> AccountTypes { get; set; }
    }
}

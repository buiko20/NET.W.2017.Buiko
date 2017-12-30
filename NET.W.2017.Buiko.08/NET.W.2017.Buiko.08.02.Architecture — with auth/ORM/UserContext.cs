using System.Data.Entity;
using ORM.Model;

namespace ORM
{
    public class UserContext : DbContext
    {
        public UserContext() : base("AccountUsers")
        {
        }

        public DbSet<User> Users { get; set; }
    }
}

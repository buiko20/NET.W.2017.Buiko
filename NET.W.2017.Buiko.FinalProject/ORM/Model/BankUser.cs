using System.Collections.Generic;

namespace ORM.Model
{
    public class BankUser
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string PasswordHash { get; set; }

        public int RoleId { get; set; }
        public Role Role { get; set; }

        public ICollection<Account> Accounts { get; set; } = new List<Account>();
    }
}

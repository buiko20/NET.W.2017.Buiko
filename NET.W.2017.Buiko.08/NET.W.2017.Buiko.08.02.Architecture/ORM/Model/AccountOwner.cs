using System.Collections.Generic;

namespace ORM.Model
{
    public class AccountOwner
    {
        public int Id { get; set; }

        public string OwnerFirstName { get; set; }

        public string OwnerSecondName { get; set; }

        public string OwnerEmail { get; set; }

        public virtual ICollection<Account> Accounts { get; set; }
    }
}

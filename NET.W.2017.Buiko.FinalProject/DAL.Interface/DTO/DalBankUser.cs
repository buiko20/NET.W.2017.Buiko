using System.Collections.Generic;

namespace DAL.Interface.DTO
{
    public class DalBankUser
    {
        public string FirstName { get; set; }

        public string SecondName { get; set; }

        public string Email { get; set; }

        public string PasswordHash { get; set; }

        public string Role { get; set; }

        public List<DalAccount> Accounts { get; set; } = new List<DalAccount>();
    }
}

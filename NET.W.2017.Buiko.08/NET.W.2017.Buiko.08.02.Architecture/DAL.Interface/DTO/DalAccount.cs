using System;

namespace DAL.Interface.DTO
{
    public class DalAccount
    {
        public Type AccountType { get; set; }

        public string Id { get; set; }

        public string OwnerFirstName { get; set; }

        public string OwnerSecondName { get; set; }

        public decimal CurrentSum { get; set; }
       
        public int BonusPoints { get; set; }
    }
}

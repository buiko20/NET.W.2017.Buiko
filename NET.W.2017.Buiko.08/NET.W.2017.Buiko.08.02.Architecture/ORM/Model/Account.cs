namespace ORM.Model
{
    public class Account
    {
        public string AccountId { get; set; }

        public decimal CurrentSum { get; set; }

        public int BonusPoints { get; set; }

        public int AccountOwnerId { get; set; }

        public virtual AccountOwner AccountOwner { get; set; }

        public int AccountTypeId { get; set; }

        public virtual AccountType AccountType { get; set; }
    }
}

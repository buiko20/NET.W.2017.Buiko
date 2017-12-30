namespace ORM.Model
{
    public class Account
    {
        public string AccountId { get; set; }
        public decimal Sum { get; set; }
        public int BonusPoints { get; set; }

        public int BankUserId { get; set; }
        public BankUser BankUser { get; set; }

        public int AccountTypeId { get; set; }
        public AccountType AccountType { get; set; }
    }
}

namespace DAL.Interface.DTO
{
    public class DalAccount
    {
        public string Id { get; set; }

        public string Type { get; set; }

        public decimal Sum { get; set; }

        public int BonusPoints { get; set; }

        public DalBankUser BankUser { get; set; }
    }
}

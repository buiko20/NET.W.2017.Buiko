namespace BLL.Interface.Account
{
    /// <inheritdoc />
    /// <summary>
    /// Represents a platinum account.
    /// </summary>
    public class PlatinumAccount : Account
    {
        /// <inheritdoc />
        public PlatinumAccount(
            string id, 
            string onwerFirstName, 
            string onwerSecondName, 
            decimal currentSum, 
            int bonusPoints,
            string ownerEmail) :
            base(id, onwerFirstName, onwerSecondName, currentSum, bonusPoints, ownerEmail)
        {
            this.BonusValue = 42;
        }

        /// <inheritdoc />
        protected override int CalculateBonusPointsForDeposit(decimal sum, int bonusValue) =>
            (((int)sum + bonusValue) % bonusValue) + (bonusValue * 2);

        /// <inheritdoc />
        protected override int CalculateBonusPointsForWithdraw(decimal sum, int bonusValue) =>
            (((int)sum + bonusValue) % bonusValue) + (bonusValue * 2);

        /// <inheritdoc />
        protected override string GetAccountAdditionalInformation() => 
            "Platinum account.";
    }
}

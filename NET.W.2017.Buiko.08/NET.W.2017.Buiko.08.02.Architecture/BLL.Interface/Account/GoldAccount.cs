namespace BLL.Interface.Account
{
    /// <inheritdoc />
    /// <summary>
    /// Represents a gold account.
    /// </summary>
    public class GoldAccount : Account
    {
        /// <inheritdoc />
        public GoldAccount(string id, string onwerFirstName, string onwerSecondName, decimal currentSum, int bonusPoints) :
            base(id, onwerFirstName, onwerSecondName, currentSum, bonusPoints)
        {
            this.BonusValue = 22;
        }

        /// <inheritdoc />
        public override string ToString() => "Gold account. " + base.ToString();

        /// <inheritdoc />
        protected override int CalculateBonusPointsForDeposit(decimal sum, int bonusValue) =>
            (((int)sum + bonusValue) % bonusValue) + bonusValue;

        /// <inheritdoc />
        protected override int CalculateBonusPointsForWithdraw(decimal sum, int bonusValue) =>
            (((int)sum + bonusValue) % bonusValue) + bonusValue;
    }
}

using BLL.Interface.Entities;

namespace BLL.Services.Accounts
{
    /// <inheritdoc />
    /// <summary>
    /// Represents a gold account.
    /// </summary>
    public class BllGoldAccount : BllAccount
    {
        /// <inheritdoc />
        public BllGoldAccount(string id, decimal sum, int bonusPoints, BankUser accountOwner) : 
            base(id, sum, bonusPoints, accountOwner)
        {
            this.BonusValue = 50;
        }

        /// <inheritdoc />
        protected override int CalculateBonusPointsForDeposit(decimal sum, int bonusValue) =>
            (((int)sum + bonusValue) % bonusValue) + bonusValue;

        /// <inheritdoc />
        protected override int CalculateBonusPointsForWithdraw(decimal sum, int bonusValue) =>
            (((int)sum + bonusValue) % bonusValue) + bonusValue;

        /// <inheritdoc />
        protected override string GetAccountAdditionalInformation() => "Gold account";
    }
}

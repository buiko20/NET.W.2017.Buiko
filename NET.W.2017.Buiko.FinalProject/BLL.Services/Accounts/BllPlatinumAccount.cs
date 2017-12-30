using BLL.Interface.Entities;

namespace BLL.Services.Accounts
{
    /// <inheritdoc />
    /// <summary>
    /// Represents a platinum account.
    /// </summary>
    public class BllPlatinumAccount : BllAccount
    {
        /// <inheritdoc />
        public BllPlatinumAccount(string id, decimal sum, int bonusPoints, BankUser accountOwner) : 
            base(id, sum, bonusPoints, accountOwner)
        {
            this.BonusValue = 100;
        }

        /// <inheritdoc />
        protected override int CalculateBonusPointsForDeposit(decimal sum, int bonusValue) =>
            (((int)sum + bonusValue) % bonusValue) + (bonusValue * 2);

        /// <inheritdoc />
        protected override int CalculateBonusPointsForWithdraw(decimal sum, int bonusValue) =>
            (((int)sum + bonusValue) % bonusValue) + (bonusValue * 2);

        /// <inheritdoc />
        protected override string GetAccountAdditionalInformation() => "Platinum account";
    }
}

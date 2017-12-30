using BLL.Interface.Entities;

namespace BLL.Services.Accounts
{
    /// <inheritdoc />
    /// <summary>
    /// Represents a basic account.
    /// </summary>
    public class BllBaseAccount : BllAccount
    {
        /// <inheritdoc />
        public BllBaseAccount(string id, decimal sum, int bonusPoints, BankUser accountOwner) : 
            base(id, sum, bonusPoints, accountOwner)
        {
            this.BonusValue = 0;
        }

        /// <inheritdoc />
        protected override int CalculateBonusPointsForDeposit(decimal sum, int bonusValue) => 0;

        /// <inheritdoc />
        protected override int CalculateBonusPointsForWithdraw(decimal sum, int bonusValue) => 0;

        /// <inheritdoc />
        protected override string GetAccountAdditionalInformation() => "Base account";
    }
}

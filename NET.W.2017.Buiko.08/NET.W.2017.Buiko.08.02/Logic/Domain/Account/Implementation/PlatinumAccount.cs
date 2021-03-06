﻿namespace Logic.Domain.Account.Implementation
{
    /// <inheritdoc />
    /// <summary>
    /// Represents a platinum account.
    /// </summary>
    public class PlatinumAccount : Account
    {
        /// <inheritdoc />
        public PlatinumAccount(string id, string onwerFirstName, string onwerSecondName, decimal currentSum, int bonusPoints) : 
            base(id, onwerFirstName, onwerSecondName, currentSum, bonusPoints)
        {
            this.BonusValue = 42;
        }

        /// <inheritdoc />
        public override string ToString() => "Platinum account. " + base.ToString();

        /// <inheritdoc />
        protected override int CalculateBonusPointsForDeposit(decimal sum, int bonusValue) =>
            (((int)sum + bonusValue) % bonusValue) + (bonusValue * 2);

        /// <inheritdoc />
        protected override int CalculateBonusPointsForWithdraw(decimal sum, int bonusValue) =>
            (((int)sum + bonusValue) % bonusValue) + (bonusValue * 2);
    }
}

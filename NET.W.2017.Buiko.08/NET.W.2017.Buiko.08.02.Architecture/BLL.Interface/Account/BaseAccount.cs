﻿namespace BLL.Interface.Account
{
    /// <inheritdoc />
    /// <summary>
    /// Represents a basic account.
    /// </summary>
    public class BaseAccount : Account
    {
        /// <inheritdoc />
        public BaseAccount(
            string id, 
            string onwerFirstName, 
            string onwerSecondName, 
            decimal currentSum, 
            int bonusPoints,
            string ownerEmail)
            : base(id, onwerFirstName, onwerSecondName, currentSum, bonusPoints, ownerEmail)
        {
            this.BonusValue = 0;
        }

        /// <inheritdoc />
        protected override int CalculateBonusPointsForDeposit(decimal sum, int bonusValue) => 0;

        /// <inheritdoc />
        protected override int CalculateBonusPointsForWithdraw(decimal sum, int bonusValue) => 0;

        /// <inheritdoc />
        protected override string GetAccountAdditionalInformation() => 
            "Base account.";
    }
}

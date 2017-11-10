using System;
using Logic.AccountCreationService.Exceptions;
using Logic.Domain.Account;

namespace Logic.AccountCreationService.Implementation
{
    /// <inheritdoc />
    public class AccountCreater : IAccountCreater
    {
        /// <inheritdoc />
        public Account CreateAccount(
            Type accountType, string id, string onwerFirstName, string onwerSecondName, decimal sum, int bonusPoints)
        {
            try
            {
                return (Account)Activator.CreateInstance(
                    accountType, id, onwerFirstName, onwerSecondName, sum, bonusPoints);
            }
            catch (Exception e)
            {
                throw new AccountCreationServiceException("Account creation error", e);
            }
        }
    }
}

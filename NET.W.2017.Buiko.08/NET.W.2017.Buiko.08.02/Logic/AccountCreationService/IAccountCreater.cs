using System;
using Logic.AccountCreationService.Exceptions;
using Logic.Domain.Account;

namespace Logic.AccountCreationService
{
    /// <summary>
    /// Interface describing account creation service contract.
    /// </summary>
    public interface IAccountCreater
    {
        /// <summary>
        /// Creates an account instance.
        /// </summary>
        /// <param name="accountType">account type</param>
        /// <param name="id">account id</param>
        /// <param name="onwerFirstName">owner name</param>
        /// <param name="onwerSecondName">surname of the owner</param>
        /// <param name="sum">account start sum</param>
        /// <param name="bonusPoints">account start bonus points</param>
        /// <returns>Account instance</returns>
        /// <exception cref="AccountCreationServiceException">
        /// Thrown when an exception occurred in service.
        /// </exception>
        Account CreateAccount(Type accountType, string id, string onwerFirstName, string onwerSecondName, decimal sum, int bonusPoints);
    }
}

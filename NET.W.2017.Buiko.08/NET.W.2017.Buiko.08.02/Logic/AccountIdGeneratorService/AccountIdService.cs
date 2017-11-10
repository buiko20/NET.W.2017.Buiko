using System;
using System.Text;
using Logic.AccountIdGeneratorService.Exceptions;

namespace Logic.AccountIdGeneratorService
{
    /// <summary>
    /// Class describing account creation service behavior.
    /// </summary>
    public abstract class AccountIdService
    {
        #region public

        /// <summary>
        /// Account id max length.
        /// </summary>
        public static readonly int IdLength = 20;

        /// <summary>
        /// Generates an account ID.
        /// </summary>
        /// <param name="onwerFirstName">owner name</param>
        /// <param name="onwerSecondName">surname of the owner</param>
        /// <returns>Account id.</returns>
        /// <exception cref="AccountIdServiceException">
        /// Thrown when an exception occurred in service.
        /// </exception>
        public string GenerateAccountId(string onwerFirstName, string onwerSecondName)
        {
            try
            {
                var accountId = new StringBuilder(GetFirstIdPart());

                accountId.Append(GetSecondIdPart(onwerFirstName));

                accountId.Append(GetThirdIdPart(onwerSecondName));

                return Shuffle(accountId.ToString()).Trim().Substring(0, IdLength);
            }
            catch (Exception e)
            {
                throw new AccountIdServiceException("Id generation error", e);
            }
        }

        #endregion // !public.

        #region protected

        protected abstract string GetFirstIdPart();

        protected abstract string GetSecondIdPart(string onwerFirstName);

        protected abstract string GetThirdIdPart(string onwerSecondName);

        protected abstract string Shuffle(string str);

        #endregion // !protected.
    }
}

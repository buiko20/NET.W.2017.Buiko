namespace BLL.Services.AccountIdService
{
    public interface IAccountIdService
    {
        /// <summary>
        /// Generates an account ID.
        /// </summary>
        /// <param name="ownerEmail">owner name</param>
        /// <param name="onwerSecondName">surname of the owner</param>
        /// <returns>Account id.</returns>
        /// <exception cref="AccountIdServiceException">
        /// Thrown when an exception occurred in service.
        /// </exception>
        string GenerateAccountId(string ownerEmail, string onwerSecondName);
    }
}

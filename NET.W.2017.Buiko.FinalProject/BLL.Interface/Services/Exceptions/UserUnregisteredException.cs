namespace BLL.Interface.Services.Exceptions
{
    /// <inheritdoc />
    /// <summary>
    /// Exception that is thrown if user not registered in bank.
    /// </summary>
    public class UserUnregisteredException : BankServiceException
    {
        /// <inheritdoc />
        /// <param name="userEmail">iser email</param>
        public UserUnregisteredException(string userEmail) : 
            base($"User {userEmail} not registered.")
        {
            this.UserEmail = userEmail;
        }

        /// <summary>
        /// User email.
        /// </summary>
        public string UserEmail { get; }
    }
}

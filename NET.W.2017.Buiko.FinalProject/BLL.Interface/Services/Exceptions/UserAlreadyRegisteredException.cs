namespace BLL.Interface.Services.Exceptions
{
    /// <inheritdoc />
    /// <summary>
    /// Exception that is thrown if user alredy registered in bank.
    /// </summary>
    public class UserAlreadyRegisteredException : BankServiceException
    {
        /// <inheritdoc />
        /// <param name="userEmail">user email</param>
        public UserAlreadyRegisteredException(string userEmail) : 
            base($"User {userEmail} already registered.")
        {
            this.UserEmail = userEmail;
        }

        /// <summary>
        /// User email.
        /// </summary>
        public string UserEmail { get; }
    }
}

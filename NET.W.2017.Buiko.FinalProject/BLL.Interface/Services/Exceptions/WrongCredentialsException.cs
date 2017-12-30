namespace BLL.Interface.Services.Exceptions
{
    /// <inheritdoc />
    /// <summary>
    /// Exception that is thrown if user email or password is wrong.
    /// </summary>
    public class WrongCredentialsException : BankServiceException
    {
        /// <inheritdoc />
        /// <param name="userEmail">user email</param>
        public WrongCredentialsException(string userEmail) : 
            base($"User {userEmail} wrong credentials.")
        {
            this.UserEmail = userEmail;
        }

        /// <summary>
        /// User email.
        /// </summary>
        public string UserEmail { get; }
    }
}

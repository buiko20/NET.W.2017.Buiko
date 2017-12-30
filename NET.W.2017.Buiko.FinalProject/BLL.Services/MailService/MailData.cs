using System;
using System.Net.Mail;

namespace BLL.Services.MailService
{
    public class MailData
    {
        #region private fields

        private string _from;
        private string _fromPassword;
        private string _to;
        private string _subject;
        private string _message;

        #endregion // !private fields.

        #region public 

        #region constructor

        /// <summary>
        /// Initializes instance with default parameters.
        /// </summary>
        public MailData()
        {
        }

        /// <summary>
        /// Initializes instance with passed parameters.
        /// </summary>
        /// <param name="from">from email</param>
        /// <param name="fromPassword">from email password</param>
        /// <param name="to">to email</param>
        /// <param name="subject">email subject</param>
        /// <param name="message">email message</param>
        /// <exception cref="ArgumentException">Exception thrown when
        /// one of the arguments is invalid.</exception>
        public MailData(string from, string fromPassword, string to, string subject, string message)
        {
            this.From = from;
            this.FromPassword = fromPassword;
            this.To = to;
            this.Subject = subject;
            this.Message = message;
        }

        #endregion // !constructor.

        #region properties

        /// <summary>
        /// From email.
        /// </summary>
        /// <exception cref="ArgumentException">Exception throws when
        /// <paramref name="value"/> is invalid.</exception>
        public string From
        {
            get
            {
                return _from;
            }

            set
            {
                VerifyEmail(value, nameof(this.From));
                _from = value;
            }
        }

        /// <summary>
        /// From email password.
        /// </summary>
        /// <exception cref="ArgumentException">Exception throws when
        /// <paramref name="value"/> is invalid.</exception>
        public string FromPassword
        {
            get
            {
                return _fromPassword;
            }

            set
            {
                VerifyString(value, nameof(this.FromPassword));
                _fromPassword = value;
            }
        }

        /// <summary>
        /// To email.
        /// </summary>
        /// <exception cref="ArgumentException">Exception throws when
        /// <paramref name="value"/> is invalid.</exception>
        public string To
        {
            get
            {
                return _to;
            }

            set
            {
                VerifyEmail(value, nameof(this.To));
                _to = value;
            }
        }

        /// <summary>
        /// Mail subject.
        /// </summary>
        /// <exception cref="ArgumentException">Exception throws when
        /// <paramref name="value"/> is invalid.</exception>
        public string Subject
        {
            get
            {
                return _subject;
            }

            set
            {
                VerifyString(value, nameof(this.Subject));
                _subject = value;
            }
        }

        /// <summary>
        /// Mail message.
        /// </summary>
        /// <exception cref="ArgumentException">Exception throws when
        /// <paramref name="value"/> is invalid.</exception>
        public string Message
        {
            get
            {
                return _message;
            }

            set
            {
                VerifyString(value, nameof(this.Message));
                _message = value;
            }
        }

        #endregion // !properties.

        #endregion // !public.

        #region private

        private static void VerifyString(string value, string paramName)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException($"{paramName} is null or white space.", paramName);
            }
        }

        private static void VerifyEmail(string email, string paramName)
        {
            try
            {
                var mailAddress = new MailAddress(email);
            }
            catch (Exception)
            {
                throw new ArgumentException($"{paramName} is invalid.", paramName);
            }
        }

        #endregion // !private.
    }
}

using System;
using System.Net.Mail;

namespace BLL.Interface.MailService
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

        #region constructor

        public MailData()
        {
        }

        public MailData(string from, string fromPassword, string to, string subject, string message)
        {
            From = from;
            FromPassword = fromPassword;
            To = to;
            Subject = subject;
            Message = message;
        }

        #endregion // !constructor.

        #region properties

        public string From
        {
            get
            {
                return _from;          
            }

            set
            {
                VerifyEmail(value, $"{nameof(From)} is invalid.", nameof(value));
                _from = value;
            }
        }

        public string FromPassword
        {
            get
            {
                return _fromPassword;    
            }

            set
            {
                VerifyString(value, $"{nameof(FromPassword)} is invalid", nameof(value));
                _fromPassword = value;
            }
        }

        public string To
        {
            get
            {
                return _to;
            }

            set
            {
                VerifyEmail(value, $"{nameof(To)} is invalid.", nameof(value));
                _to = value;
            }
        }

        public string Subject
        {
            get
            {
                return _subject;               
            }

            set
            {
                _subject = value;            
            } 
        }

        public string Message
        {
            get
            {
                return _message;
            }

            set
            {
                _message = value;
            }
        }

        #endregion // !properties.

        #region private

        private static void VerifyEmail(string email, string errorMessage, string paramName)
        {
            try
            {
                VerifyString(email, errorMessage, paramName);
                var mailAddress = new MailAddress(email);
            }
            catch (Exception)
            {
                throw new ArgumentException(errorMessage, paramName);
            }
        }

        private static void VerifyString(string data, string errorMessage, string paramName)
        {
            if (string.IsNullOrWhiteSpace(data))
            {
                throw new ArgumentException(errorMessage, paramName);
            }
        }

        #endregion
    }
}

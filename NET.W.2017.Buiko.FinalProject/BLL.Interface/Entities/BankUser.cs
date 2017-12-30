using System;
using System.Collections.Generic;
using System.Net.Mail;

namespace BLL.Interface.Entities
{
    public class BankUser : IComparable<BankUser>, IEquatable<BankUser>, IComparable
    {
        #region private fields

        private string _email;
        private string _firstName;
        private string _secondName;
        private string _role;

        #endregion // !private fields.

        #region public

        #region constructors

        /// <summary>
        /// Initializes user with default parameters.
        /// </summary>
        public BankUser()
        {
            this.Role = "user";
        }

        /// <summary>
        /// Initializes user with passed parameters.
        /// </summary>
        /// <param name="email">user email</param>
        /// <param name="firstName">user first name</param>
        /// <param name="secondName">user second name</param>
        /// <param name="role">user role</param>
        /// <exception cref="ArgumentException">Exception throws when
        /// one of the parameters is invalid.</exception>
        public BankUser(string email, string firstName, string secondName, string role = "user")
        {
            this.Email = email;
            this.FirstName = firstName;
            this.SecondName = secondName;
            this.Role = role;
        }

        #endregion // !constructors.

        #region properties

        /// <summary>
        /// User email.
        /// </summary>
        /// <exception cref="ArgumentException">Exception thrown when
        /// <paramref name="value"/> is invalid.</exception>
        public string Email
        {
            get
            {
                return _email;
            }

            set
            {
                VerifyEmail(value, nameof(this.Email));
                _email = value;
            }
        }

        /// <summary>
        /// User first name.
        /// </summary>
        /// <exception cref="ArgumentException">Exception thrown when
        /// <paramref name="value"/> is invalid.</exception>
        public string FirstName
        {
            get
            {
                return _firstName;
            }

            set
            {
                VerifyString(value, nameof(this.FirstName));
                _firstName = value;
            }
        }

        /// <summary>
        /// User second name.
        /// </summary>
        /// <exception cref="ArgumentException">Exception thrown when
        /// <paramref name="value"/> is invalid.</exception>
        public string SecondName
        {
            get
            {
                return _secondName;
            }

            set
            {
                VerifyString(value, nameof(this.SecondName));
                _secondName = value;
            }
        }

        /// <summary>
        /// User role.
        /// </summary>
        /// <exception cref="ArgumentException">Exception thrown when
        /// <paramref name="value"/> is invalid.</exception>
        public string Role
        {
            get
            {
                return _role;
            }

            set
            {
                VerifyString(value, nameof(Role));
                _role = value;
            }
        }

        /// <summary>
        /// Users accounts.
        /// </summary>
        public List<Account> Accounts { get; set; } = new List<Account>();

        #endregion // !properties.

        #region implementation of interfaces

        /// <inheritdoc />
        public int CompareTo(BankUser other)
        {
            if (ReferenceEquals(other, null))
            {
                return 1;
            }

            return string.Compare(other.Email, this.Email, StringComparison.Ordinal);
        }

        /// <inheritdoc />
        public bool Equals(BankUser other)
        {
            if (ReferenceEquals(other, null))
            {
                return false;
            }

            return string.Equals(other.Email, this.Email, StringComparison.Ordinal);
        }

        /// <inheritdoc />
        public int CompareTo(object obj)
        {
            if (ReferenceEquals(obj, null))
            {
                return 1;
            }

            return obj.GetType() == this.GetType() ? this.CompareTo((BankUser)obj) : 1;
        }

        #endregion // !implementation of interfaces.

        #region object override

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(obj, null))
            {
                return false;
            }

            return obj.GetType() == this.GetType() && this.Equals((BankUser)obj);
        }

        /// <inheritdoc />
        public override int GetHashCode() =>
            this.Email.GetHashCode();

        /// <inheritdoc />
        public override string ToString() =>
            $"{this.Email} {this.FirstName} {this.SecondName}" +
            $"{this.Role} {this.Accounts.Count}";

        #endregion // object override.

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

using System;

namespace BLL.Interface.Entities
{
    public class Account : IComparable<Account>, IEquatable<Account>, IComparable
    {
        #region private fields

        private string _id;
        private decimal _sum;
        private int _bonusPoints;
        private BankUser _bankUser;

        #endregion // !private fields.

        #region public

        #region constructors

        /// <summary>
        /// Initializes account with default parameters.
        /// </summary>
        public Account()
        {            
        }

        /// <summary>
        /// Initializes account with passed parameters.
        /// </summary>
        /// <param name="id">account number</param>
        /// <param name="sum">account sum</param>
        /// <param name="bonusPoints">account bonus points</param>
        /// <param name="type">account type</param>
        /// <param name="bankUser">account owner</param>
        /// <exception cref="ArgumentException">Exception throws when
        /// one of the parameters is invalid.</exception>
        /// <exception cref="ArgumentNullException">Exception thrown when
        /// <paramref name="bankUser"/> is null.</exception>
        public Account(string id, decimal sum, int bonusPoints, AccountType type, BankUser bankUser)
        {
            this.Id = id;
            this.Sum = sum;
            this.BonusPoints = bonusPoints;
            this.Type = type;
            this.BankUser = bankUser;
        }

        #endregion // !constructors.

        #region properties

        /// <summary>
        /// Account number.
        /// </summary>
        /// <exception cref="ArgumentException">Exception thrown when
        /// <paramref name="value"/> is invalid.</exception>
        public string Id
        {
            get
            {
                return _id;              
            }

            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Invalid id.", nameof(this.Id));
                }

                _id = value;
            }
        }

        /// <summary>
        /// Account sum.
        /// </summary>
        /// <exception cref="ArgumentException">Exception thrown when
        /// <paramref name="value"/> is less than 0.</exception>
        public decimal Sum
        {
            get
            {
                return _sum;                
            }

            set
            {
                if (value < 0)
                {
                    throw new ArgumentException($"{nameof(this.Sum)} must be greater than 0.", nameof(this.Sum));
                }

                _sum = value;
            }
        }

        /// <summary>
        /// Account bonus points.
        /// </summary>
        /// <exception cref="ArgumentException">Exception thrown when
        /// <paramref name="value"/> is less than 0.</exception>
        public int BonusPoints
        {
            get
            {
                return _bonusPoints;
            }

            set
            {
                if (value < 0)
                {
                    throw new ArgumentException($"{nameof(this.BonusPoints)} must be greater than 0.", nameof(this.BonusPoints));
                }

                _bonusPoints = value;
            }
        }

        /// <summary>
        /// Account type.
        /// </summary>
        public AccountType Type { get; set; }

        /// <summary>
        /// Account owner.
        /// </summary>
        /// <exception cref="ArgumentNullException">Exception thrown when
        /// <paramref name="value"/> is null.</exception>
        public BankUser BankUser
        {
            get
            {
                return _bankUser;
            }

            set
            {
                if (ReferenceEquals(value, null))
                {
                    throw new ArgumentNullException(nameof(this.BankUser));
                }

                _bankUser = value;
            }
        }

        #endregion // !properties.

        #region implementation of interfaces

        /// <inheritdoc />
        public int CompareTo(Account other)
        {
            if (ReferenceEquals(other, null))
            {
                return 1;
            }

            return string.Compare(this.Id, other.Id, StringComparison.Ordinal);
        }

        /// <inheritdoc />
        public bool Equals(Account other)
        {
            if (ReferenceEquals(other, null))
            {
                return false;
            }

            return string.Equals(this.Id, other.Id, StringComparison.Ordinal);
        }

        /// <inheritdoc />
        public int CompareTo(object obj)
        {
            if (ReferenceEquals(obj, null))
            {
                return 1;
            }

            return obj.GetType() == this.GetType() ? this.CompareTo((Account)obj) : 1;
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

            return obj.GetType() == this.GetType() && this.Equals((Account)obj);
        }

        /// <inheritdoc />
        public override int GetHashCode() => this.Id.GetHashCode();

        /// <inheritdoc />
        public override string ToString() =>
            $"{this.Type} {this.Id} {this.Sum} " +
            $"{this.BonusPoints} {this.BankUser.Email}";

        #endregion // object override.

        #endregion // !public.
    }
}

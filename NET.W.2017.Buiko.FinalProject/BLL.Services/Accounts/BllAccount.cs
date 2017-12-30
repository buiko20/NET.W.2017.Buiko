using System;
using BLL.Interface.Entities;

namespace BLL.Services.Accounts
{
    /// <summary>
    /// Represents an account.
    /// </summary>
    public abstract class BllAccount : IComparable<BllAccount>, IEquatable<BllAccount>, IComparable
    {
        #region private fields

        private string _id;
        private decimal _sum;
        private int _bonusPoints;
        private BankUser _bankUser;

        #endregion // !private fields.

        #region constructors

        /// <summary>
        /// Initializes instance with passed parameters.
        /// </summary>
        /// <param name="id">account number</param>
        /// <param name="sum">account sum</param>
        /// <param name="bonusPoints">account bonus points</param>
        /// <param name="bankUser">account owner</param>
        /// <exception cref="ArgumentException">Exception thrown when
        /// one of the arguments is invalid.</exception>
        /// <exception cref="ArgumentNullException">Exception thrown
        /// <paramref name="bankUser"/> is null.</exception>
        protected BllAccount(string id, decimal sum, int bonusPoints, BankUser bankUser)
        {
            this.Id = id;
            this.Sum = sum;
            this.BonusPoints = bonusPoints;
            this.BankUser = bankUser;
        }

        #endregion // !constructors.

        #region public 

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

            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException($"{this.Id} is nll or white space.", nameof(this.Id));
                }

                _id = value;
            }
        }

        /// <summary>
        /// Account sum.
        /// </summary>
        /// <exception cref="ArgumentException">Exception thrown when
        /// <paramref name="value"/> is invalid.</exception>
        public decimal Sum
        {
            get
            {
                return _sum;
            }

            private set
            {
                if (value < 0)
                {
                    throw new ArgumentException($"{nameof(this.Sum)} must be greater than or equal to zero.", nameof(this.Sum));
                }

                _sum = value;
            }
        }

        /// <summary>
        /// Account bonus points.
        /// </summary>
        /// <exception cref="ArgumentException">Exception thrown when
        /// <paramref name="value"/> is invalid.</exception>
        public int BonusPoints
        {
            get
            {
                return _bonusPoints;
            }

            private set
            {
                if (value < 0)
                {
                    throw new ArgumentException($"{nameof(this.BonusPoints)} must be greater than or equal to zero.", nameof(this.BonusPoints));
                }

                _bonusPoints = value;
            }
        }

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

        protected int BonusValue { get; set; }

        #endregion // !properties.

        #region methods

        /// <summary>
        /// Credits money to the account.
        /// </summary>
        /// <param name="sum">deposit money</param>
        /// <exception cref="ArgumentException">Thrown when <paramref name="sum"/> sum &lt;= 0.</exception>
        public void DepositMoney(decimal sum)
        {
            if (sum <= 0)
            {
                throw new ArgumentException($"{nameof(sum)} must be greater than zero.", nameof(sum));
            }

            this.Sum += sum;
            this.BonusPoints += this.CalculateBonusPointsForDeposit(sum, this.BonusValue);
        }

        /// <summary>
        /// Withdraws money from the account.
        /// </summary>
        /// <param name="sum">withdrawal amount</param>
        /// <exception cref="ArgumentException">Thrown when <paramref name="sum"/> sum &lt;= 0.</exception>
        public void WithdrawMoney(decimal sum)
        {
            if (sum <= 0)
            {
                throw new ArgumentException($"{nameof(sum)} must be greater than zero.", nameof(sum));
            }

            if (this.Sum < sum)
            {
                throw new ArgumentException($"{nameof(sum)} must be less than or equal to the account sum.", nameof(sum));
            }

            this.Sum -= sum;
            this.BonusPoints += this.CalculateBonusPointsForWithdraw(sum, this.BonusValue);
        }

        #endregion // !methods.

        #region object override methods

        /// <summary>
        /// Returns a string representation of a account.
        /// </summary>
        /// <returns>String representation of a account.</returns>
        public override string ToString() =>
            $"{this.GetAccountAdditionalInformation()} " +
            $"{this.Id} {this.Sum} {this.BonusPoints}";

        /// <summary>
        /// Verify the equivalence of the current account and the <paramref name="obj"/>.
        /// </summary>
        /// <param name="obj">object to compare</param>
        /// <returns>True if objects are equivalent, false otherwise.</returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(obj, null))
            {
                return false;
            }

            return obj.GetType() == this.GetType() && this.Equals((BllAccount)obj);
        }

        /// <summary>
        /// Returns account hash code.
        /// </summary>
        /// <returns>Account hash code.</returns>
        public override int GetHashCode() => Id.GetHashCode();

        #endregion

        #region interface implementation

        /// <inheritdoc />
        public int CompareTo(object obj)
        {
            if (ReferenceEquals(obj, null))
            {
                return 1;
            }

            return obj.GetType() != this.GetType() ? 1 : this.CompareTo((BllAccount)obj);
        }

        /// <inheritdoc />
        /// <summary>
        /// Compares two account.
        /// </summary>
        /// <param name="other">account for comparison.</param>
        /// <returns>Greater than 0, if the current account is larger, 0 if equal, -1 if less.</returns>
        public int CompareTo(BllAccount other)
        {
            if (ReferenceEquals(other, null))
            {
                return 1;
            }

            return string.Compare(other.Id, this.Id, StringComparison.Ordinal);
        }

        /// <inheritdoc />
        /// <summary>
        /// Сhecks the equivalence of the current account and the <paramref name="other" /> account.
        /// </summary>
        /// <param name="other">account to compare</param>
        /// <returns>True if accounts are equivalent, false otherwise.</returns>
        public bool Equals(BllAccount other)
        {
            if (ReferenceEquals(other, null))
            {
                return false;
            }

            return string.Equals(other.Id, this.Id, StringComparison.Ordinal);
        }

        #endregion // !interface implementation.

        #endregion // !public.

        #region protected

        /// <summary>
        /// Calculate the bonus increment when depositing money into an account.
        /// </summary>
        /// <param name="sum">deposit sum</param>
        /// <param name="bonusValue">bonus factor</param>
        /// <returns>Bonus increment.</returns>
        protected abstract int CalculateBonusPointsForDeposit(decimal sum, int bonusValue);

        /// <summary>
        /// Calculate the increment of the bonus when withdrawing money from the account.
        /// </summary>
        /// <param name="sum">deposit sum</param>
        /// <param name="bonusValue">bonus factor</param>
        /// <returns>Bonus increment.</returns>
        protected abstract int CalculateBonusPointsForWithdraw(decimal sum, int bonusValue);

        protected abstract string GetAccountAdditionalInformation();

        #endregion // !protected.
    }
}

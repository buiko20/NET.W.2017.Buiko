using System;
using System.Net.Mail;

namespace BLL.Interface.Account
{
    /// <summary>
    /// Represents an account.
    /// </summary>
    public abstract class Account : IComparable<Account>, IEquatable<Account>, IComparable
    {
        #region private fields

        private string _id;
        private string _onwerFirstName;
        private string _onwerSecondName;
        private string _ownerEmail;

        #endregion // !private fields.

        #region constructors

        /// <summary>
        /// Initializes the account.
        /// </summary>
        /// <param name="id">account id</param>
        /// <param name="onwerFirstName">owner name</param>
        /// <param name="onwerSecondName">surname of the owner</param>
        /// <param name="currentSum">account start sum</param>
        /// <param name="bonusPoints">account start bonus points</param>
        /// <param name="ownerEmail">owner email</param>
        /// <exception cref="ArgumentException">Thrown when one of the parameters is incorrect</exception>
        protected Account(
            string id, 
            string onwerFirstName, 
            string onwerSecondName, 
            decimal currentSum, 
            int bonusPoints,
            string ownerEmail)
        {
            VerifyInput(
                id, onwerFirstName, onwerSecondName, currentSum, bonusPoints, ownerEmail);

            Id = id;
            OwnerFirstName = onwerFirstName;
            OwnerSecondName = onwerSecondName;
            CurrentSum = currentSum;
            BonusPoints = bonusPoints;
            OwnerEmail = ownerEmail;
        }

        #endregion // !constructors.

        #region public 

        #region properties

        /// <summary>
        /// Return account id.
        /// </summary>
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
                    throw new ArgumentException(nameof(Id));
                }

                _id = value;
            }
        }

        /// <summary>
        /// Owner first name.
        /// </summary>
        public string OwnerFirstName
        {
            get
            {
                return _onwerFirstName;
            }

            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(nameof(OwnerFirstName));
                }

                _onwerFirstName = value;
            }
        }

        /// <summary>
        /// Owner second name.
        /// </summary>
        public string OwnerSecondName
        {
            get
            {
                return _onwerSecondName;
            }

            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(nameof(OwnerSecondName));
                }

                _onwerSecondName = value;
            }
        }

        /// <summary>
        /// Current sum.
        /// </summary>
        public decimal CurrentSum { get; private set; }

        /// <summary>
        /// Bonus points.
        /// </summary>
        public int BonusPoints { get; private set; }

        /// <summary>
        /// Account owner email.
        /// </summary>
        public string OwnerEmail
        {
            get
            {
                return _ownerEmail;
            }

            set
            {
                try
                {
                    var mailAddress = new MailAddress(value);
                }
                catch (Exception)
                {
                    throw new ArgumentException($"{nameof(OwnerEmail)} is invalid.", nameof(value));
                }

                _ownerEmail = value;
            }
        }

        /// <summary>
        /// Bonus value.
        /// </summary>
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
                throw new ArgumentException("Sum must be greater than zero", nameof(sum));
            }

            CurrentSum += sum;
            BonusPoints += CalculateBonusPointsForDeposit(sum, BonusValue);
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
                throw new ArgumentException("Sum must be greater than zero", nameof(sum));
            }

            if (CurrentSum < sum)
            {
                return;
            }

            CurrentSum -= sum;
            BonusPoints += CalculateBonusPointsForWithdraw(sum, BonusValue);
        }

        #endregion // !methods.

        #region object override methods

        /// <summary>
        /// Returns a string representation of a account.
        /// </summary>
        /// <returns>String representation of a account.</returns>
        public override string ToString() =>
            $"{GetAccountAdditionalInformation()} {Id} " +
            $"{OwnerFirstName} {OwnerSecondName} {CurrentSum} " +
            $"{BonusPoints} {OwnerEmail}";

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

            return obj.GetType() == this.GetType() && this.Equals((Account)obj);
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

            return obj.GetType() != this.GetType() ? 1 : this.CompareTo((Account)obj);
        }

        /// <inheritdoc />
        /// <summary>
        /// Compares two account.
        /// </summary>
        /// <param name="other">account for comparison.</param>
        /// <returns>Greater than 0, if the current account is larger, 0 if equal, -1 if less.</returns>
        public int CompareTo(Account other)
        {
            if (ReferenceEquals(other, null))
            {
                return 1;
            }

            return other.Id == Id ? 0 : -1;
        }

        /// <inheritdoc />
        /// <summary>
        /// Сhecks the equivalence of the current account and the <paramref name="other" /> account.
        /// </summary>
        /// <param name="other">account to compare</param>
        /// <returns>True if accounts are equivalent, false otherwise.</returns>
        public bool Equals(Account other)
        {
            if (ReferenceEquals(other, null))
            {
                return false;
            }

            return Id == other.Id;
        }

        #endregion // !interface implementation.

        #endregion // !public.

        #region protected

        #region methods

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

        #endregion // !methods.

        #endregion // !protected.

        #region private

        private static void VerifyInput(
            string id, 
            string onwerFirstName, 
            string onwerSecondName, 
            decimal currentSum, 
            int bonusPoints,
            string ownerEmail)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentException(nameof(id));
            }

            if (string.IsNullOrWhiteSpace(onwerFirstName))
            {
                throw new ArgumentException(nameof(onwerFirstName));
            }

            if (string.IsNullOrWhiteSpace(onwerSecondName))
            {
                throw new ArgumentException(nameof(onwerSecondName));
            }

            if (currentSum < 0)
            {
                throw new ArgumentException(nameof(currentSum));
            }

            if (bonusPoints < 0)
            {
                throw new ArgumentException(nameof(bonusPoints));
            }

            if (string.IsNullOrWhiteSpace(ownerEmail))
            {
                throw new ArgumentException(nameof(ownerEmail));
            }
        }

        #endregion // !private.
    }
}

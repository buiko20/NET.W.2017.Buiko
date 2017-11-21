using System;

namespace DAL.Interface.DTO
{
    public class DalAccount : IComparable<DalAccount>, IEquatable<DalAccount>, IComparable
    {
        public Type AccountType { get; set; }

        public string Id { get; set; }

        public string OwnerFirstName { get; set; }

        public string OwnerSecondName { get; set; }

        public decimal CurrentSum { get; set; }
       
        public int BonusPoints { get; set; }

        #region object override methods

        public override string ToString() =>
            $"{Id} {OwnerFirstName} {OwnerSecondName} {CurrentSum} {BonusPoints}";

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(obj, null))
            {
                return false;
            }

            return obj.GetType() == this.GetType() && this.Equals((DalAccount)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = AccountType != null ? AccountType.GetHashCode() : 0;
                hashCode = (hashCode * 397) ^ (Id != null ? Id.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (OwnerFirstName != null ? OwnerFirstName.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (OwnerSecondName != null ? OwnerSecondName.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ CurrentSum.GetHashCode();
                hashCode = (hashCode * 397) ^ BonusPoints;
                return hashCode;
            }
        }

        #endregion

        #region interface implementation

        /// <inheritdoc />
        public int CompareTo(object obj)
        {
            if (ReferenceEquals(obj, null))
            {
                return 1;
            }

            return obj.GetType() != this.GetType() ? 1 : this.CompareTo((DalAccount)obj);
        }

        /// <inheritdoc />
        public int CompareTo(DalAccount other)
        {
            if (ReferenceEquals(other, null))
            {
                return 1;
            }

            return other.Id == Id ? 0 : -1;
        }

        /// <inheritdoc />
        public bool Equals(DalAccount other)
        {
            if (ReferenceEquals(other, null))
            {
                return false;
            }

            return Id == other.Id;
        }

        #endregion // !interface implementation.
    }
}

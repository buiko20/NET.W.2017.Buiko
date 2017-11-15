using System;

namespace Algorithm.NUnitTests
{
    public class CustomClass : IComparable<CustomClass>
    {
        private readonly int _field1;

        private readonly string _field2;

        public CustomClass(int field1, string field2)
        {
            _field1 = field1;
            _field2 = field2;
        }

        public int CompareTo(CustomClass other)
        {
            if (ReferenceEquals(this, other))
            {
                return 0;
            }

            if (ReferenceEquals(null, other))
            {
                return 1;
            }

            var field1Comparison = _field1.CompareTo(other._field1);
            if (field1Comparison != 0)
            {
                return field1Comparison;
            }

            return string.Compare(_field2, other._field2, StringComparison.Ordinal);
        }
    }
}

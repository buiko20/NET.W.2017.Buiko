using System;

namespace Algorithm.NUnitTests
{
    public struct CustomStructure : IComparable<CustomStructure>
    {
        private readonly int _field1;

        private readonly string _field2;

        public CustomStructure(int field1, string field2)
        {
            this._field1 = field1;
            this._field2 = field2;
        }

        public int CompareTo(CustomStructure other)
        {
            var field1Comparison = _field1.CompareTo(other._field1);
            if (field1Comparison != 0)
            {
                return field1Comparison;
            }

            return string.Compare(_field2, other._field2, StringComparison.Ordinal);
        }
    } 
}

using System;
using System.Collections.Generic;

namespace Algorithm.NUnitTests
{
    public struct CustomStructure : IComparable<CustomStructure>
    {
        public int field1;

        public string field2;

        public KeyValuePair<int, string> field3;

        public CustomStructure(int field1, string field2)
        {
            this.field1 = field1;
            this.field2 = field2;
            this.field3 = new KeyValuePair<int, string>(field1, field2);
        }

        public int CompareTo(CustomStructure other)
        {
            var field1Comparison = field1.CompareTo(other.field1);
            if (field1Comparison != 0) return field1Comparison;
            return string.Compare(field2, other.field2, StringComparison.Ordinal);
        }
    } 
}

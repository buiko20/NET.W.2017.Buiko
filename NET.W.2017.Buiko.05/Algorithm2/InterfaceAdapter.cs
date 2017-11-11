using System;
using System.Collections.Generic;

namespace Algorithm2
{
    internal class InterfaceAdapter : IComparer<int[]>
    {
        private readonly Comparison<int[]> _delegateComparator;

        public InterfaceAdapter(Comparison<int[]> comparator)
        {
            if (ReferenceEquals(comparator, null))
            {
                throw new ArgumentNullException(nameof(comparator));
            }

            _delegateComparator = comparator;
        }

        public int Compare(int[] x, int[] y) =>
            _delegateComparator(x, y);
    }
}

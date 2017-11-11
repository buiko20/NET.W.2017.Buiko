using System;
using System.Collections.Generic;

namespace Algorithm2
{
    /// <inheritdoc />
    /// <summary>
    /// The class that adapts the delegate to the interface.
    /// </summary>
    internal class InterfaceAdapter : IComparer<int[]>
    {
        private readonly Comparison<int[]> _delegateComparator;

        /// <summary>
        /// Initializes an adapter instance.
        /// </summary>
        /// <param name="comparator">the delegate for the conversion to the interface</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="comparator"/> is null.</exception>
        public InterfaceAdapter(Comparison<int[]> comparator)
        {
            if (ReferenceEquals(comparator, null))
            {
                throw new ArgumentNullException(nameof(comparator));
            }

            _delegateComparator = comparator;
        }

        /// <inheritdoc />
        public int Compare(int[] x, int[] y) =>
            _delegateComparator(x, y);
    }
}

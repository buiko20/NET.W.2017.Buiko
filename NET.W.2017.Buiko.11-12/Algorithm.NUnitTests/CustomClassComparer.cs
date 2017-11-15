using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.NUnitTests
{
    public class CustomClassComparer : IComparer<CustomClass>
    {
        public int Compare(CustomClass x, CustomClass y)
        {
            return x.CompareTo(y);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm
{
    public interface IPredicate<in T>
    {
        bool Choose(T data);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task5.Solution
{
    public interface IVisitor
    {
        string ConvertBoldText(string text);

        string ConvertHyperlink(string text, string url);

        string ConvertPlainText(string text);
    }
}

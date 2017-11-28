using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task5.Solution
{
    public class LaTeXVisitor : IVisitor
    {
        public string ConvertBoldText(string text)
        {
            return "\\textbf{" + text + "}";
        }

        public string ConvertHyperlink(string text, string url)
        {
            return "\\href{" + url + "}{" + text + "}";
        }

        public string ConvertPlainText(string text)
        {
            return text;
        }
    }
}

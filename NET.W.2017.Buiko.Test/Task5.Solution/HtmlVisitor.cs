using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task5.Solution
{
    public class HtmlVisitor : IVisitor
    {
        public string ConvertBoldText(string text)
        {
            return "<b>" + text + "</b>";
        }

        public string ConvertHyperlink(string text, string url)
        {
            return "<a href=\"" + url + "\">" + text + "</a>";
        }

        public string ConvertPlainText(string text)
        {
            return text;
        }
    }
}

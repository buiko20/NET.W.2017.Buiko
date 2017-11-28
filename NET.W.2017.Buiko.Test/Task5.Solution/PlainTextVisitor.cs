using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task5.Solution
{
    public class PlainTextVisitor : IVisitor
    {
        public string ConvertBoldText(string text)
        {
            return "**" + text + "**";
        }

        public string ConvertHyperlink(string text, string url)
        {
            return text + " [" + url + "]";
        }

        public string ConvertPlainText(string text)
        {
            return text;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task5.Solution
{
    public class HtmlConverter : IConverter
    {
        public string Convert(DocumentPart documentPart)
        {
            if (documentPart.GetType() == typeof(BoldText))
            {
                var temp = (BoldText)documentPart;
                return "<b>" + temp.Text + "</b>";
            }

            if (documentPart.GetType() == typeof(Hyperlink))
            {
                var temp = (Hyperlink)documentPart;
                return "<a href=\"" + temp.Url + "\">" + temp.Text + "</a>";
            }

            if (documentPart.GetType() == typeof(PlainText))
            {
                var temp = (PlainText)documentPart;
                return temp.Text;
            }

            return string.Empty;
        }
    }
}

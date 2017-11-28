using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task5.Solution
{
    public class PlainTextConverter : IConverter
    {
        public string Convert(DocumentPart documentPart)
        {
            if (documentPart.GetType() == typeof(BoldText))
            {
                var temp = (BoldText)documentPart;
                return "**" + temp.Text + "**";
            }

            if (documentPart.GetType() == typeof(Hyperlink))
            {
                var temp = (Hyperlink)documentPart;
                return temp.Text + " [" + temp.Url + "]";
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

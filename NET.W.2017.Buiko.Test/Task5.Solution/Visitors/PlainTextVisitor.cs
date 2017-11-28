using System;
using Task5.Solution.Document;

namespace Task5.Solution.Visitors
{
    public class PlainTextVisitor : Visitor
    {
        public override void VisitBoldText(BoldText boldText) =>
            this.Result += "**" + boldText.Text + "**" + Environment.NewLine;

        public override void VisitHyperlink(Hyperlink hyperlink) =>
            this.Result += hyperlink.Text + " [" + hyperlink.Url + "]" + Environment.NewLine;

        public override void VisitPlainText(PlainText plainText) =>
            this.Result += plainText.Text + Environment.NewLine;
    }
}

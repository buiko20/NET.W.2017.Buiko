using System;
using Task5.Solution.Document;

namespace Task5.Solution.Visitors
{
    public class HtmlVisitor : Visitor
    {
        public override void VisitBoldText(BoldText boldText) =>
            this.Result += "<b>" + boldText.Text + "</b>" + Environment.NewLine;

        public override void VisitHyperlink(Hyperlink hyperlink) =>
            this.Result += "<a href=\"" + hyperlink.Url + "\">" + hyperlink.Text + "</a>" 
                        + Environment.NewLine;

        public override void VisitPlainText(PlainText plainText) =>
            this.Result += plainText.Text + Environment.NewLine;
    }
}

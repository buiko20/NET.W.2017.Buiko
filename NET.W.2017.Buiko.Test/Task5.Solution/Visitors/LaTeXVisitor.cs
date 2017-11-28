using System;
using Task5.Solution.Document;

namespace Task5.Solution.Visitors
{
    public class LaTeXVisitor : Visitor
    {
        public override void VisitBoldText(BoldText boldText) =>
            this.Result += "\\textbf{" + boldText.Text + "}" + Environment.NewLine;

        public override void VisitHyperlink(Hyperlink hyperlink) =>
            this.Result += "\\href{" + hyperlink.Url + "}{" + hyperlink.Text + "}" + 
                            Environment.NewLine;

        public override void VisitPlainText(PlainText plainText) =>
            this.Result += plainText.Text + Environment.NewLine;
    }
}

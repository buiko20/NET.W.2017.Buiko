using Task5.Solution.Visitors;

namespace Task5.Solution.Document
{
    public class PlainText : DocumentPart
    {
        public override void Visit(Visitor visitor) =>
            visitor.VisitPlainText(this);
    }
}

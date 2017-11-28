using Task5.Solution.Visitors;

namespace Task5.Solution.Document
{
    public class BoldText : DocumentPart
    {
        public override void Visit(Visitor visitor) =>
            visitor.VisitBoldText(this);
    }
}
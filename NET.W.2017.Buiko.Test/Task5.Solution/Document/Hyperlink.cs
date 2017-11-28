using Task5.Solution.Visitors;

namespace Task5.Solution.Document
{
    public class Hyperlink : DocumentPart
    {
        public string Url { get; set; }

        public override void Visit(Visitor visitor) =>
            visitor.VisitHyperlink(this);
    }
}

using Task5.Solution.Document;

namespace Task5.Solution.Visitors
{
    public abstract class Visitor
    {
        public string Result { get; protected set; } = string.Empty;

        public abstract void VisitBoldText(BoldText boldText);

        public abstract void VisitHyperlink(Hyperlink hyperlink);

        public abstract void VisitPlainText(PlainText plainText);
    }
}

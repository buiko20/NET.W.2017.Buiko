using Task5.Solution.Visitors;

namespace Task5.Solution.Document
{
    public abstract class DocumentPart
    {
        public string Text { get; set; }

        public abstract void Visit(Visitor visitor);
    }
}

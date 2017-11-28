namespace Task5.Solution
{
    public abstract class DocumentPart
    {
        public string Text { get; set; }

        public abstract string Visit(IVisitor visitor);
    }
}

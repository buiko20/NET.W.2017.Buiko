namespace Task5.Solution
{
    public class Hyperlink : DocumentPart
    {
        public string Url { get; set; }

        public override string Visit(IVisitor visitor)
        {
            return visitor.ConvertHyperlink(this.Text, this.Url);
        }
    }
}

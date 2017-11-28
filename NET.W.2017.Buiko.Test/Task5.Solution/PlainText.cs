namespace Task5.Solution
{
    public class PlainText : DocumentPart
    {
        public override string Visit(IVisitor visitor)
        {
            return visitor.ConvertPlainText(this.Text);
        }
    }
}

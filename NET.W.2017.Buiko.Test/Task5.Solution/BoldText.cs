namespace Task5.Solution
{
    public class BoldText : DocumentPart
    {
        public override string Visit(IVisitor visitor)
        {
            return visitor.ConvertBoldText(this.Text);
        }
    }
}
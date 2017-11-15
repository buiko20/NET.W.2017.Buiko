namespace Algorithm
{
    /// <inheritdoc />
    public class Predicate : IPredicate<int>
    {
        /// <inheritdoc />
        public bool Choose(int data) => data.ToString().Contains("7");
    }
}

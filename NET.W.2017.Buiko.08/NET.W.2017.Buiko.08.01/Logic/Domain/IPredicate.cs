namespace Logic.Domain
{
    /// <summary>
    /// An interface representing a method <see cref="Choose"/> for selecting an element.
    /// </summary>
    /// <typeparam name="T">Select item type.</typeparam>
    public interface IPredicate<in T>
    {
        /// <summary>
        /// The method that decides the task of selecting an <paramref name="item"/>.
        /// </summary>
        /// <param name="item">element for analysis</param>
        /// <returns>True if <paramref name="item"/> is selected, false otherwise.</returns>
        bool Choose(T item);
    }
}

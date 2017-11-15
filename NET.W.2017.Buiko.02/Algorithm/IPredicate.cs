namespace Algorithm
{
    /// <summary>
    /// The predicate interface.
    /// </summary>
    /// <typeparam name="T">Predicate element type.</typeparam>
    public interface IPredicate<in T>
    {
        /// <summary>
        /// Returns true or false, depending on the <paramref name="data"/>.
        /// </summary>
        /// <param name="data">data to choose</param>
        /// <returns>Returns true or false.</returns>
        bool Choose(T data);
    }
}

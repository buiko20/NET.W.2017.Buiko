namespace Converter.Interfaces
{
    /// <summary>
    /// A transformer contract that converts data of type
    /// <typeparamref name="T"/> into an xml string.
    /// </summary>
    /// <typeparam name="T">
    /// The type of data that will be converted to xml.
    /// </typeparam>
    public interface IXmlTransformer<in T>
    {
        /// <summary>
        /// Converts <paramref name="data"/> 
        /// of type <typeparamref name="T"/> 
        /// to an xml format string.
        /// </summary>
        /// <param name="data">data for conversion into xml string</param>
        /// <returns>String with xml <paramref name="data"/> representation.</returns>
        string TransformToXml(T data);
    }
}

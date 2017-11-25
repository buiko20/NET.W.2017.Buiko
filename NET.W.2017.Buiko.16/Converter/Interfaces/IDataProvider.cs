using System.Collections;
using System.Collections.Generic;

namespace Converter.Interfaces
{
    /// <summary>
    /// Contract for data providers.
    /// </summary>
    /// <typeparam name="T">Data type provided by the provider.</typeparam>
    public interface IDataProvider<out T> : IEnumerable<T>, IEnumerable
    {
        /// <summary>
        /// Returns the name of the xml root element.
        /// </summary>
        /// <returns>Name of the xml root element.</returns>
        string GetRootElementName();
    }
}

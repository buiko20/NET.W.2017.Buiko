using System.Collections.Generic;

namespace Converter.Interfaces
{
    /// <summary>
    /// Contract for data providers.
    /// </summary>
    public interface IDataProvider<out T>
    {
        /// <summary>
        /// Return some data.
        /// </summary>
        /// <typeparam name="T">Data type.</typeparam>
        /// <returns>Data</returns>
        IEnumerable<T> GetData();
    }
}

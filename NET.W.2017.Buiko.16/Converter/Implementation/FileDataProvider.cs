using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Converter.Interfaces;

namespace Converter.Implementation
{
    /// <inheritdoc />
    /// <summary>
    /// Delivers string data representing URL from a text file.
    /// </summary>
    public class FileDataProvider : IDataProvider<string>
    {
        private string _dataFilePath;

        /// <summary>
        /// Initializes the object with the passed parameters.
        /// </summary>
        /// <param name="dataFilePath">path to the data file.</param>
        /// <exception cref="ArgumentException">Exception thrown when
        /// <paramref name="dataFilePath"/> is invalid.</exception>
        public FileDataProvider(string dataFilePath)
        {
            DataFilePath = dataFilePath;
        }

        /// <summary>
        /// The path to the file that the provider reads.
        /// </summary>
        /// <exception cref="ArgumentException">Exception thrown when
        /// <paramref name="value"/> is invalid.</exception>
        public string DataFilePath
        {
            get
            {
                return _dataFilePath;
            }

            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException($"{nameof(DataFilePath)} is invalid.", nameof(DataFilePath));
                }

                _dataFilePath = value;
            }
        }

        /// <inheritdoc />
        public IEnumerable<string> GetData()
        {
            var data = new List<string>();
            using (var streamReader = new StreamReader(
                File.Open(
                    DataFilePath,
                    FileMode.Open,
                    FileAccess.Read,
                    FileShare.Read),
                Encoding.UTF8))
            {
                while (!streamReader.EndOfStream)
                {
                    data.Add(streamReader.ReadLine()?.Trim());
                }
            }

            return data;
        }
    }
}

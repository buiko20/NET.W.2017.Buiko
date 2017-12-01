using System;
using System.Collections.Generic;
using System.Xml;
using Converter.Interfaces;
using Logger;

namespace Converter
{
    public class XmlConverter
    {
        /// <summary>
        /// Initializes the object with the passed parameters.
        /// </summary>
        /// <param name="logger">logger</param>
        public XmlConverter(ILogger logger = null)
        {
            this.Logger = logger;
        }

        /// <summary>
        /// Logger.
        /// </summary>
        public ILogger Logger { get; set; }

        /// <summary>
        /// Converts data of type <typeparamref name="T"/> to xml document.
        /// </summary>
        /// <typeparam name="T">Data type to convert to xml.</typeparam>
        /// <param name="dataProvider">Class supplying data to transform</param>
        /// <param name="xmlTransformer">Class that converts data to xml</param>
        /// <param name="rootElementName">root element name</param>
        /// <returns>Xml document representing data.</returns>
        /// <exception cref="ArgumentNullException">Exception thrown when
        /// <paramref name="dataProvider"/> or <paramref name="xmlTransformer"/>
        /// is null.</exception>
        /// <exception cref="ArgumentException">Exception thrown when 
        /// <paramref name="rootElementName"/> is invalid.</exception>
        public XmlDocument ConvertDataToXml<T>(
            IDataProvider<T> dataProvider, IXmlTransformer<T> xmlTransformer, string rootElementName)
        {
            VerifyInput(dataProvider, xmlTransformer, rootElementName);

            var xmlDocument = new XmlDocument();
            var xmlDeclaration = xmlDocument.CreateXmlDeclaration("1.0", "utf-8", null);
            xmlDocument.AppendChild(xmlDeclaration);

            var xmlRootElement = xmlDocument.CreateElement(rootElementName);

            FillRootElementWithSubElements(
                dataProvider.GetData(), xmlTransformer, xmlRootElement, xmlDocument);

            xmlDocument.AppendChild(xmlRootElement);

            return xmlDocument;
        }

        private static void VerifyInput<T>(
            IDataProvider<T> dataProvider, IXmlTransformer<T> xmlTransformer, string rootElementName)
        {
            if (ReferenceEquals(dataProvider, null))
            {
                throw new ArgumentNullException(nameof(dataProvider));
            }

            if (ReferenceEquals(xmlTransformer, null))
            {
                throw new ArgumentNullException(nameof(xmlTransformer));
            }

            if (string.IsNullOrWhiteSpace(rootElementName))
            {
                throw new ArgumentException($"{nameof(rootElementName)} is invalid", nameof(rootElementName));
            }
        }

        private void FillRootElementWithSubElements<T>(
            IEnumerable<T> dataProvider, 
            IXmlTransformer<T> xmlTransformer,
            XmlNode xmlRootElement,
            XmlDocument xmlDocument)
        {
            int i = 0;
            foreach (var data in dataProvider)
            {
                i++;
                string xmlString;
                try
                {
                    xmlString = xmlTransformer.TransformToXml(data);
                }
                catch (Exception)
                {
                    this.Logger?.Info($"Data on the {i} iteration of the cycle is not valid.");
                    continue;
                }

                var xmlDocumentFragment = xmlDocument.CreateDocumentFragment();
                xmlDocumentFragment.InnerXml = xmlString;

                xmlRootElement.AppendChild(xmlDocumentFragment);
            }
        }
    }
}

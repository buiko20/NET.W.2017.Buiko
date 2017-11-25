using System;
using Converter;
using Converter.Implementation;
using Logger;

namespace ConsoleUI
{
    internal class Program
    {
        private const string DataFilePath = @"data.txt";

        private static void Main()
        {
            var dataProvider = new FileDataProvider(DataFilePath);
            var toXmlTransformer = new UrlToXmlTransformer();
            var logger = LoggerFactory.GetLogger("Program");

            var xmlConverter = new XmlConverter(logger);
            var xmlDocument = xmlConverter.ConvertDataToXml(dataProvider, toXmlTransformer);
            xmlDocument.Save("result_xml.xml");

            Console.ReadLine();
        }
    }
}

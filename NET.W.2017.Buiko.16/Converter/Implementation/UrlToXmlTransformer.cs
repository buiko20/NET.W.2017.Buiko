using System;
using System.Text;
using System.Web;
using Converter.Interfaces;

namespace Converter.Implementation
{
    /// <inheritdoc />
    /// <summary>
    /// Transformer сonverts url to xml format.
    /// </summary>
    public class UrlToXmlTransformer : IXmlTransformer<string>
    {
        /// <inheritdoc />
        /// <summary>
        /// Сonverts <paramref name="data"/> to xml format.
        /// </summary>
        /// <param name="data">url for converting into xml format.</param>
        /// <returns>Xml view of url.</returns>
        /// <exception cref="ArgumentException">Exception thrown when
        /// <paramref name="data"/> is invalid url.</exception>
        public string TransformToXml(string data)
        {
            VerifyInput(data);

            var result = new StringBuilder(data.Length);
            var uri = new Uri(data);

            result.Append("<urlAddress>");

            result.Append($"<host name=\"{uri.Host}\"/>");

            AppendUriSegments(result, uri);

            AppendParameters(result, uri);

            result.Append("</urlAddress>");

            return result.ToString();
        }

        private static void VerifyInput(string data)
        {
            try
            {
                var uri = new Uri(data);
            }
            catch (Exception e)
            {
                throw new ArgumentException(e.Message, nameof(data));
            }
        }

        private static void AppendUriSegments(StringBuilder result, Uri uri)
        {
            if (uri.Segments.Length <= 1)
            {
                return;
            }

            result.Append("<uri>");
            foreach (var segment in uri.Segments)
            {
                var temp = segment.Trim('/', ' ');
                if (!string.IsNullOrWhiteSpace(temp))
                {
                    result.Append($"<segment>{temp}</segment>");
                }
            }

            result.Append("</uri>");
        }

        private static void AppendParameters(StringBuilder result, Uri uri)
        {
            if (string.IsNullOrWhiteSpace(uri.Query))
            {
                return;
            }

            result.Append("<parameters>");

            var collection = HttpUtility.ParseQueryString(uri.Query);
            foreach (var key in collection.AllKeys)
            {
                var xmlParameter = $"<parameter value=\"{collection[key]}\" key=\"{key}\"/>";
                result.Append(xmlParameter);
            }          

            result.Append("</parameters>");
        }
    }
}

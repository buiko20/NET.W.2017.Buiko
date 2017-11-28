using System.Collections.Generic;
using Task5.Solution.Visitors;

namespace Task5.Console
{
    internal class Program
    {
        private static void Main()
        {
            /*List<DocumentPart> parts = new List<DocumentPart>
                {
                    new PlainText {Text = "Some plain text"},
                    new Hyperlink {Text = "google.com", Url = "https://www.google.by/"},
                    new BoldText {Text = "Some bold text"}
                };

            Document document = new Document(parts);

            Console.WriteLine(document.ToHtml());

            Console.WriteLine(document.ToPlainText());

            Console.WriteLine(document.ToLaTeX());*/

            var htmlVisitor = new HtmlVisitor();
            var laTeXVisitor = new LaTeXVisitor();
            var plainTextVisitor = new PlainTextVisitor();

            var parts = new List<Task5.Solution.Document.DocumentPart>
            {
                new Task5.Solution.Document.PlainText { Text = "Some plain text" },
                new Task5.Solution.Document.Hyperlink { Text = "google.com", Url = "https://www.google.by/" },
                new Task5.Solution.Document.BoldText { Text = "Some bold text" }
            };

            var document = new Task5.Solution.Document.Document(parts);

            System.Console.WriteLine(document.Convert(htmlVisitor));

            System.Console.WriteLine(document.Convert(laTeXVisitor));

            System.Console.WriteLine(document.Convert(plainTextVisitor));

            System.Console.ReadLine();
        }
    }
}

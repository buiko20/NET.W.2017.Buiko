using Task5.Solution;

namespace Task5.Console
{
    using System.Collections.Generic;
    using System;
    using Task5;

    class Program
    {
        static void Main(string[] args)
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

            var parts = new List<Task5.Solution.DocumentPart>
            {
                new Task5.Solution.PlainText {Text = "Some plain text"},
                new Task5.Solution.Hyperlink {Text = "google.com", Url = "https://www.google.by/"},
                new Task5.Solution.BoldText {Text = "Some bold text"}
            };

            var document = new Task5.Solution.Document(parts);

            Console.WriteLine(document.Convert(htmlVisitor));

            Console.WriteLine(document.Convert(laTeXVisitor));

            Console.WriteLine(document.Convert(plainTextVisitor));

            Console.ReadLine();
        }
    }
}

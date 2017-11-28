using System;
using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using Task5.Solution.Document;
using Task5.Solution.Visitors;

namespace Task5.Tests
{
    [TestFixture]
    public class DocumentTests
    {
        [Test]
        public void DocumentCallVisitor_VisitMethod()
        {
            var parts = new List<DocumentPart>
            {
                new PlainText { Text = "Some plain text" },
                new Hyperlink { Text = "google.com", Url = "https://www.google.by/" },
                new BoldText { Text = "Some bold text" }
            };

            var visitorMock = new Mock<Visitor>();
            var document = new Document(parts);

            document.Convert(visitorMock.Object);

            visitorMock.Verify(visitor => visitor.VisitPlainText(It.Is<PlainText>(text => string.Equals(text.Text, parts[0].Text, StringComparison.Ordinal))), Times.Once);
            visitorMock.Verify(visitor => visitor.VisitHyperlink(It.Is<Hyperlink>(text => string.Equals(text.Text, parts[1].Text, StringComparison.Ordinal))), Times.Once);
            visitorMock.Verify(visitor => visitor.VisitBoldText(It.Is<BoldText>(text => string.Equals(text.Text, parts[2].Text, StringComparison.Ordinal))), Times.Once);
        }
    }
}

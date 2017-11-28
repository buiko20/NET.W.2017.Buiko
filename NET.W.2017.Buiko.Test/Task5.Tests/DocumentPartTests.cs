using System;
using Moq;
using NUnit.Framework;
using Task5.Solution.Document;
using Task5.Solution.Visitors;

namespace Task5.Tests
{
    public class DocumentPartTests
    {
        [Test]
        public void BoldTextCallVisitor_VisitMethod()
        {
            var visitorMock = new Mock<Visitor>();
            var boldText = new BoldText { Text = "Some bold text" };

            boldText.Visit(visitorMock.Object);

            visitorMock.Verify(visitor => visitor.VisitBoldText(It.Is<BoldText>(text => string.Equals(text.Text, boldText.Text, StringComparison.Ordinal))), Times.Once);
        }

        [Test]
        public void HyperlinkCallVisitor_VisitMethod()
        {
            var visitorMock = new Mock<Visitor>();
            var hyperlink = new Hyperlink { Text = "google.com", Url = "https://www.google.by/" };

            hyperlink.Visit(visitorMock.Object);

            visitorMock.Verify(visitor => visitor.VisitHyperlink(It.Is<Hyperlink>(text => string.Equals(text.Text, hyperlink.Text, StringComparison.Ordinal))), Times.Once);
            visitorMock.Verify(visitor => visitor.VisitHyperlink(It.Is<Hyperlink>(text => string.Equals(text.Url, hyperlink.Url, StringComparison.Ordinal))), Times.Once);
        }

        [Test]
        public void PlainTextCallVisitor_VisitMethod()
        {
            var visitorMock = new Mock<Visitor>();
            var plainText = new PlainText { Text = "Some plain text" };

            plainText.Visit(visitorMock.Object);

            visitorMock.Verify(visitor => visitor.VisitPlainText(It.Is<PlainText>(text => string.Equals(text.Text, plainText.Text, StringComparison.Ordinal))), Times.Once);
        }
    }
}

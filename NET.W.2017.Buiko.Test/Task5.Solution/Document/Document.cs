using System;
using System.Collections.Generic;
using Task5.Solution.Visitors;

namespace Task5.Solution.Document
{
    public class Document
    {
        private readonly List<DocumentPart> _parts;

        public Document(IEnumerable<DocumentPart> parts)
        {
            if (ReferenceEquals(parts, null))
                throw new ArgumentNullException(nameof(parts));

            _parts = new List<DocumentPart>(parts);
        }

        public string Convert(Visitor visitor)
        {
            if (ReferenceEquals(visitor, null))
                throw new ArgumentNullException(nameof(visitor));

            foreach (var part in _parts)
                part.Visit(visitor);

            return visitor.Result;
        }       
    }
}

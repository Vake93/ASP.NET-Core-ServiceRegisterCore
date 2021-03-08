using System;

namespace TestServiceLibrary.Documents
{
    public class TestDocumentOne : IDocument
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public bool Deleted { get; set; }

        public DateTime Created { get; set; }

        public DateTime Modified { get; set; }

    }
}

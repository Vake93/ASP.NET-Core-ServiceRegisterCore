using System;

namespace TestServiceLibrary.Documents
{
    public interface IDocument
    {
        Guid Id { get; set; }

        bool Deleted { get; set; }

        DateTime Created { get; set; }

        DateTime Modified { get; set; }
    }
}

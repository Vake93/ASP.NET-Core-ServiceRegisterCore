using System;
using TestServiceLibrary.Documents;

namespace TestServiceLibrary.GenericService
{
    public interface IDocumentOpertation<T> where T : IDocument
    {
        T Find(Guid id);
    }
}